using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Ketoan.Common.Attributes
{
    public class DateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value!=null && DateTime.Compare(Convert.ToDateTime(value), DateTime.Now)>0)
                return false;

            return true;
        }
    }
}
