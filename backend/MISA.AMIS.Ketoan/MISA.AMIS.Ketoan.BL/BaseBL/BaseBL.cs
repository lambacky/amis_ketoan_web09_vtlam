using Dapper;
using MISA.AMIS.Ketoan.Common.Constants;
using MISA.AMIS.Ketoan.Common.DTO;
using MISA.AMIS.Ketoan.Common.Entities;
using MISA.AMIS.Ketoan.Common.Resources;
using MISA.AMIS.Ketoan.DL;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationException = MISA.AMIS.Ketoan.Common.DTO.ValidationException;

namespace MISA.AMIS.Ketoan.BL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field
        private IBaseDL<T> _baseDL;
        #endregion

        #region Constructor
        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>sanh sách bản ghi</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        public IEnumerable<T> GetAllRecords()
        {
            return _baseDL.GetAllRecords();
        }

        /// <summary>
        /// Lấy thông tin bản ghi theo ID
        /// </summary>
        /// <param name="recordID"> ID bản ghi</param>
        /// <returns>Thông tin bản ghi</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        public T GetRecordByID(Guid recordID)
        {
            return _baseDL.GetRecordByID(recordID);
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="record">Thông tin bản ghi</param>
        /// <returns>ID nhân viên thêm mới</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        public int Insert(T record)
        {
            var validateResult = ValidateData(record);
            if (!validateResult.Success)
            {
                throw new ValidationException(validateResult.Data);
            }

            return _baseDL.Insert(record);
        }

        /// <summary>
        /// Sửa bản ghi
        /// </summary>
        /// <param name="recordId"> ID bản ghi</param>
        /// <param name="record">bản ghi</param>
        /// <returns>ID nhân viên được sửa</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        public int Update(Guid recordId, T record)
        {
            var validateResult = ValidateData(record);
            if (!validateResult.Success)
            {
                throw new ValidationException(validateResult.Data);
            }

            return _baseDL.Update(recordId, record);
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="recordId">Id bản ghi</param>
        /// <returns>Id bản ghi bị xóa</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        public int Delete(Guid recordId)
        {
            return _baseDL.Delete(recordId);
        }

        /// <summary>
        /// Validate dữ liệu
        /// </summary>
        /// <param name="record"> bản ghi</param>
        /// <returns>lỗi nếu có</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        public ServiceResponse ValidateData(T record)
        {
            // Kiểm tra điều kiện của các dữ liệu
            var properties = typeof(T).GetProperties();
            var validateFailures = new List<string>();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(record);
                var validationAttribute = (ValidationAttribute?)Attribute.GetCustomAttribute(property, typeof(ValidationAttribute));
                if (validationAttribute != null && !validationAttribute.IsValid(propertyValue?.ToString()))
                {
                    validateFailures.Add(validationAttribute.ErrorMessage);
                }
            }

            // Kiểm tra trùng mã
            var recordId = (Guid)typeof(T).GetProperty($"{typeof(T).Name}Id").GetValue(record);

            var recordCode = typeof(T).GetProperty($"{typeof(T).Name}Code").GetValue(record).ToString();

            if (CheckDuplicateRecordCode(recordCode, recordId))
            {
                validateFailures.Add(ResourceVN.DuplicateRecordCode);
            }

            //Xử lí kết quả
            if (validateFailures.Count > 0)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Data = validateFailures
                };
            }
            else
            {
                return new ServiceResponse
                {
                    Success = true,
                    Data = validateFailures
                };
            }
        }

        /// <summary>
        /// Kiểm tra trùng mã
        /// </summary>
        /// <param name="recordCode"></param>
        /// <param name="recordId"></param>
        /// <returns>true nếu trùng, false nếu không trùng</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        private bool CheckDuplicateRecordCode(string recordCode, Guid recordId)
        {
            // Chuẩn bị câu lệnh MySQL
            string storedProcedureName = String.Format(Procedure.GET_RECORD_CODE, typeof(T).Name);
            var parameters = new DynamicParameters();
            parameters.Add($"@{typeof(T).Name}Code", recordCode);

            // Thực hiện gọi vào DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var res = mySqlConnection.QueryFirstOrDefault<T>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                // Xử lí kết quả trả về
                Console.WriteLine(recordId);
                if (res == null)
                {
                    return false;
                } 
                else if ((Guid)typeof(T).GetProperty($"{typeof(T).Name}Id").GetValue(res) == recordId)
                {
                    return false;
                }
                return true;
            }
        }

        #endregion
    }
}
