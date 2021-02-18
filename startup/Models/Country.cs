using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace startup.Models
{
    public class Country
    {
        [Key]
        public int CountyrId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "El campo {0} puede contener máximo {1} caracteres")]
        [Index("Country_Name_Index", IsUnique = true)]
        [Display(Name = "País")]
        public string Name { get; set; }

        public virtual ICollection<City> Cities { get; set; }

    }
}