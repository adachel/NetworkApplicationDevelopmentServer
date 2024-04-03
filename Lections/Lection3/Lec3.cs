using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkApplicationDevelopmentServer.Lections.Lection3
{
    internal class Lec3
    {
        // Task Parallel Library – библиотека параллельных задач, предназначенная для работы с асинхронными операциями.
        // Библиотека TPL основана на Task — специальном классе, отвечающим за параллельное выполнение.
        // У Task если возникло исключение, он сохраняет его в свойство Exeption (Task.Exeption)
        // var t = Task.Run(Print); // применение Run - создаст задачу и запустит ее. Возвр задачу.
        // Task.Factory.StartNew() - хранит экз объекта с помощью кот можно создавать новые задачи.
        // t.IsCompleted - false или true. Задача завершена успешно
        // t.IsCompletedSuccessfully - false или true. false - задача завершена c исключением
        // t.IsFaulted - false или true. Былла ли задача завершена с ошибкой. Обратная t.IsCompletedSuccessfully
        // t.Status = TaskStatus.RanToCompletion;  RanToCompletion - задача выполенена до конца. t.Status - статусы задачи в процессе выполнения

        // Методы Task
        // var t1 = new Task(WriteParallel);   var t2 = t1.ContinueWith((x) => WriteParallel()); задача t2 начнется только после окончания t1
        // Task.Deley(1000) - в отличии от Thread.Sleep не блокирует поток. Создает задачу, кот вып указанное число времени.
        // var t = Task.Delay(1000); t.Wait(); - тепрерь пауза есть.
        // Task.WaitAny() - принимает массив задач и ждет, когда завершится какая-нибудь залдача
        // Task.WhenAll() - возвр задачу. Ждет, когда вып входящие задачи
        // Task.WhenAny() - возвр задачу. Ждет, когда вып хотя-бы одна

        // async / await
        // await - ожидает выполнения задачи, блокируя поток, и возвращает результат ее выполнения.

        // ValueTask. По сути это всего лишь обертка над Task, у которой есть возможность ссылаться
        // либо на незавершенную задачу Task, либо же на результат выполнения метода.
        // Как несложно догадаться по названию, это value-тип, и как следствие, он располагается в стеке,
        // что дает прирост производительности при работе с ним.Если метод с Task был закончен синхронно,
        // то есть результат был получен мгновенно, ValueTask копирует его в свое свойство Result.

        // IAsyncDisposable. Данный интерфейс предоставляет механизм для асинхронного освобождения ресурсов.
        // Можно рассматривать интерфейс как асинхронный аналог IDisposable. 


        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Пример 1
        static object locObj = new object();
        static void Print()
        {
            lock (locObj) // код, кот закрыт lock будет выполняться только из одного потока, остальные потоки будут ждать получения ресурса
            {
                for (global::System.Int32 i = 0; i < 10; i++)
                {
                    Console.WriteLine($"{Thread.CurrentThread.GetHashCode()} : {i}");
                }
            }
        }

        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///  Пример 2
        static int GetValue1()
        {
            Thread.Sleep(1000);
            return 1;
        }

        static int GetValue2()
        {
            Thread.Sleep(2000);
            return 2;
        }

        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Task  <summary>
        
            /// Пример 1
        //static void WriteParallel() 
        //{
        //    Console.WriteLine("Привет");
        //}

            /// Ппример 2
        static private CancellationTokenSource cts = new CancellationTokenSource(); // механиизм, кот позволяет двум потокам взаимодействовать.
                                                                                                        // Один поток сообщает другому о том, что какое-то действие
                                                                                                        // должно быть прекращено (называется ТОКЕН ОТМЕНЫ)
        static private CancellationToken ct = cts.Token;

        static void WriteParallel()
        {
            int c = 0;
            while (!ct.IsCancellationRequested)  // способ остановить задачу
            { 
                Console.WriteLine("Привет " + c++);
                Thread.Sleep(100);
            }
        }





        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// 







        public void Run()
        {
            //for (int i = 0; i < 10; i++)
            //{
            //    //var t = new Task(Print); // все задачи в Task(по умолчанию) фоновые и закр по заверш приложения
            //    //t.Start(); // после созд Task его нужно запустить

            //    // var t = Task.Run(Print); // применение Run - создаст задачу и запустит ее. Возвр задачу
            //    //Task.Run(Print); // то же самое
            //}
            //Console.ReadLine();


            //List<Task> tasks = new List<Task>();
            //for (int i = 0; i < 10; i++)
            //{
            //    tasks.Add(Task.Run(Print));
            //}
            //Task.WaitAll(tasks.ToArray()); // теперь без Console.ReadLine()


            //////////////////////////////////////////////////////////////////////////////////////////////////////// 
            /// Пример 2.
            //Task<int> v1 = Task.Run(GetValue1); 
            //Task<int> v2 = Task.Run(GetValue2);
            //v1.Wait(); // Ожидает завершения выполнения задачи Task.
            //v2.Wait();
            //Console.WriteLine(v1.Result + v2.Result);

            //////////////////////////////////////////////////////////////////////////////////////////////////////
            /// Класс Task
            /// пример 1
            //Action action = WriteParallel;
            //var t = new Task(action);
            //t.Start();
            //t.Wait();  // ожидание выполнения Task, если не ждать, то программа выполнится быстрее чем Task 

            /// пример 2
            //Task t = new Task(WriteParallel, ct);
            //t.Start();
            //Thread.Sleep(100);
            //cts.Cancel();

            var t = Task.Delay(1000); t.Wait();
            
        }


    }
}
