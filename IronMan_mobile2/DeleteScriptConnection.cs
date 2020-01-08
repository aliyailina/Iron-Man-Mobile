using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace IronMan_mobile2
{
    public class DeleteScriptConnection
    {
        public static void StartConnection(string IP, string script)
        {
            const int port = 49100;
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
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                

            }
            catch
            {
                // ignored
            }
        }
        
        public static async void StartConnectionAsync(string IP, ScriptItem script)
        { 
            await Task.Run(() => StartConnection(IP, script.ScriptName));
        }
    }
}