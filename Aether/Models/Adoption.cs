using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aether.Models
{
    public class Adoption
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Data de criação obrigatório.")]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Status obrigatório.")]
        public int AdoptionStatusId { get; set; }

        [Required(ErrorMessage = "Animal obrigatório.")]
        public int AnimalId { get; set; }

        [Required(ErrorMessage = "Adotante obrigatório.")]
        public int UserId { get; set; }
    }
}