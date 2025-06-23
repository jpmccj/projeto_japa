﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LJJ_VITINHO.Models
{
    [Table("Usuarios")]
    public class Usuarios
    {
        //Pk
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo e-mail obrigatório")]
        [StringLength(100, ErrorMessage = "Ultrapassou o máximo permitido")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo senha obrigatório")]
        [StringLength(50, ErrorMessage = "Ultrapassou o máximo permitido")]
        [MinLength(8, ErrorMessage = "Senha com mínimo de 8 caracteres")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public bool Inativo { get; set; }

        public Perfil Perfil { get; set; }

        [ValidateNever]
        public string? Arquivo { get; set; }

        //Relacionamentos
        [ValidateNever]
        public virtual ICollection<Clientes> Clientes { get; set; }
        [ValidateNever]
        public virtual ICollection<Funcionarios> Funcionarios { get; set; }

    }

    public enum Perfil
    {
        Clientes = 0,
        Funcionarios = 1,
        Administrador = 2,
    }

}
