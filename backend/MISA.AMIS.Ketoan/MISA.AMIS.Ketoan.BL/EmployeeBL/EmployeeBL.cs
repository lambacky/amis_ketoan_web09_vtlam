using Dapper;
using MISA.AMIS.Ketoan.Common.Constants;
using MISA.AMIS.Ketoan.Common.DTO;
using MISA.AMIS.Ketoan.Common.Entities;
using MISA.AMIS.Ketoan.Common.Resources;
using MISA.AMIS.Ketoan.DL;
using MySqlConnector;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ValidationException = MISA.AMIS.Ketoan.Common.DTO.ValidationException;

namespace MISA.AMIS.Ketoan.BL
{
    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {
        #region Field
        private IEmployeeDL _employeeDL;
        #endregion

        #region Constructor
        public EmployeeBL(IEmployeeDL employeeDL) : base(employeeDL)
        {
            _employeeDL = employeeDL;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>mã nhân viên mới</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        public string GetNewEmployeeCode()
        {
            return _employeeDL.GetNewEmployeeCode();
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
            return _employeeDL.Filter(pageSize,pageNumber,employeeFilter);
        }

        /// <summary>
        /// Thực hiện xóa nhiều
        /// </summary>
        /// <param name="employeeIds"></param>
        /// <returns></returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        public bool DeleteBatch(List<Guid> employeeIds)
        {
            return _employeeDL.DeleteBatch(employeeIds);
        }

        /// <summary>
        /// Xuất dữ liệu ra file excel
        /// </summary>
        /// <returns>file dữ liệu</returns>
        /// Created by: Vũ Tùng Lâm (6/11/2022)
        public MemoryStream ExportToExcel()
        {
            var employees = GetAllRecords();
            var dataExcelEmployees = new List<DataExcelEmployee>();
            var index = 1;
            foreach (var employee in employees)
            {
                dataExcelEmployees.Add(new DataExcelEmployee(index, employee));
                index++;
            }

            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("DANH SÁCH NHÂN VIÊN");
                workSheet.Cells.LoadFromCollection(dataExcelEmployees, false);
                // Thêm 3 dòng trên cùng
                workSheet.InsertRow(1, 3);
                // Thêm tiêu đề lớn
                workSheet.Cells["A1:I1"].Merge = true;
                workSheet.Cells["A2:I2"].Merge = true;
                workSheet.Cells["A1"].LoadFromText("DANH SÁCH NHÂN VIÊN");
                workSheet.Cells["A1"].Style.Font.Bold = true;
                workSheet.Cells["A1"].Style.Font.Size = 16;
                workSheet.Cells["A1"].Style.Font.Name = "Arial";
                
                // Thêm tiêu đề các cột
                workSheet.Cells[3, 1].LoadFromText("STT, Mã nhân viên, Tên nhân viên, Giới tính, Ngày sinh, Chức danh, Tên đơn vị, Số tài khoản, Tên ngân hàng");
                workSheet.Cells["A3:I3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Cells["A3:I3"].Style.Font.Bold = true;
                workSheet.Cells["A3:I3"].Style.Font.Size = 10;
                workSheet.Cells["A3:I3"].Style.Font.Name = "Arial";
                workSheet.Cells["A3:I3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells["A3:I3"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#D8D8D8"));
                // Căn độ rộng các cột
                int[] tableWidth = { 5, 15, 20, 10, 15, 20, 25, 18, 20 };
                for (int i = 0; i < tableWidth.Length; i++)
                {
                    workSheet.Columns[i + 1].Width = tableWidth[i];
                }
                // Format ngày sinh
                workSheet.Columns[5].Style.Numberformat.Format = "dd/mm/yyyy";

                // Căn giữa ngày sinh
                workSheet.Columns[5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //Căn trái STT
                workSheet.Columns[1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                //Căn giữa tiêu đề
                workSheet.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                // Set Border
                using (var range = workSheet.Cells[$"A3:I{dataExcelEmployees.Count() + 3}"])
                {
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                }
                // Lưu pack
                package.Save();
            }
            stream.Position = 0;
            return stream;
        }

        #endregion
    }
}