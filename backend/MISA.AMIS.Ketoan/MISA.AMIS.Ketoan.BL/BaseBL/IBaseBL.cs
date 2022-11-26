using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Ketoan.BL
{
    public interface IBaseBL<T>
    {
        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>sanh sách bản ghi</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        public IEnumerable<T> GetAllRecords();

        /// <summary>
        /// Lấy thông tin bản ghi theo ID
        /// </summary>
        /// <param name="recordID"> ID bản ghi</param>
        /// <returns>Thông tin bản ghi</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        public T GetRecordByID(Guid recordID);

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="record">Thông tin bản ghi</param>
        /// <returns>ID nhân viên thêm mới</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        public int Insert(T record);

        /// <summary>
        /// Sửa bản ghi
        /// </summary>
        /// <param name="recordId"> ID bản ghi</param>
        /// <param name="record">bản ghi</param>
        /// <returns>ID nhân viên được sửa</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        public int Update(Guid recordId, T record);

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="recordId">Id bản ghi</param>
        /// <returns>Id bản ghi bị xóa</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        public int Delete(Guid recordId);
    }
}
