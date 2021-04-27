using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aether.Models
{
    [Table("animal_address")]
    public class AnimalAddress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "CEP obrigatório.")]
        [StringLength(8, ErrorMessage = "O CEP deve ter no máximo {1} caracteres.")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Rua obrigatória.")]
        [StringLength(100, ErrorMessage = "A rua deve ter no máximo {1} caracteres.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Bairro obrigatório.")]
        [StringLength(100, ErrorMessage = "O bairro deve ter no máximo {1} caracteres.")]
        public string District { get; set; }

        [Required(ErrorMessage = "Número obrigatório.")]
        [StringLength(6, ErrorMessage = "O número deve ter no máximo {1} caracteres.")]
        public string Number { get; set; }

        [StringLength(100, ErrorMessage = "O complemento deve ter no máximo {1} caracteres.")]
        public string Complement { get; set; }

        [ForeignKey("city_id")]
        [Column("city_id")]
        [Required(ErrorMessage = "Cidade obrigatória.")]
        public int CityId { get; set; }

        [ForeignKey("animal_id")]
        [Column("animal_id")]
        [Required(ErrorMessage = "Animal obrigatório.")]
        public int AnimalId { get; set; }
    }
}
