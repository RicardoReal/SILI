using SILI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SILI
{
    public partial class ErrorLog
    {
        public static void LogError(Exception ex, string module)
        {
            using (SILI_DBEntities db = new SILI_DBEntities())
            {
                ErrorLog error = new ErrorLog();

                error.ErrorMessage = ex.Message;
                error.InnerException = ex.InnerException == null ? "" : ex.InnerException.ToString();
                error.StackTrace = ex.StackTrace;
                error.Module = module;
                error.Instant = DateTime.Now;

                db.ErrorLog.Add(error);
                db.SaveChanges();
            }
        }

        public static void LogError(string errorMessage, string innerExceptipn, string stackTrace, string module)
        {
            using (SILI_DBEntities db = new SILI_DBEntities())
            {
                ErrorLog error = new ErrorLog();

                error.ErrorMessage = errorMessage;
                error.InnerException = innerExceptipn;
                error.StackTrace = stackTrace;
                error.Module = module;
                error.Instant = DateTime.Now;

                db.ErrorLog.Add(error);
                db.SaveChanges();
            }
        }
    }
    
}