using MyMvcProject.CommonLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvcProject.CommonLayer
{
    public class DefaultCommon : ICommon
    {
        public string GetUserName()
        {
            return "System";
        }
    }
}
