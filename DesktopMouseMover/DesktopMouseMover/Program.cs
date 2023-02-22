using System;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace DesktopMouseMover
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //Loops though all possible LAN IP addresses and checks server exist on certain port
             while(true){
                 string ipbase = "192.168.1.";
                 for(int i = 1; i < 255; i++){
                     string ip = ipbase + i.ToString();
                    bool serverFound = pingCheck(ip, 100, 4201);
                     if(serverFound){
                         ListenAndMove(ip);
                     }
                 } 
            }         


        }
        static bool pingCheck(string host, int timeout, int port)
        {
            System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
            System.Net.NetworkInformation.PingReply pingReply;
            try
            {
                pingReply = ping.Send(host, timeout);
                if (pingReply != null && pingReply.Status == System.Net.NetworkInformation.IPStatus.Success)
                    using (TcpClient tcpClient = new TcpClient())
                    {
                        Console.WriteLine(host);
                        try
                        {
                            tcpClient.Connect(host, port);
                            return true;
                        }
                        catch
                        {
                            return false;
                        }
                    }
            }
            catch
            {
                return false;
            }
            return false;
        }

        static void ListenAndMove(string ip)
        {
            //To Store Mouse Positions in Buffer
            var result = new byte[1024];

            string url = "ws://" + ip + ":4201";
            Uri link = new Uri(url);
            using (ClientWebSocket ws = new ClientWebSocket())
            {
                try
                {
                     ws.ConnectAsync(link, CancellationToken.None).Wait();
                }
                catch{
                    return;
                }

                //Moves mouse after getting data from server
                while (ws.State == WebSocketState.Open)
                {
                    WebSocketReceiveResult res = ws.ReceiveAsync(new ArraySegment<byte>(result), CancellationToken.None).GetAwaiter().GetResult();
                    var data = System.Text.Encoding.UTF8.GetString(result);
                    var list = data.Split(' ');
                    Console.WriteLine(list[0] + ' ' + list[1]);
                    var cursor = new Cursor(Cursor.Current.Handle);
                    int x = Cursor.Position.X + Int32.Parse(list[0]);
                    int y = Cursor.Position.Y + Int32.Parse(list[1]);

                    Cursor.Position = new Point(x, y);
                    
                }
                ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed", CancellationToken.None).Wait();
                
            }
        }
    }

}
