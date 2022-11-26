using MISA.AMIS.Ketoan.Common.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MISA.AMIS.Ketoan.Common.Entities
{
    /// <summary>
    /// Phòng ban
    /// </summary>
    public class Department : BaseEntity
    {
        /// <summary>
        /// ID phòng ban
        /// </summary>
        [Key]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        [RecordCode]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        [Required(ErrorMessage ="Tên phòng ban không được để trống")]
        public string DepartmentName { get; set; }



    }
}
