using MyMvcProject.Entities.Messages;
using MyMvcProject.Entities.Messages.Obj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvcProject.BusinessLayer.BusinessLayerResults
{
    public class BusinessLayerResult<T> : IBusinessBaseLayerResult<T> where T : class
    {
        public BusinessLayerResult()
        {
            MessageObjList = new List<MessageObj>();
        }
        public List<MessageObj> MessageObjList { get; }

        public T Result { get; set; }

    }
}
