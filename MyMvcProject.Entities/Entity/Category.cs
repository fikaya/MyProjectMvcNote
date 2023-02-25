using MyMvcProject.Entities.Base.EntityBase;
using MyMvcProject.Entities.Interface.ListInterface;
 using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMvcProject.Entities.Entity
{
    [Table("Categories")]
    public class Category : MyEntityBase<int>,
        INotes
    {
        public Category()
        {
            Notes = new List<Note>();
        }

        [DisplayName("Kategori"),
                    Required(ErrorMessage = "{0} alanı gereklidir."),
                    StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter içermeli.")]
        public string Title { get; set; }

        [DisplayName("Açıklama"),
                    StringLength(150, ErrorMessage = "{0} alanı max. {1} karakter içermeli.")]
        public string Description { get; set; }

        //Bir kategorinin birden çok notu olabilir ama bir notun bir kategorisi olabilir.
        public ICollection<Note> Notes { get; }
       
     }
}

