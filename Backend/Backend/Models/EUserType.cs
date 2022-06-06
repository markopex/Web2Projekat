using System;

namespace Backend.Models
{
    public enum EUserType
    {
         ADMIN, DELIVERER, CUSTOMER
    };

    static class UserType
    {

        public static String GetString(this EUserType userType)
        {
            switch (userType)
            {
                case EUserType.ADMIN:
                    return "ADMIN";
                case EUserType.DELIVERER:
                    return "DELIVERER";
                case EUserType.CUSTOMER:
                    return "CUSTOMER";
                default:
                    return "";
            }
        }
    }
}