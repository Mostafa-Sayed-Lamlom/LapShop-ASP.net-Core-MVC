using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LapShop.EF.Migrations
{
    public partial class addVwOrderDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"create view VwOrderDetails
            as
            SELECT dbo.TbSalesInvoices.InvoiceDate, dbo.TbSalesInvoices.DelivryDate, dbo.TbSalesInvoices.CustomerId, dbo.TbSalesInvoices.InvoiceId, dbo.TbSalesInvoiceItems.Qty, dbo.TbSalesInvoiceItems.InvoicePrice, dbo.TbItems.ItemName, dbo.TbItems.ItemId, dbo.TbItems.ImageName, 
             dbo.TbSalesInvoiceItems.InvoiceItemId
             FROM   dbo.TbSalesInvoices INNER JOIN
             dbo.TbSalesInvoiceItems ON dbo.TbSalesInvoices.InvoiceId = dbo.TbSalesInvoiceItems.InvoiceId INNER JOIN
             dbo.TbItems ON dbo.TbSalesInvoiceItems.ItemId = dbo.TbItems.ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
