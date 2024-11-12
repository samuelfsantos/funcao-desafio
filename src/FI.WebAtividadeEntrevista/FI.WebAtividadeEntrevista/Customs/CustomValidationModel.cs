using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace FI.WebAtividadeEntrevista.Customs
{
    public class CustomValidationModelCPF : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return new ValidationResult("CPF é obrigatório");

            string cpf = value.ToString();
            cpf = Regex.Replace(cpf, @"[^\d]", ""); // Remove caracteres especiais

            if (cpf.Length != 11 || cpf.All(c => c == cpf[0]))
                return new ValidationResult("Digite um CPF válido");

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            int digito = resto < 2 ? 0 : 11 - resto;

            tempCpf += digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            digito = resto < 2 ? 0 : 11 - resto;

            if (cpf.EndsWith(digito.ToString()))
                return ValidationResult.Success;
            else
                return new ValidationResult("Digite um CPF válido");
        }
    }
}