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
        public DateTime? CreatedAt { get; set; }

        [Column("adoption_status_id")]
        [ForeignKey("adoption_status_id")]
        [Required(ErrorMessage = "Status obrigatório.")]
        public int AdoptionStatusId { get; set; }

        [Column("animal_id")]
        [ForeignKey("animal_id")]
        [Required(ErrorMessage = "Animal obrigatório.")]
        public int AnimalId { get; set; }

        [Column("user_id")]
        [ForeignKey("user_id")]
        [Required(ErrorMessage = "Adotante obrigatório.")]
        public int UserId { get; set; }

        [Column("adoption_queue_id")]
        [ForeignKey("adoption_queue_id")]
        [Required(ErrorMessage = "Identificador da fila de adoção é obrigatório.")]
        public int AdoptionQueueId { get; set; }

        public Animal Animal { get; set; }
        public AdoptionStatus AdoptionStatus { get; set; }
        public User User { get; set; }
    }
}
