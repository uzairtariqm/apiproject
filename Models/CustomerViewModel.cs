﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_project.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email{ get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

    }
}