using FI.WebAtividadeEntrevista.Customs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAtividadeEntrevista.Models
{
    /// <summary>
    /// Classe de Modelo de Cliente
    /// </summary>
    public class ClienteModel
    {
        public long Id { get; set; }
        
        /// <summary>
        /// CEP
        /// </summary>
        [Required(ErrorMessage = "CEP do Cliente é obrigatório")]
        public string CEP { get; set; }

        /// <summary>
        /// Cidade
        /// </summary>
        [Required(ErrorMessage = "Cidade do Cliente é obrigatório")]
        public string Cidade { get; set; }

        /// <summary>
        /// E-mail
        /// </summary>
        [Required(ErrorMessage = "Email do Cliente é obrigatório")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "E-mail do Cliente está inválido")]
        public string Email { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        [Required(ErrorMessage = "Estado do Cliente é obrigatório")]
        [MaxLength(2, ErrorMessage = "Estado do Cliente deve ter no máximo 2 caracteres")]
        public string Estado { get; set; }

        /// <summary>
        /// Logradouro
        /// </summary>
        [Required(ErrorMessage = "Logradouro do Cliente é obrigatório")]
        public string Logradouro { get; set; }

        /// <summary>
        /// Nacionalidade
        /// </summary>
        [Required(ErrorMessage = "Nacionalidade do Cliente é obrigatório")]
        public string Nacionalidade { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        [Required(ErrorMessage = "Nome do Cliente é obrigatório")]
        public string Nome { get; set; }

        /// <summary>
        /// Sobrenome
        /// </summary>
        [Required(ErrorMessage = "Sobrenome do Cliente é obrigatório")]
        public string Sobrenome { get; set; }

        /// <summary>
        /// Telefone
        /// </summary>
        public string Telefone { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        [Required(ErrorMessage = "CPF do Cliente é obrigatório")]
        [CustomValidationModelClienteCPF(ErrorMessage = "CPF do Cliente está inválido")]
        public string CPF { get; set; }

        /// <summary>
        /// Beneficiarios
        /// </summary>
        public List<BeneficiarioModel> Beneficiarios { get; set; }

    }    
}