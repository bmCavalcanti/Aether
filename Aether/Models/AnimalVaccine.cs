using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aether.Models
{
    [Table("animal_vaccine")]
    public class AnimalVaccine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("vaccine_date")]
        [Required(ErrorMessage = "Data da vacinação obrigatório.")]
        public DateTime VaccineDate { get; set; }

        [ForeignKey("vaccine_id")]
        [Column("vaccine_id")]
        [Required(ErrorMessage = "Vacina obrigatória.")]
        public int VaccineId { get; set; }

        [ForeignKey("animal_id")]
        [Column("animal_id")]
        [Required(ErrorMessage = "Animal obrigatório.")]
        public int AnimalId { get; set; }
    }
}
