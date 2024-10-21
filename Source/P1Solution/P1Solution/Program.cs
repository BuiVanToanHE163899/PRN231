using AutoMapper;
using Microsoft.EntityFrameworkCore;
using P1Solution.Mapper;
using P1Solution.Models;

namespace P1Solution
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(opts =>
            {
                opts.AddPolicy("CORSPolicy", builder => builder.AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowed((host) => true));
            });
            builder.Services.AddDbContext<PRN231_P1Context>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<PRN231_P1Context>(); // thêm dependence
                                                            //MAPPER
            var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new P1Solution.Mapper.AutoMapper()); });
            var mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("CORSPolicy");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}