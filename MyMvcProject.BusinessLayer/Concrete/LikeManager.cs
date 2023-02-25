using MyMvcProject.BusinessLayer.Abstract;
using MyMvcProject.DataAccessLayer.Abstract;
using MyMvcProject.Entities.Entity;
using MyMvcProject.Entities.IntermediateTable.Liked;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvcProject.BusinessLayer.Concrete
{
    public class LikeManager : BaseManager<Liked>, ILikeManager<Liked>
    {
        public LikeManager(IRepository<Liked> repository) : base(repository)
        {
        }

    }
}
