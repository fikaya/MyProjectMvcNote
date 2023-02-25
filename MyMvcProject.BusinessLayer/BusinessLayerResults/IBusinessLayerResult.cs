using MyMvcProject.Entities.Messages;
using MyMvcProject.Entities.Messages.Obj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvcProject.BusinessLayer.BusinessLayerResults
{
    public interface IBusinessBaseLayerResult<T>
    {
        List<MessageObj> MessageObjList { get; }
        T Result { get; set; }
    }
}
