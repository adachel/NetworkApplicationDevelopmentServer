using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkApplicationDevelopmentServer.HomeWorks.HomeWork1
{
    internal class ServerHW1
    {
        

        public MessageHW1? message;

        public byte[]? buffer;

        public string? messageText;




        public void MyServer(int port)
        {
            UdpClient udpClient = new UdpClient(port);

            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0);

            Console.WriteLine("Сервер ждет сообщение от Клиента");

            while (true)
            {
                buffer = udpClient.Receive(ref iPEndPoint);

                if (buffer == null) break;

                messageText = Encoding.UTF8.GetString(buffer);

                message = MessageHW1.DeserializeFromJson(messageText);
                message?.Print();



                buffer = Encoding.UTF8.GetBytes("Сервер получил сообщение");

                //udpClient.Send(buffer, buffer.Length, iPEndPoint);
            }
        }
    }
}
