﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{
    public class JwtUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Role { get; set; }
    }
}
