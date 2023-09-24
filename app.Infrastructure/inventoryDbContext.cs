using app.EntityModel.CoreModel;
using app.EntityModel.DatabaseView;
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
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ProductSubCategory> ProductSubCategory { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Vendor> Vendor { get; set; }
        public virtual DbSet<Division> Division { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<Upazila> Upazila { get; set; }
        public virtual DbSet<Company> Company { get; set; }

        public virtual DbSet<VoucherType> VoucherType { get; set; }
        public virtual DbSet<Voucher> Voucher { get; set; }
        public virtual DbSet<VoucherDetails> VoucherDetails { get; set; }
        public virtual DbSet<StockInfo> StockInfo { get; set; }
        public virtual DbSet<UserProduct> UserProduct { get; set; }
        public virtual DbSet<AccountHeads> AccountHeads { get; set; }

        public virtual DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public virtual DbSet<PurchaseOrderDetails> PurchaseOrderDetails { get; set; }
        public virtual DbSet<SalesOrder> SalesOrder { get; set; }
        public virtual DbSet<SalesOrderDetails> SalesOrderDetails { get; set; }
        [NotMapped]
        public virtual DbSet<StockView> StockView { get; set; }


        
    }
    }
