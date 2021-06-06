using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public interface IErrorBLL
    {
        public void RecordErrLog(string msg);
    }
}
