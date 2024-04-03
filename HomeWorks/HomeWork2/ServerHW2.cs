using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkApplicationDevelopmentServer.HomeWorks.HomeWork2
{
    // В коде сервера добавьте ожидание нажатия клавиши, чтобы также прекратить его работу.
    internal class ServerHW2
    {
        private MessageHW2? message;
        private byte[]? buffer;
        private string? messageText;

        private void MyServer()
        {
            UdpClient udpClient = new UdpClient(12345);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0);

            Console.WriteLine("Сервер ждет сообщение от Клиента");

            while (true)
            {
                buffer = udpClient.Receive(ref iPEndPoint);

                if (buffer == null) break;

                messageText = Encoding.UTF8.GetString(buffer);
                message = MessageHW2.DeserializeFromJson(messageText);
                message?.Print();

                buffer = Encoding.UTF8.GetBytes("Сервер получил сообщение");
                udpClient.Send(buffer, buffer.Length, iPEndPoint);
            }
        }

        private void ReadKey()
        { 
            Console.ReadKey();
        }

        public void RunServer()
        {
            Task.WaitAny(new Task[] {Task.Run(MyServer),  Task.Run(ReadKey)});
        }
    }
}
