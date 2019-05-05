using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Vidly.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;
            if (customer.MembershipTypeId == MembershipType.Unkown ||
                customer.MembershipTypeId == MembershipType.PayAsYouGo)
            {
                return ValidationResult.Success;
            }
            else
            {
                if(customer.Birthday == null)
                {
                    return new ValidationResult("Birthday is required");
                }
                else
                {
                    var age = DateTime.Today.Year - customer.Birthday.Value.Year;
                    return (age >= 18) ? ValidationResult.Success : new ValidationResult("Customer should be atleast 18 to go on subscription.");
                }
            }
        }
    }
}