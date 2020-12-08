using Microsoft.EntityFrameworkCore;

namespace Angular_Sample.Data
{
    public partial class SecuritiesTransactionContext : DbContext
    {
        public SecuritiesTransactionContext()
        {
        }

        public SecuritiesTransactionContext(DbContextOptions<SecuritiesTransactionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<CompanyProfit> CompanyProfit { get; set; }
        public virtual DbSet<SecuritiesType> SecuritiesType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=SecuritiesTransaction;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.ComValue).IsUnicode(false);

                entity.Property(e => e.EngAbbreviation).IsUnicode(false);

                entity.Property(e => e.EngName).IsUnicode(false);

                entity.HasOne(d => d.SecuritiesType)
                    .WithMany(p => p.Company)
                    .HasForeignKey(d => d.SecuritiesTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Company_SecuritiesType");
            });

            modelBuilder.Entity<CompanyProfit>(entity =>
            {
                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyProfit)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyProfit_Company");
            });

            modelBuilder.Entity<SecuritiesType>(entity =>
            {
                entity.Property(e => e.Category).IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
