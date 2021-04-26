using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aether.Models
{
    public class AnimalTemperament
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome deve ter no máximo {1} caracteres.")]
        public string Name { get; set; }
    }
}