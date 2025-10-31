using System.ComponentModel.DataAnnotations;

namespace Formulario_Cadastro_Cliente.Models.Validations
{
    public class DigitsOnlyAttribute : ValidationAttribute
    {
        private readonly int _minLength;
        private readonly int _maxLength;

        public DigitsOnlyAttribute(int minLength, int maxLength)
        {
            _minLength = minLength;
            _maxLength = maxLength;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var stringValor = value as string;
            if (string.IsNullOrWhiteSpace(stringValor))
                return ValidationResult.Success;
            
                
            string digits = new string(value.ToString().Where(char.IsDigit).ToArray());

            if (digits.Length < _minLength || digits.Length > _maxLength)
                return new ValidationResult(ErrorMessage ?? $"O campo {validationContext.DisplayName} deve ter entre {_minLength} e {_maxLength} dígitos.");

            return ValidationResult.Success;
        }
    }
}
