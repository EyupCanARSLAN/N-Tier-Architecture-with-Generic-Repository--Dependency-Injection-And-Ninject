using Sistem.Core.Data.OrderK;
using System;
namespace Sistem.Core.Data.CritiqK
{
    public class CritiqOrder
    {
        public int CritiqOrderId { get; set; }
        public String CritiqMessage { get; set; }
        public int OrderId { get; set; }
        public  Order Order { get; set; }
        public Boolean IsSeen { get; set; }
        public String Answer { get; set; }
    }
}
