﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OPFC.API.DTO;
using OPFC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Repositories.UnitOfWork
{
    /// <summary>
    /// The OPFC system DbContext
    /// </summary>
    public class OpfcDbContext : DbContext
    {

        /// <summary>
        /// The constructor
        /// </summary>
        public OpfcDbContext() : base() { }

        /// <summary>
        /// Configurations
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Config connection string here
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                         .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                         .AddJsonFile("appsettings.json")
                                         .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("OpfcDbConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        // Declare mapping models belows
        // Example:
        // public DbSet<[Model]> Model {get; set;}

        /// <summary>
        /// [dbo].[User]
        /// </summary>
        public DbSet<User> User { get; set; }
    }
}
