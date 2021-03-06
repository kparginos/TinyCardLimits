﻿using System;
using System.Reflection;

using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

using TinyCardLimits.Core.Data;
using TinyCardLimits.Core.Config.Extentions;

namespace TinyCardLimits.Migrations
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }

    //Use the following to create the migrations
    public class DbContextFactory : IDesignTimeDbContextFactory<TinyCardLimitsDBContext>
    {
        public TinyCardLimitsDBContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath($"{AppDomain.CurrentDomain.BaseDirectory}")
                .AddJsonFile("appsettings.json", false)
                .Build();
            var config = configuration.ReadAppConfiguration();
            var optionsBuilder = new DbContextOptionsBuilder<TinyCardLimitsDBContext>();
            optionsBuilder.UseSqlServer(
                config.ConnString,
                options =>
                {
                    options.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name); // ("TinyCardLimits");
                });

            return new TinyCardLimitsDBContext(optionsBuilder.Options);
        }
    }
}
