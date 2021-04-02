using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using mssqltest1.Models;
using Microsoft.Extensions.Logging;

    public class EmployeeContext : DbContext
    {
        private readonly ILogger<EmployeeContext> _logger;

        public EmployeeContext (ILogger<EmployeeContext> logger, DbContextOptions<EmployeeContext> options)
            : base(options)
        {
            _logger = logger;
        }

        public DbSet<mssqltest1.Models.Employee> Employee { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.LogTo(l => _logger.LogInformation(l));
    }
