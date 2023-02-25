using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvcProject.Entities.Base.EntityBase
{
    public abstract class MyEntityBase<T>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T ID { get; set; }

        //Oluşturulma ve değiştirilme tarihlerini de her bir entity için tutmak istiyorum.
        [DisplayName("Oluşturma Tarihi"), ScaffoldColumn(false), Required]
        public DateTime CreatedOn { get; set; }

        [DisplayName("Güncelleme Tarihi"), ScaffoldColumn(false), Required]
        public DateTime ModifiedOn { get; set; }

        [DisplayName("Güncelleyen"), ScaffoldColumn(false), Required, StringLength(30)]
        public string ModifiedUsername { get; set; }
    }
}
