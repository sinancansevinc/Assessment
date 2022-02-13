using Assessment.Data;
using Assessment.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace Assessment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var assessmentDbContext = serviceProvider.GetRequiredService<AssessmentDbContext>();

                assessmentDbContext.Database.Migrate();

                if (!assessmentDbContext.ContactTypes.Any())
                {
                    assessmentDbContext.ContactTypes.Add(new ContactType
                    {
                        Type = "Phone"

                    });

                    assessmentDbContext.SaveChanges();



                    assessmentDbContext.ContactTypes.Add(new ContactType
                    {
                        Type = "Email"

                    });

                    assessmentDbContext.SaveChanges();

                    assessmentDbContext.ContactTypes.Add(new ContactType
                    {
                        Type = "Location"

                    });

                    assessmentDbContext.SaveChanges();
                }

            }



            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
