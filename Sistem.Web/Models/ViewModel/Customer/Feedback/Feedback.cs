using System;
namespace Sistem.Web.Models.ViewModel.Customer.Feedback
{
    public class Feedback
    {
        public int criticMenuID { get; set; }
        public String CustomerId { get; set; }
        public String CritiqText { get; set; }
        //Bu eleştiri bir menuye ait... veya genel bir konuya...
        public String Subject { get; set; }
        //Okunan ve Okunmayan eleştiriler; Yonetim Tarafından...
        public Boolean IsSeen { get; set; }
        //Bu eleştriye verilen cevap...
        public String Answer { get; set; }
        public DateTime Date { get; set; }
    }
}