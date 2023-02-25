using MyMvcProject.Entities.Messages.Obj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyMvcProject.Entities.ViewModels.Notify
{
    public class OKViewModel:BaseNotifyViewModel<MessageObj, OKViewModel>
    {
        public OKViewModel()
        {
            Title = "İşlem Başarılı";
        }
    }
}
