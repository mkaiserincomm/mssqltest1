using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using mssqltest1.Models;
using Microsoft.Extensions.Logging;

    public class ProductContext : DbContext
    {
        private readonly ILogger<ProductContext> _logger;

        public ProductContext (ILogger<ProductContext> logger, DbContextOptions<ProductContext> options)
            : base(options)
        {
            _logger = logger;
        }

        public DbSet<mssqltest1.Models.Product> Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.LogTo(l => _logger.LogInformation(l));
    }
