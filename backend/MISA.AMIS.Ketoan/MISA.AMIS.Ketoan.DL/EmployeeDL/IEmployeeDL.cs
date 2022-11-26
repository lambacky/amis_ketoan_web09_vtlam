using MISA.AMIS.Ketoan.Common.DTO;
using MISA.AMIS.Ketoan.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Ketoan.DL
{
    public interface IEmployeeDL : IBaseDL<Employee>
    {
        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>mã nhân viên mới</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        public string GetNewEmployeeCode();

        /// <summary>
        /// Tìm kiếm và phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="employeeFilter"></param>
        /// <returns> Danh sách nhân viên</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        public PagingResult Filter(int? pageSize, int? pageNumber, string? employeeFilter);

        /// <summary>
        /// Thực hiện xóa nhiều
        /// </summary>
        /// <param name="employeeIds"></param>
        /// <returns></returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        public bool DeleteBatch(List<Guid> employeeIds);
    }
}

