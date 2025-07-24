using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class Constant
    {
        public const string SeatStatus_Available = "Available";
        public const string SeatStatus_Reserved = "Reserved";
        public const string SeatStatus_Booked = "Booked";

        public const string OrderStatus_Pending = "Pending";
        public const string OrderStatus_Paid = "Paid";
        public const string OrderStatus_Cancelled = "Cancelled";

        public const string TransactionStatus_Pending = "Pending";
        public const string TransactionStatus_Completed = "Completed";
        public const string TransactionStatus_Failed = "Failed";

        public const string UserRole_Admin = "Admin";
        public const string UserRole_Manager = "Manager";
        public const string UserRole_Staff = "Staff";
        public const string UserRole_Customer = "Customer";

        public const string PaymentMethod_Online = "Online";
        public const string PaymentMethod_Offline = "Offline";

        // Authorization Policies
        public const string AuthPolicy_AdminOnly = "AdminOnly";
        public const string AuthPolicy_ManagerOnly = "ManagerOnly";
        public const string AuthPolicy_StaffOnly = "StaffOnly";
        public const string AuthPolicy_CustomerOnly = "CustomerOnly";
        public const string AuthPolicy_ManagerAndAbove = "ManagerAndAbove";
        public const string AuthPolicy_StaffAndAbove = "StaffAndAbove";

    }
}
