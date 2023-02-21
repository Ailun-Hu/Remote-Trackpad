import { StatusBar } from 'expo-status-bar';
import React from 'react';
import { SafeAreaView, StyleSheet, Text, View } from 'react-native';
import TouchScreen from './TouchScreen';
import Touchpad from './touchpad';


//Create Backend For localhost: 4201
//Send Data to Backend localHost: 4201
//Backend Create Connect and Disconnect option for m
export default function App() {

  return (
    <Touchpad/>
    
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'black',
    width: 20,
    alignItems: 'center',
    justifyContent: 'center',
  },
  text: {
    fontSize: 100
  }
});
