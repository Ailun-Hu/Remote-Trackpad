import React, { Component , useRef} from "react";
import {  View , Animated, useWindowDimensions, SafeAreaView, StatusBar, Text, Button} from "react-native";

const CURSOR_SIDE_SIZE = 20;
const CURSOR_HALF_SIDE_SIZE = CURSOR_SIDE_SIZE / 2;

function Touchpad(props) {
  //WebSocket Server
  var ws = React.useRef(new WebSocket('ws://localhost:4201')).current;


  //Movement Controls
  const touch = useRef( new Animated.ValueXY({ x: 0, y: 0})).current;
  const dimensions = useWindowDimensions();
  var res = {
    xChange: 0,
    yChange: 0
  }

    return( 
      
       <Animated.View style = {{ flex: 1 , backgroundColor: '#green' } } 
       onStartShouldSetResponder ={() => true}
       onResponderMove = {(event) => {
        let isFirstChange = (res.xChange == 0 && res.yChange == 0 ? true : false);
        res.xChange = event.nativeEvent.locationX - touch.x._value;
        res.yChange = event.nativeEvent.locationY - touch.y._value;
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
        <View style={{flexDirection: "row", alignItems: "center", justifyContent: "center" , backgroundColor:"yellow", marginTop: "14%"}}><Button style={{ flex: 1}} color="black" title="WORAKJJKHSJA"/><Button style={{flex: 1}} title="jsdahkjdhasjhdjkhs"/></View>
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