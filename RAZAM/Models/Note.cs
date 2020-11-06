﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace RAZAM.Models
{
    public enum State
    {
        unread,
        deflected,
        executed,
        accepted
    }
    public class Note
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        //[InverseProperty("SendedNotes")]
        //public RazamUser Sender { get; set; }
        public string SenderId { get; set; }
        //[InverseProperty("ReceivedNotes")]
        //public RazamUser Receiver { get; set; }
        public string ReceiverId { get; set; }
        public State Status { get; set; }
    }
}