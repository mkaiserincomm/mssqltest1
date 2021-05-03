using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using mssqltest1.Models;
using Microsoft.Extensions.Logging;

    public class CategoryContext : DbContext
    {
        private readonly ILogger<CategoryContext> _logger;

        public CategoryContext (ILogger<CategoryContext> logger, DbContextOptions<CategoryContext> options)
            : base(options)
        {
            _logger = logger;
        }

        public DbSet<mssqltest1.Models.Category> Category { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.LogTo(l => _logger.LogInformation(l));
    }
