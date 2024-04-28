using NetworkApplicationDevelopment.Lections.Lection1;
using NetworkApplicationDevelopmentServer.HomeWorks.HomeWork2;
using NetworkApplicationDevelopmentServer.Lections.Lection2;
using NetworkApplicationDevelopmentServer.Lections.Lection3;
using NetworkApplicationDevelopmentServer.Lections.Lection4;
using NetworkApplicationDevelopmentServer.Lections.Lection5;
using NetworkApplicationDevelopmentServer.Lections.Lection6;
using NetworkApplicationDevelopmentServer.Lections.Lection7;
using NetworkApplicationDevelopmentServer.Seminars.Seminar1;
using NetworkApplicationDevelopmentServer.Seminars.Seminar2;

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
            //int port = 12345;
            //ServerHW2 server = new ServerHW2();
            //server.MyServer(port);

            ////////////////////////////////////////////////////////////////////////////////////////////
            /// Лекция 2. Синхронизации: многопоточность, создание и завершение потоков
            /// 
            //Lec2 lec1 = new Lec2();
            //lec1.Run();

            /////////////////////////////////////////////////////////////////////////////////////
            /// Семинар 2
            /// 
            //Sem2 sem2 = new Sem2();
            //sem2?.Run();

            ////////////////////////////////////////////////////////////////////////////////////
            /// HomeWork 2
            /// 
            //ServerHW2 hw2 = new ServerHW2();
            //hw2.RunServer();


            ////////////////////////////////////////////////////////////////////////////////////////////
            /// Лекция 3.  PLINQ и асинхронность: как работает, области применения
            /// 
            //Lec3 lec3 = new Lec3();
            //lec3.Run();

            ////////////////////////////////////////////////////////////////////////////////////////////
            /// Лекция 4. GOF: паттерны проектирования в .Net разработке
            /// 
            //Lec4 lec4 = new Lec4();
            //lec4.Run();


            ///////////////////////////////////////////////////////////////////////////////////////////
            /// Лекция 5. Базы данных: Entity framework, code first/db first
            /// 
            //Lec5 lec5 = new Lec5();
            //lec5.Run();


            //////////////////////////////////////////////////////////////////////////////////////////////
            /// Лекция 6. Тестирование приложений: test driven development
            /// 
            //Lec6 lec6 = new Lec6();
            //lec6.Run();


            /////////////////////////////////////////////////////////////////////////////////////////////
            /// Лекция 7. Nuget и разработка собственных библиотек
            /// 
            Lec7 lec7 = new Lec7();
            lec7.Run();
        }

    }
}

