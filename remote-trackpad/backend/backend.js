const WebSocket = require('ws');
const server = new WebSocket.Server({port: '4201'});
const readline = require('readline');
const rl = readline.createInterface({
    input: process.stdin,
    output: process.stdout
  });


server.on('connection', socket => {
    console.log("Connected User");
    
    socket.on('message', message => {
        let res = JSON.parse(message.toString('utf8'))
        console.log(res.xChange);
        console.log(res.yChange);
        server.clients.forEach((client) => {
            if(client !== socket && client.readyState === WebSocket.OPEN){
                client.send(res.xChange + " " + res.yChange + " ");
            }
        })
    })
});

