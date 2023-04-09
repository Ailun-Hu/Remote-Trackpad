using System;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Text;



namespace DesktopMouseMover
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hello");
            ListenAndMove("wss://1vrvtxidx1.execute-api.us-east-1.amazonaws.com/production");
        }


        static void ListenAndMove(string websocket)
        {
            //To Store Mouse Positions in Buffer
            var result = new byte[1024];

            Uri link = new Uri(websocket);
            using (ClientWebSocket ws = new ClientWebSocket())
            {
                try
                {
                     ws.ConnectAsync(link, CancellationToken.None).Wait();
                    var data = new { action = "SendData" };
                    string topass = JsonConvert.SerializeObject(data);
                    ArraySegment<Byte> RecieverInfo = new ArraySegment<byte>(Encoding.ASCII.GetBytes(topass));
                    ws.SendAsync(RecieverInfo, WebSocketMessageType.Text, true, CancellationToken.None);
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
                    int x = Cursor.Position.X + Int32.Parse(list[0])*10;
                    int y = Cursor.Position.Y + Int32.Parse(list[1])*10;

                    Cursor.Position = new Point(x, y);
                    
                }
                ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed", CancellationToken.None).Wait();
                
            }
        }
    }

}
