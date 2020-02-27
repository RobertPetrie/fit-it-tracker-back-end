using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fix_it_tracker_back_end.Helpers
{
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("fix-it-tracker-back-end-error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "fix-it-tracker-back-end-error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}
