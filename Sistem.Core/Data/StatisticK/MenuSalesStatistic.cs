using System;
using Sistem.Core.Data.MenuK;
namespace Sistem.Core.Data.StatisticK
{
    /// <summary>
    /// This class purpose is that create statistic about on sales like daily, mounthly, yearly.
    /// For these pupose at there a UniqueIndex key created at there MenuId, SalesMonth, SalesYear
    /// </summary>
    public class MenuSalesStatistic
    {
        public int MenuSalesStatisticId { get; set; }
        public int MenuId { get; set; }
        public  Menu Menu { get; set; }
        public int SalesCount { get; set; }
        public int Vote { get; set; }
        public int CustomerCountThatGiveVote { get; set; }
        public String MenuContent { get; set;}
        public decimal SalesUniquePrice { get; set; }
        //[Index("IX_FirstAndSecond", 2, IsUnique = true)]
        public int SalesMonth { get; set; }
        //[Index("IX_FirstAndSecond", 2, IsUnique = true)]
        public int SalesYear { get; set; }
    }
}
