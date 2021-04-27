using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aether.Models
{
    [Table("adoption_status")]
    public class AdoptionStatus
    {
        public static int WAITING = 1;
        public static int FINISHED = 2;
        public static int CANCELED = 3;
        public static int RETURNED = 4;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome deve ter no máximo {1} caracteres.")]
        public string Name { get; set; }
    }
}
