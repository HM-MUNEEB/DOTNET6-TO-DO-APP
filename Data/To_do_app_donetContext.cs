using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using To_do_app_donet.Models;

namespace To_do_app_donet.Data
{
    public class To_do_app_donetContext : DbContext
    {
        public To_do_app_donetContext (DbContextOptions<To_do_app_donetContext> options)
            : base(options)
        {
        }

        public DbSet<To_do_app_donet.Models.Todos> Todos { get; set; } = default!;
    }
}
