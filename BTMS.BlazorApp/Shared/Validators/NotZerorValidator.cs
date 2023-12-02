using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTMS.BlazorApp.Shared.Validators
{
    public class NotZeroAttribute : ValidationAttribute
    {


        public override bool IsValid(object? value)
        {
            if (value == null) return false;
            else if((int)value == 0) return false;
            else return true;
            
        }
        //protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        //{
        //    if (value == null) return new ValidationResult("Field is required");

        //    if ((int)value == 0) return new ValidationResult("Field is required");
        //    else
        //        return ValidationResult.Success;
        //}
    }
}
