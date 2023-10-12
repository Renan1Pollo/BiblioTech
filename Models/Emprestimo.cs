using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiblioTech.Models
{
    [Table("Emprestimos")]
    public class Emprestimo
    {
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [ForeignKey("Usuario")]
        [Display(Name = "Código do Usuario")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public int IdUsuario { get; set; }

        [ForeignKey("Livro")]
        [Display(Name = "Código do Livro")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public int IdLivro { get; set; }

        [Display(Name = "Data de Retirada")]
        [Required(ErrorMessage = "Campo Obrigatório!")]
        public DateTime DataRetirada { get; set; }

        [Display(Name = "Devolvido")]
        public bool Devolvido { get; set; }

    }
}
