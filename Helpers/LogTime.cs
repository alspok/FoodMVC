using System;
using System.Data.Entity;
using FoodMVC.Models;

namespace FoodMVC.Helpers
{
    public class LogTime
    {
        private readonly FoodDatabaseEntities10 ldb = new FoodDatabaseEntities10();
        public void LogTimeChange(int userId)
        {
            LogReg rowToUpdate = ldb.LogRegs.Find(userId);
            rowToUpdate.LastLog = DateTime.Now;
            ldb.Entry(rowToUpdate).State = EntityState.Modified;
            ldb.SaveChanges();
            ldb.Dispose();
        }

       
    }
}