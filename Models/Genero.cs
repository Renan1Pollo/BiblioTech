using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiblioTech.Models
{
    [Table("Generos")]
    public class Genero
    {
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Descricao")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public string Descricao { get; set; }
    }
}
