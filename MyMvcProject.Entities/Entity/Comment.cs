using MyMvcProject.Entities.Base.EntityBase;
using MyMvcProject.Entities.Interface.ListInterface;
using MyMvcProject.Entities.Interface.NavigationInterface;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMvcProject.Entities.Entity
{
    [Table("Comments")]
    public class Comment : MyEntityBase<int>,
        IMyProjectUser,
        INote
    {
       
        [Required,StringLength(500)]
        public string Text { get; set; }

        //Bir notun birden çok yorumu olabilir ama bir yorum bir notun olabilir.
        [ForeignKey("Note")]
        public int? NoteID { get; set; }
        public Note Note { get; set; }

        //Bir yorumun bir kullanıcısı olabilir ama bir kulllanıcın birden çok yorumu olabilir.
        [ForeignKey("MyProjectUser")]
        public int? MyProjectUserID { get; set; }
        public MyProjectUser MyProjectUser { get; set; }

        

    }
}
