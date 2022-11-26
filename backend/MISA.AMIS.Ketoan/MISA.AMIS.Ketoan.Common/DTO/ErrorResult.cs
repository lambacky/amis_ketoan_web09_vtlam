using MISA.AMIS.Ketoan.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Ketoan.Common.DTO
{
    public class ErrorResult
    {
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public ErrorCode ErrorCode { get; set; }

        /// <summary>
        /// Message trả về cho dev
        /// </summary>
        public string DevMsg { get; set; }

        /// <summary>
        /// Message trả về cho người dùng
        /// </summary>
        public string UserMsg { get; set; }

        /// <summary>
        /// Thông tin chi tiết mã lỗi
        /// </summary>
        public object MoreInfo { get; set; }

        /// <summary>
        /// ID dùng để trace lỗi khi log lại
        /// </summary>
        public string TraceId { get; set; }
    }
}
