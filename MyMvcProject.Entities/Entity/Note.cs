using MyMvcProject.Entities.Base.EntityBase;
using MyMvcProject.Entities.Interface.ListInterface;
using MyMvcProject.Entities.Interface.NavigationInterface;
using MyMvcProject.Entities.IntermediateTable.Liked;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyMvcProject.Entities.Entity
{
    [Table("Notes")]

    public class Note : MyEntityBase<int>,
        IMyProjectUser,
        ICategory,
        ILikes,
        IComments
    {
        public Note()
        {
            Comments = new List<Comment>();
            Likes = new List<Liked>();
        }


        [DisplayName("Not Başlığı"), Required, StringLength(60)]
        public string Title { get; set; }

        [DisplayName("Not Metni"), Required, StringLength(2000)]
        public string Text { get; set; }

        //Bu bir taslakmı kontrolü yapıyor olacağız.Eğer taslak ise o not yayınlanmamıştır.Sonra yayına alacaktır.
        [DisplayName("Taslak")]
        public bool IsDraft { get; set; }
        //Kaç kişi bu notu beğendi onu sorgulayacağız.
        [DisplayName("Beğenilme")]
        public int LikeCount { get; set; }
        //Bir kullanıcın birden çok notu olabilir ama bir notun bir kullanıcısı olabilir.
        [ForeignKey("MyProjectUser")]
        public int? MyProjectUserID { get; set; }
        public MyProjectUser MyProjectUser { get; set; }

        //Bir kategorinin birden çok notu olabilir ama bir notun bir kategorisi olabilir.
        [ForeignKey("Category")]
        public int? CategoryID { get; set; }
        public Category Category { get; set; }

        //Bir notun birden çok yorumu olabilir ama bir yorum bir notun olabilir.
        public ICollection<Comment> Comments { get; }

        //Bir notu birden çok kullanıcı beğenebilir aynı şekilde bir kullanıcı birden çok notu beğenebilir.Onun için ara tablo yaptık.
        public ICollection<Liked> Likes { get; }

    }
}
