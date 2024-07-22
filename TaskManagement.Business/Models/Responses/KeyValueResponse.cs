using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Business.Models.Responses
{
    public class KeyValueResponse
    {
        public int Key {  get; set; }
        public string Value { get; set; } = null!;
    }
}
