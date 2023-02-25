using MyMvcProject.Entities.Base.EntityBase;
using MyMvcProject.Entities.Interface.ListInterface;
using MyMvcProject.Entities.IntermediateTable.Liked;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMvcProject.Entities.Entity
{
    [Table("MyProjectUser")]
    public class MyProjectUser : MyEntityBase<int>,
        INotes, 
        IComments, 
        ILikes
    {
        public MyProjectUser()
        {
            Notes = new List<Note>();
            Comments = new List<Comment>();
            Likes = new List<Liked>();
        }

        [DisplayName("İsim"),
                   StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Name { get; set; }

        [DisplayName("Soyad"),
                  StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Surname { get; set; }

        [DisplayName("Kullanıcı Adı"),
           Required(ErrorMessage = "{0} alanı gereklidir."),
           StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string UserName { get; set; }

        [DisplayName("E-Posta"),
                   Required(ErrorMessage = "{0} alanı gereklidir."),
                   StringLength(70, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string EMail { get; set; }

        [DisplayName("Şifre"),
                   Required(ErrorMessage = "{0} alanı gereklidir."),
                   StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Password { get; set; }

        //ScaffoldColumn otomatik controller oluşurken hangi propertyler oluşturulmasın ayarının ta kenidisidir.
        [StringLength(30), ScaffoldColumn(false)]
        public string ProfileImageFileName { get; set; }

        //Kullanıcım aktif mi değil mi bunun kontrolünü yapacağım.Aktif olmadan notlar üzerinde herhangi bir işlem yapamayacak.
        //Kullanıcım ilk olarak hesabını oluşturunca bunun değeri false olarak gelecek.Sonrasında kullanıcı aktive edince mailine düşen linke tıklayacak ve biz bunu true olarak değiştireceğiz. İşte bundan sonra notlar üzerinde istedği işlemi yapabilir.
        [DisplayName("Aktif")]
        public bool IsActive { get; set; }

        //Kullanıcıya aktivite için göndereceğimiz alan benzersiz alan olmalı.Peki neden?.Çünkü maile sonuçta bir Url gelecek. /1 /2 /3 yerine /Guid Değeri gelmesi en doğrusu olacaktır. Tahmin edilmemesi gereken bir alan olmalı. Yoksa Url e herkes yazıp kendi kendine insanların üyeliklerini aktive eder. Velhasıl... Ben burada url üzerinden Url sonunda bir Guid değeri göndereceğinm.Ve bunu karşılayarak değeri True yapacağım.
        [Required, ScaffoldColumn(false)]
        public Guid ActiveCode { get; set; } = Guid.NewGuid();

        //Bu kullanıcı Admin mi sorusunun cevabını vereceğiz.
        [DisplayName("Yönetici")]
        public bool IsAdmin { get; set; }

        //Bir kullanıcın birden çok notu olabilir ama bir notun bir kullanıcısı olabilir.
        public ICollection<Note> Notes { get; }

        //Bir kullanıcın birden çok yorumu olabilir ama bir yorumun bir kullanıcısı olabilir.
        public ICollection<Comment> Comments { get; }

        //Bir notu birden çok kullanıcı beğenebilir aynı şekilde bir kullanıcı birden çok notu beğenebilir.
        public ICollection<Liked> Likes { get; }

       
    }
}
