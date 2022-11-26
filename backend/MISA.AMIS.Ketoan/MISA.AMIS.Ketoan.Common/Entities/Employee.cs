using MISA.AMIS.Ketoan.Common.Attributes;
using MISA.AMIS.Ketoan.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace MISA.AMIS.Ketoan.Common.Entities
{
    /// <summary>
    /// Nhân viên
    /// </summary>
    public class Employee : BaseEntity
    {
        #region constructor
        /*public Employee()
        {
            this.EmployeeId = new Guid();
        }*/
        #endregion

        #region Field
        /// <summary>
        /// ID nhân viên
        /// </summary>
        [Key]
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [RecordCode]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [Required(ErrorMessage = "Tên không được để trống")]
        public string EmployeeName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        [Date(ErrorMessage ="Ngày sinh không được lớn hơn hiện tại")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// Tên giới tính
        /// </summary>
        public string? GenderName 
        {
            get
            {
                switch (Gender)
                {
                    case Enums.Gender.Male:
                        return Resources.ResourceVN.GenderMale;
                    case Enums.Gender.Female:
                        return Resources.ResourceVN.GenderFemale;
                    case Enums.Gender.Other:
                        return Resources.ResourceVN.GenderOther;
                    default:
                        return Resources.ResourceVN.GenderOther;
                }
            }
        }

        /// <summary>
        /// ID phòng ban
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Chức danh
        /// </summary>
        public string? EmployeePosition { get; set; }

        /// <summary>
        /// Số CMND
        /// </summary>
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp CMND
        /// </summary>
        [Date(ErrorMessage = "Ngày cấp không được lớn hơn hiện tại")]
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp CMND
        /// </summary>
        public string? IdentityPlace { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Số ĐT di động
        /// </summary>
        public string? TelephoneNumber { get; set; }

        /// <summary>
        /// Số ĐT cố định
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email không đúng định dạng")]
        public string? Email { get; set; }

        /// <summary>
        /// Số TK ngân hàng
        /// </summary>
        public string? BankAccountNumber { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }

        /// <summary>
        /// Tên chi nhánh ngân hàng
        /// </summary>
        public string? BankBranchName { get; set; }
        #endregion
    }
}
