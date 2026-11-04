using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionManager.API.Models
{
    public class ScryfallAutocompleteResponse
    {
        public string? Object { get; set; }
        public int Total_Values { get; set; }
        public List<string> Data { get; set; } = [];
    }
}
