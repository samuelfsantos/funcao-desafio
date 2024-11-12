using System;


namespace FI.WebAtividadeEntrevista.Customs
{
    public static class CustomExtensionMethods
    {
        public static string LimparFormatacaoCpf(this string value)
        {
            return (String.IsNullOrEmpty(value))
                ? value
                : value.Replace(" ", "").Replace(".", "").Replace("-", "");

        }
    }
}