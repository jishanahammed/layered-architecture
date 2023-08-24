using app.EntityModel.CoreModel;
using app.Infrastructure.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace app.Infrastructure
{
    public class inventoryDbContext: DbContext
    {
        public inventoryDbContext(DbContextOptions<inventoryDbContext> options) : base(options)
        {
        }
        public virtual DbSet<MenuItem> MenuItem { get; set; }
        public virtual DbSet<MainMenu> MainMenu { get; set; }

        
    }
    }
