using HRIS.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRIS.Api.Services
{
    public class RequestService
    {
        public static void InsertRequest(Request inputRequest, DBModel db)
        {
            /*Get and Set latest RequestID from the Database*/
            inputRequest.RequestID = GetLatestRequestID(db);
            db.Requests.Add(inputRequest);
        }

        private static string GetLatestRequestID(DBModel db)
        {
            var newRequestID = string.Empty;
            var getCurrentYYMM = DateTime.Now.ToString("yyyyMM");
            if (!db.Requests.ToList().Any())
            {
                return string.Concat(getCurrentYYMM, "0000001");
            }

            var result = db.Requests.OrderByDescending(data => data.RequestID).ToList().First();
            var latestRequestID = result.RequestID;
            var latestYYMM = latestRequestID.Substring(0, 6);

            if (latestYYMM != getCurrentYYMM)
            {
                return string.Concat(getCurrentYYMM, "0000001");
            }

            var getLatestNo = int.Parse(latestRequestID.Substring(5, latestRequestID.Length - 6)) + 1;

            return string.Concat(getCurrentYYMM, getLatestNo.ToString().PadLeft(7));
        }

    }
}