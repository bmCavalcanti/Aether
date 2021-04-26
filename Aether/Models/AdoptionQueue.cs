using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aether.Models
{
    public class AdoptionQueue
    { 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Animal obrigatório.")]
        public int AnimalId { get; set; }

        [Required(ErrorMessage = "Adotante obrigatório.")]
        public int UserId { get; set; }
    }
}