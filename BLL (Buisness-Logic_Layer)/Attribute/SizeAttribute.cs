using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL__Buisness_Logic_Layer_.Attribute
{
    public class SizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public SizeAttribute(int MaxFileSize)
        {
            _maxFileSize = MaxFileSize;
        }
        protected override ValidationResult? IsValid
            (object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult($"File size should be less than {_maxFileSize} bytes");
                }
            }
            return ValidationResult.Success;
        }
    }
}
