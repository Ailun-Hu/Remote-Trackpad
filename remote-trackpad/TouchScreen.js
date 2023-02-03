import React, {useState} from 'react';
import { Button, View , Text, StyleSheet} from 'react-native';
import Touchpad from './touchpad';

function TouchScreen (props) {
    const  [count, setCount] = useState(0);
    return(
       
            <Touchpad/>

    )
}

const styles = StyleSheet.create({
    container: {
      flex: 1,
      backgroundColor: '#fff',
      alignItems: 'center',
      justifyContent: 'center',
    },
  });

export default TouchScreen;