import React, { Component , useRef} from "react";
import { TouchableHighlight, View , Animated, useWindowDimensions} from "react-native";

const CURSOR_SIDE_SIZE = 20;
const CURSOR_HALF_SIDE_SIZE = CURSOR_SIDE_SIZE / 2;

function Touchpad() {
  //WebSocket Server
  var ws = React.useRef(new WebSocket('ws://localhost:8080')).current;


  //Movement Controls
  const touch = useRef( new Animated.ValueXY({ x: 0, y: 0})).current;
  const dimensions = useWindowDimensions();
  let xChange = 0;
  let yChange = 0;

    return( 
       <Animated.View style = {{ flex: 1 } } 
       onStartShouldSetResponder ={() => true}
       onResponderMove = {(event) => {
        let isFirstChange = (xChange == 0 && yChange == 0 ? true : false);
        xChange = event.nativeEvent.locationX - touch.x._value;
        yChange = event.nativeEvent.locationY - touch.y._value;
        if(!isFirstChange)
          ws.send("x: " + Math.round(xChange) + " y: " + Math.round(yChange));
        touch.setValue({
          x: event.nativeEvent.locationX,
          y: event.nativeEvent.locationY,
        },
        );

       }}
       onResponderRelease= {()=>{
          xChange = 0; yChange = 0;
          ws.send("x: " + xChange + " y: " + yChange);
          Animated.spring(touch, {
            toValue: {
              x: dimensions.width/2 - CURSOR_HALF_SIDE_SIZE,
              y: dimensions.height/2 - CURSOR_HALF_SIDE_SIZE,
            },
            useNativeDriver : false,
          }).start();
        }}
       >
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