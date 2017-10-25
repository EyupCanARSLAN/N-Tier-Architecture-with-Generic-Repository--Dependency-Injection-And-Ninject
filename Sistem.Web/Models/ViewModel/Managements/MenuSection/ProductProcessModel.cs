using System.ComponentModel.DataAnnotations;
namespace Sistem.Web.Models.ViewModel.Managements.MenuSection
{
    public class ProductProcessModel
    {
        [Required]
        public string ProductName { get; set; }
    }
}