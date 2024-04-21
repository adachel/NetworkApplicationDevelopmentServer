using Microsoft.EntityFrameworkCore;
using NetworkApplicationDevelopmentServer.Lections.Lection5.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkApplicationDevelopmentServer.Lections.Lection5
{
    internal class Lec5
    {
        public void Run()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TestDbContext>() // подготавливаем объект options из TestDbContext
                .UseNpgsql("Host=localhost;Username=lec5;Password=Lec1234;Database=Lec5_1")
                .UseLazyLoadingProxies();

            //using (var ctx = new TestDbContext(optionsBuilder.Options)) // замена существующего имени
            //{
            //    var user = ctx.Users.FirstOrDefault(x => x.Name == "DDDDDD");
            //    if (user != null)
            //    {
            //        user.Name = "Николай";
            //    }
            //    ctx.SaveChanges();
            //}

            using (var ctx = new TestDbContext(optionsBuilder.Options)) // создаем объект контекста
            {
                var user = new User();
                user.Name = "Коля";
                user.Messages = new HashSet<Message>();
                user.Messages.Add(new Message() { MessageContent = "Привет" });
                user.Messages.Add(new Message() { MessageContent = "Я Коля" });

                ctx.Add(user); // добавили в контекст
                ctx.SaveChanges(); // сохранили изменения


                //var users = ctx.Users.ToList(); // обращаемся к таблице Users
                //foreach (var user in users)     // перебираем всех юзеров
                //{
                //    Console.WriteLine($"Имя {user.Name}");
                //    Console.WriteLine("_______________Сообщения: ");

                //    //var messages = user.Messages;   // для каждого юзера получаем его сообщение
                //    //foreach (var message in messages)
                //    //{
                //    //    Console.WriteLine($"__________: {message.MessageContent}");
                //    //}
                //}
            }
        }
    }
}
