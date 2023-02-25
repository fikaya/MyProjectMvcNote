using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvcProject.Entities.Interface.ViewModelInterface
{
    public interface ILoginViewModel
    {
        string UserMail { get; set; }
        string Password { get; set; }

    }
}
