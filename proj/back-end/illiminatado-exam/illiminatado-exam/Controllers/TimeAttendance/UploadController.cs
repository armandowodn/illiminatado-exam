using illiminatado_exam.Data;
using illiminatado_exam.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using TimeAttendanceModel = illiminatado_exam.Model.TimeAttendance;

using OfficeOpenXml;
using ClosedXML.Excel;
using EFCore.BulkExtensions;


namespace illiminatado_exam.Controllers.TimeAttendance
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UploadController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }
        [HttpGet("export/{payrollNo}")]
        public IActionResult ExportPayroll(int payrollNo)
        {
            var service = new PayrollExportService(_context);
            var fileBytes = service.ExportPayroll(payrollNo);
            return File(
                fileBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Payroll_{payrollNo}.xlsx"
            );
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            int insertedRows = 0;
            string excelPassword = "In1";
            int sheetNo = 2;
            int rowNoStart = 2;
            string msgType = "invalid";
            bool success = false;
            string msg = "Invalid Upload";

            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var list = new List<TimeAttendanceModel>();
            using (var stream = new MemoryStream()) {
                await file.CopyToAsync(stream);
                stream.Position = 0;
                using (var package = new ExcelPackage(stream, excelPassword)) {
                    var worksheet = package.Workbook.Worksheets[sheetNo];
                    int rowCount = worksheet.Dimension.Rows;
                    
                    for (int row = rowNoStart; row <= rowCount; row++) {
                        var item = new TimeAttendanceModel
                        {
                            employee_id = Convert.ToInt32(worksheet.Cells[row, 1].Text),
                            last_name = worksheet.Cells[row, 2].Text,
                            first_name = worksheet.Cells[row, 3].Text,
                            underTime_hrs = checkDeci(worksheet.Cells[row, 6].Text),
                            absent_days = checkDeci(worksheet.Cells[row, 7].Text),
                            lwop_days = checkDeci(worksheet.Cells[row, 8].Text),
                            vl_days = checkDeci(worksheet.Cells[row, 9].Text),
                            sl_days = checkDeci(worksheet.Cells[row, 10].Text),
                            otherlv_days = checkDeci(worksheet.Cells[row, 11].Text),
                            nit_diff = checkDeci(worksheet.Cells[row, 12].Text),
                            reg_ot = checkDeci(worksheet.Cells[row, 13].Text),
                            reg_otNp_hr = checkDeci(worksheet.Cells[row, 14].Text),
                        };
                        _context.TimeAttendances.AddRange(item);
                        _context.SaveChanges();
                        //var item = new TimeAttendanceModel
                        //{
                        //    WorkDate = DateTime.Parse(worksheet.Cells[row, 1].Text),
                        //    EmployeeNo = int.Parse(worksheet.Cells[row, 2].Text),
                        //    EmployeeName = worksheet.Cells[row, 3].Text,
                        //    TimeIn = string.IsNullOrEmpty(worksheet.Cells[row, 4].Text)
                        //        ? null
                        //        : DateTime.Parse(worksheet.Cells[row, 4].Text),
                        //    TimeOut = string.IsNullOrEmpty(worksheet.Cells[row, 5].Text)
                        //        ? null
                        //        : DateTime.Parse(worksheet.Cells[row, 5].Text)
                        //};
                        insertedRows++;
                    }
                }
            }
                //success
                msgType = "success";
            success = true;
            msg = "Successfully Uploaded";
            return Ok(new {
                msgType = msgType,
                msg = msg,
                success = success,
                count = insertedRows
            });
        }

        private decimal checkDeci(string value) { 
            return (value != "" ? Convert.ToDecimal(value) : 0);
        }
    }
}
