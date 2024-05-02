using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LapShop.Domains.Models
{
    public partial class TbItem
    {
        public TbItem()
        {
            TbItemDiscounts = new HashSet<TbItemDiscount>();
            TbItemImages = new HashSet<TbItemImage>();
            TbPurchaseInvoiceItems = new HashSet<TbPurchaseInvoiceItem>();
            TbSalesInvoiceItems = new HashSet<TbSalesInvoiceItem>();
            Customers = new HashSet<TbCustomer>();
        }

        [ValidateNever]
        public int ItemId { get; set; }
        [Required(ErrorMessage = "Please enter item name")]
        public string ItemName { get; set; } = null!;
        [Required(ErrorMessage = "Please enter Sales price")]
        [DataType(DataType.Currency, ErrorMessage = "please enter currency")]
        [Range(50, 100000, ErrorMessage = "pelase enter price in system range")]
        public decimal SalesPrice { get; set; }
        [Required(ErrorMessage = "Please enter Purchase price")]
        [DataType(DataType.Currency, ErrorMessage = "please enter currency")]
        [Range(50, 100000, ErrorMessage = "pelase enter price in system range")]
        public decimal PurchasePrice { get; set; }
        [Required(ErrorMessage = "Please enter category")]
        public int CategoryId { get; set; }
        public string? ImageName { get; set; }
        [ValidateNever]
        public DateTime CreatedDate { get; set; }
        [ValidateNever]
        public string CreatedBy { get; set; } = null!;
        [ValidateNever]
        public int CurrentState { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? Description { get; set; }

        public string? Gpu { get; set; }
        public string? HardDisk { get; set; }
        [Required(ErrorMessage = "Please enter item type")]
        public int? ItemTypeId { get; set; }
        public string? Processor { get; set; }
        [Required(ErrorMessage = "Please enter ram size")]
        [Range(1, 500, ErrorMessage = "please enter ram in ragne")]
        public int? RamSize { get; set; }
        public string? ScreenReslution { get; set; }
        public string? ScreenSize { get; set; }
        public string? Weight { get; set; }
        [Required(ErrorMessage = "Please enter os")]
        public int? OsId { get; set; }

        [ValidateNever]
        public virtual TbCategory Category { get; set; } = null!;
        [ValidateNever]
        public virtual TbItemType? ItemType { get; set; }
        [ValidateNever]
        public virtual TbO? Os { get; set; }
        public virtual ICollection<TbItemDiscount> TbItemDiscounts { get; set; }
        public virtual ICollection<TbItemImage> TbItemImages { get; set; }
        public virtual ICollection<TbPurchaseInvoiceItem> TbPurchaseInvoiceItems { get; set; }
        public virtual ICollection<TbSalesInvoiceItem> TbSalesInvoiceItems { get; set; }

        public virtual ICollection<TbCustomer> Customers { get; set; }
    }
}
