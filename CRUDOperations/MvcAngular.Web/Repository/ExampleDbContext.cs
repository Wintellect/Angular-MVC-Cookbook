using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace MvcAngular.Web.Repository
{
    public class ExampleDbContext : DbContext
    {
        public static void CreateDatabase()
        {
            using (var ctx = new ExampleDbContext())
            {
                ctx.Database.Initialize(false);
            }
        }

        public ExampleDbContext()
            : base("ExampleData")
        {
        }

        public DbSet<Person> People { get; set; }
        public DbSet<PostalAddress> PostalAddresses { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<EmailAddress> EmailAddresses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Configure<Person>(
                modelBuilder,
                entity =>
                    {
                        entity.ToTable("People");
                        entity
                            .Property(e => e.PersonId)
                            .HasColumnName("Id")
                            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
                        entity.HasKey(e => e.PersonId);
                        entity.Property(e => e.FirstName).IsRequired().HasMaxLength(Person.FirstNameMaxLength);
                        entity.Property(e => e.LastName).IsRequired().HasMaxLength(Person.LastNameMaxLength);
                        entity.Property(e => e.Title).HasMaxLength(Person.TitleMaxLength);
                        entity.Property(e => e.MiddleName).HasMaxLength(Person.MiddleNameMaxLength);
                        entity.Property(e => e.Suffix).HasMaxLength(Person.SuffixMaxLength);
                    });
            Configure<PostalAddress>(
                modelBuilder,
                entity =>
                {
                    entity.ToTable("Postal");
                    entity
                        .Property(pa => pa.PostalAddressId)
                        .HasColumnName("Id")
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
                    entity.HasKey(pa => pa.PostalAddressId);
                    entity.Property(pa => pa.LineOne).IsRequired().HasMaxLength(PostalAddress.AddressLineMaxLength);
                    entity.Property(pa => pa.LineTwo).HasMaxLength(PostalAddress.AddressLineMaxLength);
                    entity.Property(pa => pa.City).IsRequired().HasMaxLength(PostalAddress.CityMaxLength);
                    entity.Property(pa => pa.StateProvince).HasMaxLength(PostalAddress.StateProviceMaxLength);
                    entity.Property(pa => pa.Country).IsRequired().HasMaxLength(PostalAddress.CountryMaxLength);
                    entity.Property(pa => pa.PostalCode).IsRequired().HasMaxLength(PostalAddress.PostalCodeMaxLength);
                    entity
                        .HasRequired(pa => pa.Person)
                        .WithMany(p => p.PostalAddresses)
                        .HasForeignKey(pa => pa.PersonId);
                });
            Configure<PhoneNumber>(
                modelBuilder,
                entity =>
                {
                    entity.ToTable("Phone");
                    entity
                        .Property(ph => ph.PhoneNumberId)
                        .HasColumnName("Id")
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
                    entity.HasKey(ph => ph.PhoneNumberId);
                    entity.Property(ph => ph.Number).IsRequired().HasMaxLength(PhoneNumber.NumberMaxLength);
                    entity.Property(ph => ph.NumberType).IsRequired().HasMaxLength(PhoneNumber.NumberTypeMaxLength);
                    entity
                        .HasRequired(ph => ph.Person)
                        .WithMany(p => p.PhoneNumbers)
                        .HasForeignKey(ph => ph.PersonId);
                });
            Configure<EmailAddress>(
                modelBuilder,
                entity =>
                {
                    entity.ToTable("Email");
                    entity
                        .Property(ea => ea.EmailAddressId)
                        .HasColumnName("Id")
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
                    entity.HasKey(ea => ea.EmailAddressId);
                    entity.Property(ea => ea.Address).IsRequired().HasMaxLength(EmailAddress.AddressMaxLength);
                    entity
                        .HasRequired(ea => ea.Person)
                        .WithMany(p => p.EmailAddresses)
                        .HasForeignKey(ea => ea.PersonId);
                });
        }

        private void Configure<T>(DbModelBuilder modelBuilder, Action<EntityTypeConfiguration<T>> configureEntityMethod)
            where T : class
        {
            var entityConfig = modelBuilder.Entity<T>();
            configureEntityMethod(entityConfig);
        }
    }
}