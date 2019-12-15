using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodMVC.Models
{
    public static class GlobalProp
    {
        public static int CurrentUserId { get; set; }
        public static string CurrentUserEmail { get; set; }
        public static List<string> CurrentUsersEmailList { get; set; }
        public static DateTime CuruentUserLastLog { get; set; }
        public static DateTime CurrentUserLastMod { get; set; }
        public static DateTime LastLogin { get; set; }
        public static string CurrentUserRole { get; set; }
    }
}