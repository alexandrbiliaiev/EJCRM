using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EuroJobsCrm.Models
{
    public partial class DB_A12601_bielkaContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Server=SQL5016.SmarterASP.NET;Initial Catalog=DB_A12601_bielka;User Id=DB_A12601_bielka_admin;Password=azanezege22;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Addresses>(entity =>
            {
                entity.HasKey(e => e.AdrId)
                    .HasName("PK_Addresses");

                entity.Property(e => e.AdrId).HasColumnName("adr_id");

                entity.Property(e => e.AdrAddress)
                    .HasColumnName("adr_address")
                    .HasMaxLength(500);

                entity.Property(e => e.AdrAduitMu)
                    .HasColumnName("adr_aduit_mu")
                    .HasMaxLength(100);

                entity.Property(e => e.AdrAduitRu)
                    .HasColumnName("adr_aduit_ru")
                    .HasMaxLength(100);

                entity.Property(e => e.AdrAuditCd)
                    .HasColumnName("adr_audit_cd")
                    .HasColumnType("datetime");

                entity.Property(e => e.AdrAuditCu)
                    .HasColumnName("adr_audit_cu")
                    .HasMaxLength(100);

                entity.Property(e => e.AdrAuditMd)
                    .HasColumnName("adr_audit_md")
                    .HasColumnType("datetime");

                entity.Property(e => e.AdrAuditRd)
                    .HasColumnName("adr_audit_rd")
                    .HasColumnType("datetime");

                entity.Property(e => e.AdrCgtId).HasColumnName("adr_cgt_id");

                entity.Property(e => e.AdrCity)
                    .HasColumnName("adr_city")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.AdrCountry)
                    .HasColumnName("adr_country")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.AdrPay)
                    .HasColumnName("adr_pay")
                    .HasColumnType("varchar(1)");

                entity.Property(e => e.AdrPostCode)
                    .HasColumnName("adr_post_code")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.AdrType)
                    .HasColumnName("adr_type")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.ArdCltId).HasColumnName("ard_clt_id");

                entity.Property(e => e.ArdEmpId).HasColumnName("ard_emp_id");
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex");

                entity.Property(e => e.Id).HasMaxLength(450);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey })
                    .HasName("PK_AspNetUserLogins");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(450);

                entity.Property(e => e.ProviderKey).HasMaxLength(450);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PK_AspNetUserRoles");

                entity.HasIndex(e => e.RoleId)
                    .HasName("IX_AspNetUserRoles_RoleId");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_AspNetUserRoles_UserId");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.Property(e => e.RoleId).HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name })
                    .HasName("PK_AspNetUserTokens");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.Property(e => e.LoginProvider).HasMaxLength(450);

                entity.Property(e => e.Name).HasMaxLength(450);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasMaxLength(450);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Clients>(entity =>
            {
                entity.HasKey(e => e.CltId)
                    .HasName("PK_Clients");

                entity.Property(e => e.CltId).HasColumnName("clt_id");

                entity.Property(e => e.CltAuditCd)
                    .HasColumnName("clt_audit_cd")
                    .HasColumnType("datetime");

                entity.Property(e => e.CltAuditCu)
                    .HasColumnName("clt_audit_cu")
                    .HasMaxLength(100);

                entity.Property(e => e.CltAuditMd)
                    .HasColumnName("clt_audit_md")
                    .HasColumnType("datetime");

                entity.Property(e => e.CltAuditMu)
                    .HasColumnName("clt_audit_mu")
                    .HasMaxLength(100);

                entity.Property(e => e.CltAuditRd)
                    .HasColumnName("clt_audit_rd")
                    .HasColumnType("datetime");

                entity.Property(e => e.CltAuditRu)
                    .HasColumnName("clt_audit_ru")
                    .HasMaxLength(100);

                entity.Property(e => e.CltKrs)
                    .IsRequired()
                    .HasColumnName("clt_krs")
                    .HasMaxLength(20);

                entity.Property(e => e.CltName)
                    .IsRequired()
                    .HasColumnName("clt_name")
                    .HasMaxLength(100);

                entity.Property(e => e.CltNip)
                    .IsRequired()
                    .HasColumnName("clt_nip")
                    .HasMaxLength(20);

                entity.Property(e => e.CltRegon)
                    .IsRequired()
                    .HasColumnName("clt_regon")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<ContactPersons>(entity =>
            {
                entity.HasKey(e => e.CtpId)
                    .HasName("PK_ContactPersons");

                entity.Property(e => e.CtpId).HasColumnName("ctp_id");

                entity.Property(e => e.CtpCd)
                    .HasColumnName("ctp_cd")
                    .HasColumnType("datetime");

                entity.Property(e => e.CtpCgtId).HasColumnName("ctp_cgt_id");

                entity.Property(e => e.CtpCltId).HasColumnName("ctp_clt_id");

                entity.Property(e => e.CtpCu)
                    .HasColumnName("ctp_cu")
                    .HasMaxLength(100);

                entity.Property(e => e.CtpEmail)
                    .HasColumnName("ctp_email")
                    .HasMaxLength(250);

                entity.Property(e => e.CtpMd)
                    .HasColumnName("ctp_md")
                    .HasColumnType("datetime");

                entity.Property(e => e.CtpMessanger)
                    .HasColumnName("ctp_messanger")
                    .HasMaxLength(150);

                entity.Property(e => e.CtpMu)
                    .HasColumnName("ctp_mu")
                    .HasMaxLength(100);

                entity.Property(e => e.CtpName)
                    .HasColumnName("ctp_name")
                    .HasMaxLength(150);

                entity.Property(e => e.CtpPhoneNumber)
                    .HasColumnName("ctp_phone_number")
                    .HasMaxLength(150);

                entity.Property(e => e.CtpPosition)
                    .HasColumnName("ctp_position")
                    .HasMaxLength(250);

                entity.Property(e => e.CtpRd)
                    .HasColumnName("ctp_rd")
                    .HasColumnType("datetime");

                entity.Property(e => e.CtpRu)
                    .HasColumnName("ctp_ru")
                    .HasMaxLength(100);

                entity.Property(e => e.CtpSkype)
                    .HasColumnName("ctp_skype")
                    .HasMaxLength(150);

                entity.Property(e => e.CtpSurname)
                    .HasColumnName("ctp_surname")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Contragents>(entity =>
            {
                entity.HasKey(e => e.CgtId)
                    .HasName("PK_Contragents");

                entity.Property(e => e.CgtId).HasColumnName("cgt_id");

                entity.Property(e => e.CgtAuditCd)
                    .HasColumnName("cgt_audit_cd")
                    .HasColumnType("datetime");

                entity.Property(e => e.CgtAuditCu)
                    .HasColumnName("cgt_audit_cu")
                    .HasMaxLength(100);

                entity.Property(e => e.CgtAuditMd)
                    .HasColumnName("cgt_audit_md")
                    .HasColumnType("datetime");

                entity.Property(e => e.CgtAuditMu)
                    .HasColumnName("cgt_audit_mu")
                    .HasMaxLength(100);

                entity.Property(e => e.CgtAuditRd)
                    .HasColumnName("cgt_audit_rd")
                    .HasColumnType("datetime");

                entity.Property(e => e.CgtAuditRu)
                    .HasColumnName("cgt_audit_ru")
                    .HasMaxLength(100);

                entity.Property(e => e.CgtLicenseNumber)
                    .IsRequired()
                    .HasColumnName("cgt_license_number")
                    .HasMaxLength(100);

                entity.Property(e => e.CgtName)
                    .IsRequired()
                    .HasColumnName("cgt_name")
                    .HasMaxLength(100);

                entity.Property(e => e.CgtStatus)
                    .IsRequired()
                    .HasColumnName("cgt_status")
                    .HasMaxLength(1);
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("PK_Employees");

                entity.Property(e => e.EmpId).HasColumnName("emp_id");

                entity.Property(e => e.EmpAuditCd)
                    .HasColumnName("emp_audit_cd")
                    .HasColumnType("datetime");

                entity.Property(e => e.EmpAuditCu)
                    .HasColumnName("emp_audit_cu")
                    .HasMaxLength(100);

                entity.Property(e => e.EmpAuditMd)
                    .HasColumnName("emp_audit_md")
                    .HasColumnType("datetime");

                entity.Property(e => e.EmpAuditMu)
                    .HasColumnName("emp_audit_mu")
                    .HasMaxLength(100);

                entity.Property(e => e.EmpAuditRd)
                    .HasColumnName("emp_audit_rd")
                    .HasColumnType("datetime");

                entity.Property(e => e.EmpAuditRu)
                    .HasColumnName("emp_audit_ru")
                    .HasMaxLength(100);

                entity.Property(e => e.EmpBirthDate)
                    .HasColumnName("emp_birth_date")
                    .HasColumnType("date");

                entity.Property(e => e.EmpFirstName)
                    .IsRequired()
                    .HasColumnName("emp_first_name")
                    .HasMaxLength(100);

                entity.Property(e => e.EmpLastName)
                    .IsRequired()
                    .HasColumnName("emp_last_name")
                    .HasMaxLength(100);

                entity.Property(e => e.EmpMiddleName)
                    .IsRequired()
                    .HasColumnName("emp_middle_name")
                    .HasMaxLength(100);
            });
        }

        public virtual DbSet<Addresses> Addresses { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<ContactPersons> ContactPersons { get; set; }
        public virtual DbSet<Contragents> Contragents { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
    }
}