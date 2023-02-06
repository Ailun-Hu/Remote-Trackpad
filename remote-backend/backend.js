const WebSocket = require('ws');
const server = new WebSocket.Server({port: '8080'});
const readline = require('readline');
const rl = readline.createInterface({
    input: process.stdin,
    output: process.stdout
  });


server.on('connection', socket => {
    console.log("Connected User");
    
    for(let i = 0; i < 10; i++){
        socket.send(i);
    }
    socket.on('message', message => {
        console.log(message.toString('utf8'));
    })
});


