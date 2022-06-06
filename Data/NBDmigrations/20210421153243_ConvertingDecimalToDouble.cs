using Microsoft.EntityFrameworkCore.Migrations;

namespace NbdAplication.Data.NBDmigrations
{
    public partial class ConvertingDecimalToDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Designer_DesignerID",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Nbdstaffs_NbdstaffID",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Occupations_OccupationID",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Sale_SaleID",
                table: "Bids");

            migrationBuilder.AlterColumn<int>(
                name: "SaleID",
                table: "Bids",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OccupationID",
                table: "Bids",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NbdstaffID",
                table: "Bids",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "EstAmount",
                table: "Bids",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DesignerID",
                table: "Bids",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "ActAmount",
                table: "Bids",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Designer_DesignerID",
                table: "Bids",
                column: "DesignerID",
                principalTable: "Designer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Nbdstaffs_NbdstaffID",
                table: "Bids",
                column: "NbdstaffID",
                principalTable: "Nbdstaffs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Occupations_OccupationID",
                table: "Bids",
                column: "OccupationID",
                principalTable: "Occupations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Sale_SaleID",
                table: "Bids",
                column: "SaleID",
                principalTable: "Sale",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Designer_DesignerID",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Nbdstaffs_NbdstaffID",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Occupations_OccupationID",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Sale_SaleID",
                table: "Bids");

            migrationBuilder.AlterColumn<int>(
                name: "SaleID",
                table: "Bids",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "OccupationID",
                table: "Bids",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "NbdstaffID",
                table: "Bids",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "EstAmount",
                table: "Bids",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "DesignerID",
                table: "Bids",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "ActAmount",
                table: "Bids",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Designer_DesignerID",
                table: "Bids",
                column: "DesignerID",
                principalTable: "Designer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Nbdstaffs_NbdstaffID",
                table: "Bids",
                column: "NbdstaffID",
                principalTable: "Nbdstaffs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Occupations_OccupationID",
                table: "Bids",
                column: "OccupationID",
                principalTable: "Occupations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Sale_SaleID",
                table: "Bids",
                column: "SaleID",
                principalTable: "Sale",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
