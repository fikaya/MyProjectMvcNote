using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMvcProject.Entities.Entity;

namespace MyMvcProject.Entities.Interface.NavigationInterface
{
    public interface IMyProjectUser
    {
        int? MyProjectUserID { get; set; }
        MyProjectUser MyProjectUser { get; set; }
    }
}
