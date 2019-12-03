using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Android.Util;

namespace IronMan_mobile2
{
    public class SendScriptConnection
    {
        private static void StartConnection(string IP, string fileName, string script)
        {
            const int port = 6121;
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(IP), port);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(endPoint);
                var fileNameAndScript = fileName + "*" + script;
                var fileNameAndScriptData = Encoding.UTF8.GetBytes(fileNameAndScript);
                socket.Send(fileNameAndScriptData);
                /*byte[] arr = new byte[1024];
                int bytes = 0;
                do
                {
                    bytes = socket.Receive(arr);
                    Editor.filmData = Encoding.UTF8.GetString(arr, 0, bytes).Split("*");
                } while (socket.Available > 0);*/
                
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        public static async void StartConnectionAsync(string IP, string fileName, string script)
        {
            await Task.Run(() => StartConnection(IP, fileName, script));
        }
    }
}