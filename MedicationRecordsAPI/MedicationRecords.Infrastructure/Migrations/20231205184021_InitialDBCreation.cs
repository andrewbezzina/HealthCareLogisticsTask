using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicationRecordsAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialDBCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActiveIngredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveIngredientList = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveIngredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ATCCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATCCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassificationName = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PharmaceuticalForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PharmaceuticalFormsList = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PharmaceuticalForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TherapeuticClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TherapeuticClassName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TherapeuticClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ActiveIngredientsId = table.Column<int>(type: "int", nullable: false),
                    PharmaceuticalFormsId = table.Column<int>(type: "int", nullable: false),
                    TherapeuticClassId = table.Column<int>(type: "int", nullable: false),
                    ATCCodeId = table.Column<int>(type: "int", nullable: false),
                    ClassificationId = table.Column<int>(type: "int", nullable: false),
                    CompetentAuthorityStatus = table.Column<int>(type: "int", nullable: false),
                    InternalStatus = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medications_ATCCodes_ATCCodeId",
                        column: x => x.ATCCodeId,
                        principalTable: "ATCCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medications_ActiveIngredients_ActiveIngredientsId",
                        column: x => x.ActiveIngredientsId,
                        principalTable: "ActiveIngredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medications_Classifications_ClassificationId",
                        column: x => x.ClassificationId,
                        principalTable: "Classifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medications_PharmaceuticalForms_PharmaceuticalFormsId",
                        column: x => x.PharmaceuticalFormsId,
                        principalTable: "PharmaceuticalForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medications_TherapeuticClasses_TherapeuticClassId",
                        column: x => x.TherapeuticClassId,
                        principalTable: "TherapeuticClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medications_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medications_ActiveIngredientsId",
                table: "Medications",
                column: "ActiveIngredientsId");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_ATCCodeId",
                table: "Medications",
                column: "ATCCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_ClassificationId",
                table: "Medications",
                column: "ClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_PharmaceuticalFormsId",
                table: "Medications",
                column: "PharmaceuticalFormsId");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_TherapeuticClassId",
                table: "Medications",
                column: "TherapeuticClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_UnitId",
                table: "Medications",
                column: "UnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medications");

            migrationBuilder.DropTable(
                name: "ATCCodes");

            migrationBuilder.DropTable(
                name: "ActiveIngredients");

            migrationBuilder.DropTable(
                name: "Classifications");

            migrationBuilder.DropTable(
                name: "PharmaceuticalForms");

            migrationBuilder.DropTable(
                name: "TherapeuticClasses");

            migrationBuilder.DropTable(
                name: "Units");
        }
    }
}
