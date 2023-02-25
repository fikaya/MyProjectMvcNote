using MyMvcProject.Entities.Base.IntermediateTableBase;
using MyMvcProject.Entities.Entity;
using MyMvcProject.Entities.Interface.NavigationInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvcProject.Entities.IntermediateTable.Liked
{
    //Bir notu birden çok kullanıcı beğenebilir aynı şekilde bir kullanıcı birden çok notu beğenebilir.Onun için ara tablo yaptık.
    [Table("Likes")]
    public class Liked : MyIntermediateTableBase<int>,IMyProjectUser,INote
    {
        [ForeignKey("Note")]
        public int? NoteID { get; set; }
        public Note Note { get; set; }
        [ForeignKey("MyProjectUser")]
        public int? MyProjectUserID { get; set; }
        public MyProjectUser MyProjectUser { get; set; }
    }
}
