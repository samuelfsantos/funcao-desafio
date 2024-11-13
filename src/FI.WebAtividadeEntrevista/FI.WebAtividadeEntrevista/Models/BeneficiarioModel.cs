using FI.WebAtividadeEntrevista.Customs;
using System.ComponentModel.DataAnnotations;

namespace WebAtividadeEntrevista.Models
{
    /// <summary>
    /// Classe de Modelo de Beneficiario
    /// </summary>
    public class BeneficiarioModel
    {
        public long Id { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        [Required(ErrorMessage = "Nome do Beneficiário é obrigatório")]
        public string Nome { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        [Required(ErrorMessage = "CPF do Beneficiário é obrigatório")]
        [CustomValidationModelBeneficiarioCPF(ErrorMessage = "CPF do Beneficiário está inválido")]
        public string CPF { get; set; }

        /// <summary>
        /// Id do Cliente
        /// </summary>
        public long IdCliente { get; set; }

    }    
}