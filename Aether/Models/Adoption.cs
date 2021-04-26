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

        [Column("created_at")]
        [Required(ErrorMessage = "Data de criação obrigatório.")]
        public DateTime CreatedAt { get; set; }

        [ForeignKey("adoption_status_id")]
        [Required(ErrorMessage = "Status obrigatório.")]
        public int AdoptionStatusId { get; set; }

        [ForeignKey("animal_id")]
        [Required(ErrorMessage = "Animal obrigatório.")]
        public int AnimalId { get; set; }

        [ForeignKey("user_id")]
        [Required(ErrorMessage = "Adotante obrigatório.")]
        public int UserId { get; set; }
    }
}
