using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aether.Models
{
    public class Animal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome deve ter no máximo {1} caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Data de nascimento obrigatória.")]
        public DateTime Birthdate { get; set; }

        public bool IsCastrated { get; set; }

        public bool IsSpecial { get; set; }

        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo {1} caracteres.")]
        public string Description { get; set; }

        [StringLength(80, ErrorMessage = "O nome deve ter no máximo {1} caracteres.")]
        public string Breed { get; set; }

        [Required(ErrorMessage = "Data da criação obrigatória.")]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Dono obrigatório.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Tipo obrigatório.")]
        public int AnimalTypeId { get; set; }

        [Required(ErrorMessage = "Pelagem obrigatória.")]
        public int AnimalColorId { get; set; }
        
        [Required(ErrorMessage = "Temperamento obrigatório.")]
        public int AnimalTemperamentId { get; set; }
        
        [Required(ErrorMessage = "Tamanho obrigatório.")]
        public int AnimalSizeId { get; set; }

        [Required(ErrorMessage = "Sexo do animal obrigatório.")]
        public int AnimalGenderId { get; set; }
    }
}