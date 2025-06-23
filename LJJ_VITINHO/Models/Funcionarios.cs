using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LJJ_VITINHO.Models
{
    [Table("Funcionarios")]
    public class Funcionarios
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo nome é obrigatório")]
        [MaxLength(100, ErrorMessage = "Ultrapassou o máximo permitido")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo nome é obrigatório")]
        [MaxLength(50, ErrorMessage = "Ultrapassou o máximo permitido")]
        public string Especialidade { get; set; }

        //Fks
        public int UsuarioId { get; set; }
        [ForeignKey(nameof(UsuarioId))]
        [ValidateNever]
        public virtual Usuarios Usuario { get; set; }

        //Relacionamento
        [ValidateNever]
        public virtual ICollection<Agendamentos> Agendamentos { get; set; }
    }
}
