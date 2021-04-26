using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aether.Models
{
    public class State
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome obrigatório.")]
        [StringLength(150, ErrorMessage = "O nome deve ter no máximo {1} caracteres.")]
        public string Name { get; set; }


        [Required(ErrorMessage = "UF obrigatório.")]
        [StringLength(4, ErrorMessage = "UF deve ter no máximo {1} caracteres.")]
        public string Uf { get; set; }
    }
}
