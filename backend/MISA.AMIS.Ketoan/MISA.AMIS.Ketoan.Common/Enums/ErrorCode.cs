using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Ketoan.Common.Enums
{
    public enum ErrorCode
    {
        /// <summary>
        /// Exception
        /// </summary>
        Exception = 1,
        /// <summary>
        /// Thêm dữ liệu thất bại
        /// </summary>
        InsertFailed = 2,

        /// <summary>
        /// Sửa dữ liệu thất bại
        /// </summary>
        UpdateFailed = 3,

        /// <summary>
        /// Xóa dữ liệu thất bại
        /// </summary>
        DeleteFailed = 4,

        /// <summary>
        /// Dữ liệu không hợp lệ
        /// </summary>
        InvalidData = 5,

        /// <summary>
        /// Trùng mã
        /// </summary>
        DuplicateCode = 6
    }
}
