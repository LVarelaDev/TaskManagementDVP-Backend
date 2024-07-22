using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Entities;
using TaskManagement.Models.Models.DTO;

namespace TaskManagement.Models.Models.Responses
{
    public record LoginResponse
    {
        public string Token { get; set; } = null!;
        public UserDto user { get; set; } = null!;
    }
}
