using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Auth.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentUploads",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    FileSize = table.Column<string>(nullable: true),
                    userID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentUploads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationalBackground",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Institution = table.Column<string>(nullable: true),
                    Qualification = table.Column<string>(nullable: true),
                    Specialization = table.Column<string>(nullable: true),
                    Grade = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    userID = table.Column<string>(nullable: true),
                    isEdit = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalBackground", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Interview",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Venue = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    isEdit = table.Column<bool>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    InterViewDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interview", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InterviewAndEvaluationViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MaxPoints = table.Column<int>(nullable: false),
                    Venue = table.Column<string>(nullable: true),
                    InterViewDate = table.Column<DateTime>(nullable: false),
                    isEdit = table.Column<bool>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    Question = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewAndEvaluationViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PracticingLicenses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Issuer = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    IssuedOn = table.Column<DateTime>(nullable: false),
                    ValidFrom = table.Column<DateTime>(nullable: false),
                    ValidTo = table.Column<DateTime>(nullable: false),
                    LicenseNumber = table.Column<string>(nullable: true),
                    isEdit = table.Column<bool>(nullable: false),
                    userID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticingLicenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    AccessLink = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    ProductImagePath = table.Column<string>(nullable: true),
                    isEdit = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfessionalBodies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Qualifications = table.Column<string>(nullable: true),
                    MemberSince = table.Column<DateTime>(nullable: false),
                    Ongoing = table.Column<bool>(nullable: false),
                    userID = table.Column<string>(nullable: true),
                    isEdit = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessionalBodies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SeminarAndWorkshops",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Award = table.Column<string>(nullable: true),
                    From = table.Column<DateTime>(nullable: false),
                    To = table.Column<DateTime>(nullable: false),
                    userID = table.Column<string>(nullable: true),
                    Institution = table.Column<string>(nullable: true),
                    isEdit = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeminarAndWorkshops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailSettings = table.Column<string>(nullable: true),
                    SMTPPort = table.Column<string>(nullable: true),
                    SMTServer = table.Column<string>(nullable: true),
                    SMTPUserName = table.Column<string>(nullable: true),
                    SenderFromEmail = table.Column<string>(nullable: true),
                    SenderFromPassword = table.Column<string>(nullable: true),
                    SMTPPassword = table.Column<string>(nullable: true),
                    ClientName = table.Column<string>(nullable: true),
                    TagLine = table.Column<string>(nullable: true),
                    TeamVision = table.Column<string>(nullable: true),
                    PrimaryColor = table.Column<string>(nullable: true),
                    SecondaryColor = table.Column<string>(nullable: true),
                    LogoImage = table.Column<string>(nullable: true),
                    LayoutImage = table.Column<string>(nullable: true),
                    TeamLogo = table.Column<string>(nullable: true),
                    ContacUsEmail = table.Column<string>(nullable: true),
                    SenderFromName = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    Radio = table.Column<string>(nullable: true),
                    ReportPassword = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkExperience",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Employer = table.Column<string>(nullable: true),
                    JobTitle = table.Column<string>(nullable: true),
                    From = table.Column<DateTime>(nullable: false),
                    To = table.Column<DateTime>(nullable: false),
                    Industry = table.Column<string>(nullable: true),
                    WorkCategory = table.Column<string>(nullable: true),
                    JobResponsibilities = table.Column<string>(nullable: true),
                    userID = table.Column<string>(nullable: true),
                    isEdit = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkExperience", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                name: "Evaluations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    isEdit = table.Column<bool>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    InterviewAndEvaluationViewModelId = table.Column<int>(nullable: true),
                    InterviewId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evaluations_InterviewAndEvaluationViewModel_InterviewAndEvaluationViewModelId",
                        column: x => x.InterviewAndEvaluationViewModelId,
                        principalTable: "InterviewAndEvaluationViewModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluations_Interview_InterviewId",
                        column: x => x.InterviewId,
                        principalTable: "Interview",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Employer = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Industry = table.Column<string>(nullable: true),
                    OverallPurpose = table.Column<string>(nullable: true),
                    Specifications = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    PersonalSpecifications = table.Column<string>(nullable: true),
                    ClosingDate = table.Column<DateTime>(nullable: false),
                    IsFeatured = table.Column<bool>(nullable: false),
                    InterviewAndEvaluationViewModelId = table.Column<int>(nullable: true),
                    InterviewId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_InterviewAndEvaluationViewModel_InterviewAndEvaluationViewModelId",
                        column: x => x.InterviewAndEvaluationViewModelId,
                        principalTable: "InterviewAndEvaluationViewModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Jobs_Interview_InterviewId",
                        column: x => x.InterviewId,
                        principalTable: "Interview",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(nullable: true),
                    MaxPoints = table.Column<int>(nullable: false),
                    EvaluationId = table.Column<int>(nullable: true),
                    InterviewAndEvaluationViewModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Evaluations_EvaluationId",
                        column: x => x.EvaluationId,
                        principalTable: "Evaluations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Questions_InterviewAndEvaluationViewModel_InterviewAndEvaluationViewModelId",
                        column: x => x.InterviewAndEvaluationViewModelId,
                        principalTable: "InterviewAndEvaluationViewModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "JobApplications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    ReferenceNumber = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    JobTitle = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    JobId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    CoverLetter = table.Column<string>(nullable: true),
                    Cv = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    AlternatePhoneNumber = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    IDDocument = table.Column<string>(nullable: true),
                    IDDocumentNumber = table.Column<int>(nullable: false),
                    MaritalStatus = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Nationality = table.Column<string>(nullable: true),
                    County = table.Column<string>(nullable: true),
                    Ethnicity = table.Column<string>(nullable: true),
                    Languages = table.Column<string>(nullable: true),
                    Disability = table.Column<bool>(nullable: false),
                    DisabilityCondition = table.Column<string>(nullable: true),
                    Skills = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(nullable: true),
                    IsLoggedIn = table.Column<bool>(nullable: false),
                    ActivationStatus = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Role = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    DocumentUploadsId = table.Column<int>(nullable: true),
                    EducationalBackgroundId = table.Column<int>(nullable: true),
                    JobApplicationsId = table.Column<int>(nullable: true),
                    PracticingLicensesId = table.Column<int>(nullable: true),
                    ProfessionalBodiesId = table.Column<int>(nullable: true),
                    SeminarAndWorkshopsId = table.Column<int>(nullable: true),
                    WorkExperienceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_DocumentUploads_DocumentUploadsId",
                        column: x => x.DocumentUploadsId,
                        principalTable: "DocumentUploads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_EducationalBackground_EducationalBackgroundId",
                        column: x => x.EducationalBackgroundId,
                        principalTable: "EducationalBackground",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_JobApplications_JobApplicationsId",
                        column: x => x.JobApplicationsId,
                        principalTable: "JobApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_PracticingLicenses_PracticingLicensesId",
                        column: x => x.PracticingLicensesId,
                        principalTable: "PracticingLicenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_ProfessionalBodies_ProfessionalBodiesId",
                        column: x => x.ProfessionalBodiesId,
                        principalTable: "ProfessionalBodies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_SeminarAndWorkshops_SeminarAndWorkshopsId",
                        column: x => x.SeminarAndWorkshopsId,
                        principalTable: "SeminarAndWorkshops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_WorkExperience_WorkExperienceId",
                        column: x => x.WorkExperienceId,
                        principalTable: "WorkExperience",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                name: "IX_AspNetUsers_DocumentUploadsId",
                table: "AspNetUsers",
                column: "DocumentUploadsId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EducationalBackgroundId",
                table: "AspNetUsers",
                column: "EducationalBackgroundId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_JobApplicationsId",
                table: "AspNetUsers",
                column: "JobApplicationsId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PracticingLicensesId",
                table: "AspNetUsers",
                column: "PracticingLicensesId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProfessionalBodiesId",
                table: "AspNetUsers",
                column: "ProfessionalBodiesId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SeminarAndWorkshopsId",
                table: "AspNetUsers",
                column: "SeminarAndWorkshopsId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WorkExperienceId",
                table: "AspNetUsers",
                column: "WorkExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_InterviewAndEvaluationViewModelId",
                table: "Evaluations",
                column: "InterviewAndEvaluationViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_InterviewId",
                table: "Evaluations",
                column: "InterviewId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_UserId",
                table: "JobApplications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_InterviewAndEvaluationViewModelId",
                table: "Jobs",
                column: "InterviewAndEvaluationViewModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_InterviewId",
                table: "Jobs",
                column: "InterviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_EvaluationId",
                table: "Questions",
                column: "EvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_InterviewAndEvaluationViewModelId",
                table: "Questions",
                column: "InterviewAndEvaluationViewModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_AspNetUsers_UserId",
                table: "JobApplications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_AspNetUsers_UserId",
                table: "JobApplications");

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
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Evaluations");

            migrationBuilder.DropTable(
                name: "InterviewAndEvaluationViewModel");

            migrationBuilder.DropTable(
                name: "Interview");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "DocumentUploads");

            migrationBuilder.DropTable(
                name: "EducationalBackground");

            migrationBuilder.DropTable(
                name: "JobApplications");

            migrationBuilder.DropTable(
                name: "PracticingLicenses");

            migrationBuilder.DropTable(
                name: "ProfessionalBodies");

            migrationBuilder.DropTable(
                name: "SeminarAndWorkshops");

            migrationBuilder.DropTable(
                name: "WorkExperience");
        }
    }
}
