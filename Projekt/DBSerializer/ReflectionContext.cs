using DBSerializer.DbModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSerializer
{
    public class ReflectionContext : DbContext
    {
        public DbSet<DbReflectionModel> AssemblyModels { get; set; }
        public DbSet<DbReflectedType> ReflectedTypes { get; set; }
        public DbSet<DbMethodModel> MethodModels { get; set; }
        public DbSet<DbParameterModel> ParameterModels { get; set; }
        public DbSet<DbFieldModel> FieldModels { get; set; }
        public DbSet<DbPropertyModel> PropertyModels { get; set; }
        public DbSet<DbNamespaceModel> NamespaceModels { get; set; }

        public ReflectionContext(string name) : base(name)
        {
        }

        public ReflectionContext(System.Data.Common.DbConnection connection) : base(connection, true)
        {
        }

        public ReflectionContext() : base("SQL")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbReflectionModel>()
                .Property(a => a.Name)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(100);

            modelBuilder.Entity<DbReflectedType>()
                .Property(a => a.Name)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(150);

            modelBuilder.Entity<DbMethodModel>()
                .Property(a => a.Name)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(150);

            //modelBuilder.Entity<DbMethodModel>()
            //    .HasMany(e => e.Attributes)
            //    .WithMany(e => e.Methods);

            modelBuilder.Entity<DbFieldModel>()
                .Property(a => a.Name)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(150);

            modelBuilder.Entity<DbParameterModel>()
                .Property(a => a.Name)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(150);

            modelBuilder.Entity<DbNamespaceModel>()
                .Property(a => a.Name)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(150);

            modelBuilder.Entity<DbPropertyModel>()
                .Property(a => a.Name)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(150);
        }
    }
}
