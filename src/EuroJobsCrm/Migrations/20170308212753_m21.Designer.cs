using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using EuroJobsCrm.Models;

namespace EuroJobsCrm.Migrations
{
    [DbContext(typeof(DB_A12601_bielkaContext))]
    [Migration("20170308212753_m21")]
    partial class m21
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EuroJobsCrm.Models.Addresses", b =>
                {
                    b.Property<int>("AdrId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("adr_id");

                    b.Property<string>("AdrAddress")
                        .HasColumnName("adr_address")
                        .HasAnnotation("MaxLength", 500);

                    b.Property<string>("AdrAduitMu")
                        .HasColumnName("adr_aduit_mu")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("AdrAduitRu")
                        .HasColumnName("adr_aduit_ru")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime?>("AdrAuditCd")
                        .HasColumnName("adr_audit_cd")
                        .HasColumnType("datetime");

                    b.Property<string>("AdrAuditCu")
                        .HasColumnName("adr_audit_cu")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime?>("AdrAuditMd")
                        .HasColumnName("adr_audit_md")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("AdrAuditRd")
                        .HasColumnName("adr_audit_rd")
                        .HasColumnType("datetime");

                    b.Property<int?>("AdrCgtId")
                        .HasColumnName("adr_cgt_id");

                    b.Property<string>("AdrCity")
                        .HasColumnName("adr_city")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("AdrCountry")
                        .HasColumnName("adr_country")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("AdrPostCode")
                        .HasColumnName("adr_post_code")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("AdrType")
                        .HasColumnName("adr_type")
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("ArdCltId")
                        .HasColumnName("ard_clt_id");

                    b.Property<int?>("ArdEmpId")
                        .HasColumnName("ard_emp_id");

                    b.HasKey("AdrId")
                        .HasName("PK_Addresses");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("EuroJobsCrm.Models.AspNetRoleClaims", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 450);

                    b.HasKey("Id");

                    b.HasIndex("RoleId")
                        .HasName("IX_AspNetRoleClaims_RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("EuroJobsCrm.Models.AspNetRoles", b =>
                {
                    b.Property<string>("Id")
                        .HasAnnotation("MaxLength", 450);

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("EuroJobsCrm.Models.AspNetUserClaims", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 450);

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .HasName("IX_AspNetUserClaims_UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("EuroJobsCrm.Models.AspNetUserLogins", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasAnnotation("MaxLength", 450);

                    b.Property<string>("ProviderKey")
                        .HasAnnotation("MaxLength", 450);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 450);

                    b.HasKey("LoginProvider", "ProviderKey")
                        .HasName("PK_AspNetUserLogins");

                    b.HasIndex("UserId")
                        .HasName("IX_AspNetUserLogins_UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("EuroJobsCrm.Models.AspNetUserRoles", b =>
                {
                    b.Property<string>("UserId")
                        .HasAnnotation("MaxLength", 450);

                    b.Property<string>("RoleId")
                        .HasAnnotation("MaxLength", 450);

                    b.HasKey("UserId", "RoleId")
                        .HasName("PK_AspNetUserRoles");

                    b.HasIndex("RoleId")
                        .HasName("IX_AspNetUserRoles_RoleId");

                    b.HasIndex("UserId")
                        .HasName("IX_AspNetUserRoles_UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("EuroJobsCrm.Models.AspNetUsers", b =>
                {
                    b.Property<string>("Id")
                        .HasAnnotation("MaxLength", 450);

                    b.Property<int>("AccessFailedCount");

                    b.Property<bool?>("Blocked")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("0");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("EuroJobsCrm.Models.AspNetUserTokens", b =>
                {
                    b.Property<string>("UserId")
                        .HasAnnotation("MaxLength", 450);

                    b.Property<string>("LoginProvider")
                        .HasAnnotation("MaxLength", 450);

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 450);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name")
                        .HasName("PK_AspNetUserTokens");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("EuroJobsCrm.Models.Clients", b =>
                {
                    b.Property<int>("CltId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("clt_id");

                    b.Property<DateTime?>("CltAuditCd")
                        .HasColumnName("clt_audit_cd")
                        .HasColumnType("datetime");

                    b.Property<string>("CltAuditCu")
                        .HasColumnName("clt_audit_cu")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime?>("CltAuditMd")
                        .HasColumnName("clt_audit_md")
                        .HasColumnType("datetime");

                    b.Property<string>("CltAuditMu")
                        .HasColumnName("clt_audit_mu")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime?>("CltAuditRd")
                        .HasColumnName("clt_audit_rd")
                        .HasColumnType("datetime");

                    b.Property<string>("CltAuditRu")
                        .HasColumnName("clt_audit_ru")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("CltBranch")
                        .HasColumnName("clt_branch")
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("CltKrs")
                        .IsRequired()
                        .HasColumnName("clt_krs")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<string>("CltName")
                        .IsRequired()
                        .HasColumnName("clt_name")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("CltNip")
                        .IsRequired()
                        .HasColumnName("clt_nip")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<string>("CltRegon")
                        .IsRequired()
                        .HasColumnName("clt_regon")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<int?>("CltStatus")
                        .HasColumnName("clt_status");

                    b.Property<int?>("CltType")
                        .HasColumnName("clt_type");

                    b.HasKey("CltId")
                        .HasName("PK_Clients");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("EuroJobsCrm.Models.ContactPersons", b =>
                {
                    b.Property<int>("CtpId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ctp_id");

                    b.Property<DateTime?>("CtpAuditCd")
                        .HasColumnName("ctp_audit_cd")
                        .HasColumnType("datetime");

                    b.Property<string>("CtpAuditCu")
                        .HasColumnName("ctp_audit_cu")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime?>("CtpAuditMd")
                        .HasColumnName("ctp_audit_md")
                        .HasColumnType("datetime");

                    b.Property<string>("CtpAuditMu")
                        .HasColumnName("ctp_audit_mu")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime?>("CtpAuditRd")
                        .HasColumnName("ctp_audit_rd")
                        .HasColumnType("datetime");

                    b.Property<string>("CtpAuditRu")
                        .HasColumnName("ctp_audit_ru")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int?>("CtpCgtId")
                        .HasColumnName("ctp_cgt_id");

                    b.Property<int?>("CtpCltId")
                        .HasColumnName("ctp_clt_id");

                    b.Property<string>("CtpEmail")
                        .HasColumnName("ctp_email")
                        .HasAnnotation("MaxLength", 250);

                    b.Property<string>("CtpMessanger")
                        .HasColumnName("ctp_messanger")
                        .HasAnnotation("MaxLength", 150);

                    b.Property<int?>("CtpMessangerType")
                        .HasColumnName("ctp_messanger_type");

                    b.Property<string>("CtpName")
                        .HasColumnName("ctp_name")
                        .HasAnnotation("MaxLength", 150);

                    b.Property<string>("CtpPhoneNumber")
                        .HasColumnName("ctp_phone_number")
                        .HasAnnotation("MaxLength", 150);

                    b.Property<string>("CtpPosition")
                        .HasColumnName("ctp_position")
                        .HasAnnotation("MaxLength", 250);

                    b.Property<string>("CtpSkype")
                        .HasColumnName("ctp_skype")
                        .HasAnnotation("MaxLength", 150);

                    b.Property<string>("CtpSurname")
                        .HasColumnName("ctp_surname")
                        .HasAnnotation("MaxLength", 150);

                    b.HasKey("CtpId")
                        .HasName("PK_ContactPersons");

                    b.ToTable("ContactPersons");
                });

            modelBuilder.Entity("EuroJobsCrm.Models.Contracts", b =>
                {
                    b.Property<int>("CntId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("cnt_id");

                    b.Property<DateTime?>("CntAuditCd")
                        .HasColumnName("cnt_audit_cd")
                        .HasColumnType("datetime");

                    b.Property<string>("CntAuditCu")
                        .HasColumnName("cnt_audit_cu")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime?>("CntAuditMd")
                        .HasColumnName("cnt_audit_md")
                        .HasColumnType("datetime");

                    b.Property<string>("CntAuditMu")
                        .HasColumnName("cnt_audit_mu")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime?>("CntAuditRd")
                        .HasColumnName("cnt_audit_rd")
                        .HasColumnType("datetime");

                    b.Property<string>("CntAuditRu")
                        .HasColumnName("cnt_audit_ru")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int>("CntCgtId")
                        .HasColumnName("cnt_cgt_id");

                    b.Property<int>("CntEmpId")
                        .HasColumnName("cnt_emp_id");

                    b.Property<DateTime?>("CntEndDate")
                        .HasColumnName("cnt_end_date")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("CntIssueDate")
                        .HasColumnName("cnt_issue_date")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("CntStartDate")
                        .HasColumnName("cnt_start_date")
                        .HasColumnType("datetime");

                    b.HasKey("CntId")
                        .HasName("PK__Contract__CDB1F3C18F3440BF");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("EuroJobsCrm.Models.Contragents", b =>
                {
                    b.Property<int>("CgtId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("cgt_id");

                    b.Property<DateTime?>("CgtAuditCd")
                        .HasColumnName("cgt_audit_cd")
                        .HasColumnType("datetime");

                    b.Property<string>("CgtAuditCu")
                        .HasColumnName("cgt_audit_cu")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime?>("CgtAuditMd")
                        .HasColumnName("cgt_audit_md")
                        .HasColumnType("datetime");

                    b.Property<string>("CgtAuditMu")
                        .HasColumnName("cgt_audit_mu")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime?>("CgtAuditRd")
                        .HasColumnName("cgt_audit_rd")
                        .HasColumnType("datetime");

                    b.Property<string>("CgtAuditRu")
                        .HasColumnName("cgt_audit_ru")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("CgtLicenseNumber")
                        .IsRequired()
                        .HasColumnName("cgt_license_number")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("CgtName")
                        .IsRequired()
                        .HasColumnName("cgt_name")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("CgtResponsibleUser")
                        .HasColumnName("cgt_responsible_user")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("CgtStatus")
                        .IsRequired()
                        .HasColumnName("cgt_status")
                        .HasAnnotation("MaxLength", 1);

                    b.HasKey("CgtId")
                        .HasName("PK_Contragents");

                    b.ToTable("Contragents");
                });

            modelBuilder.Entity("EuroJobsCrm.Models.ContragentsUsersMappings", b =>
                {
                    b.Property<int>("CumId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("cum_id");

                    b.Property<int>("CumCtgId")
                        .HasColumnName("cum_ctg_id");

                    b.Property<string>("CumUserId")
                        .IsRequired()
                        .HasColumnName("cum_user_id")
                        .HasAnnotation("MaxLength", 450);

                    b.HasKey("CumId")
                        .HasName("PK_ContragentsUsersMappings");

                    b.ToTable("ContragentsUsersMappings");
                });

            modelBuilder.Entity("EuroJobsCrm.Models.DocumentFiles", b =>
                {
                    b.Property<int>("DcfId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("dcf_id");

                    b.Property<DateTime?>("DcfAuditCd")
                        .HasColumnName("dcf_audit_cd")
                        .HasColumnType("datetime");

                    b.Property<string>("DcfAuditCu")
                        .HasColumnName("dcf_audit_cu")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime?>("DcfAuditMd")
                        .HasColumnName("dcf_audit_md")
                        .HasColumnType("datetime");

                    b.Property<string>("DcfAuditMu")
                        .HasColumnName("dcf_audit_mu")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime?>("DcfAuditRd")
                        .HasColumnName("dcf_audit_rd")
                        .HasColumnType("datetime");

                    b.Property<string>("DcfAuditRu")
                        .HasColumnName("dcf_audit_ru")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int?>("DcfCliId")
                        .HasColumnName("dcf_cli_id");

                    b.Property<int?>("DcfCntId")
                        .HasColumnName("dcf_cnt_id");

                    b.Property<string>("DcfDescription")
                        .HasColumnName("dcf_description")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<string>("DcfGoogleFileId")
                        .HasColumnName("dcf_google_file_id")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int?>("DcfIdcId")
                        .HasColumnName("dcf_idc_id");

                    b.Property<int?>("DcfInvId")
                        .HasColumnName("dcf_inv_id");

                    b.Property<string>("DcfName")
                        .IsRequired()
                        .HasColumnName("dcf_name")
                        .HasAnnotation("MaxLength", 300);

                    b.Property<int?>("DcfOfrId")
                        .HasColumnName("dcf_ofr_id");

                    b.Property<string>("DcfUrl")
                        .IsRequired()
                        .HasColumnName("dcf_url")
                        .HasAnnotation("MaxLength", 500);

                    b.HasKey("DcfId")
                        .HasName("PK__Document__2996CAF62F188655");

                    b.ToTable("DocumentFiles");
                });

            modelBuilder.Entity("EuroJobsCrm.Models.Employees", b =>
                {
                    b.Property<int>("EmpId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("emp_id");

                    b.Property<DateTime?>("EmpAuditCd")
                        .HasColumnName("emp_audit_cd")
                        .HasColumnType("datetime");

                    b.Property<string>("EmpAuditCu")
                        .HasColumnName("emp_audit_cu")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime?>("EmpAuditMd")
                        .HasColumnName("emp_audit_md")
                        .HasColumnType("datetime");

                    b.Property<string>("EmpAuditMu")
                        .HasColumnName("emp_audit_mu")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime?>("EmpAuditRd")
                        .HasColumnName("emp_audit_rd")
                        .HasColumnType("datetime");

                    b.Property<string>("EmpAuditRu")
                        .HasColumnName("emp_audit_ru")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime>("EmpBirthDate")
                        .HasColumnName("emp_birth_date")
                        .HasColumnType("date");

                    b.Property<int?>("EmpCltId")
                        .HasColumnName("emp_clt_id");

                    b.Property<int>("EmpCtgId")
                        .HasColumnName("emp_ctg_id");

                    b.Property<string>("EmpDescription")
                        .HasColumnName("emp_description")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<string>("EmpFirstName")
                        .IsRequired()
                        .HasColumnName("emp_first_name")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("EmpLastName")
                        .IsRequired()
                        .HasColumnName("emp_last_name")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("EmpMiddleName")
                        .HasColumnName("emp_middle_name")
                        .HasAnnotation("MaxLength", 150);

                    b.Property<int?>("EmpOffId")
                        .HasColumnName("emp_off_id");

                    b.Property<string>("EmpResponsibleUser")
                        .HasColumnName("emp_responsible_user")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int?>("EmpStatus")
                        .HasColumnName("emp_status");

                    b.HasKey("EmpId")
                        .HasName("PK_Employees");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("EuroJobsCrm.Models.EmploymentRequests", b =>
                {
                    b.Property<int>("EtrId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("etr_id");

                    b.Property<DateTime?>("EtrAuditCd")
                        .HasColumnName("etr_audit_cd")
                        .HasColumnType("datetime");

                    b.Property<string>("EtrAuditCu")
                        .HasColumnName("etr_audit_cu")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime?>("EtrAuditMd")
                        .HasColumnName("etr_audit_md")
                        .HasColumnType("datetime");

                    b.Property<string>("EtrAuditMu")
                        .HasColumnName("etr_audit_mu")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime?>("EtrAuditRd")
                        .HasColumnName("etr_audit_rd")
                        .HasColumnType("datetime");

                    b.Property<string>("EtrAuditRu")
                        .HasColumnName("etr_audit_ru")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int?>("EtrCltId")
                        .HasColumnName("etr_clt_id");

                    b.Property<int?>("EtrCntId")
                        .HasColumnName("etr_cnt_id");

                    b.Property<int>("EtrEmpId")
                        .HasColumnName("etr_emp_id");

                    b.Property<int>("EtrOfrId")
                        .HasColumnName("etr_ofr_id");

                    b.Property<int>("EtrStatus")
                        .HasColumnName("etr_status");

                    b.HasKey("EtrId")
                        .HasName("PK__Employme__D200B475365274A4");

                    b.ToTable("EmploymentRequests");
                });

            modelBuilder.Entity("EuroJobsCrm.Models.IdentityDocuments", b =>
                {
                    b.Property<int>("IdcId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("idc_id");

                    b.Property<DateTime?>("IdcAuditCd")
                        .HasColumnName("idc_audit_cd")
                        .HasColumnType("datetime");

                    b.Property<string>("IdcAuditCu")
                        .HasColumnName("idc_audit_cu")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime?>("IdcAuditMd")
                        .HasColumnName("idc_audit_md")
                        .HasColumnType("datetime");

                    b.Property<string>("IdcAuditMu")
                        .HasColumnName("idc_audit_mu")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime?>("IdcAuditRd")
                        .HasColumnName("idc_audit_rd")
                        .HasColumnType("datetime");

                    b.Property<string>("IdcAuditRu")
                        .HasColumnName("idc_audit_ru")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int>("IdcEmpId")
                        .HasColumnName("idc_emp_id");

                    b.Property<DateTime?>("IdcIssueDate")
                        .HasColumnName("idc_issue_date")
                        .HasColumnType("datetime");

                    b.Property<string>("IdcNumber")
                        .HasColumnName("idc_number")
                        .HasAnnotation("MaxLength", 32);

                    b.Property<int?>("IdcParentIdcId")
                        .HasColumnName("idc_parent_idc_id");

                    b.Property<string>("IdcRemarks")
                        .HasColumnName("idc_remarks")
                        .HasAnnotation("MaxLength", 500);

                    b.Property<string>("IdcSeria")
                        .HasColumnName("idc_seria")
                        .HasAnnotation("MaxLength", 16);

                    b.Property<int>("IdcType")
                        .HasColumnName("idc_type");

                    b.Property<DateTime?>("IdcValidFrom")
                        .HasColumnName("idc_valid_from")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("IdcValidTo")
                        .HasColumnName("idc_valid_to")
                        .HasColumnType("datetime");

                    b.Property<string>("IdcVisaType")
                        .HasColumnName("idc_visa_type")
                        .HasAnnotation("MaxLength", 10);

                    b.HasKey("IdcId")
                        .HasName("PK__Identity__CC1873659A101A22");

                    b.ToTable("IdentityDocuments");
                });

            modelBuilder.Entity("EuroJobsCrm.Models.Notes", b =>
                {
                    b.Property<string>("NotId")
                        .HasColumnName("not_id")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime?>("NotAuditCd")
                        .HasColumnName("not_audit_cd")
                        .HasColumnType("datetime");

                    b.Property<string>("NotAuditCu")
                        .HasColumnName("not_audit_cu")
                        .HasAnnotation("MaxLength", 150);

                    b.Property<DateTime?>("NotAuditMd")
                        .HasColumnName("not_audit_md")
                        .HasColumnType("datetime");

                    b.Property<string>("NotAuditMu")
                        .HasColumnName("not_audit_mu")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime?>("NotAuditRd")
                        .HasColumnName("not_audit_rd")
                        .HasColumnType("datetime");

                    b.Property<string>("NotAuditRu")
                        .HasColumnName("not_audit_ru")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int?>("NotCltId")
                        .HasColumnName("not_clt_id");

                    b.Property<int?>("NotCtgId")
                        .HasColumnName("not_ctg_id");

                    b.Property<int?>("NotEmp")
                        .HasColumnName("not_emp");

                    b.Property<DateTime?>("NotEndDate")
                        .HasColumnName("not_end_date")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("NotRemindDate")
                        .HasColumnName("not_remind_date")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("NotStartDate")
                        .HasColumnName("not_start_date")
                        .HasColumnType("datetime");

                    b.Property<string>("NotSubject")
                        .HasColumnName("not_subject")
                        .HasAnnotation("MaxLength", 200);

                    b.Property<string>("NotTargetUser")
                        .HasColumnName("not_target_user")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("NotText")
                        .HasColumnName("not_text")
                        .HasAnnotation("MaxLength", 1000);

                    b.HasKey("NotId")
                        .HasName("PK_Notes");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("EuroJobsCrm.Models.Offers", b =>
                {
                    b.Property<int>("OfrId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ofr_id");

                    b.Property<double?>("OfrAccomodationPrice")
                        .HasColumnName("ofr_accomodation_price");

                    b.Property<int?>("OfrAccomodationType")
                        .HasColumnName("ofr_accomodation_type");

                    b.Property<string>("OfrAdditionalInfo")
                        .HasColumnName("ofr_additional_info")
                        .HasColumnType("text");

                    b.Property<double?>("OfrAdvanceAmount")
                        .HasColumnName("ofr_advance_amount");

                    b.Property<int?>("OfrAgeFrom")
                        .HasColumnName("ofr_age_from");

                    b.Property<int?>("OfrAgeTo")
                        .HasColumnName("ofr_age_to");

                    b.Property<DateTime?>("OfrAuditCd")
                        .HasColumnName("ofr_audit_cd")
                        .HasColumnType("datetime");

                    b.Property<string>("OfrAuditCu")
                        .HasColumnName("ofr_audit_cu")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime?>("OfrAuditMd")
                        .HasColumnName("ofr_audit_md")
                        .HasColumnType("datetime");

                    b.Property<string>("OfrAuditMu")
                        .HasColumnName("ofr_audit_mu")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<DateTime?>("OfrAuditRd")
                        .HasColumnName("ofr_audit_rd")
                        .HasColumnType("datetime");

                    b.Property<string>("OfrAuditRu")
                        .HasColumnName("ofr_audit_ru")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int?>("OfrBranch")
                        .HasColumnName("ofr_branch");

                    b.Property<int>("OfrCltId")
                        .HasColumnName("ofr_clt_id");

                    b.Property<string>("OfrComments")
                        .HasColumnName("ofr_comments")
                        .HasColumnType("text");

                    b.Property<int?>("OfrContractType")
                        .HasColumnName("ofr_contract_type");

                    b.Property<double?>("OfrDistanceToWork")
                        .HasColumnName("ofr_distance_to_work");

                    b.Property<string>("OfrDocuments")
                        .HasColumnName("ofr_documents")
                        .HasAnnotation("MaxLength", 500);

                    b.Property<int?>("OfrEducation")
                        .HasColumnName("ofr_education");

                    b.Property<DateTime?>("OfrEndingDate")
                        .HasColumnName("ofr_ending_date")
                        .HasColumnType("datetime");

                    b.Property<string>("OfrExperience")
                        .HasColumnName("ofr_experience")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("OfrFacilities")
                        .HasColumnName("ofr_facilities")
                        .HasAnnotation("MaxLength", 1000);

                    b.Property<int?>("OfrGender")
                        .HasColumnName("ofr_gender");

                    b.Property<int?>("OfrHoursPerMonth")
                        .HasColumnName("ofr_hours_per_month");

                    b.Property<string>("OfrLanguages")
                        .HasColumnName("ofr_languages")
                        .HasColumnType("nchar(300)");

                    b.Property<double?>("OfrOvertimeRate")
                        .HasColumnName("ofr_overtime_rate");

                    b.Property<int?>("OfrPaymentMethod")
                        .HasColumnName("ofr_payment_method");

                    b.Property<string>("OfrPosition")
                        .HasColumnName("ofr_position")
                        .HasAnnotation("MaxLength", 150);

                    b.Property<double?>("OfrRatePerHour")
                        .HasColumnName("ofr_rate_per_hour");

                    b.Property<double?>("OfrRatePerMonth")
                        .HasColumnName("ofr_rate_per_month");

                    b.Property<string>("OfrResponsibilities")
                        .HasColumnName("ofr_responsibilities")
                        .HasAnnotation("MaxLength", 1500);

                    b.Property<int?>("OfrRoomPeopleNumber")
                        .HasColumnName("ofr_room_people_number");

                    b.Property<DateTime?>("OfrStartingDate")
                        .HasColumnName("ofr_starting_date")
                        .HasColumnType("datetime");

                    b.Property<double?>("OfrTransportPrice")
                        .HasColumnName("ofr_transport_price");

                    b.Property<int?>("OfrTransportToWork")
                        .HasColumnName("ofr_transport_to_work");

                    b.Property<int?>("OfrVacanciesNumber")
                        .HasColumnName("ofr_vacancies_number");

                    b.Property<int>("OfrWorkDays")
                        .HasColumnName("ofr_work_days");

                    b.Property<DateTime?>("OfrWorkEnd")
                        .HasColumnName("ofr_work_end")
                        .HasColumnType("datetime");

                    b.Property<string>("OfrWorkPlace")
                        .HasColumnName("ofr_work_place")
                        .HasAnnotation("MaxLength", 500);

                    b.Property<DateTime?>("OfrWorkStart")
                        .HasColumnName("ofr_work_start")
                        .HasColumnType("datetime");

                    b.HasKey("OfrId")
                        .HasName("PK_Offers");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("EuroJobsCrm.Models.UsersToContragents", b =>
                {
                    b.Property<int>("UtcId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("utc_id");

                    b.Property<int?>("UtcCtgId")
                        .HasColumnName("utc_ctg_id");

                    b.Property<string>("UtcLng")
                        .HasColumnName("utc_lng")
                        .HasColumnType("varchar(2)");

                    b.Property<string>("UtcUsrId")
                        .HasColumnName("utc_usr_id")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UtcUsrName")
                        .HasColumnName("utc_usr_name")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("UtcId")
                        .HasName("UsersToContragents_utc_id_uindex");

                    b.ToTable("UsersToContragents");
                });

            modelBuilder.Entity("EuroJobsCrm.Models.AspNetRoleClaims", b =>
                {
                    b.HasOne("EuroJobsCrm.Models.AspNetRoles", "Role")
                        .WithMany("AspNetRoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EuroJobsCrm.Models.AspNetUserClaims", b =>
                {
                    b.HasOne("EuroJobsCrm.Models.AspNetUsers", "User")
                        .WithMany("AspNetUserClaims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EuroJobsCrm.Models.AspNetUserLogins", b =>
                {
                    b.HasOne("EuroJobsCrm.Models.AspNetUsers", "User")
                        .WithMany("AspNetUserLogins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EuroJobsCrm.Models.AspNetUserRoles", b =>
                {
                    b.HasOne("EuroJobsCrm.Models.AspNetRoles", "Role")
                        .WithMany("AspNetUserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EuroJobsCrm.Models.AspNetUsers", "User")
                        .WithMany("AspNetUserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
