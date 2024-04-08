using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sicoob.API.Auth.Migrations
{
    /// <inheritdoc />
    public partial class TBLOGIN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBLOGIN",
                columns: table => new
                {
                    idLogin = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    senhaHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    senhaSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBLOGIN", x => x.idLogin);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBLOGIN");
        }
    }
}
