namespace Sistem.Service.SystemHelpers
{
    public class Enums
    {
        public enum FeedBackType
        {
            None,
            General,
            Menu,
            Order
        }
        public enum StatusCode
        {
            NotSet,
            WaitPhoneCodeAccept,
            WaitForProgressFromRst,
            Complated,
            CancelledByRestaurant       
        }
    }
}