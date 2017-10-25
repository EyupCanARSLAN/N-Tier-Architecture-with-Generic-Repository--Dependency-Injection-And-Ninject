using Sistem.Core.Data.MenuK;
using System;
namespace Sistem.Core.Data.CritiqK
{
    public class MenuCritiq
    {
        public int CritiqMenuId { get; set; }
        public String CustomerId { get; set; }
        public String CritiqMessage { get; set; }
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
        public DateTime Date { get; set; }
        public Boolean IsSeen { get; set; }
        public String Answer { get; set; }
        //Bu eleştiriin yayımlanıp / yaymlanmayacağı
        public Boolean IsPublished { get; set; }
    }
}
