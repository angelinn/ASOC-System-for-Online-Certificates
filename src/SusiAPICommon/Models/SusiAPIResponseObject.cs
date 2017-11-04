using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SusiAPI.Common.Models
{

    public enum SusiAPIResponseCode
    {
        MissingNo = -1,
        Success,
        InvalidCredentials
    }

    public class SusiAPIResponseObject
    {
        public SusiAPIResponseCode ResponseCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public T Parse<T>()
        {
            return JsonConvert.DeserializeObject<T>(Data.ToString());
        }
    }
}
