using Microsoft.EntityFrameworkCore;
using Q2.Models;

namespace Q2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorPages();

            builder.Services.AddDbContext<PE_PRN_Fall2023B1Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();
            app.Run();
        }
    }
}