using MyMvcProject.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvcProject.Entities.Interface.ListInterface
{
    public interface ICategories
    {
        ICollection<Category> Categories {get; }
    }
}
