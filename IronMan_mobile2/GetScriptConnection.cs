using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Widget;

namespace IronMan_mobile2
{
    public class GetScriptConnection
    {
        private static void StartConnection(string IP)
        {
            const int port = 13000;
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(IP), port);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(endPoint);
                byte[] arr = new byte[20000];
                string[] files;
                int bytes = 0;
                do
                {
                    bytes = socket.Receive(arr);
                    files = Encoding.UTF8.GetString(arr, 0, bytes).Split('*');
                } while (socket.Available > 0);

                foreach (var file in files)
                {
                    if (!Editor.scriptsList.Contains(file) && !String.IsNullOrEmpty(file))
                    {
                        Editor.scriptsList.Add(file);
                    }
                }

                Editor.ConnectMessage = "Connected";
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();

            }
            catch (Exception e)
            {
                Editor.ConnectMessage = "Not connected";
            }
        }
        
        public static async void StartConnectionAsync(string IP)
        {
            await Task.Run(() => StartConnection(IP));
        }
    }
}