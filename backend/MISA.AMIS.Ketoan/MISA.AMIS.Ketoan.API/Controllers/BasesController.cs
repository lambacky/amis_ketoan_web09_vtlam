using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Ketoan.BL;
using MISA.AMIS.Ketoan.Common.DTO;
using MISA.AMIS.Ketoan.Common.Entities;
using MISA.AMIS.Ketoan.Common.Enums;
using MISA.AMIS.Ketoan.Common.Resources;
using MySqlConnector;

namespace MISA.AMIS.Ketoan.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {
        #region Field
        private IBaseBL<T> _baseBL;
        #endregion

        #region Constructor
        public BasesController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>sanh sách bản ghi</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        [HttpGet]
        public IActionResult GetAllRecords()
        {
            try
            {
                var records = _baseBL.GetAllRecords();
                return StatusCode(StatusCodes.Status200OK, records);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Lấy thông tin bản ghi theo ID
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns>Thông tin bản ghi</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        [HttpGet("{recordID}")]
        public IActionResult GetRecordByID([FromRoute] Guid recordID)
        {
            try
            {
                var record = _baseBL.GetRecordByID(recordID);
                // Xử lí kết quả trả về
                if (record != null)
                {
                    return StatusCode(StatusCodes.Status200OK, record);
                }
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Thêm mới nhân viên
        /// </summary>
        /// <param name="record">Thông tin bản ghi</param>
        /// <returns>ID nhân viên thêm mới</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        [HttpPost]
        public IActionResult Insert([FromBody] T record)
        {
            try
            {
                int numberOfRowsAffected = _baseBL.Insert(record);
                // Xử lí kết quả trả về
                if (numberOfRowsAffected > 0)
                {
                    return StatusCode(StatusCodes.Status201Created, record.GetType().GetProperty($"{typeof(T).Name}Id").GetValue(record));
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.InsertFailed,
                    DevMsg = ResourceVN.DevMsg_InsertFailed,
                    UserMsg = ResourceVN.UserMsg_InsertFailed,
                    MoreInfo = "https://openapi.misa.com.vn/errorcode/2",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (ValidationException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                {
                    ErrorCode = ErrorCode.InvalidData,
                    DevMsg = ResourceVN.DevMsg_InvalidData,
                    UserMsg = ResourceVN.UserMsg_InvalidData,
                    MoreInfo = e.validateFailures,
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Sửa bản ghi
        /// </summary>
        /// <param name="recordId"> ID bản ghi</param>
        /// <param name="record">bản ghi</param>
        /// <returns>ID nhân viên được sửa</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        [HttpPut("{recordId}")]
        public IActionResult Update([FromRoute] Guid recordId, [FromBody] T record)
        {
            try
            {
                int numberOfRowsAffected = _baseBL.Update(recordId, record);

                // Xử lí kết quả trả về
                if (numberOfRowsAffected > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, recordId);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.UpdateFailed,
                    DevMsg = ResourceVN.DevMsg_UpdateFailed,
                    UserMsg = ResourceVN.DevMsg_UpdateFailed,
                    MoreInfo = "https://openapi.misa.com.vn/errorcode/3",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (ValidationException e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                {
                    ErrorCode = ErrorCode.InvalidData,
                    DevMsg = ResourceVN.DevMsg_InvalidData,
                    UserMsg = ResourceVN.UserMsg_InvalidData,
                    MoreInfo = e.validateFailures,
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (Exception e)
            {
                if (e.Message == ResourceVN.DuplicateEmployeeCode)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = ErrorCode.DuplicateCode,
                        DevMsg = ResourceVN.DevMsg_UpdateFailed,
                        UserMsg = e.Message,
                        MoreInfo = "https://openapi.misa.com.vn/errorcode/6",
                        TraceId = HttpContext.TraceIdentifier
                    });
                }
                return HandleException(e);
            }
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="recordId">Id bản ghi</param>
        /// <returns>Id bản ghi bị xóa</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        [HttpDelete("{recordId}")]
        public IActionResult Delete([FromRoute] Guid recordId)
        {
            try
            {
                int numberOfRowsAffected = _baseBL.Delete(recordId);

                // Xử lí kết quả trả về
                if (numberOfRowsAffected > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, recordId);
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
        /// Xử lí Exception
        /// </summary>
        /// <param name="e"></param>
        /// <returns>Status 500 và lỗi</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        protected IActionResult HandleException(Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
            {
                ErrorCode = ErrorCode.Exception,
                DevMsg = e.Message,
                UserMsg = ResourceVN.UserMsg_Exception,
                MoreInfo = ResourceVN.MoreInfo_Exception,
                TraceId = HttpContext.TraceIdentifier
            });
        }

        #endregion
    }
}
