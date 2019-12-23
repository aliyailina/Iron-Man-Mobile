using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace IronMan_mobile2
{
    public static class GetScriptConnection
    {
        private static void StartConnection(string ip)
        {
            const int port = 13000;
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                {
                    SendTimeout = 3000, ReceiveTimeout = 3000
                };
                socket.Connect(endPoint);
                byte[] arr = new byte[1024];
                string[] scripts;
                do
                {
                    var bytes = socket.Receive(arr);
                    scripts = Encoding.UTF8.GetString(arr, 0, bytes).Split('*');
                } while (socket.Available > 0);

                foreach (var script in scripts)
                {
                    Scripts.AddToScriptList(script);
                }

                Editor.ConnectMessage = "Connected";
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();

            }
            catch
            {
                Editor.ConnectMessage = "Not connected";
            }
        }
        
        public static async void StartConnectionAsync(string ip)
        {
            await Task.Run(() => StartConnection(ip));
        }
    }
}