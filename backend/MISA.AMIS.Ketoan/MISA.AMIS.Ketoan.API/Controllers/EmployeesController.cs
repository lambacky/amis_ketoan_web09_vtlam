using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Ketoan.BL;
using MISA.AMIS.Ketoan.Common.Constants;
using MISA.AMIS.Ketoan.Common.DTO;
using MISA.AMIS.Ketoan.Common.Entities;
using MISA.AMIS.Ketoan.Common.Enums;
using MISA.AMIS.Ketoan.Common.Resources;
using MISA.AMIS.Ketoan.DL;
using MySqlConnector;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace MISA.AMIS.Ketoan.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class EmployeesController : BasesController<Employee>
    {
        #region Field
        private IEmployeeBL _employeeBL;
        
        #endregion

        #region Constructor
        public EmployeesController(IEmployeeBL employeeBL) : base(employeeBL)
        {
            _employeeBL = employeeBL;
            
        }
        #endregion

        #region Methods

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>mã nhân viên mới</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        [HttpGet("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                string newEmployeeCode = _employeeBL.GetNewEmployeeCode();

                // Xử lí kết quả trả về
                if (newEmployeeCode != null)
                {
                    return StatusCode(StatusCodes.Status200OK, newEmployeeCode);
                }
                return StatusCode(StatusCodes.Status200OK, 0);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Tìm kiếm và phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="employeeFilter"></param>
        /// <returns> Danh sách nhân viên</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        [HttpGet("filter")]
        public IActionResult Filter([FromQuery] int? pageSize, [FromQuery] int? pageNumber, [FromQuery] string? employeeFilter)
        {
            try
            {
                var res = _employeeBL.Filter(pageSize, pageNumber, employeeFilter);
                return StatusCode(StatusCodes.Status200OK,res);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Thực hiện xóa nhiều
        /// </summary>
        /// <param name="employeeIds"></param>
        /// <returns></returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        [HttpPost("deleteBatch")]
        public IActionResult DeleteBatch([FromBody] List<Guid> employeeIds)
        {
            try
            {
                bool isDeleted = _employeeBL.DeleteBatch(employeeIds);
                if (isDeleted == true)
                {
                    return StatusCode(StatusCodes.Status200OK, employeeIds);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.DeleteFailed,
                    DevMsg = ResourceVN.DevMsg_DeleteFailed,
                    UserMsg = ResourceVN.UserMsg_DeleteFailed,
                    MoreInfo = "https://openapi.misa.com.vn/errorcode/4",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (Exception e)
            {
                    return HandleException(e);
            }
        }

        /// <summary>
        /// Xuất dữ liệu ra file excel
        /// </summary>
        /// <returns>file dữ liệu</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        [HttpGet("ExportToExcel")]
        public IActionResult ExportToExcel()
        {
            try
            {
                var content = _employeeBL.ExportToExcel();
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string filename = "Danh_sach_nhan_vien.xlsx";
                return File(content, contentType, filename);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        #endregion
    }
}