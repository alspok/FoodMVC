using FoodMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FoodMVC.Helpers
{
    public class ModTime
    {
        private readonly FoodDatabaseEntities10 ldb = new FoodDatabaseEntities10();
        public void ModTimeChange(int userId)
        {
            var rowToUpdate = ldb.LogRegs.Find(userId);
            rowToUpdate.LastMod = DateTime.Now;
            ldb.Entry(rowToUpdate).State = EntityState.Modified;
            ldb.SaveChanges();
            ldb.Dispose();
        }
    }
}