using FoodMVC.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FoodMVC.Controllers
{
    public class TimeController : Controller
    {
        private readonly FoodDatabaseEntities10 ldb = new FoodDatabaseEntities10();

        //public ActionResult LogModTime(GlobalProp globalProp)
        public ActionResult LogModTime(LogReg logReg)
        {
            var lastLogs = ldb.LogRegs.Select(v => v.LastLog).ToList();

            var sortedLogDates = lastLogs.OrderByDescending(x => x).ToList();
            logReg.LastLog = sortedLogDates[0].Value.AddHours(2);

            var lastMods = ldb.LogRegs.Select(v => v.LastMod).ToList();

            var sortedModDates = lastMods.OrderByDescending(x => x).ToList();
            logReg.LastMod = sortedModDates[0].Value.AddHours(2);

            return PartialView("_LogModTime", logReg);
        }
    }
}