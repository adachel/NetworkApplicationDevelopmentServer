using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

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





        public void Run()
        {
            // мин код для работы с Socket ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            using (Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)) // AddressFamily - задает
                                                                                                                  // тип адресации сети(InterNetwork - это IP),
                                                                                                                  // SocketType - тип сокета(Stream - потоковый),
                                                                                                                  // ProtocolType - тип протокола(Tcp)
                                                                                                                  // сокет listener создан
            {
                // var localEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345); // создаем экз класса IPEndPoint для хранения ip адреса и порта
                // var localEndPoint = new IPEndPoint(IPAddress.Any, 12345); // если не знаем адрес
                var localEndPoint = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 12345); // можно так


                listener.Blocking = false; // устанавливаем свойства сокета Blocking в true, будет ждать завершение операции, блокируя ожидающий поток,
                                           // если falde - то сокет не ждет завершения, генерирует исключениеб если операция не будет выполнена.
                                           // заставляем сокет совершать все операции в блокирующим основной поток режиме

                Console.WriteLine("Сокет привязан к " + listener.IsBound);

                listener.Bind(localEndPoint); // привязываем сокет к ip адресу и порту методом Bind

                Console.WriteLine("Сокет привязан к " + listener.IsBound);

                listener.Listen(100); // переводит сокет в режим прослушивания

                Console.WriteLine("I`m server. Waiting for connection...");

                // var socket = listener.Accept();  // метод  Accept ожидает соединения. Поскольку блокирующий режим был выбран,
                                                    // метод ожидает соединения и блокирует выполнения приложения,
                                                    // до тех пор пока соединение не будет установлено 
                                                    // когда соед установлено, метод создает новый сокет. В данном случае соединение сохраняем в socket
                                                    // в дальнейшем socket, кот вернули из метода, можно исп для обмена данными по уст соед, а сокет,
                                                    // кот был создан ранее listener(серверный сокет) будет заниматься ожиданием очередного соединения.

                // это другой вариант do while
                while (!listener.Poll(100, SelectMode.SelectRead))  // SelectRead - означает, что в сокете есть данные.
                                                                    // Если true, значит данные есть, иначе рисуем точки
                {
                    Console.Write('.');
                    Thread.Sleep(1000);
                }


                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                /// измениели listener.Blocking на false
                /// 
                Socket? socket = null;
                do
                {
                    try
                    {
                        //socket = listener.Accept(); // созд новый соке для свершившегося подключения
                        // socket.LingerState = new LingerOption(true, 1); // сколько сокет будет дожидаться отправки всех записанных
                        // в сокет данных после того как вызвали метод Close

                        Task<Socket> task = listener.AcceptAsync(); // возвр задача. на момент, когда получили, еще соединения нет, задача вып паралельно
                        task.Wait(); // ожидаем ее выполнение

                        socket = task.Result;  // после того как выполн, получаем Result из task. Result будет = <Socket>
                    }
                    catch
                    {
                        Console.Write('.');
                        Thread.Sleep(1000); // приостанавливает выполнение потока
                    }
                }
                while (socket == null);  // ждет, пока не ответит клиент


                Console.WriteLine("Connected!");
                Console.WriteLine("localEndPoint" + socket.LocalEndPoint);
                Console.WriteLine("remoteEndPoint" + socket.RemoteEndPoint);


                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                /// получаем данные
                /// 
                byte[] buffer = new byte[255]; // созд пустой массив байт

                /////////////////////////////////////////////////////////////////////////////////////
                /// свойства сокета
                /// 
                while (socket.Available == 0) ; // Available хранит кол-во инфо в сокете, доступной для чтения
                
                Console.WriteLine("доступно " + socket.Available + " байт для чтения");
                

                // socket.ReceiveTimeout = 5000;

                int count = socket.Receive(buffer); // хранит кол-во байт 

                if (count > 0) // значит что-то получили
                {
                    string message = Encoding.UTF8.GetString(buffer); // здесь Encoding преобр байты в строку
                    Console.WriteLine(message);
                }
                else Console.WriteLine("Сообщение не получено...");


                listener.Close();
                // listener.Close(1000); // работает с задержкой

            }
            // для проверки: 1. запускаем программу. 2. в адресной строке браузера вбиваем 127.0.0.1. должен быть Connected!

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /// TCP Client
            /// cоздаем в проекте Client





        }
    }
}
