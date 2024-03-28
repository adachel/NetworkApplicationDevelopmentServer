using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkApplicationDevelopmentServer.Seminars.Seminar1.Task1
{
    internal class Util
    {

        public void MyServer(string Name)
        {
            UdpClient udpClient = new UdpClient(12345);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0);
            Console.WriteLine("Сервер ждет сообщение от Клиента");

            while (true) 
            {
                byte[] buffer = udpClient.Receive(ref iPEndPoint); // это то что получили

                if (buffer == null) break;

                var messageText = Encoding.UTF8.GetString(buffer);
                
                Message? message = Message.DeserializeFromJson(messageText);
                message?.Print();
            }
        
        
        }
        
    }
}
