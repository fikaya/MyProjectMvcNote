using MyMvcProject.Entities.Entity;
using MyMvcProject.Entities.IntermediateTable.Liked;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvcProject.Entities.Interface.ListInterface
{
    public interface ILikes
    {
        ICollection<Liked> Likes { get; }

    }
}
