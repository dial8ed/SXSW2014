using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace SxswFeed
{
    public static class Dynamic
    {
        public static object ToJson(dynamic target)
        {
            return JsonConvert.SerializeObject(target);
        }
    }
}