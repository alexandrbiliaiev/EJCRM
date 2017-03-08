using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EuroJobsCrm.Migrations
{
    public partial class m21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    adr_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    adr_address = table.Column<string>(maxLength: 500, nullable: true),
                    adr_aduit_mu = table.Column<string>(maxLength: 100, nullable: true),
                    adr_aduit_ru = table.Column<string>(maxLength: 100, nullable: true),
                    adr_audit_cd = table.Column<DateTime>(type: "datetime", nullable: true),
                    adr_audit_cu = table.Column<string>(maxLength: 100, nullable: true),
                    adr_audit_md = table.Column<DateTime>(type: "datetime", nullable: true),
                    adr_audit_rd = table.Column<DateTime>(type: "datetime", nullable: true),
                    adr_cgt_id = table.Column<int>(nullable: true),
                    adr_city = table.Column<string>(type: "varchar(100)", nullable: true),
                    adr_country = table.Column<string>(type: "varchar(100)", nullable: true),
                    adr_post_code = table.Column<string>(type: "varchar(10)", nullable: true),
                    adr_type = table.Column<string>(type: "varchar(50)", nullable: true),
                    ard_clt_id = table.Column<int>(nullable: true),
                    ard_emp_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.adr_id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Blocked = table.Column<bool>(nullable: true, defaultValueSql: "0"),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(maxLength: 450, nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 450, nullable: false),
                    Name = table.Column<string>(maxLength: 450, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    clt_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    clt_audit_cd = table.Column<DateTime>(type: "datetime", nullable: true),
                    clt_audit_cu = table.Column<string>(maxLength: 100, nullable: true),
                    clt_audit_md = table.Column<DateTime>(type: "datetime", nullable: true),
                    clt_audit_mu = table.Column<string>(maxLength: 100, nullable: true),
                    clt_audit_rd = table.Column<DateTime>(type: "datetime", nullable: true),
                    clt_audit_ru = table.Column<string>(maxLength: 100, nullable: true),
                    clt_branch = table.Column<string>(maxLength: 30, nullable: true),
                    clt_krs = table.Column<string>(maxLength: 20, nullable: false),
                    clt_name = table.Column<string>(maxLength: 100, nullable: false),
                    clt_nip = table.Column<string>(maxLength: 20, nullable: false),
                    clt_regon = table.Column<string>(maxLength: 20, nullable: false),
                    clt_status = table.Column<int>(nullable: true),
                    clt_type = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.clt_id);
                });

            migrationBuilder.CreateTable(
                name: "ContactPersons",
                columns: table => new
                {
                    ctp_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ctp_audit_cd = table.Column<DateTime>(type: "datetime", nullable: true),
                    ctp_audit_cu = table.Column<string>(maxLength: 100, nullable: true),
                    ctp_audit_md = table.Column<DateTime>(type: "datetime", nullable: true),
                    ctp_audit_mu = table.Column<string>(maxLength: 100, nullable: true),
                    ctp_audit_rd = table.Column<DateTime>(type: "datetime", nullable: true),
                    ctp_audit_ru = table.Column<string>(maxLength: 100, nullable: true),
                    ctp_cgt_id = table.Column<int>(nullable: true),
                    ctp_clt_id = table.Column<int>(nullable: true),
                    ctp_email = table.Column<string>(maxLength: 250, nullable: true),
                    ctp_messanger = table.Column<string>(maxLength: 150, nullable: true),
                    ctp_messanger_type = table.Column<int>(nullable: true),
                    ctp_name = table.Column<string>(maxLength: 150, nullable: true),
                    ctp_phone_number = table.Column<string>(maxLength: 150, nullable: true),
                    ctp_position = table.Column<string>(maxLength: 250, nullable: true),
                    ctp_skype = table.Column<string>(maxLength: 150, nullable: true),
                    ctp_surname = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactPersons", x => x.ctp_id);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    cnt_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    cnt_audit_cd = table.Column<DateTime>(type: "datetime", nullable: true),
                    cnt_audit_cu = table.Column<string>(maxLength: 100, nullable: true),
                    cnt_audit_md = table.Column<DateTime>(type: "datetime", nullable: true),
                    cnt_audit_mu = table.Column<string>(maxLength: 100, nullable: true),
                    cnt_audit_rd = table.Column<DateTime>(type: "datetime", nullable: true),
                    cnt_audit_ru = table.Column<string>(maxLength: 100, nullable: true),
                    cnt_cgt_id = table.Column<int>(nullable: false),
                    cnt_emp_id = table.Column<int>(nullable: false),
                    cnt_end_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    cnt_issue_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    cnt_start_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Contract__CDB1F3C18F3440BF", x => x.cnt_id);
                });

            migrationBuilder.CreateTable(
                name: "Contragents",
                columns: table => new
                {
                    cgt_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    cgt_audit_cd = table.Column<DateTime>(type: "datetime", nullable: true),
                    cgt_audit_cu = table.Column<string>(maxLength: 100, nullable: true),
                    cgt_audit_md = table.Column<DateTime>(type: "datetime", nullable: true),
                    cgt_audit_mu = table.Column<string>(maxLength: 100, nullable: true),
                    cgt_audit_rd = table.Column<DateTime>(type: "datetime", nullable: true),
                    cgt_audit_ru = table.Column<string>(maxLength: 100, nullable: true),
                    cgt_license_number = table.Column<string>(maxLength: 100, nullable: false),
                    cgt_name = table.Column<string>(maxLength: 100, nullable: false),
                    cgt_responsible_user = table.Column<string>(maxLength: 100, nullable: true),
                    cgt_status = table.Column<string>(maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contragents", x => x.cgt_id);
                });

            migrationBuilder.CreateTable(
                name: "ContragentsUsersMappings",
                columns: table => new
                {
                    cum_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    cum_ctg_id = table.Column<int>(nullable: false),
                    cum_user_id = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContragentsUsersMappings", x => x.cum_id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentFiles",
                columns: table => new
                {
                    dcf_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    dcf_audit_cd = table.Column<DateTime>(type: "datetime", nullable: true),
                    dcf_audit_cu = table.Column<string>(maxLength: 100, nullable: true),
                    dcf_audit_md = table.Column<DateTime>(type: "datetime", nullable: true),
                    dcf_audit_mu = table.Column<string>(maxLength: 100, nullable: true),
                    dcf_audit_rd = table.Column<DateTime>(type: "datetime", nullable: true),
                    dcf_audit_ru = table.Column<string>(maxLength: 100, nullable: true),
                    dcf_cli_id = table.Column<int>(nullable: true),
                    dcf_cnt_id = table.Column<int>(nullable: true),
                    dcf_description = table.Column<string>(maxLength: 1000, nullable: true),
                    dcf_google_file_id = table.Column<string>(maxLength: 50, nullable: true),
                    dcf_idc_id = table.Column<int>(nullable: true),
                    dcf_inv_id = table.Column<int>(nullable: true),
                    dcf_name = table.Column<string>(maxLength: 300, nullable: false),
                    dcf_ofr_id = table.Column<int>(nullable: true),
                    dcf_url = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Document__2996CAF62F188655", x => x.dcf_id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    emp_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    emp_audit_cd = table.Column<DateTime>(type: "datetime", nullable: true),
                    emp_audit_cu = table.Column<string>(maxLength: 100, nullable: true),
                    emp_audit_md = table.Column<DateTime>(type: "datetime", nullable: true),
                    emp_audit_mu = table.Column<string>(maxLength: 100, nullable: true),
                    emp_audit_rd = table.Column<DateTime>(type: "datetime", nullable: true),
                    emp_audit_ru = table.Column<string>(maxLength: 100, nullable: true),
                    emp_birth_date = table.Column<DateTime>(type: "date", nullable: false),
                    emp_clt_id = table.Column<int>(nullable: true),
                    emp_ctg_id = table.Column<int>(nullable: false),
                    emp_description = table.Column<string>(maxLength: 1000, nullable: true),
                    emp_first_name = table.Column<string>(maxLength: 100, nullable: false),
                    emp_last_name = table.Column<string>(maxLength: 100, nullable: false),
                    emp_middle_name = table.Column<string>(maxLength: 150, nullable: true),
                    emp_off_id = table.Column<int>(nullable: true),
                    emp_responsible_user = table.Column<string>(maxLength: 100, nullable: true),
                    emp_status = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.emp_id);
                });

            migrationBuilder.CreateTable(
                name: "EmploymentRequests",
                columns: table => new
                {
                    etr_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    etr_audit_cd = table.Column<DateTime>(type: "datetime", nullable: true),
                    etr_audit_cu = table.Column<string>(maxLength: 100, nullable: true),
                    etr_audit_md = table.Column<DateTime>(type: "datetime", nullable: true),
                    etr_audit_mu = table.Column<string>(maxLength: 100, nullable: true),
                    etr_audit_rd = table.Column<DateTime>(type: "datetime", nullable: true),
                    etr_audit_ru = table.Column<string>(maxLength: 100, nullable: true),
                    etr_clt_id = table.Column<int>(nullable: true),
                    etr_cnt_id = table.Column<int>(nullable: true),
                    etr_emp_id = table.Column<int>(nullable: false),
                    etr_ofr_id = table.Column<int>(nullable: false),
                    etr_status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Employme__D200B475365274A4", x => x.etr_id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityDocuments",
                columns: table => new
                {
                    idc_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    idc_audit_cd = table.Column<DateTime>(type: "datetime", nullable: true),
                    idc_audit_cu = table.Column<string>(maxLength: 100, nullable: true),
                    idc_audit_md = table.Column<DateTime>(type: "datetime", nullable: true),
                    idc_audit_mu = table.Column<string>(maxLength: 100, nullable: true),
                    idc_audit_rd = table.Column<DateTime>(type: "datetime", nullable: true),
                    idc_audit_ru = table.Column<string>(maxLength: 100, nullable: true),
                    idc_emp_id = table.Column<int>(nullable: false),
                    idc_issue_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    idc_number = table.Column<string>(maxLength: 32, nullable: true),
                    idc_parent_idc_id = table.Column<int>(nullable: true),
                    idc_remarks = table.Column<string>(maxLength: 500, nullable: true),
                    idc_seria = table.Column<string>(maxLength: 16, nullable: true),
                    idc_type = table.Column<int>(nullable: false),
                    idc_valid_from = table.Column<DateTime>(type: "datetime", nullable: true),
                    idc_valid_to = table.Column<DateTime>(type: "datetime", nullable: true),
                    idc_visa_type = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Identity__CC1873659A101A22", x => x.idc_id);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    not_id = table.Column<string>(maxLength: 100, nullable: false),
                    not_audit_cd = table.Column<DateTime>(type: "datetime", nullable: true),
                    not_audit_cu = table.Column<string>(maxLength: 150, nullable: true),
                    not_audit_md = table.Column<DateTime>(type: "datetime", nullable: true),
                    not_audit_mu = table.Column<string>(maxLength: 100, nullable: true),
                    not_audit_rd = table.Column<DateTime>(type: "datetime", nullable: true),
                    not_audit_ru = table.Column<string>(maxLength: 100, nullable: true),
                    not_clt_id = table.Column<int>(nullable: true),
                    not_ctg_id = table.Column<int>(nullable: true),
                    not_emp = table.Column<int>(nullable: true),
                    not_end_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    not_remind_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    not_start_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    not_subject = table.Column<string>(maxLength: 200, nullable: true),
                    not_target_user = table.Column<string>(maxLength: 100, nullable: true),
                    not_text = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.not_id);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    ofr_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ofr_accomodation_price = table.Column<double>(nullable: true),
                    ofr_accomodation_type = table.Column<int>(nullable: true),
                    ofr_additional_info = table.Column<string>(type: "text", nullable: true),
                    ofr_advance_amount = table.Column<double>(nullable: true),
                    ofr_age_from = table.Column<int>(nullable: true),
                    ofr_age_to = table.Column<int>(nullable: true),
                    ofr_audit_cd = table.Column<DateTime>(type: "datetime", nullable: true),
                    ofr_audit_cu = table.Column<string>(maxLength: 100, nullable: true),
                    ofr_audit_md = table.Column<DateTime>(type: "datetime", nullable: true),
                    ofr_audit_mu = table.Column<string>(maxLength: 100, nullable: true),
                    ofr_audit_rd = table.Column<DateTime>(type: "datetime", nullable: true),
                    ofr_audit_ru = table.Column<string>(maxLength: 100, nullable: true),
                    ofr_branch = table.Column<int>(nullable: true),
                    ofr_clt_id = table.Column<int>(nullable: false),
                    ofr_comments = table.Column<string>(type: "text", nullable: true),
                    ofr_contract_type = table.Column<int>(nullable: true),
                    ofr_distance_to_work = table.Column<double>(nullable: true),
                    ofr_documents = table.Column<string>(maxLength: 500, nullable: true),
                    ofr_education = table.Column<int>(nullable: true),
                    ofr_ending_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    ofr_experience = table.Column<string>(maxLength: 100, nullable: true),
                    ofr_facilities = table.Column<string>(maxLength: 1000, nullable: true),
                    ofr_gender = table.Column<int>(nullable: true),
                    ofr_hours_per_month = table.Column<int>(nullable: true),
                    ofr_languages = table.Column<string>(type: "nchar(300)", nullable: true),
                    ofr_overtime_rate = table.Column<double>(nullable: true),
                    ofr_payment_method = table.Column<int>(nullable: true),
                    ofr_position = table.Column<string>(maxLength: 150, nullable: true),
                    ofr_rate_per_hour = table.Column<double>(nullable: true),
                    ofr_rate_per_month = table.Column<double>(nullable: true),
                    ofr_responsibilities = table.Column<string>(maxLength: 1500, nullable: true),
                    ofr_room_people_number = table.Column<int>(nullable: true),
                    ofr_starting_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    ofr_transport_price = table.Column<double>(nullable: true),
                    ofr_transport_to_work = table.Column<int>(nullable: true),
                    ofr_vacancies_number = table.Column<int>(nullable: true),
                    ofr_work_days = table.Column<int>(nullable: false),
                    ofr_work_end = table.Column<DateTime>(type: "datetime", nullable: true),
                    ofr_work_place = table.Column<string>(maxLength: 500, nullable: true),
                    ofr_work_start = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.ofr_id);
                });

            migrationBuilder.CreateTable(
                name: "UsersToContragents",
                columns: table => new
                {
                    utc_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    utc_ctg_id = table.Column<int>(nullable: true),
                    utc_lng = table.Column<string>(type: "varchar(2)", nullable: true),
                    utc_usr_id = table.Column<string>(type: "varchar(100)", nullable: true),
                    utc_usr_name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UsersToContragents_utc_id_uindex", x => x.utc_id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 450, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 450, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(maxLength: 450, nullable: false),
                    RoleId = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "ContactPersons");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Contragents");

            migrationBuilder.DropTable(
                name: "ContragentsUsersMappings");

            migrationBuilder.DropTable(
                name: "DocumentFiles");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "EmploymentRequests");

            migrationBuilder.DropTable(
                name: "IdentityDocuments");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "UsersToContragents");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
