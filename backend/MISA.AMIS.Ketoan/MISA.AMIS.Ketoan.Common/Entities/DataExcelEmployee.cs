using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Ketoan.Common.Entities
{
    public class DataExcelEmployee
    {
        #region Contructor
        public DataExcelEmployee(int index, Employee employee) 
        {
            Index = index;
            EmployeeCode = employee.EmployeeCode;
            EmployeeName = employee.EmployeeName;
            GenderName = employee.GenderName;
            DateOfBirth = employee.DateOfBirth;
            EmployeePosition = employee.EmployeePosition;
            DepartmentName = employee.DepartmentName;
            BankAccountNumber = employee.BankAccountNumber;
            BankName = employee.BankName;
        }
        #endregion

        #region Field
        /// <summary>
        /// Số thứ tự
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }
        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string? EmployeeName { get; set; }
        /// <summary>
        /// Tên giới tính
        /// </summary>
        public string? GenderName { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Chức vụ 
        /// </summary>
        public string? EmployeePosition { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string? DepartmentName { get; set; }
        
        /// <summary>
        /// Số tài khoản
        /// </summary>
        public string? BankAccountNumber { get; set; }
        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }

        #endregion
    }
}
