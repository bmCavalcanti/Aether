using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aether.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome obrigatório.")]
        [StringLength(40, ErrorMessage = "O nome deve ter no máximo {1} caracteres.")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Sobrenome obrigatório.")]
        [StringLength(100, ErrorMessage = "O sobrenome deve ter no máximo {1} caracteres.")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Data de aniversário obrigatória.")]
        public DateTime Birthdate { get; set; }

        [StringLength(11, ErrorMessage = "O CPF deve ter no máximo {1} caracteres.")]
        [Required(ErrorMessage = "CPF obrigatório.")]
        public string Cpf { get; set; }

        [StringLength(80, ErrorMessage = "O E-mail deve ter no máximo {1} caracteres.")]
        [Required(ErrorMessage = "E-mail obrigatório.")]
        public string Email { get; set; }

        [StringLength(11, ErrorMessage = "O celular deve ter no máximo {1} caracteres.")]
        public string Cellphone { get; set; }
    }
}
