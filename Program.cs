using NetworkApplicationDevelopment.Lections.Lection1;

namespace NetworkApplicationDevelopment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ///////////////////////////////////////////////////////////
            /// Лекция1. Работа с сетью: чтение и запись данных в сеть. Клиентские и серверные приложения
            Lec1Server lec1 = new Lec1Server ();
            lec1.Run();
        }
    }
}
