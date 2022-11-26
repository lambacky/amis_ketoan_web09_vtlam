using Dapper;
using MISA.AMIS.Ketoan.Common.Constants;
using MISA.AMIS.Ketoan.Common.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Ketoan.DL
{
    public class BaseDL<T> : IBaseDL<T>
    {
        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>sanh sách bản ghi</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        public IEnumerable<T> GetAllRecords()
        {
            // Chuẩn bị câu lệnh MySQL
            string storedProcedureName = String.Format(Procedure.GET_ALL, typeof(T).Name);

            // Thực hiện gọi vào DB

            var records = new List<T>();

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                records = (List<T>)mySqlConnection.Query<T>(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure);
            }
          
            return records;

        }

        /// <summary>
        /// Lấy thông tin bản ghi theo ID
        /// </summary>
        /// <param name="recordID"> ID bản ghi</param>
        /// <returns>Thông tin bản ghi</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        public T GetRecordByID(Guid recordID)
        {
            // Chuẩn bị câu lệnh MySQL
            string storedProcedureName = String.Format(Procedure.GET_BY_ID, typeof(T).Name);
            var parameters = new DynamicParameters();
            parameters.Add($"@{typeof(T).Name}Id", recordID);

            // Thực hiện gọi vào DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var record = mySqlConnection.QueryFirstOrDefault<T>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return record;
            }
            
        }
        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="record">Thông tin bản ghi</param>
        /// <returns>ID nhân viên thêm mới</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        public int Insert(T record)
        {
            typeof(T).GetProperty($"{typeof(T).Name}Id").SetValue(record, Guid.NewGuid());
            

            // Chuẩn bị câu lệnh MySQL
            string storedProcedureName = String.Format(Procedure.INSERT, typeof(T).Name);

            // Thực hiện gọi vào DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                int numberOfRowsAffected = mySqlConnection.Execute(storedProcedureName, record, commandType: System.Data.CommandType.StoredProcedure);
                return numberOfRowsAffected;
            }
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
            // Chuẩn bị câu lệnh MySQL
            string storedProcedureName = String.Format(Procedure.UPDATE, typeof(T).Name);

            // Thực hiện gọi vào DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                int numberOfRowsAffected = mySqlConnection.Execute(storedProcedureName, record, commandType: System.Data.CommandType.StoredProcedure);
                return numberOfRowsAffected;
            }
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="recordId">Id bản ghi</param>
        /// <returns>Id bản ghi bị xóa</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        public int Delete(Guid recordId)
        {
            string storedProcedureName = String.Format(Procedure.DELETE, typeof(T).Name);
            var parameters = new DynamicParameters();
            parameters.Add($"@{typeof(T).Name}Id", recordId);

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                int numberOfRowsAffected = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return numberOfRowsAffected;
            }
        }
    }
}
