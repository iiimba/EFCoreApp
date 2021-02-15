using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExistingDb.Models.Manual
{
    [Table("Colors")]
    public class Style
    {
        [Key]
        [Column("Id")]
        public long UniqueIdent { get; set; }

        [Column("Name")]
        public string StyleName { get; set; }

        public string MainColor { get; set; }

        public string HighlightColor { get; set; }

        public IEnumerable<Shoe> Products { get; set; }
    }
}
