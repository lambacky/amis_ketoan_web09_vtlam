using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.AMIS.Ketoan.Common.Attributes
{
    public class RecordCodeAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (String.IsNullOrEmpty(value?.ToString()))
            {
                ErrorMessage = Resources.ResourceVN.EmptyRecordCode;
                return false;
            }
            else if (!Regex.Match(value?.ToString(), @"(NV)(\d+)").Success)
            {
                ErrorMessage = Resources.ResourceVN.InvalidRecordCode;
                return false;
            }
            return true;

        }
    }
}
