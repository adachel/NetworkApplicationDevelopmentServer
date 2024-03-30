using NetworkApplicationDevelopment.Lections.Lection1;
using NetworkApplicationDevelopmentServer.HomeWorks.HomeWork1;
using NetworkApplicationDevelopmentServer.Lections.Lection2;
using NetworkApplicationDevelopmentServer.Seminars.Seminar1;

namespace NetworkApplicationDevelopment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ///////////////////////////////////////////////////////////
            /// Лекция1. Работа с сетью: чтение и запись данных в сеть. Клиентские и серверные приложения
            //Lec1Server lec1 = new Lec1Server ();
            //lec1.Run();

            //////////////////////////////////////////////////////////////////////////////////////////////////
            /// Seminar1
            /// 
            //Sem1 sem1 = new Sem1();
            //sem1.Run();

            /////////////////////////////////////////////////////////////////////////////////////
            /// HomeWork 1
            /// 
            int port = 12345;
            ServerHW1 server = new ServerHW1();
            server.MyServer(port);

            ////////////////////////////////////////////////////////////////////////////////////////////
            /// Лекция 2. Синхронизации: многопоточность, создание и завершение потоков
            /// 
            //Lec2 lec1 = new Lec2();
            //lec1.Run(); 
        }
    }
}
