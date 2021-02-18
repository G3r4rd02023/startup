using startup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace startup.Helpers
{
    public class DBHelper
    {
        public static Response SaveChanges(StartupContext db)
        {
            try
            {
                db.SaveChanges();
                return new Response { Succedeed = true, };
            }
            catch (Exception ex)
            {
                var response = new Response { Succedeed = false, };
                if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("_Index"))
                {
                    response.Message = "Ya existe un registro con ese nombre";
                }
                else if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                {
                    response.Message = "El registro no puede ser borrado porque tiene otros registros relacionados";
                }
                else
                {
                    response.Message = ex.Message;
                }

                return response;
            }
        }
    }
}