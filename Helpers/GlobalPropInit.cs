using FoodMVC.Models;
using System.Data;
using System;
using System.Linq;
using System.Collections.Generic;

namespace FoodMVC.Helpers
{
    public class GlobalPropInit
    {
        private FoodDatabaseEntities10 db = new FoodDatabaseEntities10();
        
        public DateTime PropInit()
        {
            var lastLogs = db.LogRegs.Select(v => v.LastLog).ToList();

            for (int i = 0; i < lastLogs.Count - 1; i++)
            {
                if ((DateTime)lastLogs[i] < (DateTime)lastLogs[i + 1])
                {
                    GlobalProp.LastLogin = (DateTime) lastLogs[i + 1];
                }
            }

            return GlobalProp.LastLogin;
        }

        public void EmailPropInit()
        {
            GlobalProp.CurrentUsersEmailList = db.LogRegs.Select(v => v.Email).ToList();
        }
    }
}