using MyMvcProject.BusinessLayer.Abstract;
using MyMvcProject.DataAccessLayer.Abstract;
using MyMvcProject.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvcProject.BusinessLayer.Concrete
{
    public class CommentManager : BaseManager<Comment>, ICommentManager<Comment>
    {
        public CommentManager(IRepository<Comment> repository) : base(repository)
        {

        }
    }
}
