﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvcProject.Entities.Interface.ViewModelInterface
{
    public interface IRegisterViewModel:ILoginViewModel
    {
        string RePassword { get; set; }
    }
}
