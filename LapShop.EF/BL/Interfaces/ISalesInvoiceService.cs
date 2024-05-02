using LapShop.Domains.Models;

namespace LapShop.EF.BL.Interfaces
{
    public interface ISalesInvoiceService
    {
        public List<VwSalesInvoice> GetAll();
        public List<VwOrderDetails> GetAllOrderDetails(Guid userId);

        public TbSalesInvoice GetById(int id);

        public bool Save(TbSalesInvoice Item, List<TbSalesInvoiceItem> lstItems, bool isNew);

        public bool Delete(int id);
        public bool DeleteSalesInvoiceItemWithSalesInvoice(int invoiceItemId, int invoiceId);
    }
}
