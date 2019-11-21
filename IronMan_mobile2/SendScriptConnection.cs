using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace IronMan_mobile2
{
    public class SendScriptConnection
    {
        private static void StartConnection(string IP, string fileName, string script)
        {
            const int port = 8080;
            TcpClient client = null;
            try
            {
                client = new TcpClient(IP, port);
                NetworkStream stream = client.GetStream();
                var fileNameAndScript = fileName + "*" + script;
                var fileNameAndScriptData = Encoding.UTF8.GetBytes(fileNameAndScript);
                while (true)
                {
                    stream.Write(fileNameAndScriptData, 0, fileNameAndScriptData.Length);
                    byte[] buffer = new byte[256];
                    string[] files;
                    do
                    {
                        var fileBytes = stream.Read(buffer, 0, buffer.Length);
                        files = Encoding.Unicode.GetString(buffer, 0, fileBytes).Split("*");
                    } while (stream.DataAvailable);
                    foreach (var file in files)
                    {
                        string newScript = file;
                        if (Editor.scriptsList.Contains(newScript) == false)
                        {
                            Editor.scriptsList.Add(newScript);
                        }
                    }
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                client?.Close();
            }
        }
        
        public static async void StartConnectionAsync(string IP, string fileName, string script)
        {
            await Task.Run(() => StartConnection(IP, fileName, script));
        }
    }
}