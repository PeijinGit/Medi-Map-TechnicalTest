using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public interface IErrorBLL
    {
        void RecordErrLog(string msg);
    }
}
