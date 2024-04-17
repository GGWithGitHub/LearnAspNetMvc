using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learn_mvc.Models
{
    public class CustomValidation
    {
        
    }

    public class CheckBoxAtLeastOneValidation : ValidationAttribute//, IClientValidatable
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            List<Check_box> instance = value as List<Check_box>;
            int count = instance == null ? 0 : (from p in instance
                                                where p.Checked == true
                                                select p).Count();
            if (count >= 1)
                return ValidationResult.Success;
            else
                return new ValidationResult(ErrorMessage);
        }


        //public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        //{
        //    ModelClientValidationRule mvr = new ModelClientValidationRule();
        //    mvr.ErrorMessage = ErrorMessage;
        //    mvr.ValidationType = "checkboxatleastone";
        //    return new[] { mvr };
        //}
    }
}