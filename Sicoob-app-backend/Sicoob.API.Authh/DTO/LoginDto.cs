﻿using System.ComponentModel.DataAnnotations;

namespace Sicoob.API.Authh.DTO
{
    public class LoginDto
    {
        [Required(ErrorMessage = "O campo email é obrigatório!"), EmailAddress(ErrorMessage = "Email Inválido!")]
        public string email { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório!")]
        public string senha { get; set; }  
    }
}
