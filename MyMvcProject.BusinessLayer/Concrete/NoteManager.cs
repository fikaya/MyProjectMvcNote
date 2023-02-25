using MyMvcProject.BusinessLayer.Abstract;
using MyMvcProject.BusinessLayer.Concrete;
using MyMvcProject.DataAccessLayer.Abstract;
using MyMvcProject.Entities.Entity;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyMvcProject.BusinessLayer
{
    public class NoteManager:BaseManager<Note>,INoteManager<Note> 
    {
        public NoteManager(IRepository<Note> repository) : base(repository)
        {

        }
    }
}
