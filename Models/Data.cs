using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDataRecognizer.Models
{
    public class Data
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// When the picture is observed.
        /// </summary>
        public DateTime? Date { get; set; }

        [Column(TypeName = "VARCHAR(100)")]
        public string Brand { get; set; }
    }
}
