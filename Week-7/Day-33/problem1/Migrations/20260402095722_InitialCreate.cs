using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace relationships.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Courseid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Coursename = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Courseid);
                });

            migrationBuilder.CreateTable(
                name: "Depts",
                columns: table => new
                {
                    Deptid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depts", x => x.Deptid);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Studentid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Studentname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Courseid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Studentid);
                    table.ForeignKey(
                        name: "FK_Students_Courses_Courseid",
                        column: x => x.Courseid,
                        principalTable: "Courses",
                        principalColumn: "Courseid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employeess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Empname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Job = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<long>(type: "bigint", nullable: false),
                    Deptid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employeess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employeess_Depts_Deptid",
                        column: x => x.Deptid,
                        principalTable: "Depts",
                        principalColumn: "Deptid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employeess_Deptid",
                table: "Employeess",
                column: "Deptid");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Courseid",
                table: "Students",
                column: "Courseid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employeess");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Depts");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
