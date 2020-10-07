﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAZAM.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime DateTime { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
    }
}