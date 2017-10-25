using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Sistem.Web.Models.ViewModel.Managements.MenuSection
{
    public class MenuProcessModel
    {
        [Required]
        public string MenuName { get; set; }
        [Required]
        [Column(TypeName = "Money")]
        public decimal Price { get; set; }
    }
}