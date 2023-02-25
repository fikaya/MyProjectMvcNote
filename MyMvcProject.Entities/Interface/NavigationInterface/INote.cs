using MyMvcProject.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvcProject.Entities.Interface.NavigationInterface
{
    public interface INote
    {
        int? NoteID { get; set; }
        Note Note { get; set; }
    }
}
