using MyMvcProject.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvcProject.Entities.ViewModels.Notify
{
    public class BaseNotifyViewModel<T,U> : INotifyViewModel<T,U>
    {
        public List<T> Items { get; }
        public string Header { get; set; }
        public string Title { get; set; }
        public bool IsRedirecting { get; set; }
        public string RedirectingUrl { get; set; }
        public int RedirectingTimeout { get; set; }
        public BaseNotifyViewModel()
        {
            //Varsayılan değerleri veriyoruz.
            Header = "Yönlendiriliyorsunuz...";
            Title = "Geçersiz İşlem";
            IsRedirecting = true;
            RedirectingTimeout = 4000;
            RedirectingUrl = "/Home/Index";
            Items=new List<T>();
        }
    }
}
