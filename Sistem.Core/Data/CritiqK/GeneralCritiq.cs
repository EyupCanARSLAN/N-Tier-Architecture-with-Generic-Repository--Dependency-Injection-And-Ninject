using System;
namespace Sistem.Core.Data.CritiqK
{
    public class GeneralCritiq
    {
        public int GeneralCritiqId { get; set; }
        public String CritiqText { get; set; }
        public String CustomerId { get; set; }
        public Boolean IsSeen { get; set; }
        public String AnswerText { get; set; }
        public DateTime Date { get; set; }
        public Boolean IsPublished { get; set; }
    }
}
