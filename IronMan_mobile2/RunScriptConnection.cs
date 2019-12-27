using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Android.Widget;

namespace IronMan_mobile2
{
    public class RunScriptConnection
    {
        static string result;
        public static string StartConnection(string IP, string script)
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

                //socket.Send(Encoding.UTF8.GetBytes(Scripts.SelectedScripts));
                socket.Send(Encoding.UTF8.GetBytes(script));
                
                byte[] arr = new byte[1024];
                int bytes = 0;
                do
                {
                    bytes = socket.Receive(arr);
                    result = Encoding.UTF8.GetString(arr, 0, bytes);
                } while (socket.Available > 0);
                
                

                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                

            }
            catch
            {
                // ignored
            }

            return result;
        }
        
        public static async void StartConnectionAsync(string IP)
        { 
            foreach (var script in Scripts.SelectedScripts)
            {
                Running.Result = await Task.Run(() => StartConnection(IP, script.ScriptName));
            }
        }
    }
}