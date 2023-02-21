// See https://aka.ms/new-console-template for more information
using System.Net.WebSockets;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace MoveMouse
{
    class Program{

        static void Main(string[] args){
        foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                Console.WriteLine("Name: " + netInterface.Name);
                Console.WriteLine("Description: " + netInterface.Description);
                Console.WriteLine("Addresses: ");
                IPInterfaceProperties ipProps = netInterface.GetIPProperties();
                foreach (UnicastIPAddressInformation addr in ipProps.UnicastAddresses)
                {
                Console.WriteLine(" " + addr.Address.ToString());
                }
                Console.WriteLine("");
            }    
            
            //ListenAndMove();  
        }

        static async void ListenAndMove()
        {
            // int x = 0;
            // int y = 0;
            var result = new byte[1024];
            Uri link = new Uri("ws://192.168.1.121:4201");
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
    }
}