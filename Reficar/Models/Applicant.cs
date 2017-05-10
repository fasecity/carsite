using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Reficar.Models
{
    public class Applicant
    {

        public int Id { get; set; }


        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\-][a-zA-Z])?[a-zA-Z]*)*$", ErrorMessage = "Names can only contain letters")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\-][a-zA-Z])?[a-zA-Z]*)*$", ErrorMessage = "Names can only contain letters")]
        public string LastName { get; set; }

        [Required]
        [Range(18, 100,
        ErrorMessage = "Value for {0} must be over {1} .")]
        public int Age { get; set; }

        [Required]
        [StringLength(100)]
        public string Province { get; set; }

        [Required]
        //[MaxLength(10)]
        //[MinLength(10)]doesnt work on non string types
        [RegularExpression(@"^\(\d{3}\) ?\d{3}( |-)?\d{4}|^\d{3}( |-)?\d{3}( |-)?\d{4}", ErrorMessage = "Please enter numbers in this format 4165556666")]
        public double PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string CarMake { get; set; }

        [Required]
        [Range(2010, 2050,
        ErrorMessage = "Value for {0} must be over {1} .")]
        public int CarYear { get; set; }

        [Required]
        [Range(1, 250000,
        ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public double Carkilometer { get; set; }

        [Required]
        [StringLength(255)]
        public string CarModel { get; set; }

        [Required]
        public double DesiredLoanAmmount { get; set; }

        [Required]
        public double MonthlyIncome { get; set; }


        public DateTime DateTime { get; set; }

        [Required]
        public string CarInsurance { get; set; }
    }
}