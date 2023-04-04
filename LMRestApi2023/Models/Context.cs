using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LMRestApi2023.Models
{
    public partial class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }
        
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        
        public virtual DbSet<Documentation> Documentations { get; set; } = null!;
       
        public virtual DbSet<User> Users { get; set; } = null!;
       

        // Connection String alkuperäinen "huono" sijainti
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                return;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

           
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => e.City, "City");

                entity.HasIndex(e => e.CompanyName, "CompanyName");

                entity.HasIndex(e => e.PostalCode, "PostalCode");

                entity.HasIndex(e => e.Region, "Region");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(5)
                    .HasColumnName("CustomerID")
                    .IsFixedLength();

                entity.Property(e => e.VauvaIka).HasMaxLength(15);
                entity.Property(e => e.SynnytysViikko).HasMaxLength(15);
                entity.Property(e => e.SynnytysResume).HasMaxLength(1000);
                entity.Property(e => e.SynnytysHaaste).HasMaxLength(1000);

                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.City).HasMaxLength(15);

                entity.Property(e => e.CompanyName).HasMaxLength(40);

                entity.Property(e => e.ContactName).HasMaxLength(30);

                entity.Property(e => e.ContactTitle).HasMaxLength(30);

                entity.Property(e => e.Country).HasMaxLength(15);

                entity.Property(e => e.Fax).HasMaxLength(24);

                entity.Property(e => e.Phone).HasMaxLength(24);

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.Region).HasMaxLength(15);

                //entity.HasMany(d => d.CustomerTypes)
                //    .WithMany(p => p.Customers)
                //    .UsingEntity<Dictionary<string, object>>(
                //        "CustomerCustomerDemo",
                //        l => l.HasOne<CustomerDemographic>().WithMany().HasForeignKey("CustomerTypeId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_CustomerCustomerDemo"),
                //        r => r.HasOne<Customer>().WithMany().HasForeignKey("CustomerId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_CustomerCustomerDemo_Customers"),
                //        j =>
                //        {
                //            j.HasKey("CustomerId", "CustomerTypeId").IsClustered(false);

                //            j.ToTable("CustomerCustomerDemo");

                //            j.IndexerProperty<string>("CustomerId").HasMaxLength(5).HasColumnName("CustomerID").IsFixedLength();

                //            j.IndexerProperty<string>("CustomerTypeId").HasMaxLength(10).HasColumnName("CustomerTypeID").IsFixedLength();
                //        });
            });

           
            modelBuilder.Entity<Documentation>(entity =>
            {
                entity.ToTable("Documentation");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("description");

                entity.Property(e => e.Keycode)
                    .HasMaxLength(10)
                    .HasColumnName("keycode");

                entity.Property(e => e.Method)
                    .HasMaxLength(10)
                    .HasColumnName("method")
                    .IsFixedLength();

                entity.Property(e => e.Url)
                    .HasMaxLength(100)
                    .HasColumnName("url")
                    .IsFixedLength();
            });

           

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(30);

                entity.Property(e => e.Firstname).HasMaxLength(30);

                entity.Property(e => e.Lastname).HasMaxLength(30);

                entity.Property(e => e.Password).HasMaxLength(200);

                entity.Property(e => e.Username).HasMaxLength(10);

                entity.Property(e => e.UserId).HasMaxLength(10);
            });

           
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}