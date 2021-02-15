using ExistingDb.Models.Scaffold;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExistingDb.Models.Manual
{
    [Table("Shoes")]
    public class Shoe
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        [Column("ColorId")]
        public long StyleId { get; set; }

        public Style Style { get; set; }

        public long WidthId { get; set; }

        public ShoeWidth Width { get; set; }

        public SalesCampaign Campaign { get; set; }

        public IEnumerable<ShoeCategoryJunction> Categories { get; set; }
    }
}
