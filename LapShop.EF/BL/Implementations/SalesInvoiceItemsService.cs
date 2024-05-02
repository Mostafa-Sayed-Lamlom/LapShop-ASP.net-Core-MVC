using LapShop.Domains.Models;
using LapShop.EF.BL.Interfaces;
using LapShop.EF.DBContext;
using Microsoft.EntityFrameworkCore;

namespace LapShop.EF.BL.Implementations
{
    public class SalesInvoiceItemsService : ISalesInvoiceItemsService
    {
        private readonly LapShopContext _ctx;
        public SalesInvoiceItemsService(LapShopContext context)
        {
            _ctx = context;
        }

        public List<TbSalesInvoiceItem> GetSalesInvoiceId(int id)
        {
            try
            {
                var Items = _ctx.TbSalesInvoiceItems.Where(a => a.InvoiceId == id).ToList();
                if (Items == null)
                    return new List<TbSalesInvoiceItem>();
                else
                    return Items;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public bool Save(IList<TbSalesInvoiceItem> Items, int salesInvoiceId, bool isNew)
        {
            List<TbSalesInvoiceItem> dbInvoiceItems =
                GetSalesInvoiceId(Items[0].InvoiceId);

            foreach (var interfaceItems in Items)
            {
                var dbObject = dbInvoiceItems.Where(a => a.InvoiceItemId == interfaceItems.InvoiceItemId).FirstOrDefault();
                if (dbObject != null)
                {
                    _ctx.Entry(dbObject).State = EntityState.Modified;
                }

                else
                {
                    interfaceItems.InvoiceId = salesInvoiceId;
                    _ctx.TbSalesInvoiceItems.Add(interfaceItems);
                }
            }

            foreach (var item in dbInvoiceItems)
            {
                var interfaceObject = Items.Where(a => a.InvoiceItemId == item.InvoiceItemId).FirstOrDefault();
                if (interfaceObject == null)
                    _ctx.TbSalesInvoiceItems.Remove(item);
            }

            _ctx.SaveChanges();
            return true;
        }

    }
}
