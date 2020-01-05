using FoodMVC.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FoodMVC.Controllers
{
    public class GlobalPropController : Controller
    {
        private readonly FoodDatabaseEntities10 ldb = new FoodDatabaseEntities10();

        public ActionResult LogModProp(GlobalProp globalProp)
        {
            var lastLogs = ldb.LogRegs.Select(v => v.LastLog).ToList();

            for (int i = 0; i < lastLogs.Count - 1; i++)
            {
                if ((DateTime)lastLogs[i] < (DateTime)lastLogs[i + 1])
                {
                    globalProp.LastLogin = (DateTime)lastLogs[i + 1];
                }
            }

            var lastMods = ldb.LogRegs.Select(v => v.LastMod).ToList();

            for (int i = 0; i < lastMods.Count - 1; i++)
            {
                if ((DateTime)lastMods[i] < (DateTime)lastMods[i + 1])
                {
                    globalProp.LastMod = (DateTime)lastMods[i + 1];
                }
            }


            return PartialView("_LogModProp", globalProp);
        }
    }
}