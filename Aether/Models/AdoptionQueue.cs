using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aether.Models
{
    [Table("adoption_queue")]
    public class AdoptionQueue
    { 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [ForeignKey("animal_id")]
        [Column("animal_id")]
        [Required(ErrorMessage = "Animal obrigatório.")]
        public int AnimalId { get; set; }

        [ForeignKey("user_id")]
        [Column("user_id")]
        [Required(ErrorMessage = "Adotante obrigatório.")]
        public int UserId { get; set; }
    }
}
