using NetworkApplicationDevelopmentServer.Seminars.Seminar1.Task1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkApplicationDevelopmentServer.Seminars.Seminar1
{
    internal class Sem1
    {
        // Task1. Cделать на UDP. пишем чат приложение, способное передавать сообщения с компъютера на компъютер - создали класс Message
        // Task2. Написать два метода класса, добавим Json сериализацию и десериализацию. Протестируем его путем пустого сообщения.
        // Task3. Написать прообраз чата. Это утилита, кот работает как сервер так и клиент,
        // взависимости от параметров командной строки. Клиент отправляет серверу и ждет ответа.
        public void Run()
        {
            Util util = new Util();
            util.MyServer("Hello");




            //var mess = new Message() {Text = "Text message", DateTime = DateTime.Now, NickNameFrom = "qwerty", NickNameTo = "asd" };
            //string str = mess.SerializeMessageToJson();
            //Console.WriteLine(str);
            //Console.WriteLine();

            //var deserializedMess = Message.DeserializeFromJson(str);
            //Console.WriteLine(deserializedMess);
        }
    }
}
