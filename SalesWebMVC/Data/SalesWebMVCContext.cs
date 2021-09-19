using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Models;

namespace SalesWebMVC.Data
{
    public class SalesWebMVCContext : DbContext
    {
        public SalesWebMVCContext (DbContextOptions<SalesWebMVCContext> options)
            : base(options)
        {
        }

        //Adiconando dados no banco de dados
        public DbSet<SalesWebMVC.Models.Department> Department { get; set; }
        public DbSet<SalesWebMVC.Models.Seller> Seller { get; set; }
        public DbSet<SalesWebMVC.Models.SalesRecord> SalesRecords { get; set; }
    }
}
