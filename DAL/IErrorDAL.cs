using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IErrorDAL
    {
        public void RecordErrLog(string msg);
    }
}
