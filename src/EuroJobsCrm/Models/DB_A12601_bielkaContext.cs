using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EuroJobsCrm.Models
{
    public partial class DB_A12601_bielkaContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=SQL6001.myASP.NET;Initial Catalog=DB_A1DD1B_bwg;User Id=DB_A1DD1B_bwg_admin;Password=1q2w3e4r5t;MultipleActiveResultSets=True");
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

                entity.Property(e => e.Blocked).HasDefaultValueSql("0");

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

                entity.Property(e => e.CltBranch)
                    .HasColumnName("clt_branch")
                    .HasMaxLength(30);

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

                entity.Property(e => e.CltStatus).HasColumnName("clt_status");

                entity.Property(e => e.CltType).HasColumnName("clt_type");
            });

            modelBuilder.Entity<ContactPersons>(entity =>
            {
                entity.HasKey(e => e.CtpId)
                    .HasName("PK_ContactPersons");

                entity.Property(e => e.CtpId).HasColumnName("ctp_id");

                entity.Property(e => e.CtpAuditCd)
                    .HasColumnName("ctp_audit_cd")
                    .HasColumnType("datetime");

                entity.Property(e => e.CtpAuditCu)
                    .HasColumnName("ctp_audit_cu")
                    .HasMaxLength(100);

                entity.Property(e => e.CtpAuditMd)
                    .HasColumnName("ctp_audit_md")
                    .HasColumnType("datetime");

                entity.Property(e => e.CtpAuditMu)
                    .HasColumnName("ctp_audit_mu")
                    .HasMaxLength(100);

                entity.Property(e => e.CtpAuditRd)
                    .HasColumnName("ctp_audit_rd")
                    .HasColumnType("datetime");

                entity.Property(e => e.CtpAuditRu)
                    .HasColumnName("ctp_audit_ru")
                    .HasMaxLength(100);

                entity.Property(e => e.CtpCgtId).HasColumnName("ctp_cgt_id");

                entity.Property(e => e.CtpCltId).HasColumnName("ctp_clt_id");

                entity.Property(e => e.CtpEmail)
                    .HasColumnName("ctp_email")
                    .HasMaxLength(250);

                entity.Property(e => e.CtpMessanger)
                    .HasColumnName("ctp_messanger")
                    .HasMaxLength(150);

                entity.Property(e => e.CtpMessangerType).HasColumnName("ctp_messanger_type");

                entity.Property(e => e.CtpName)
                    .HasColumnName("ctp_name")
                    .HasMaxLength(150);

                entity.Property(e => e.CtpPhoneNumber)
                    .HasColumnName("ctp_phone_number")
                    .HasMaxLength(150);

                entity.Property(e => e.CtpPosition)
                    .HasColumnName("ctp_position")
                    .HasMaxLength(250);

                entity.Property(e => e.CtpSkype)
                    .HasColumnName("ctp_skype")
                    .HasMaxLength(150);

                entity.Property(e => e.CtpSurname)
                    .HasColumnName("ctp_surname")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Contracts>(entity =>
            {
                entity.HasKey(e => e.CntId)
                    .HasName("PK__Contract__CDB1F3C18F3440BF");

                entity.Property(e => e.CntId).HasColumnName("cnt_id");

                entity.Property(e => e.CntAuditCd)
                    .HasColumnName("cnt_audit_cd")
                    .HasColumnType("datetime");

                entity.Property(e => e.CntAuditCu)
                    .HasColumnName("cnt_audit_cu")
                    .HasMaxLength(100);

                entity.Property(e => e.CntAuditMd)
                    .HasColumnName("cnt_audit_md")
                    .HasColumnType("datetime");

                entity.Property(e => e.CntAuditMu)
                    .HasColumnName("cnt_audit_mu")
                    .HasMaxLength(100);

                entity.Property(e => e.CntAuditRd)
                    .HasColumnName("cnt_audit_rd")
                    .HasColumnType("datetime");

                entity.Property(e => e.CntAuditRu)
                    .HasColumnName("cnt_audit_ru")
                    .HasMaxLength(100);

                entity.Property(e => e.CntCgtId).HasColumnName("cnt_cgt_id");

                entity.Property(e => e.CntEmpId).HasColumnName("cnt_emp_id");

                entity.Property(e => e.CntEndDate)
                    .HasColumnName("cnt_end_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.CntIssueDate)
                    .HasColumnName("cnt_issue_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.CntStartDate)
                    .HasColumnName("cnt_start_date")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Contragent>(entity =>
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

                entity.Property(e => e.CgtResponsibleUser)
                    .HasColumnName("cgt_responsible_user")
                    .HasMaxLength(100);

                entity.Property(e => e.CgtStatus)
                    .IsRequired()
                    .HasColumnName("cgt_status")
                    .HasMaxLength(1);
            });

            modelBuilder.Entity<ContragentsUsersMappings>(entity =>
            {
                entity.HasKey(e => e.CumId)
                    .HasName("PK_ContragentsUsersMappings");

                entity.Property(e => e.CumId).HasColumnName("cum_id");

                entity.Property(e => e.CumCtgId).HasColumnName("cum_ctg_id");

                entity.Property(e => e.CumUserId)
                    .IsRequired()
                    .HasColumnName("cum_user_id")
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<DocumentFiles>(entity =>
            {
                entity.HasKey(e => e.DcfId)
                    .HasName("PK__Document__2996CAF62F188655");

                entity.Property(e => e.DcfId).HasColumnName("dcf_id");

                entity.Property(e => e.DcfAuditCd)
                    .HasColumnName("dcf_audit_cd")
                    .HasColumnType("datetime");

                entity.Property(e => e.DcfAuditCu)
                    .HasColumnName("dcf_audit_cu")
                    .HasMaxLength(100);

                entity.Property(e => e.DcfAuditMd)
                    .HasColumnName("dcf_audit_md")
                    .HasColumnType("datetime");

                entity.Property(e => e.DcfAuditMu)
                    .HasColumnName("dcf_audit_mu")
                    .HasMaxLength(100);

                entity.Property(e => e.DcfAuditRd)
                    .HasColumnName("dcf_audit_rd")
                    .HasColumnType("datetime");

                entity.Property(e => e.DcfAuditRu)
                    .HasColumnName("dcf_audit_ru")
                    .HasMaxLength(100);

                entity.Property(e => e.DcfCliId).HasColumnName("dcf_cli_id");

                entity.Property(e => e.DcfCntId).HasColumnName("dcf_cnt_id");

                entity.Property(e => e.DcfDescription)
                    .HasColumnName("dcf_description")
                    .HasMaxLength(1000);

                entity.Property(e => e.DcfGoogleFileId)
                    .HasColumnName("dcf_google_file_id")
                    .HasMaxLength(50);

                entity.Property(e => e.DcfIdcId).HasColumnName("dcf_idc_id");

                entity.Property(e => e.DcfInvId).HasColumnName("dcf_inv_id");

                entity.Property(e => e.DcfName)
                    .IsRequired()
                    .HasColumnName("dcf_name")
                    .HasMaxLength(300);

                entity.Property(e => e.DcfOfrId).HasColumnName("dcf_ofr_id");

                entity.Property(e => e.DcfUrl)
                    .IsRequired()
                    .HasColumnName("dcf_url")
                    .HasMaxLength(500);
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

                entity.Property(e => e.EmpCltId).HasColumnName("emp_clt_id");

                entity.Property(e => e.EmpCtgId).HasColumnName("emp_ctg_id");

                entity.Property(e => e.EmpDescription)
                    .HasColumnName("emp_description")
                    .HasMaxLength(1000);

                entity.Property(e => e.EmpFirstName)
                    .IsRequired()
                    .HasColumnName("emp_first_name")
                    .HasMaxLength(100);

                entity.Property(e => e.EmpLastName)
                    .IsRequired()
                    .HasColumnName("emp_last_name")
                    .HasMaxLength(100);

                entity.Property(e => e.EmpMiddleName)
                    .HasColumnName("emp_middle_name")
                    .HasMaxLength(150);

                entity.Property(e => e.EmpOffId).HasColumnName("emp_off_id");

                entity.Property(e => e.EmpResponsibleUser)
                    .HasColumnName("emp_responsible_user")
                    .HasMaxLength(100);

                entity.Property(e => e.EmpStatus).HasColumnName("emp_status");
            });

            modelBuilder.Entity<EmploymentRequests>(entity =>
            {
                entity.HasKey(e => e.EtrId)
                    .HasName("PK__Employme__D200B475365274A4");

                entity.Property(e => e.EtrId).HasColumnName("etr_id");

                entity.Property(e => e.EtrAuditCd)
                    .HasColumnName("etr_audit_cd")
                    .HasColumnType("datetime");

                entity.Property(e => e.EtrAuditCu)
                    .HasColumnName("etr_audit_cu")
                    .HasMaxLength(100);

                entity.Property(e => e.EtrAuditMd)
                    .HasColumnName("etr_audit_md")
                    .HasColumnType("datetime");

                entity.Property(e => e.EtrAuditMu)
                    .HasColumnName("etr_audit_mu")
                    .HasMaxLength(100);

                entity.Property(e => e.EtrAuditRd)
                    .HasColumnName("etr_audit_rd")
                    .HasColumnType("datetime");

                entity.Property(e => e.EtrAuditRu)
                    .HasColumnName("etr_audit_ru")
                    .HasMaxLength(100);

                entity.Property(e => e.EtrCltId).HasColumnName("etr_clt_id");

                entity.Property(e => e.EtrCntId).HasColumnName("etr_cnt_id");

                entity.Property(e => e.EtrEmpId).HasColumnName("etr_emp_id");

                entity.Property(e => e.EtrOfrId).HasColumnName("etr_ofr_id");

                entity.Property(e => e.EtrStatus).HasColumnName("etr_status");
            });

            modelBuilder.Entity<IdentityDocuments>(entity =>
            {
                entity.HasKey(e => e.IdcId)
                    .HasName("PK__Identity__CC1873659A101A22");

                entity.Property(e => e.IdcId).HasColumnName("idc_id");

                entity.Property(e => e.IdcAuditCd)
                    .HasColumnName("idc_audit_cd")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdcAuditCu)
                    .HasColumnName("idc_audit_cu")
                    .HasMaxLength(100);

                entity.Property(e => e.IdcAuditMd)
                    .HasColumnName("idc_audit_md")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdcAuditMu)
                    .HasColumnName("idc_audit_mu")
                    .HasMaxLength(100);

                entity.Property(e => e.IdcAuditRd)
                    .HasColumnName("idc_audit_rd")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdcAuditRu)
                    .HasColumnName("idc_audit_ru")
                    .HasMaxLength(100);

                entity.Property(e => e.IdcEmpId).HasColumnName("idc_emp_id");

                entity.Property(e => e.IdcIssueDate)
                    .HasColumnName("idc_issue_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdcNumber)
                    .HasColumnName("idc_number")
                    .HasMaxLength(32);

                entity.Property(e => e.IdcParentIdcId).HasColumnName("idc_parent_idc_id");

                entity.Property(e => e.IdcRemarks)
                    .HasColumnName("idc_remarks")
                    .HasMaxLength(500);

                entity.Property(e => e.IdcSeria)
                    .HasColumnName("idc_seria")
                    .HasMaxLength(16);

                entity.Property(e => e.IdcType).HasColumnName("idc_type");

                entity.Property(e => e.IdcValidFrom)
                    .HasColumnName("idc_valid_from")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdcValidTo)
                    .HasColumnName("idc_valid_to")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdcVisaType)
                    .HasColumnName("idc_visa_type")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Notes>(entity =>
            {
                entity.HasKey(e => e.NotId)
                    .HasName("PK_Notes");

                entity.Property(e => e.NotId)
                    .HasColumnName("not_id")
                    .HasMaxLength(100);

                entity.Property(e => e.NotAuditCd)
                    .HasColumnName("not_audit_cd")
                    .HasColumnType("datetime");

                entity.Property(e => e.NotAuditCu)
                    .HasColumnName("not_audit_cu")
                    .HasMaxLength(150);

                entity.Property(e => e.NotAuditMd)
                    .HasColumnName("not_audit_md")
                    .HasColumnType("datetime");

                entity.Property(e => e.NotAuditMu)
                    .HasColumnName("not_audit_mu")
                    .HasMaxLength(100);

                entity.Property(e => e.NotAuditRd)
                    .HasColumnName("not_audit_rd")
                    .HasColumnType("datetime");

                entity.Property(e => e.NotAuditRu)
                    .HasColumnName("not_audit_ru")
                    .HasMaxLength(100);

                entity.Property(e => e.NotCltId).HasColumnName("not_clt_id");

                entity.Property(e => e.NotCtgId).HasColumnName("not_ctg_id");

                entity.Property(e => e.NotEmp).HasColumnName("not_emp");

                entity.Property(e => e.NotEndDate)
                    .HasColumnName("not_end_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.NotRemindDate)
                    .HasColumnName("not_remind_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.NotStartDate)
                    .HasColumnName("not_start_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.NotSubject)
                    .HasColumnName("not_subject")
                    .HasMaxLength(200);

                entity.Property(e => e.NotTargetUser)
                    .HasColumnName("not_target_user")
                    .HasMaxLength(100);

                entity.Property(e => e.NotText)
                    .HasColumnName("not_text")
                    .HasMaxLength(1000);

                entity.Property(e => e.NotReminded)
                    .HasColumnName("not_reminded");

                entity.Property(e => e.NotStatus)
                    .HasColumnName("not_status")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Offers>(entity =>
            {
                entity.HasKey(e => e.OfrId)
                    .HasName("PK_Offers");

                entity.Property(e => e.OfrId).HasColumnName("ofr_id");

                entity.Property(e => e.OfrAccomodationPrice).HasColumnName("ofr_accomodation_price");

                entity.Property(e => e.OfrAccomodationType).HasColumnName("ofr_accomodation_type");

                entity.Property(e => e.OfrAdditionalInfo)
                    .HasColumnName("ofr_additional_info")
                    .HasColumnType("nchar(1000)");

                entity.Property(e => e.OfrAdvanceAmount).HasColumnName("ofr_advance_amount");

                entity.Property(e => e.OfrAgeFrom).HasColumnName("ofr_age_from");

                entity.Property(e => e.OfrAgeTo).HasColumnName("ofr_age_to");

                entity.Property(e => e.OfrAuditCd)
                    .HasColumnName("ofr_audit_cd")
                    .HasColumnType("datetime");

                entity.Property(e => e.OfrAuditCu)
                    .HasColumnName("ofr_audit_cu")
                    .HasMaxLength(100);

                entity.Property(e => e.OfrAuditMd)
                    .HasColumnName("ofr_audit_md")
                    .HasColumnType("datetime");

                entity.Property(e => e.OfrAuditMu)
                    .HasColumnName("ofr_audit_mu")
                    .HasMaxLength(100);

                entity.Property(e => e.OfrAuditRd)
                    .HasColumnName("ofr_audit_rd")
                    .HasColumnType("datetime");

                entity.Property(e => e.OfrAuditRu)
                    .HasColumnName("ofr_audit_ru")
                    .HasMaxLength(100);

                entity.Property(e => e.OfrBranch).HasColumnName("ofr_branch");

                entity.Property(e => e.OfrCltId).HasColumnName("ofr_clt_id");

                entity.Property(e => e.OfrComments)
                    .HasColumnName("ofr_comments")
                    .HasColumnType("nchar(1000)");

                entity.Property(e => e.OfrContractType).HasColumnName("ofr_contract_type");

                entity.Property(e => e.OfrDistanceToWork).HasColumnName("ofr_distance_to_work");

                entity.Property(e => e.OfrDocuments)
                    .HasColumnName("ofr_documents")
                    .HasMaxLength(500);

                entity.Property(e => e.OfrEducation).HasColumnName("ofr_education");

                entity.Property(e => e.OfrEndingDate)
                    .HasColumnName("ofr_ending_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.OfrExperience)
                    .HasColumnName("ofr_experience")
                    .HasMaxLength(100);

                entity.Property(e => e.OfrFacilities)
                    .HasColumnName("ofr_facilities")
                    .HasMaxLength(1000);

                entity.Property(e => e.OfrGender).HasColumnName("ofr_gender");

                entity.Property(e => e.OfrHoursPerMonth).HasColumnName("ofr_hours_per_month");

                entity.Property(e => e.OfrLanguages)
                    .HasColumnName("ofr_languages")
                    .HasColumnType("nchar(300)");

                entity.Property(e => e.OfrOvertimeRate)
                .HasColumnName("ofr_overtime_rate");

                entity.Property(e => e.OfrPaymentMethod).HasColumnName("ofr_payment_method");

                entity.Property(e => e.OfrPosition)
                    .HasColumnName("ofr_position")
                    .HasMaxLength(150);

                entity.Property(e => e.OfrRatePerHour).HasColumnName("ofr_rate_per_hour");

                entity.Property(e => e.OfrRatePerMonth).HasColumnName("ofr_rate_per_month");

                entity.Property(e => e.OfrResponsibilities)
                    .HasColumnName("ofr_responsibilities")
                    .HasMaxLength(1500);

                entity.Property(e => e.OfrRoomPeopleNumber).HasColumnName("ofr_room_people_number");

                entity.Property(e => e.OfrStartingDate)
                    .HasColumnName("ofr_starting_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.OfrTransportPrice).HasColumnName("ofr_transport_price");

                entity.Property(e => e.OfrTransportToWork).HasColumnName("ofr_transport_to_work");

                entity.Property(e => e.OfrVacanciesNumber).HasColumnName("ofr_vacancies_number");

                entity.Property(e => e.OfrWorkDays).HasColumnName("ofr_work_days");

                entity.Property(e => e.OfrWorkEnd)
                    .HasColumnName("ofr_work_end")
                    .HasColumnType("datetime");

                entity.Property(e => e.OfrWorkPlace)
                    .HasColumnName("ofr_work_place")
                    .HasMaxLength(500);

                entity.Property(e => e.OfrWorkStart)
                    .HasColumnName("ofr_work_start")
                    .HasColumnType("datetime");
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
        public virtual DbSet<Contracts> Contracts { get; set; }
        public virtual DbSet<Contragent> Contragents { get; set; }
        public virtual DbSet<ContragentsUsersMappings> ContragentsUsersMappings { get; set; }
        public virtual DbSet<DocumentFiles> DocumentFiles { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<EmploymentRequests> EmploymentRequests { get; set; }
        public virtual DbSet<IdentityDocuments> IdentityDocuments { get; set; }
        public virtual DbSet<Notes> Notes { get; set; }
        public virtual DbSet<Offers> Offers { get; set; }
    }
}