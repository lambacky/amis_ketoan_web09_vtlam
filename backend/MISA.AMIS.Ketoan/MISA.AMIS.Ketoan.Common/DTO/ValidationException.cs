using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Ketoan.Common.DTO
{
    public class ValidationException : Exception
    {
        public ValidationException(List<string> validateFailures)
        {
            this.validateFailures = validateFailures;
        }

        public List<string> validateFailures { get; set; }
    }
}
