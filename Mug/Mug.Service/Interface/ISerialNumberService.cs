using Mug.Dao;
using Mug.Service.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 

namespace Mug.Service.Interface
{
    public interface ISerialNumberService
    {
        IResult Create(SerialNumber instance);

        IResult Update(SerialNumber instance);

        SerialNumber GetByID(int Id);

        IEnumerable<SerialNumber> GetAll();
    }
}
