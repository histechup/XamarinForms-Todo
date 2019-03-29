using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace XFTodo.Models
{
    public class TodoItem : BindableObject
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        private string _name;

        [JsonProperty("name")]
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        [JsonProperty("isComplete")]
        public bool IsComplete { get; set; }
    }
}
