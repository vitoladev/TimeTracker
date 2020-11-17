using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTracker.Models
{
    public class UpdateTime
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("seconds")]
        public double Time { get; set; }

    }
}
