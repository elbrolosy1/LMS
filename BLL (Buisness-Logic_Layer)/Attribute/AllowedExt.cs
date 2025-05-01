using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL__Buisness_Logic_Layer_.Attribute
{
    public class AllowedExtentionValidation : ValidationAttribute
    {
        private readonly string _allowedExtentions;

        public AllowedExtentionValidation(string AllowedExtentions)
        {
            _allowedExtentions = AllowedExtentions;
        }

        protected override ValidationResult? IsValid
            (object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extention = Path.GetExtension(file.FileName);
                var isAllowed = _allowedExtentions.Split(',').Contains(extention, StringComparer.OrdinalIgnoreCase);
                if (!isAllowed)
                {
                    return new ValidationResult($"" +
                        $"This extention is not allowed, " +
                        $"allowed extentions are {_allowedExtentions}");
                }
            }
            return ValidationResult.Success;
        }

    }
}