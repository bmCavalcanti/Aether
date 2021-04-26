using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aether.Models
{
    [Table("animal_photo")]
    public class AnimalPhoto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Caminho do arquivo obrigatório.")]
        [StringLength(200, ErrorMessage = "O caminho do arquivo deve ter no máximo {1} caracteres.")]
        public string Path { get; set; }

        [ForeignKey("animal_id")]
        [Required(ErrorMessage = "Animal obrigatório.")]
        public int AnimalId { get; set; }
    }
}
