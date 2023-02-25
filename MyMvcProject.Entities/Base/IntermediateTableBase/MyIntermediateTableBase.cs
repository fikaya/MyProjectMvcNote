using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvcProject.Entities.Base.IntermediateTableBase
{
    public class MyIntermediateTableBase<T>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T ID { get; set; }
    }
}
