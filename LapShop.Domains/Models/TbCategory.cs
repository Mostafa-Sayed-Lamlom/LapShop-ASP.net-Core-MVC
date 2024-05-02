using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LapShop.Domains.Models
{
    public partial class TbCategory
    {
        public TbCategory()
        {
            TbItems = new HashSet<TbItem>();
            ShowInHomePage = false;
        }

        public int CategoryId { get; set; }
        [Required(ErrorMessage = "please enter category name")]
        public string CategoryName { get; set; } = null!;
        [ValidateNever]
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public int CurrentState { get; set; }
        [ValidateNever]
        public string ImageName { get; set; } = "";
        public bool ShowInHomePage { get; set; } = false;
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<TbItem> TbItems { get; set; }
    }
}
