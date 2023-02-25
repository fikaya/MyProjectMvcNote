using MyMvcProject.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvcProject.Entities.Interface.NavigationInterface
{
    public interface ICategory
    {
        int? CategoryID { get; set;}
        Category Category { get; set; }
    }
}
