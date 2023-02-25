using MyMvcProject.Entities.Entity;
using MyMvcProject.Entities.Interface.ListInterface;
using MyMvcProject.Entities.Interface.NavigationInterface;
using MyMvcProject.Entities.Interface.ViewModelInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvcProject.Entities.ViewModels
{
    public class HomeIndexViewModel:IHomeIndexViewModel
    {
        public HomeIndexViewModel()
        {
            Categories= new List<Category>();
            Notes = new List<Note>();
        }
        public ICollection<Category> Categories { get;}
        public ICollection<Note> Notes { get; }
    }
}
