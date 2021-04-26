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

        [Column("is_castrated")]
        public bool IsCastrated { get; set; }

        [Column("is_special")]
        public bool IsSpecial { get; set; }

        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo {1} caracteres.")]
        public string Description { get; set; }

        [StringLength(80, ErrorMessage = "O nome deve ter no máximo {1} caracteres.")]
        public string Breed { get; set; }

        [Column("created_at")]
        [Required(ErrorMessage = "Data da criação obrigatória.")]
        public DateTime CreatedAt { get; set; }

        [ForeignKey("user_id")]
        [Required(ErrorMessage = "Dono obrigatório.")]
        public int UserId { get; set; }

        [ForeignKey("animal_type_id")]
        [Required(ErrorMessage = "Tipo obrigatório.")]
        public int AnimalTypeId { get; set; }

        [ForeignKey("animal_color_id")]
        [Required(ErrorMessage = "Pelagem obrigatória.")]
        public int AnimalColorId { get; set; }

        [ForeignKey("animal_temperament_id")]
        [Required(ErrorMessage = "Temperamento obrigatório.")]
        public int AnimalTemperamentId { get; set; }

        [ForeignKey("animal_size_id")]
        [Required(ErrorMessage = "Tamanho obrigatório.")]
        public int AnimalSizeId { get; set; }

        [ForeignKey("animal_gender_id")]
        [Required(ErrorMessage = "Sexo do animal obrigatório.")]
        public int AnimalGenderId { get; set; }
    }
}
