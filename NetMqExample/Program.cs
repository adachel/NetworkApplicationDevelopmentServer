using NetMQ;
using NetMQ.Sockets;






internal class Program
{
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// для однопотока
    //static void Client(int number)
    //{
    //    using (var client = new RequestSocket())
    //    {
    //        var r = new Random();   // генерируем случайное число
    //        Task.Delay(r.Next(100, 1000)).Wait();

    //        client.Connect("tcp://127.0.0.1:5556"); // соединение с адресом
    //        client.SendFrame(number.ToString()); // отправляем случайное число NetMQ серверу

    //        var msg = client.ReceiveFrameString();  // получаем ответ от сервера
    //        Console.WriteLine($"Client {number} recieved from Server: {msg}");  // выводим, что отправили, что получили
    //    }
    //}

    //static void Server()
    //{
    //    using (var server = new ResponseSocket()) // ResponseSocket - принимать данные и отвечать. Еденица данных - Frame
    //    {
    //        int i = 0;
    //        server.Bind("tcp://*:5556"); // методом Bind привязываем к любому ip адресу с портом 5556
    //        while (i < 10)  // 10 раз получаем сообщение
    //        {
    //            string msg = server.ReceiveFrameString(); // сообщение получаем с помощью метода ReceiveFrameString
    //            server.SendFrame(msg); // ответ посылаем с помощью метода SendFrame, туда, откуда получили в ReceiveFrameString
    //            i++;
    //        }
    //    }
    //}

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// для многопотока
    //static void Client(int number)
    //{
    //    using (var client = new DealerSocket()) // DealerSocket - добавляет у себя под капотом инфо отправителя
    //    {
    //        var r = new Random();   // генерируем случайное число
    //        Task.Delay(r.Next(100, 1000)).Wait();

    //        client.Connect("tcp://127.0.0.1:5556"); // соединение с адресом
    //        client.SendFrame(number.ToString()); // отправляем два фрейма: адрес и случайное число

    //        var msg = client.ReceiveFrameString();  // получаем ответ от сервера
    //        Console.WriteLine($"Client {number} recieved from Server: {msg}");  // выводим, что отправили, что получили
    //    }
    //}

    //static void Server()
    //{
    //    using (var server = new RouterSocket()) // ResponseSocket - принимать данные и отвечать. Еденица данных - Frame
    //    {
    //        int i = 0;
    //        server.Bind("tcp://*:5556"); // методом Bind привязываем к любому ip адресу с портом 5556
    //        while (i < 10)  // 10 раз получаем сообщение
    //        {
    //            var msg = server.ReceiveMultipartMessage(); // принимает спец тип ReceiveMultipartMessage

    //            Console.WriteLine("Получено сообщение: " + msg.Last.ConvertToString());
    //            Task.Run(() =>
    //            {
    //                var responseMessage = new NetMQMessage(); // создаем responseMessage тип NetMQMessage
    //                responseMessage.Append(msg.First()); // из msg добавляем первый фрейм, кот содержит адрес
    //                responseMessage.Append(msg.Last.ConvertToString()); // второй фрейм содержит случайное число, кот отправляли
    //                server.SendMultipartMessage(responseMessage);
    //            });
    //            i++;
    //        }
    //    }
    //}


    /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// Тип Издатель - Подписчик. NetMQ может отправлять цепочку фреймов
    static void Client(string topicName)
    {
        using (var subscriber = new SubscriberSocket()) // DealerSocket - добавляет у себя под капотом инфо отправителя
        {
            subscriber.Connect("tcp://127.0.0.1:5556"); // соединение с адресом
            subscriber.Subscribe(topicName);

            while (true)
            {
                var topic = subscriber.ReceiveFrameString(); // получаем имя канала 
                var message = subscriber.ReceiveFrameString(); // само сообщение
                Console.WriteLine($"Recieved message with topic '{topic}': {message}");
            }
        }
    }

    static void Server()
    {
        using (var publisher = new PublisherSocket()) // сервер отпр сообщение из двух фреймов, где первый имя канала и второй само сообщение
        {

            publisher.Bind("tcp://*:5556"); // методом Bind привязываем к любому ip адресу с портом 5556
            while (true)  
            {
                Console.WriteLine("Publishing message to topic1..."); // topic - имя канала
                publisher.SendMoreFrame("topic1").SendFrame("This is a message with topic1"); // SendMoreFrame - отпр имя канала,
                                                                                              // SendFrame - сообщение

                Console.WriteLine("Publishing message to topic2...");
                publisher.SendMoreFrame("topic2").SendFrame("This is a message with topic2");
            }
        }
    }


    private static void Main(string[] args)
    {
        // обычный запуск
        //Task taskSrv = Task.Run(Server); // запускаем сервер

        //Task.Delay(1000).Wait(); // ждем 1000 млсек

        //for (int i = 0; i < 10; i++) // запускаем 10 клиентов
        //{
        //    int x = i;
        //    Task TaskCln = Task.Run(() => Client(x));
        //}

        //Task.WaitAll(taskSrv);


        // заменим последовательность. Сначала запустим клиентов. NetMQ умеет буферизировать сообшения и гарантирует доставку
        //for (int i = 0; i < 10; i++) // запускаем 10 клиентов
        //{
        //    int x = i;
        //    Task TaskCln = Task.Run(() => Client(x));
        //}

        //Task.Delay(1000).Wait();

        //Task taskSrv = Task.Run(Server);

        //Task.WaitAll(taskSrv);   

        ////////////////////////////////////////////////////////////////////////////////
        /// для издатель - подписчик
        /// 
        Task taskSrv = Task.Run(Server);

        Task taskCln1 = Task.Run(() => Client("topic1")); // ервый клиент подписан на topic1
        Task taskCln2 = Task.Run(() => Client("topic2"));

        Task.Delay(1000).Wait();
    }
}