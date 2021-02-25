using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace startup.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar una {0}")]
        [Index("Product_CompanyId_Description_Index", 1, IsUnique = true)]
        [Index("Product_CompanyId_Code_Index", 1, IsUnique = true)]
        [Display(Name = "Empresa")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "El campo{0} puede contener un máximo de {1} caracteres")]
        [Index("Product_CompanyId_Description_Index", 2, IsUnique = true)]
        [Display(Name = "Producto")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(13, ErrorMessage = "El campo{0} puede contener un máximo de {1} caracteres")]
        [Index("Product_CompanyId_Code_Index", 2, IsUnique = true)]
        [Display(Name = "Código")]
        public string Code { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar una {0}")]
        [Display(Name = "Categoria")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(0, double.MaxValue, ErrorMessage = "Debe seleccionar un {0} entre {1} y {2}")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Display(Name = "Precio")]
        public decimal Price { get; set; }


        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción")]
        public string Remarks { get; set; }

        [Display(Name = "Image")]
        public string ImageFullPath => ProductImages.ToString();

        public virtual Company Company { get; set; }

        public virtual Category Category { get; set; }


        public virtual ICollection<ProductImage> ProductImages { get; set; }

        [DisplayName("No Imagenes")]
        public int ProductImagesNumber => ProductImages == null ? 0 : ProductImages.Count;


    }
}