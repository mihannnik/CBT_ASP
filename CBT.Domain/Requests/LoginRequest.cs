﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBT.Domain.Requests
{
    public class LoginRequest
    {
        public required string Identity {  get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
