using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;

namespace MVCProjectTotalizator.Models
{
    public class EventViewModel
    {
        public SportEvent SportEvent { get; set; }
        public List<Team> Teams { get; set; }
    }
}