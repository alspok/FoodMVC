using FoodMVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace FoodMVC.Helpers
{
    public class EmailList
    {
        private readonly FoodDatabaseEntities10 ldb = new FoodDatabaseEntities10();
        public List<string> Email()
        {
            //SendMailModel sendMailModel = new SendMailModel();
            //sendMailModel.EmailToList = ldb.LogRegs.Select(u => u.Email).ToList();
            var emailToList = ldb.LogRegs.Select(u => u.Email).ToList();
            return emailToList;
        }
    }
}