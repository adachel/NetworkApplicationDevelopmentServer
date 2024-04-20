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
                .UseNpgsql("Host=localhost;Username=postgres;Password=example;Database=Test").UseLazyLoadingProxies();

        
        }

    }
}
