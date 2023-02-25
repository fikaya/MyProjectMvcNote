using MyMvcProject.CommonLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvcProject.CommonLayer
{
    public static class App
    {
        public static ICommon Common = new DefaultCommon();
    }
}
