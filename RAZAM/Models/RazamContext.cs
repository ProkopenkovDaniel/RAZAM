﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace RAZAM.Models
{
    public class RazamContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
    }
}