using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Android.Util;

namespace IronMan_mobile2
{
    public static class SendScriptConnection
    {
        private static void StartConnection(string ip, string fileName, string script)
        {
            const int port = 6121;
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(endPoint);
                var fileNameAndScript = fileName + "*" + script;
                var fileNameAndScriptData = Encoding.UTF8.GetBytes(fileNameAndScript);
                socket.Send(fileNameAndScriptData);
                
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        public static async void StartConnectionAsync(string ip, string fileName, string script)
        {
            await Task.Run(() => StartConnection(ip, fileName, script));
        }
    }
}