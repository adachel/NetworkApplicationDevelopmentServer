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
            var optionsBuilder = new DbContextOptionsBuilder<TestDbContext>()
                .UseNpgsql("Host=localhost;Username=postgres;Password=example;Database=Test")
                .UseLazyLoadingProxies(); // UseLazyLoadingProxies поможет с ленивой загрузкой

            using (var ctx = new TestDbContext(optionsBuilder.Options))
            {
                var user = new User();
                user.Name = "Коля";
                user.Messages = new HashSet<Message>();
                user.Messages.Add(new Message() {MessageContent = "Привет"});
                user.Messages.Add(new Message() {MessageContent = "Я Коля"});

                ctx.Add(user);
                ctx.SaveChanges();
            }

            using (var ctx = new TestDbContext(optionsBuilder.Options))
            {
                var user = ctx.Users.FirstOrDefault(x => x.Name == "Коля");

                if (user != null)
                {
                    user.Name = "Николай";
                }

                ctx.SaveChanges();
            }

            using (var ctx = new TestDbContext(optionsBuilder.Options))
            { 
                var users = ctx.Users.ToList(); // обращаемся к таблице users
                foreach (var user in users) // перебираем всез user из таблицы
                {
                    Console.WriteLine($"Имя: {user.Name}");
                    Console.WriteLine($"_______ Сообщения");

                    var messages = user.Messages;
                    foreach (var message in messages)
                    {
                        Console.WriteLine($"__________: {message.MessageContent}");
                    }
                }
            }

        }

    }
}
