using System;
using System.Collections.Generic;

namespace ExistingDb.Models.Scaffold
{
    public partial class SalesCampaigns
    {
        public long Id { get; set; }
        public string Slogan { get; set; }
        public int? MaxDiscount { get; set; }
        public DateTime? LaunchDate { get; set; }
        public long Shoeld { get; set; }

        public virtual Shoes ShoeldNavigation { get; set; }
    }
}
