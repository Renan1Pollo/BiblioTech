using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiblioTech.Models
{
    [Table("Livros")]
    public class Livro
    {
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [ForeignKey("Genero")]
        [Display(Name = "IdGenero")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public int IdGenero { get; set; }

        [Display(Name = "Titulo")]
        [StringLength(40)]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string Titulo { get; set; }

        [Display(Name = "Sinopse")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string Sinopse { get; set; }

        [Display(Name = "Autor")]
        [StringLength(40)]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string Autor { get; set; }

        [Display(Name = "Volume")]
        [StringLength(3, MinimumLength = 1)]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string Volume { get; set; }

        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public int Quantidade { get; set; }
    }
}
