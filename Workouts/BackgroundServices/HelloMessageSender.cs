using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workouts.BackgroundServices
{
    public class HelloMessageSender : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScope;

        public HelloMessageSender(IServiceScopeFactory serviceScope)
        {
            _serviceScope = serviceScope;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (stoppingToken.IsCancellationRequested == false)
            {
                await Task.Delay(1000);

                //using IServiceScope? scope = _serviceScope.CreateScope();
                //DbContext context = scope.ServiceProvider.GetService<Context>();

                Console.WriteLine("Hello Darling");
            }

        }
    }
}
