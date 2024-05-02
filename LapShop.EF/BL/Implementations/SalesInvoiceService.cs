using LapShop.Domains.Models;
using LapShop.EF.BL.Interfaces;
using LapShop.EF.DBContext;
using Microsoft.EntityFrameworkCore;

namespace LapShop.EF.BL.Implementations
{
    public class SalesInvoiceService : ISalesInvoiceService
    {
        private readonly LapShopContext _ctx;
        private readonly ISalesInvoiceItemsService _salesInvoiceItemsService;
        public SalesInvoiceService(LapShopContext context,
                                   ISalesInvoiceItemsService salesInvoiceItemsService)
        {
            _ctx = context;
            _salesInvoiceItemsService = salesInvoiceItemsService;
        }
        public List<VwSalesInvoice> GetAll()
        {
            try
            {
                return _ctx.VwSalesInvoices.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public TbSalesInvoice GetById(int id)
        {
            try
            {
                var Item = _ctx.TbSalesInvoices.Where(a => a.InvoiceId == id).FirstOrDefault();
                if (Item == null)
                    return new TbSalesInvoice();
                else
                    return Item;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public bool Save(TbSalesInvoice Item, List<TbSalesInvoiceItem> lstItems, bool isNew)
        {
            using var transaction = _ctx.Database.BeginTransaction();
            try
            {
                Item.CurrentState = 1;
                if (isNew)
                {
                    Item.CreatedBy = "1";
                    Item.CreatedDate = DateTime.Now;
                    _ctx.TbSalesInvoices.Add(Item);
                }

                else
                {
                    Item.UpdatedBy = "1";
                    Item.UpdatedDate = DateTime.Now;
                    _ctx.Entry(Item).State = EntityState.Modified;
                }

                _ctx.SaveChanges();
                _salesInvoiceItemsService.Save(lstItems, Item.InvoiceId, true);

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception(ex.ToString());
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var Item = _ctx.TbSalesInvoices.Where(a => a.InvoiceId == id).FirstOrDefault();
                if (Item != null)
                {
                    _ctx.TbSalesInvoices.Remove(Item);
                    _ctx.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public List<VwOrderDetails> GetAllOrderDetails(Guid userId)
        {
            return _ctx.VwOrderDetails.Where(o => o.CustomerId == userId).ToList();
        }

        public bool DeleteSalesInvoiceItemWithSalesInvoice(int invoiceItemId, int invoiceId)
        {
            var isDeleted = false;
            var item = _ctx.TbSalesInvoiceItems.Where(x => x.InvoiceItemId == invoiceItemId).FirstOrDefault();
            if (item is null)
                return isDeleted;
            _ctx.Remove(item);
            var effectedRows = _ctx.SaveChanges();
            if (effectedRows > 0)
            {
                isDeleted = true;
                var IsItemsinInvoiceExisted = _salesInvoiceItemsService.GetSalesInvoiceId(invoiceId).Count() > 0;
                bool isInvoiceDeteted = false;
                if (!IsItemsinInvoiceExisted)
                    isInvoiceDeteted = Delete(invoiceId);
            }

            return isDeleted;
        }
    }
}
