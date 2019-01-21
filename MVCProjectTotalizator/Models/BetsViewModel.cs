using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MVCProjectTotalizator.Models
{
    public class BetsViewModel
    {
        public List<BetViewModel> Bets { get; set; }
        public SportEvent SportEvent { get; set; }
        public int SportEventId { get; set; }
        public int RateId { get; set; }
    }
}