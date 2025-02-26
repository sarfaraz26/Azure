using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.CRUD.DTOs
{
    public class QueueDTO
    {
        [JsonProperty("payLoad")]
        public dynamic PayLoad { get; set; }

        [JsonProperty("operation")]
        public string Operation { get; set; }
    }
}
