# Remote-Trackpad
This is a personal project i worked on mostly to help me understand how the networks function. 

To Test Project:
You will need to run React-Native app, Node js Backend, and Desktop exe

The front-end react native app sends data to a backend on the same machine in a certain port. 

The Desktop Executable searches through the entire lan network and checks to see if there is a connection and if the port is used (meaning the server exist there). After getting the conection it waits for the messages to be passed to it (location to move the x and y position) and moves cursor accordingly. 

## Things to Note
  - Desktop exe only works on windows machine. 
  - There may be some networking firewalls that could prevent the connection from going through. 
