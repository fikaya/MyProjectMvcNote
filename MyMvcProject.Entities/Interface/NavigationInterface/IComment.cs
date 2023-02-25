using MyMvcProject.Entities.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvcProject.Entities.Interface.NavigationInterface
{
    public interface IComment
    {
        int? CommentID { get; set; }
        Comment Comment { get; set; }
    }
}
