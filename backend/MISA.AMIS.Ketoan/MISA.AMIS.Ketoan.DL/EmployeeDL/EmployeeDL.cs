using Dapper;
using MISA.AMIS.Ketoan.Common.Constants;
using MISA.AMIS.Ketoan.Common.DTO;
using MISA.AMIS.Ketoan.Common.Entities;
using MISA.AMIS.Ketoan.Common.Resources;
using MISA.AMIS.Ketoan.DL;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.AMIS.Ketoan.DL
{
    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    {
        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>mã nhân viên mới</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        public string GetNewEmployeeCode()
        {
            // Chuẩn bị câu lệnh MySQL
            string storedProcedureName = "Proc_employee_GetMaxEmployeeCode";
            long maxEmployeeCode;
            // Thực hiện gọi vào DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                maxEmployeeCode = mySqlConnection.QueryFirstOrDefault<long>(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure);
            }
            /*if(maxEmployeeCode != null)
            {
                string letters = new string(maxEmployeeCode.Where(char.IsLetter).ToArray());
                string digits = new string(maxEmployeeCode.Where(char.IsDigit).ToArray());
                int number = Int32.Parse(digits) + 1;
                string newEmployeeCode = letters + number;
                return newEmployeeCode;
            }*/
            if (maxEmployeeCode != 0)
            {
                long newCode = maxEmployeeCode + 1;
                string newEmployeeCode = "NV" + newCode.ToString();
                return newEmployeeCode;
            }
            return null;
        }

        /// <summary>
        /// Tìm kiếm và phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="employeeFilter"></param>
        /// <returns> Danh sách nhân viên</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        public PagingResult Filter(int? pageSize, int? pageNumber, string? employeeFilter)
        {
            // Kiểm tra dữ liệu
            if (pageSize == 0 || pageSize == null) { pageSize = 10; };
            if (pageNumber == 0 || pageNumber == null) { pageNumber = 1; };

            // Chuẩn bị câu lệnh MySQL
            string storedProcedureName = "Proc_employee_Filter";
            var parameters = new DynamicParameters();
            if (employeeFilter != null)
            {
                parameters.Add("@EmployeeFilter", employeeFilter);
            }
            else
            {
                parameters.Add("@EmployeeFilter", "");
            }

            parameters.Add("@Limit", pageSize);
            parameters.Add("@Offset", (pageNumber - 1) * pageSize);
            
            // Thực hiện gọi vào DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var res = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                var employees = res.Read<Employee>();
                var totalRecord = res.ReadFirst<int>();
                int totalPage = 0;
                if (employees != null)
                {
                    totalPage = (int)Math.Ceiling((double)totalRecord / (int)pageSize);
                }
                else 
                {
                    employees = new List<Employee>();
                }
                return new PagingResult
                {
                    TotalPage = totalPage,
                    TotalRecord = totalRecord,
                    Data = employees
                };
            }
        }

        /// <summary>
        /// Thực hiện xóa nhiều
        /// </summary>
        /// <param name="employeeIds"></param>
        /// <returns></returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        public bool DeleteBatch(List<Guid> employeeIds)
        {
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();

                //Khởi tạo transaction
                var transaction = mySqlConnection.BeginTransaction();

                try
                {
                    // Chuẩn bị câu lệnh MySQL
                    string storedProcedureName = "Proc_employee_DeleteBatch";
                    var result = string.Join("\",\"", employeeIds.ToArray());
                    result = "\"" + result + "\"";
                    var parameters = new DynamicParameters();
                    parameters.Add("@ListID", result);

                    // Thực hiện gọi vào DB
                    mySqlConnection.Execute(storedProcedureName, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);
                    transaction.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}