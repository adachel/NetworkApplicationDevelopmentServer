using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace NetworkApplicationDevelopment.Lections.Lection1
{
    internal class Lec1Server
    {
        //● Пространство имен System.Net
        //● Модель OSI
        //● Socket
        //● TCP
        //● TcpClient
        //● UDP
        //● UdpClient
        //● ICMP
        //● HTTP

        //● Пространство имен System.Net 

        // Модель OSI
        //1. Физический слой(Physical Layer) - ethernet, wifi, bluetooth, usb. основная ед передачи инфо  --- бит 
        //2. Канальный слой(Data Link Layer) - протоколы ppp, ppp over ethernet, 802.1. ед передачи инфо  --- фрейм или кадр 
        //3. Сетевой слой(Network Layer)     - протоколы ip, nat. ед передачи инфо  --- пакет
        //4. Транспортный слой(Transport Layer) - протоколы TCP и UDP. ед передачи инфо  --- сегмент или датаграмма
        //5. Сеансовый слой(Session Layer)   - протоколы rpc, netBios, pptp. ед передачи инфо  --- данные
        //6. Представительский слой(Presentation Layer) - преобразовывает данные в формат, понятный для приложений. протоколы tlc, ccl. ед передачи инфо  --- данные
        //7. Прикладной слой(Application Layer) - предоставляет интерфейс для доступа к сети. протоколы http, ftp. ед передачи инфо  --- данные

        // C# плохо работает с первыми тремя

        // сетевой протокол - набор правил, в соотв с кот происходит передача данными между системами

        // класс Socket -  это абстракция, программный интерфейс, позволяющий процессам общаться между собой путем обмена данными.
        // Сетевой сокет представляет из себя комбинацию IP-адреса и номера порта, идентифицирующие конкретное сетевое соединение.
        // Чтобы установить соединение, сокеты должны находится как в клиентском, так и в серверном процессе.

        // TCP. Протокол гарантирует доставку данных а также их целостность и очерёдность.
        // На основе протокола строят системы передачи данных, где важна гарантия доставки.
        // На основе протокола TCP работают многие протоколы прикладного уровня, такие как HTTP, FTP,
        // почтовые протоколы и многие другие. Чтобы передать данные посредством протокола,
        // сначала нужно установить соединение с получателем.

        // TcpListener. Основное назначение класса – это “слушать” сеть, ожидая новых TCP-подключений.
        // Класс предоставляет удобные методы создания серверного соединения и ожидания.

        // UDP – протокол (4 уровень модели OSI) транспортного уровня модели OSI.
        // Является самым быстрым сетевым протоколом.Скорость его работы обусловлена отсутствием
        // подтверждения получения сообщения получателем.Отправителю не важно,
        // получил ли получатель данные – он просто отправляет пакеты и не ждет ничего в ответ.
        // Протокол находит свое применение в тех системах, где потеря данных менее важна, чем ожидание этих данных.
        // Примерами таких систем является сервисы потокового вещания (например Netflix или Кинопоиск),
        // сервисы видео или же голосовых звонков, такие как Google-meet или Skype, системы, транслирующие удаленный экран.

        // UdpClient. Поскольку UDP не нужен коннект для обмена данными, то для него нет аналога TcpListener.
        // Таким образом, все, что нужно для работы UDP, это два клиента и данные, которые они будут отсылать.
        //........................................
        // В UdpClient не существует потоков!!!

        // ICMP – протокол (3 уровень модели OSI), предназначенный для определения маршрутов,
        // определения доступности удаленного сервера и многих других низкоуровневых задач,
        // обеспечивающих работоспособность сетей связи.Многим протокол уже знаком по утилите ping,
        // предназначенной для проверки доступности узлов сети и их времени отклика.

        // HTTP (Hypertext transfer protocol) – протокол (7 уровень модели OSI) интернета, работающий по принципу клиент-сервер(запрос-ответ).
        // Протокол является основой для передачи данных в интернете. Например, веб-браузер,
        // открывая страницу вашего любимого сайта, делает это именно по протоколу HTTP.


        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Сервер на сокете начало
        //public void Run()
        //{
        //    // мин код для работы с Socket ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    using (Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)) // AddressFamily - задает
        //                                                                                                          // тип адресации сети(InterNetwork - это IP),
        //                                                                                                          // SocketType - тип сокета(Stream - потоковый),
        //                                                                                                          // ProtocolType - тип протокола(Tcp)
        //                                                                                                          // сокет listener создан
        //    {
        //        // var localEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345); // создаем экз класса IPEndPoint для хранения ip адреса и порта
        //        // var localEndPoint = new IPEndPoint(IPAddress.Any, 12345); // если не знаем адрес
        //        var localEndPoint = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 12345); // можно так


        //        listener.Blocking = false; // устанавливаем свойства сокета Blocking в true, будет ждать завершение операции, блокируя ожидающий поток,
        //                                   // если falde - то сокет не ждет завершения, генерирует исключениеб если операция не будет выполнена.
        //                                   // заставляем сокет совершать все операции в блокирующим основной поток режиме

        //        Console.WriteLine("Сокет привязан к " + listener.IsBound);

        //        listener.Bind(localEndPoint); // привязываем сокет к ip адресу и порту методом Bind

        //        Console.WriteLine("Сокет привязан к " + listener.IsBound);

        //        listener.Listen(100); // переводит сокет в режим прослушивания

        //        Console.WriteLine("I`m server. Waiting for connection...");

        //        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        //        /// метод селект
        //        /// 
        //        while (true) 
        //        {
        //            List<Socket> sread = new List<Socket>{listener};
        //            List<Socket> swrite = new List<Socket>{};
        //            List<Socket> serror = new List<Socket>{};

        //            Socket.Select(sread, swrite, serror, 100);

        //            if (sread.Count > 0)
        //            { 
        //                break;
        //            }
        //            else 
        //            {
        //                Console.Write('.');
        //                Thread.Sleep(500);
        //            }
        //        }
        //        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        //        // закоментил, т.к есть select


        //        // var socket = listener.Accept();  // метод  Accept ожидает соединения. Поскольку блокирующий режим был выбран,
        //                                            // метод ожидает соединения и блокирует выполнения приложения,
        //                                            // до тех пор пока соединение не будет установлено 
        //                                            // когда соед установлено, метод создает новый сокет. В данном случае соединение сохраняем в socket
        //                                            // в дальнейшем socket, кот вернули из метода, можно исп для обмена данными по уст соед, а сокет,
        //                                            // кот был создан ранее listener(серверный сокет) будет заниматься ожиданием очередного соединения.

        //        // это другой вариант do while
        //        //while (!listener.Poll(100, SelectMode.SelectRead))  // SelectRead - означает, что в сокете есть данные.
        //        //                                                    // Если true, значит данные есть, иначе рисуем точки
        //        //{
        //        //    Console.Write('.');
        //        //    Thread.Sleep(1000);
        //        //}


        //        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //        /// измениели listener.Blocking на false
        //        /// 
        //        Socket? socket = null;
        //        do
        //        {
        //            try
        //            {
        //                //socket = listener.Accept(); // созд новый соке для свершившегося подключения
        //                // socket.LingerState = new LingerOption(true, 1); // сколько сокет будет дожидаться отправки всех записанных
        //                // в сокет данных после того как вызвали метод Close

        //                Task<Socket> task = listener.AcceptAsync(); // возвр задача. на момент, когда получили, еще соединения нет, задача вып паралельно
        //                task.Wait(); // ожидаем ее выполнение

        //                socket = task.Result;  // после того как выполн, получаем Result из task. Result будет = <Socket>
        //            }
        //            catch
        //            {
        //                Console.Write('.');
        //                Thread.Sleep(1000); // приостанавливает выполнение потока
        //            }
        //        }
        //        while (socket == null);  // ждет, пока не ответит клиент


        //        Console.WriteLine("Connected!");
        //        Console.WriteLine("localEndPoint" + socket.LocalEndPoint);
        //        Console.WriteLine("remoteEndPoint" + socket.RemoteEndPoint);


        //        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //        /// получаем данные
        //        /// 
        //        byte[] buffer = new byte[255]; // созд пустой массив байт

        //        /////////////////////////////////////////////////////////////////////////////////////
        //        /// свойства сокета
        //        /// 
        //        while (socket.Available == 0) ; // Available хранит кол-во инфо в сокете, доступной для чтения

        //        Console.WriteLine("доступно " + socket.Available + " байт для чтения");


        //        // socket.ReceiveTimeout = 5000;

        //        int count = socket.Receive(buffer); // хранит кол-во байт 

        //        if (count > 0) // значит что-то получили
        //        {
        //            string message = Encoding.UTF8.GetString(buffer); // здесь Encoding преобр байты в строку
        //            Console.WriteLine(message);
        //        }
        //        else Console.WriteLine("Сообщение не получено...");


        //        listener.Close();
        //        // listener.Close(1000); // работает с задержкой

        //    }
        //    // для проверки: 1. запускаем программу. 2. в адресной строке браузера вбиваем 127.0.0.1. должен быть Connected!

        //}

        /// Сервер на сокете конец
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Сервер для UDP начало
        /// 
        //public void Run()
        //{

        //    using (var socketUDP = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
        //    {
        //        var localEndPoint = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 1234);

        //        socketUDP.Bind(localEndPoint);

        //        byte[] buffer = new byte[1];

        //        int count = 0;

        //        while (count < 200)
        //        {
        //            EndPoint endPoint = new IPEndPoint(IPAddress.Any, 0); // добавили для фильр приема с опред сокета оба варианта

        //            var sf = new SocketFlags(); // сорт для второго варианта

        //            // int c = socketUDP.Receive(buffer);
        //            // int c = socketUDP.ReceiveFrom(buffer, ref endPoint); // добавили для фильр приема с опред сокета первый вариант
        //            int c = socketUDP.ReceiveMessageFrom(buffer, 0, 1, ref sf, ref endPoint, out IPPacketInformation info);
        //            // добавили для фильр приема с опред сокета второй вариант

        //            if (c == 1)
        //            {
        //                //if ((endPoint as IPEndPoint)?.Port == 2235) // добавили для фильр приема с опред сокета
        //                //    Console.Write(buffer[0]);

        //                //////////////////////////////
        //                /// умножим на 2 и отпр клиенту результат
        //                /// 
        //                var buffOut = new byte[] { (byte)(buffer[0] * 2) };
        //                socketUDP.SendTo(buffOut, endPoint);  // SendTo отпр данные туда, откуда пришли
        //            }

        //            count += c;
        //        }

        //        Console.WriteLine("Причитали 200 байт");
        //    }

        //}

        /// Сервер для UDP конец
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Сервер TcpListener
        /// Основное назначение класса – это “слушать” сеть, ожидая новых TCP-подключений. 
        /// Класс предоставляет удобные методы создания серверного соединения и ожидания.

        //public void Run()
        //{
        //    var listener = new TcpListener(IPAddress.Any, 12345);
        //    listener.Start();

        //    using (Socket socket = listener.AcceptSocket())
        //    {
        //        listener.Stop();

        //        Console.WriteLine("localEndPoint" + socket.LocalEndPoint);
        //        Console.WriteLine("remoteEndPoint" + socket.RemoteEndPoint);

        //        byte[] buffer = new byte[255];
        //        while (socket.Available == 0) ;

        //        Console.WriteLine("доступно " + socket.Available + " байт для чтения");

        //        int count = socket.Receive(buffer);

        //        if (count > 0)
        //        {
        //            string message = Encoding.UTF8.GetString(buffer);
        //            Console.WriteLine(message);
        //        }
        //        else Console.WriteLine("Сообщение не получено...");
        //    }
        //}
        /// Сервер TcpListener конец
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Сепвер TCPClient начало
        ///
        //public void Run()
        //{
        //    var listener = new TcpListener(IPAddress.Any, 12345);
        //    listener.Start();

        //    using (TcpClient client = listener.AcceptTcpClient())
        //    {
        //        listener.Stop();

        //        Console.WriteLine($"localEndPoint" + client.Client.LocalEndPoint);
        //        Console.WriteLine($"remoteEndPoint" + client.Client.RemoteEndPoint);

        //        while (client.Available == 0) ;

        //        Console.WriteLine("доступно " + client.Available + " байт для чтения");

        //        using (var stream = client.GetStream())
        //        {
        //            byte[] buffer = new byte[255];

        //            int count = stream.Read(buffer, 0, buffer.Length);

        //            if (count > 0)
        //            {
        //                string message = Encoding.UTF8.GetString(buffer);
        //                Console.WriteLine(message);
        //            }
        //            else Console.WriteLine("Сообщение не получено...");
        //        }
        //    }
        //}
        /// Сервер TCPClient конец
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Сервер со StreamReader и StreamWriter(умеют раюотать с потоками как с текстом)
        /// 
        //public void Run()
        //{
        //    var listener = new TcpListener(IPAddress.Any, 12345);
        //    listener.Start();

        //    using (TcpClient client = listener.AcceptTcpClient())
        //    {
        //        Console.WriteLine($"Connected");

        //        using (var reader = new StreamReader(client.GetStream()))
        //        {
        //            Console.WriteLine(reader.ReadLine());
        //        }
        //    }
        //}

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Сервер со StreamReader и StreamWriter заставим получать строку, переворачивать ее и отправлять обратно
        /// 
        //public void Run()
        //{
        //    var listener = new TcpListener(IPAddress.Any, 12345);
        //    listener.Start();

        //    using (TcpClient client = listener.AcceptTcpClient())
        //    {
        //        Console.WriteLine($"Connected");

        //        var reader = new StreamReader(client.GetStream());
        //        var writer = new StreamWriter(client.GetStream());

        //        var s = reader.ReadLine(); // получаем строку из ридера и выводим ее на экран
        //        Console.WriteLine(s);

        //        var r = new System.String(s.Reverse().ToArray()); // реверсируем строку

        //        writer.WriteLine(r); // 'записываем' в сеть

        //        writer.Flush(); // чтобы успеть сбрисить внутренние буферы потока до того как мы выйдем отсюда
        //                        // и закончим работу с TCPClient
        //    }

        //}

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// UDP. На более высоком уровне. Напишем в одном классе при помощи многопоточности
        /// 
        //private static IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
        //static void Receiver()
        //{
        //    using (var client = new UdpClient(1234))
        //    { 
        //        var lp = new IPEndPoint(IPAddress.Any, 1234);

        //        int cnt = 0;
        //        while (cnt < 9_000) 
        //        {
        //            var recv = client.Receive(ref lp);

        //            foreach (var item in recv)
        //            {
        //                Console.Write(item + " ");
        //            }
        //            cnt += recv.Length;
        //        }
        //        Console.WriteLine("End");
        //    }
        //}

        //static void UdpSender(byte b)
        //{
        //    using (var client = new UdpClient())
        //    {
        //        client.Connect(ep);

        //        for (int i = 0; i < 3000; i++) // шлем 3000 раз по одному байту
        //        {
        //            client.Send(new byte[] { b });
        //        }

        //    }

        //}

        //public void Run()
        //{
        //    new Thread(Receiver).Start();   // запускаем поток ресивера

        //    for (int i = 0; i < 3; i++) // три раза созд отправителей данных
        //    {
        //        byte t = (byte)i; // преобразуем в локальную переменную

        //        new Thread(() => { UdpSender(t); }).Start(); // запускаем поток
        //    }

        //}

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// UDP. На более высоком уровне. Напишем в одном классе при помощи многопоточности.  <summary>
        /// UDP. На более высоком уровне. Напишем в одном классе при помощи многопоточности. 
        /// 
        /// 
        /// Реализуем свой протокол поверх UDP, нумерующий каждый отправляемый пакет и игнорирующий те, 
        /// что пришли с нарушенной очередностью.
        /// Модернизируем и покажем, как можно реализовать подобный алгоритм на примере передачи строк, 
        /// очередность кот по какойто причине крайне важна и лучше пропустить одну, чем отобразить ее в не очереди.
        /// 
        //private static IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
        //static void Receiver()
        //{
        //    using (var client = new UdpClient(1234))
        //    {
        //        var lp = new IPEndPoint(IPAddress.Any, 1234);

        //        int lastPacketNum = -1;

        //        while (lastPacketNum < 255)
        //        {
        //            var recv = client.Receive(ref lp);

        //            // смотрим текущий пакет
        //            var curPacketNum = recv[0];

        //            if (curPacketNum > lastPacketNum) // если пакет, кот мы получили больше пакета по номеру кот у нас есть 
        //            {
        //                lastPacketNum = curPacketNum; // делаем последним полученный пакет

        //                var bs = new byte[recv.Length - 1];

        //                Array.Copy(recv, 1, bs, 0, recv.Length - 1); // копир, то что получили в массив

        //                var s = Encoding.ASCII.GetString(bs);   // получаем строку

        //                Console.WriteLine(s);
        //            }
        //            else Console.WriteLine("пакет пропущен");  // если номер не совпадает
        //        }
        //        Console.WriteLine("End");
        //    }
        //}

        //static void UdpSender()
        //{
        //    using (var client = new UdpClient())
        //    {
        //        client.Connect(ep);

        //        for (int i = 0; i < 256; i++) // после того как клиент соед с сервером, он шлет 256 пакетов
        //        {
        //            // каждый из пакетов будет выглядеть так:
        //            var data = $"Line ${i}"; // i - номер строки
        //            var bdata = Encoding.ASCII.GetBytes(data);
        //            var packet = new byte[bdata.Length + 1];
        //            packet[0] = (byte)i;

        //            Array.Copy(bdata, 0, packet, 1, bdata.Length);

        //            client.Send(packet); // отправляем
        //        }
        //    }
        //}

        //public void Run()
        //{
        //    new Thread(Receiver).Start();   // запускаем поток ресивера
        //    new Thread(UdpSender).Start();   // запускаем поток ресивера

        //}

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ICMP ping. Ping - время до мервера и обратно
        /// 
        public void Run()
        {
            Ping ping = new Ping();
            for (int i = 0; i <= 10; i++)
            {
                PingReply reply = ping.Send("yandex.ru", 1000);
                Console.WriteLine($"Ping replied in {reply.RoundtripTime}");
            }

        }








    }
}
