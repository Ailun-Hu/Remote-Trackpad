import React, { Component , useRef, useState} from "react";
import {  View , Animated, useWindowDimensions, SafeAreaView, StatusBar, Text, Button} from "react-native";

const CURSOR_SIDE_SIZE = 20;
const CURSOR_HALF_SIDE_SIZE = CURSOR_SIDE_SIZE / 2;

function Touchpad(props) {
  //WebSocket Server
  var ws1 = React.useRef(new WebSocket('ws://localhost:4201')).current;
  var ws2;
  var ws = ws1;

  


  //Movement Controls
  const touch = useRef( new Animated.ValueXY({ x: 0, y: 0})).current;
  const dimensions = useWindowDimensions();
  var res = {
    xChange: 0,
    yChange: 0
  }
  //True = use Lan, False = Online
  
  const [ButtonOption, setButtonOption] = useState(true);

    return( 
      
       <Animated.View style = {{ flex: 1 , backgroundColor: '#green' } } 
       onStartShouldSetResponder ={() => true}
       onResponderMove = {(event) => {
        let isFirstChange = (res.xChange == 0 && res.yChange == 0 ? true : false);
        res.xChange = Math.round(event.nativeEvent.locationX - touch.x._value);
        res.yChange = Math.round(event.nativeEvent.locationY - touch.y._value);
        
        if(!isFirstChange)
          ws.send(JSON.stringify(res));
        touch.setValue({
          x: event.nativeEvent.locationX,
          y: event.nativeEvent.locationY,
        },
        );


       }}
       onResponderRelease= {()=>{
          res.xChange = 0; res.yChange = 0;
          ws.send(JSON.stringify(res));
          Animated.spring(touch, {
            toValue: {
              x: dimensions.width/2 - CURSOR_HALF_SIDE_SIZE,
              y: dimensions.height/2 - CURSOR_HALF_SIDE_SIZE,
            },
            useNativeDriver : false,
          }).start();
        }}
       >
        <View style={{flexDirection: "row", alignItems: "center", justifyContent: "center", marginTop: "14%"}}>
          <Button style={{ flex: 1}} color="black" title={ButtonOption ? "Switch To Online" : "Switch To Local"} onPress={() => {setButtonOption(!ButtonOption); 
          ws = ButtonOption ?  ws1 : ws2}}/>
          </View>
        <Animated.View style={{
          position: "absolute",
          left: Animated.subtract(touch.x, CURSOR_HALF_SIDE_SIZE),
          top: Animated.subtract(touch.y, CURSOR_HALF_SIDE_SIZE),
          height: CURSOR_SIDE_SIZE,
          width: CURSOR_SIDE_SIZE,
          borderRadius: CURSOR_HALF_SIDE_SIZE,
          backgroundColor: "orange",
        }} 
      
        />
        
       </Animated.View>

    )
};

export default Touchpad;