// See https://aka.ms/new-console-template for more information
using System.Net.WebSockets;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using ArpLookup;
using System.Net;
using System.Windows.

namespace MoveMouse
{
    class Program{
        
        static void Main(string[] args){
            // while(true){
            // string ipbase = "192.168.1.";
            // for(int i = 1; i < 255; i++){
            //     string ip = ipbase + i.ToString();
            //     bool serverFound = pingCheck(ip, 50, 4201);
            //     if(serverFound){
            //         Console.WriteLine("Listened");
            //         ListenAndMove(ip);
            //     }

            // } 
            // }                 
            ListenAndMove("192.168.1.121");  
        }
        static bool pingCheck(string host, int timeout, int port){
            
            System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
            System.Net.NetworkInformation.PingReply pingReply;

            try{
                pingReply = ping.Send(host, timeout);
                if(pingReply != null && pingReply.Status == System.Net.NetworkInformation.IPStatus.Success)
                    using (TcpClient tcpClient = new TcpClient()){
                        try{
                            tcpClient.Connect(host, port);
                            return true;
                        }catch{
                            return false;
                        }
                    }
            }catch{
                return false;
            }
            return false;
        }

        static async void ListenAndMove(string ip)
        {
            // int x = 0;
            // int y = 0;
            var result = new byte[1024];
            string url = "ws://" + ip + ":4201";
            Uri link = new Uri(url);
            using(ClientWebSocket ws = new ClientWebSocket()){
            Console.WriteLine(link.ToString());

            ws.ConnectAsync(link, CancellationToken.None).Wait();
            Console.WriteLine(ws.Options.KeepAliveInterval);
            while (ws.State == WebSocketState.Open)
            {
                WebSocketReceiveResult res = ws.ReceiveAsync(new ArraySegment<byte>(result), CancellationToken.None).WaitAsync(TimeSpan.FromMinutes(1)).Result;
                var data = System.Text.Encoding.UTF8.GetString(new ArraySegment<byte>(result, 0, res.Count));
                var list = data.Split(' ');
                Console.WriteLine(list[0] + ' ' + list[1]);
            }
            ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed", CancellationToken.None).Wait();
            }
        }

        static void MoveCursor()
        {
        // Set the Current cursor, move the cursor's Position,
        // and set its clipping rectangle to the form. 

        this.Cursor = new Cursor(Cursor.Current.Handle);
        Cursor.Position = new Point(Cursor.Position.X - 50, Cursor.Position.Y - 50);
        Cursor.Clip = new Rectangle(this.Location, this.Size);
        }
    }
}