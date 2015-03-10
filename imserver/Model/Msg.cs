using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Msg
    {
        public Msg(string from, string to, string content)
        {
            From = from;
            To = to;
            Content = content;
            Id = Guid.NewGuid();
            Time = DateTime.Now;
        }

        public Guid Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Content { get; set; }
        public DateTime Time { get; set; }
    }
}
