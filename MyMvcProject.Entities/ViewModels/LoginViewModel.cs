using MyMvcProject.Entities.Interface.ViewModelInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvcProject.Entities.ViewModels
{
    public class LoginViewModel : ILoginViewModel
    {
        //[DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "{0} Alanı Boş Geçilemez."),StringLength(100,ErrorMessage ="{0} max. {1} karakter olmalı.")]
        //public string UserName { get; set; }
        
        [DisplayName("Şifre"), Required(ErrorMessage = "{0} Alanı Boş Geçilemez."), DataType(DataType.Password),StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Password { get; set; }

        [DisplayName("E-Mail"), Required(ErrorMessage = "{0} Alanı Boş Geçilemez."), DataType(DataType.EmailAddress),StringLength(50, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string UserMail { get; set; }
    }
}
