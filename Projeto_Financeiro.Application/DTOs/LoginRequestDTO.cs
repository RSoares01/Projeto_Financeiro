using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Financeiro.Application.DTOs
{
    public class LoginRequestDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = "ADMIN";
    }
}
