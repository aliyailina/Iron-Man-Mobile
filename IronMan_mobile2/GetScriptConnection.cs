using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace IronMan_mobile2
{
    public class GetScriptConnection
    {
        private static void StartConnection(string IP)
        {
            const int port = 13000;
            string[] files;
            TcpClient client = null;
            try
            {
                client = new TcpClient(IP, port);
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[5000];
                do
                {
                    var fileBytes = stream.Read(buffer, 0, buffer.Length);
                    files = Encoding.Unicode.GetString(buffer, 0, fileBytes).Split("*");
                    
                } while (stream.DataAvailable);

                foreach (var file in files)
                {
                    string newScript = file;
                    if (!Editor.scriptsList.Contains(newScript) || !string.IsNullOrWhiteSpace(file))
                    {
                        Editor.scriptsList.Add(newScript);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                client?.Close();
            }
        }
        
        public static async void StartConnectionAsync(string IP)
        {
            await Task.Run(() => StartConnection(IP));
        }
    }
}