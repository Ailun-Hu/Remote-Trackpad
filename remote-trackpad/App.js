import { StatusBar } from 'expo-status-bar';
import React from 'react';
import { StyleSheet, Text, View } from 'react-native';
import { Component } from 'react';
import TouchScreen from './TouchScreen';
import Touchpad from './touchpad';



export default function App() {

  return (
    <Touchpad/>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
    alignItems: 'center',
    justifyContent: 'center',
  },
});
