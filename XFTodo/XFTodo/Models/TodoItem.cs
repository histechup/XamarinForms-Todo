using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace XFTodo.Models
{
    public class TodoItem
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isComplete")]
        public bool IsComplete { get; set; }
    }
}
