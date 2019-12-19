using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace IronMan_mobile2
{
    public class RunScriptConnection
    {
        public static void StartConnection(string IP)
        {
            const int port = 30000;
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(IP), port);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                {
                    SendTimeout = 3000, ReceiveTimeout = 3000
                };
                socket.Connect(endPoint);

                socket.Send(Encoding.UTF8.GetBytes(MainActivity.SelectedScripts));
                
                byte[] arr = new byte[1024];
                string result;
                int bytes = 0;
                bytes = socket.Receive(arr);
                result = Encoding.UTF8.GetString(arr, 0, bytes);

                Running.result = result;

                socket.Shutdown(SocketShutdown.Both);
                socket.Close();

            }
            catch (Exception e)
            {
                
            }
        }
        
        public static async void StartConnectionAsync(string IP)
        {
            await Task.Run(() => StartConnection(IP));
        }
    }
}