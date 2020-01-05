using System;
using System.Data.Entity;
using FoodMVC.Models;

namespace FoodMVC.Helpers
{
    public class LogModTime
    {
        private readonly FoodDatabaseEntities10 ldb = new FoodDatabaseEntities10();
        public void LogTimeChange(int userId)
        {
            var rowToUpdate = ldb.LogRegs.Find(userId);
            rowToUpdate.LastLog = DateTime.Now;
            ldb.Entry(rowToUpdate).State = EntityState.Modified;
            ldb.SaveChanges();
            ldb.Dispose();
        }

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