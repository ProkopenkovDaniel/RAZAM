using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public RazamUser Sender { get; set; }
        public RazamUser Receiver { get; set; }
        public State Status { get; set; }
    }
}