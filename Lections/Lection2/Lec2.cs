using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NetworkApplicationDevelopmentServer.Lections.Lection2
{
    internal class Lec2
    {
        // Многопоточность – это способность приложения (или среды) выполнять несколько задач (участков кода) одновременно(параллельно).

        // Поток – это последовательность инструкций, выполняемых одновременно с другой последовательностью инструкций.
        // В каждом приложении есть как минимум один поток, который называется главный поток приложения.
        // Код метода Main консольных приложений, например, выполняется в этом потоке. 
        // Потоки позволяют использовать всю мощь современных процессоров, повышая эффективность приложений.

        // Пул потоков позволяет нивелировать все издержки, связанные с созданием и уничтожением потоков,
        // путем предоставления уже готового потока для выполнения произвольного кода.

        // Примитивы синхронизации – это специальные классы, объекты которых помогают организовать совместный доступ к чему-либо из разных потоков.

        // Monitor/lock. Инструкция позволяет блокировать доступ к блоку кода, следующим за инструкцией,
        // для всех потоков, кроме того, который первым выполнит инструкцию.

        // Mutex. Примитив синхронизации, во многом похожий на lock, но, в отличие от последнего,
        // может быть применен для синхронизации доступа независимых процессов/приложений.

        // AutoResetEvent. С помощью этого примитива синхронизации поток может сигнализировать о наступлении события,
        // ожидаемого в другом потоке(переход в сигнальное состояние),
        // после чего автоматически сбрасывать объект в состояние ожидания сигнала.

        // ManualResetEven. Не переходит в несигнальное состояние автоматически по завершению WaitOne.
        // Для перехода в несигнальное состояние используется метод Reset.

        // EventWaitHandle. Примитив синхронизации, наследник абстрактного WaitHandle,
        // реализующий синхронизацию потоков в стиле Manual & AutoResetEvent, родителем которых он является.

        // WaitHandle. Абстрактный примитив синхронизации, унаследованный такими классами, как EventWaitHandle, Mutex, Semaphore.

        // Semaphore. Примитив синхронизации, позволяющий одновременную работу заранее определенного числа потоков,
        // тогда как остальные ждут своей очереди.

        // Interlocked. Атомарная операция – это такая операция, которая производится как одно целое
        // и не может быть выполнена наполовину или быть прервана в процессе из другого потока.
        // В силу архитектуры ПК и также современных языков программирования большинство операций не атомарны.

        // Concurrent collections. Пространство имен System.Collection.Generic предоставляет ряд потокобезопасных коллекций,
        // работать с которыми можно безопасно из нескольких потоков одновременно.При этом блокировка такая, как, например lock,
        // не требуется – коллекции умеют блокировать потоки только при доступе к определенным элементам или операциям,
        // выполняемым в данный момент из другого потока. Примерами таких коллекций являются:
        // ConcurrentDictionary<TKey,TValue> - словарь, позволяющий безопасно работать со своими элементами из разных потоков.
        // BlockingCollection<T> - коллекция позволяет добавлять и читать элементы из разных потоков.
        // ConcurrentQueue<T>, ConcurrentStack<T> – многопоточные версии очереди и стека.

        // ЛЮБОЙ ПОТОК ЛИБО ФОНОВЫЙ BACKGROUND  ЛИБО ПРИОРИТЕТНЫЙ FORGROUND.
        // Фоновые потоки не могут задержать приложение от завершения.
        // Приоритетные не дают приложению закрыться, пока они не завершаться.
        // Все потоки класса Threаd - приоритетные. 

        // ThreadPull.  Все потоки в пулле потоков являются фоновыми. Умеет динамически добавлять потоки. Потом он их не уничтожает,
        // а оставляет в своем хранилище для переиспользования.

        // PendingWorkItemCount - кол-во задач, кот планируется запустить

        // методы ThreadPull:
        // GetAvailableThreads - разница между макс кол-во потоков и кол-вом, выполн сейчас
        // GetMaxTreads - возвр мах кол-во workerTreads и ioTreads. workerTreads - это то,
        // что мы запускаем с QueueUserWorkItem, а ioTreads - то, что исп dotnet для работы с вводом выводом
        // QueueUserWorkItem<например string> - отвечает, за специфичный механизм помещения в локальный пулл потоков, доступный библиотеки TPL
        // RegisterWaitForSingleObject -    . Чтобы метод заработал нужно создать AutoResetEvent  в сост false (var qqq = new AutoResetEvent(false))
        // Этот метод позволяет выполнить какой-то метод по достижению определенного промежутка времени (Thread.RegisterWaitForSingleObject(qqq, какой-то метод, 1000 мс, true))
        // SetMaxThreads - мах кол-во потоков
        // SetMinThreads - мin кол-во потоков. Говорим, сколько мин потоков постоянно нужно держать


        // 11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111
        //static void PrintThread()
        //{
        //    for (int i = 0; i < 10; i++)
        //    {
        //        Console.Write($"{Thread.CurrentThread.ManagedThreadId} + one");
        //        Console.WriteLine();
        //        Console.Write($"{Thread.CurrentThread.ManagedThreadId} + two");
        //        Console.WriteLine();
        //        Console.Write($"{Thread.CurrentThread.ManagedThreadId} + three"); 
        //    }
        //}

        //static void PrintThreadThree(object x) // для третьего способа
        //{
        //    for (int i = 0; i < 10; i++)
        //    {
        //        Console.Write($"{x} + one");
        //        Console.WriteLine();
        //        Console.Write($"{x} + two");
        //        Console.WriteLine();
        //        Console.Write($"{x} + three");
        //    }
        //}

        // 222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222
        //static bool isActive = true; // указывает потоку,что ему нужно продолжать работу

        //static void ThreadProc() // для priority
        //{ 
        //    long c = 0;
        //    while ( isActive ) 
        //    {
        //        c++;
        //    }

        //    var t = Thread.CurrentThread;

        //    Console.WriteLine($"Thread {t.Name} with priority {t.Priority} made {c} interation"); // сколько получилось сделать итераций пока поток не завершился
        //}

        //static void ThreadProcState() // для ThreadState
        //{

        //    var t = Thread.CurrentThread;

        //    Console.WriteLine($"Состояние потока (вызов из ThreadProcState) - {t.ThreadState}"); // сколько получилось сделать итераций пока поток не завершился

        //    Thread.Sleep(300);
        //}

        // 333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333
        // методы потоков

        // метод AllocateDataSlot()
        // первый вариант
        //static LocalDataStoreSlot localDataStoreSlot = Thread.AllocateDataSlot(); // чтобы получить объукт localDataStoreSlot, нужно обр к методу AllocateDataSlot().

        //static void ThreadProcLocalDataStoreSlot(int x)
        //{
        //    for (int i = 0; i < 10; i++)
        //    {
        //        var data = ((int?)Thread.GetData(localDataStoreSlot)) ?? 0; // пусть будет хранится int, и мы будем считать сумму всех чисел от 0 до 9.
        //                                                                    // Если там ничего нет, то получаем 0.
        //                                                                    // localDataStoreSlot - это объект,кот будет уникален для каждогоиз потоков.
        //                                                                    // Чтобы в потоке получить экземпляр этого объекта мы используем (GetData), он возвр object,
        //                                                                    // хранящийся в этом слоте. чтобы нам потом записать данные мы делаем Thread.SetData,
        //                                                                    // указываем слот и данные, кот хотим записать. Если Thread.SetData ниразу не происходила,
        //                                                                    // то Thread.GetData из localDataStoreSlot вернет null,
        //                                                                    // поэтому возвращаемое значение проверяем на null и заменяем на 0, потом прибавляем i и пишем назад.

        //        Thread.SetData(localDataStoreSlot, data + x); // 
        //    }
        //    Console.WriteLine("Total = " + (((int?)Thread.GetData(localDataStoreSlot)) ?? 0));
        //}

        // второй вариант
        //[ThreadStatic]  // поле, помеченное этим атрибутом будет гарантированно уникальным, для каждого потока
        //static int a = 0;

        //static void ThreadProcLocalDataStoreSlot(int x)
        //{
        //    for (int i = 0; i < 10000; i++)
        //    {
        //        a += x;
        //    }
        //    Console.WriteLine("Total = " + a);
        //}


        // методы BeginCriticalRegion() и EndCriticalRegion() - актуальны, когда приложение вып код, частичное вып кот,
        // может привести к неработоспособности иных частей программы,
        // таким образом CLR лучше завершить работу приложения, чем продолжить с ошибками.

        //static void ThreadProcLocal(int x)
        //{
        //    Thread.BeginCriticalRegion();

        //    // здесь код, на который CLR, в случай ошибки отреагирует всегда завершением

        //    Thread.EndCriticalRegion();
        //}


        // GetCurrentProccessorId  -  можем выяснить, на каком процессоре сейчас выполняемся
        //static bool isActive = true ;
        //static void ThreadProc()
        //{
        //    while (isActive)
        //    {
        //        Console.WriteLine(Thread.CurrentThread.Name + " : " + Thread.GetCurrentProcessorId());
        //        Thread.Sleep(100);
        //    }

        //}


        // Join - блокирует вызов main пока поток не отработает

        //static void ThreadProc()
        //{
        //    for (int i = 0; i < 10; i++)
        //    {
        //        Thread.Sleep(100);
        //    }
        //    Console.WriteLine("Я завершаюсь");
        //}


        // 4444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444
        // Пулл потоков. Все потоки в пулле потоков являются фоновыми.
        //static void ThreadProc(object? stateInfo) // здесь нужно что-то принимать
        //{
        //    for (int i = 0; i < 10; i++)
        //    {
        //        Console.WriteLine("This is " + i);
        //        Thread.Sleep(1000);
        //    }
        //}


        // Свойства класса ThreadPool:

        //   свойство хранит кол-во задач, кот вып наш пулл
        //static void ThreadProc(object? stateInfo) // здесь нужно что-то принимать
        //{
        //    for (int i = 0; i < 10; i++)
        //    {
        //        Console.WriteLine("This is " + i + " " + stateInfo);
        //        Thread.Sleep(1000);
        //    }
        //}


        // 55555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555
        // Синхронизация потоков.

        // Примитивы синхронизации – это специальные классы, объекты которых помогают организовать совместный доступ
        // к чему-либо из разных потоков.

        // Monitor/lock. Инструкция позволяет блокировать доступ к блоку кода, следующим за инструкцией,
        // для всех потоков, кроме того, который первым выполнит инструкцию.

        // Пусть у нас есть несколько потоков, каждый из которых последовательно выводит в цикле свое имя и числа от 0 до 9.
        //static object lockObj = new object();
        //static void ThreadProc() 
        //{
        //    lock (lockObj)  // заблокировали доступ к консоли, путем блокирования участка кода, кот пишет в консоль(упорядочили)
        //    {
        //        for (int i = 0; i < 10; i++)
        //        {
        //            Console.WriteLine($"{Thread.CurrentThread.Name} : {i}");
        //        }
        //    }
        //}

        //static object lockObj = new object();
        //static void ThreadProc() // (упорядочили)
        //{
        //    bool lockWasTaken = false;

        //    try
        //    {
        //        Monitor.Enter(lockObj, ref lockWasTaken); // начинает ждать, до тех пор пока не освободится предыдущая блокировка
        //        for (int i = 0; i < 10; i++)
        //        {
        //            Console.WriteLine($"{Thread.CurrentThread.Name} : {i}");
        //        }
        //    }
        //    finally 
        //    {
        //        if (lockWasTaken) // если блокировка получена
        //            Monitor.Exit(lockObj);
        //    }
        //}


        // Mutex. Примитив синхронизации, во многом похожий на lock, но, в отличие от последнего,
        // может быть применен для синхронизации доступа независимых процессов/приложений.


        // AutoResetEvent. С помощью этого примитива синхронизации поток может сигнализировать о наступлении события,
        // ожидаемого в другом потоке(переход в сигнальное состояние), после чего автоматически с
        // брасывать объект в состояние ожидания сигнала.
        static AutoResetEvent ar1 = new AutoResetEvent(false);
        static AutoResetEvent ar2 = new AutoResetEvent(false);



        public void Run()
        {
            // 11111111111111111111111111111111111111111111111111111111111111111111111111111111111111
            // Конструкторы потоков
            //for (int i = 0; i < 10; i++)
            //{
            // первый способ запустить поток
            // Thread thread = new Thread(new ThreadStart(PrintThread)); // это делегат (new ThreadStart(PrintThread))
            //Thread thread1 = new Thread(PrintThread); // упрощенный вывод. Метод PrintThread преобразуется в делегат
            // thread1.Start();

            // второй способ c исп анонимного метода
            //Thread thread2 = new Thread(() => 
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        Console.Write($"{Thread.CurrentThread.ManagedThreadId} + one");
            //        Console.WriteLine();
            //        Console.Write($"{Thread.CurrentThread.ManagedThreadId} + two");
            //        Console.WriteLine();
            //        Console.Write($"{Thread.CurrentThread.ManagedThreadId} + three");
            //    }
            //});
            // thread2.Start();

            // третий способ
            //Thread thread3 = new Thread(PrintThreadThree);
            //thread3.Start(i); // переменную i через Start передали в метод PrintThreadThree

            // 222222222222222222222222222222222222222222222222222222222222222222222222222222222
            // Свойства потоков
            // CurrentThread

            //Console.WriteLine(Thread.CurrentThread.GetHashCode()); // выводит хэшкод гл потока. Получили при помощи (CurrentThread.GetHashCode())
            //Console.WriteLine();

            //new Thread(() => 
            //{
            //    Console.WriteLine(Thread.CurrentThread.GetHashCode()); // статическое свойство CurrentThread ссылается на текущий экземпляр потока
            //    Console.WriteLine($"Current Culture = {Thread.CurrentThread.CurrentCulture}"); // задает культуру (CurrentThread.CurrentCulture)
            //    Console.WriteLine(DateTime.Now.ToString()); // здесь русская культура. Дити и время показаны как положено
            //    Console.WriteLine();

            //    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            //    Console.WriteLine($"Current Culture = {Thread.CurrentThread.CurrentCulture}"); // здесь американская культура, дата и время по американски
            //    Console.WriteLine(DateTime.Now.ToString());
            //}).Start();


            // isAlive - показать, что поток жив
            //var t = new Thread(() =>
            //{
            //    Console.WriteLine("2 " + Thread.CurrentThread.IsAlive); // это происходит после того, как запустили поток
            //});

            //Console.WriteLine("1 " + t.IsAlive); // это перед тем как запустили поток
            //t.Start();


            // isBackGround - показывает(переводит), является ли поток фоновым. true - фоновый
            //var t = new Thread(() =>
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        Console.Write(".");
            //        Thread.Sleep(1000);
            //    }
            //});

            //t.IsBackground = true; // здесь поток становится фоновым

            //t.Start();
            //Thread.Sleep(10000); // данный фоновый поток будет выполняться пока не истечет 10000 мс


            // priority - 
            //var t1 = new Thread(ThreadProc);
            //t1.Priority = ThreadPriority.Lowest;
            //t1.Name = "t1"; // задаем имя потоку

            //var t2 = new Thread(ThreadProc);
            //t2.Priority = ThreadPriority.Highest;
            //t2.Name = "t2";

            //t1.Start();
            //t2.Start();

            //Thread.Sleep(10000); // ждем 10000 мс

            //isActive = false; // завершаем оба потока


            // ThreadState - храрит состояние потока в виде одного или нескольких флагов

            //var t = new Thread(ThreadProcState);
            //Console.WriteLine($"Состояние потока до запуска - {t.ThreadState}");

            //t.Start();
            //Console.WriteLine($"Состояние потока сразу после запуска - {t.ThreadState}");

            //Thread.Sleep(100);
            //Console.WriteLine($"Состояние потока на паузе - {t.ThreadState}");

            //Thread.Sleep(400);
            //Console.WriteLine($"Состояние потока после завершения - {t.ThreadState}");


            // 333333333333333333333333333333333333333333333333333333333333333333333333333333333333
            // Методы потоков

            //new Thread(() => { ThreadProcLocalDataStoreSlot(1); }).Start(); // запускаем поток, альтернативная запись
            //new Thread(() => { ThreadProcLocalDataStoreSlot(10); }).Start(); // запускаем поток, альтернативная запись

            // для GetCurrentProccessorId  -  можем выяснить, на каком процессоре сейчас выполняемся
            //var t1 = new Thread(() => { ThreadProc(); });
            //var t2 = new Thread(() => { ThreadProc(); });
            //t1.Name = "t1";
            //t2.Name = "t2";
            //t1.Start();
            //t2.Start();
            //Thread.Sleep(3000);
            //isActive = false;
            /////////////////////////////////////////////////////////////////////////////////

            // для Join - блокирует вызов main пока поток не отработает
            //var t = new Thread(() => { ThreadProc(); });
            //t.Start();

            //// t.Join(); // так сначало поток завершиться

            ////if (t.Join(3000)) // либо такой вариант
            ////{
            ////    Console.WriteLine("Дождались");
            ////}
            ////else Console.WriteLine("Не дождались");

            //if (t.Join(300)) // либо такой вариант, наверно еще лучше
            //{
            //    Console.WriteLine("Дождались");
            //}
            //else
            //{
            //    Console.WriteLine("Не дождались, отменяем поток");
            //    t.Interrupt();
            //} 
            //Console.WriteLine("main завершается");


            // 444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444
            // Пулл потоков

            //ThreadPool.QueueUserWorkItem(ThreadProc);
            //Thread.Sleep(20000);

            // свойство 
            //Console.WriteLine("count = " + ThreadPool.CompletedWorkItemCount);
            //ThreadPool.QueueUserWorkItem(ThreadProc, "t1");
            //ThreadPool.QueueUserWorkItem(ThreadProc, "t2");
            //ThreadPool.QueueUserWorkItem(ThreadProc, "t3");
            //Thread.Sleep(20000);
            //Console.WriteLine("count = " + ThreadPool.CompletedWorkItemCount);


            // 555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555
            // Синхронизация потоков.
            // Пусть у нас есть несколько потоков, каждый из которых последовательно выводит в цикле свое имя и числа от 0 до 9.
            //for (int i = 0; i < 10; i++)
            //{
            //    var t = new Thread(ThreadProc);
            //    t.Name = "Thread - " + i;
            //    t.Start();
            //}

            // AutoResetEvent.
            new Thread(() => {Thread.Sleep(1000); Console.WriteLine("поток 1 завершился"); ar1.Set();}).Start();
            new Thread(() => {Thread.Sleep(2000); Console.WriteLine("поток 2 завершился"); ar2.Set();}).Start();

            //AutoResetEvent.WaitAll(new AutoResetEvent[] { ar1, ar2 });  // этот метод дождется выполнения обоих потоков
            //Console.WriteLine("дождались");


            AutoResetEvent.WaitAny(new AutoResetEvent[] { ar1, ar2 });  // этот метод дождется любого из потоков
            Console.WriteLine("дождались");
        }
    }
}
