using app.EntityModel.CoreModel;
using app.Infrastructure.Auth;
using Microsoft.AspNetCore.Builder.Internal;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace app.Infrastructure
{
    public class inventoryDbContext: IdentityDbContext<ApplicationUser>
    {
        public inventoryDbContext(DbContextOptions<inventoryDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            FixedData.SeedData(builder);
            base.OnModelCreating(builder);
        }
        public virtual DbSet<MenuItem> MenuItem { get; set; }
        public virtual DbSet<MainMenu> MainMenu { get; set; }
        public virtual DbSet<Userpermissions> Userpermissions { get; set; }      
        public virtual DbSet<Company> Company { get; set; } 
    }
    }
