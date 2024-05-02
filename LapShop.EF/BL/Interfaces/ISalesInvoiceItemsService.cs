using LapShop.Domains.Models;

namespace LapShop.EF.BL.Interfaces
{
    public interface ISalesInvoiceItemsService
    {
        public List<TbSalesInvoiceItem> GetSalesInvoiceId(int id);

        public bool Save(IList<TbSalesInvoiceItem> Items, int salesInvoiceId, bool isNew);
    }
}
