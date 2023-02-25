using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvcProject.Entities.Messages
{
    //Ben burada bunu yaparak ne yapmış oldum.Merkezi bir hata bilgi uyarı ve tamamlandı sayfalarını kurmayı başardım.Çeşitli ActionResultlarda  her hata veya başarı için tek tek hata sayfaları veya uyarı sayfaları yapmayacağım.Ve o sayfaların içeriklerini ben değiştireceğim.
    public interface INotifyViewModel<T,U>
    {
        //Mesela Error Sayfamız tek olacak ama değerleri değişecek. Mesela başlığı, mesela içeriği,mesela bir sayfaya yönlendirecekmiyiz,mesela yönlendirilecek js de setTimeOut fonksiyonumun nasıl bir değer alması gibi. 
        List<T> Items { get;}
        string Header { get; set; }
        string Title { get; set; }
        bool IsRedirecting { get; set; }
        string RedirectingUrl { get; set; }
        int RedirectingTimeout { get; set; }

    }
}
