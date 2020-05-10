﻿using System;

namespace MainMusicStore.Utility
{
    public static class ProjectConstant
    {
        public const string ResultNotFound = "Data Not Found // Bulunamadi.";


        // ----------------- //
        public const string Proc_CoverType_GetAll = "usp_GetCoverTypes";
        public const string Proc_CoverType_Get = "usp_GetCoverType";
        public const string Proc_CoverType_Delete = "usp_DeleteCoverType";
        public const string Proc_CoverType_Create = "usp_CreateCoverType";
        public const string Proc_CoverType_Update = "usp_UpdateCoverType";

        // ----------------- //

        public const string Role_User_Indi = "Individual Customer";
        public const string Role_User_Comp = "Company Customer";
        public const string Role_Admin = "Admin";
        public const string Role_Employee = "Employee";

        // ----------------- //

        public const string shoppingCart = "ShoppingCart";


        // ----------------- Status //
        public const string PaymentStatusPending = "Pending";
        public const string PaymentStatusRejected = "Rejected";
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusDelayed = "Delayed";
        // ----------------- Status //


        // ----------------- Order Status //
        public const string StatusPending = "Pending";
        public const string StatusApproved = "Approved";
        // ----------------- Order Status //






        public static double GetPriceBaseOnQuantity(int quantity, double price, double price50, double price100)
        {
            if(quantity < 50)
            {
                return price;
            }
            else if (quantity < 100)
            {
                return price50;
            }
            else
            {
                return price100;
            }
        }

        public static string ConvertToRawHtml(string description)
        {
            char[] array = new char[description.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < description.Length; i++)
            {
                char let = description[i];
                if(let == '<')
                {
                    inside = true;
                    continue;
                }
                if(let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }

            }
            return new string(array, 0,arrayIndex);
        }


    }
}
