using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Business.Models.Payload
{
    public record LoginPayload
    {
        public string Value { get; set; } = null!;
    }
}
