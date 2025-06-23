using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LJJ_VITINHO.Models
{
    [Table("Clientes")]
    public class Clientes
    {
        [Key] //chave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // codigo automatico
        [Display(Name = "Código")]
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "Ultrapassou o máximo permitido")]
        [Required(ErrorMessage = "Campo nome é obrigatório")]
        public string Nome { get; set; }

        [MaxLength(50, ErrorMessage = "Ultrapassou o máximo permitido")]
        [Required(ErrorMessage = "Campo nome é obrigatório")]
        [Display(Name = "Profissão")]
        public string Profissao { get; set; }

        [MaxLength(30, ErrorMessage = "Ultrapassou o máximo permitido")]
        public string? Setor { get; set; } //? permite nulo

        //Fks

        [Display(Name = "Identificação do Usuário")]
        public int UsuarioId { get; set; }
        [ForeignKey(nameof(UsuarioId))]
        [ValidateNever]
        public virtual Usuarios Usuario { get; set; }

        //Relacionamento 
        //N
        [ValidateNever]
        public virtual ICollection<Agendamentos> Agendamentos { get; set; }
    }
}
