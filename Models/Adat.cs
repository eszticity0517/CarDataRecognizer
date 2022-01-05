using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDataRecognizer.Models
{
    public class Adat
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int KameraId { get; set; }

        /// <summary>
        /// When the picture is observed.
        /// </summary>
        public DateTime? Date { get; set; }

        [Column(TypeName = "VARCHAR(100)")]
        public string PlateNumber { get; set; }

        [Column(TypeName = "VARCHAR(100)")]
        public string Color { get; set; }

        /// <summary>
        ///Vehicle type (p.es. bus vehicle).
        /// </summary>
        [Column(TypeName = "VARCHAR(100)")]
        public string Type { get; set; }

        [Column(TypeName = "VARCHAR(100)")]
        public string Brand { get; set; }

        /// <summary>
        /// How accurate the provided data is.
        /// </summary>
        [Column(TypeName = "VARCHAR(100)")]
        public string Validity { get; set; }
    }
}
