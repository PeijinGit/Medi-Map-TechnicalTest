using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public class ErrorRecord : IErrorBLL
    {
        private readonly IErrorDAL errorDAL;

        public ErrorRecord(IErrorDAL errorDAL)
        {
            this.errorDAL = errorDAL;
        }

        public void RecordErrLog(string msg)
        {
            errorDAL.RecordErrLog(msg);
        }
    }
}

