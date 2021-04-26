using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aether.Models
{
    public class AnimalVaccine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Data da vacinação obrigatório.")]
        public DateTime VaccineDate { get; set; }

        [Required(ErrorMessage = "Vacina obrigatória.")]
        public int VaccineId { get; set; }

        [Required(ErrorMessage = "Animal obrigatório.")]
        public int AnimalId { get; set; }
    }
}