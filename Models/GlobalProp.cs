using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodMVC.Models
{
    public class GlobalProp
    {
        public int CurrentUserId { get; set; }
        public string CurrentUserEmail { get; set; }
        public List<string> UsersEmailList { get; set; }
        public DateTime CuruentUserLastLog { get; set; }
        public DateTime CurrentUserLastMod { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime LastMod { get; set; }
        public string CurrentUserRole { get; set; }
    }
}