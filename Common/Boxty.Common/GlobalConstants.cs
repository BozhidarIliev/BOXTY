namespace Boxty.Common
{
    public class GlobalConstants
    {
        public const string Error = "Error";

        public const string ModelError = "LoginError";

        public const string LoginError = "Nickname or password don't match!";

        public const string UserNameNotUnique = "The Username you've chosen is already in use.";

        public const string DefaultRole = "user";

        public const string Admin = "admin";

        public const string Manager = "manager";

        public const string Employee = "employee";

        public const string Waiter = "waiter";

        public const string Driver = "driver";

        public const string KitchenStaff = "kitchenStaff";

        public const string Yes = "yes";

        public const string No = "no";

        public const string Sent = "sent";

        public const string Open = "open";

        public const string Delivering = "delivering";

        public const string Delivered = "delivered";

        public const string TableAvailable = "available";

        public const string TableUnavailable = "unavailable";

        public const string Ready = "ready";

        public const string All = "all";

        public const string Completed = "completed"; 
        
        public const string Deleted = "deleted";

        public const string OrderCompleted = "orderCompleted";

        public const string ShoppingCart = "shoppingCart";

        public const string DriverControllerAuthorizeRoles = "driver,manager,admin";

        public const string DriverArea = "Driver";

        public const string KitchenStaffArea = "kitchenStaff";

        public const string DefaultApiRoute = "api/[controller]";

        public const double MaxPriceValue = 9999.99;

        public const int RestaurantDailyWorkingHours = 16;
    }
}
