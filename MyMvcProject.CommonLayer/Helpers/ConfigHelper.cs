using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace MyMvcProject.CommonLayer.Helpers
{
    public class ConfigHelper
    {
        //Bir konfigürasyon yapılandırma dosyasından bizlere bir şey okuyarak vermesini sağlayacağız.
        //Burada burayı kullanacak sınıfın bir key alanı yollayarak sonucunda value değerini göndermesini sağlayan metodu inşaa edecez.
        public static T Get<T>(string key)
        {
            //Port numarası gibi değerlerde ben metodun int olarak dönmesini isterim mesela(Mail Helper). Buraya direkt bir tip dön demek yanlış olacaktır. Onun için ben bu metoda ne tip verirsem o tip dönsün diyeceğim.
            //Verdiğim değerdeki tipi değiştirecek.
            return (T)Convert.ChangeType(ConfigurationManager.AppSettings[key],typeof(T));
        }
    }
}
