using ClosedXML;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Presentation;
using EFCore.BulkExtensions;
using illiminatado_exam.Data;
using illiminatado_exam.DTOs;
using illiminatado_exam.Model;
using illiminatado_exam.Services;
using illiminatado_exam.Services.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.ComponentModel;
using TimeAttendanceModel = illiminatado_exam.Model.TimeAttendance;
using TimeAttendanceUpload = illiminatado_exam.Model.Upload.TimeAttendance;

namespace illiminatado_exam.Controllers.TimeAttendance
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly Helpers _helper;
        public UploadController(AppDbContext context, Helpers helpers)
        {
            _context = context;
            _helper = helpers;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }
        [HttpGet("export/{payrollNo}")]
        public IActionResult ExportPayroll(int payrollNo)
        {
            var service = new PayrollExportService(_context, _helper);
            var fileBytes = service.ExportPayroll(payrollNo);
            return File(
                fileBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Payroll_{payrollNo}.xlsx"
            );
        }

        [HttpGet("employee-info")]
        public IActionResult EmployeeInfo() {
            int payrollNo = 64;

            int daily_rate = 0;
            var info = new TimeAttendanceUpload();
            var result = (
                from u in _context.EmpMaster

                join pr in _context.PayRegs
                    on u.employee_id equals pr.employee_id into prGroup
                from pr in prGroup.DefaultIfEmpty()

                join ta in _context.TimeAttendances
                    on u.employee_id equals ta.employee_id into taGroup
                from ta in taGroup.DefaultIfEmpty()

                where ta.payroll_id == payrollNo

                select new
                {
                    u.employee_id,
                    u.last_name,
                    u.first_name,
                    u.file_status,
                    u.tax_status,
                    monthly_salary_rate = pr != null ? pr.monthly_salary_rate : 0,
                    undertime_hrs = ta != null ? ta.undertime_hrs : 0,
                    tardy_hrs = ta != null ? ta.tardy_hrs : 0,
                    absent_days = ta != null ? ta.absent_days : 0,
                    vl_days = ta != null ? ta.vl_days : 0,
                    lwop_days = ta != null ? ta.lwop_days : 0,

                    nit_diff = ta != null ? ta.nit_diff : 0,
                    reg_ot = ta != null ? ta.reg_ot : 0,
                    reg_otNp_hr = ta != null ? ta.reg_otNp_hr : 0,

                    legal_hol_ot = ta != null ? ta.legal_hol_ot : 0,
                    legal_hol_ot_1 = ta != null ? ta.legal_hol_ot_1 : 0,
                    legal_hol_ot_2 = ta != null ? ta.legal_hol_ot_2 : 0,
                    legal_hol_ot_3 = ta != null ? ta.legal_hol_ot_3 : 0,
                    legal_hol_rd = ta != null ? ta.legal_hol_rd : 0,
                    legal_hol_rd_1 = ta != null ? ta.legal_hol_rd_1 : 0,
                    legal_hol_rd_2 = ta != null ? ta.legal_hol_rd_2 : 0,
                    legal_hol_rd_3 = ta != null ? ta.legal_hol_rd_3 : 0,
                    specl_hol_ot = ta != null ? ta.specl_hol_ot : 0,
                    specl_hol_ot_1 = ta != null ? ta.specl_hol_ot_1 : 0,
                    specl_hol_ot_2 = ta != null ? ta.specl_hol_ot_2 : 0,
                    specl_hol_ot_3 = ta != null ? ta.specl_hol_ot_3 : 0,
                    specl_hol_rd = ta != null ? ta.specl_hol_rd : 0,
                    specl_hol_rd_1 = ta != null ? ta.specl_hol_rd_1 : 0,
                    specl_hol_rd_2 = ta != null ? ta.specl_hol_rd_2 : 0,
                    specl_hol_rd_3 = ta != null ? ta.specl_hol_rd_3 : 0,
                    res_day_ot = ta != null ? ta.res_day_ot : 0,
                    res_day_ot_1 = ta != null ? ta.res_day_ot_1 : 0,
                    res_day_ot_2 = ta != null ? ta.res_day_ot_2 : 0,
                    res_day_ot_3 = ta != null ? ta.res_day_ot_3 : 0,
                    dbl_hol_ot = ta != null ? ta.dbl_hol_ot : 0,
                    dbl_hol_ot_1 = ta != null ? ta.dbl_hol_ot_1 : 0,
                    dbl_hol_ot_2 = ta != null ? ta.dbl_hol_ot_2 : 0,
                    dbl_hol_ot_3 = ta != null ? ta.dbl_hol_ot_3 : 0,
                    dbl_hol_rd = ta != null ? ta.dbl_hol_rd : 0,
                    dbl_hol_rd_1 = ta != null ? ta.dbl_hol_rd_1 : 0,
                    dbl_hol_rd_2 = ta != null ? ta.dbl_hol_rd_2 : 0,
                    dbl_hol_rd_3 = ta != null ? ta.dbl_hol_rd_3 : 0,
                }
            ).ToList();
            var setEmpDetails = new List<EmployeeDto>();
            foreach (var _info in result)
            {
                //ATT DEDUCTION
                daily_rate = Convert.ToInt32(_info.monthly_salary_rate);
                decimal[] att_deductionArr = {
                        _helper.getHrsAmount(daily_rate, _info.tardy_hrs),
                        _helper.getHrsAmount(daily_rate, _info.undertime_hrs),
                        _helper.getAbsentAmount(daily_rate, _info.absent_days),
                        _helper.getAbsentAmount(daily_rate, _info.lwop_days)
                    };
                setEmpDetails.Add(new EmployeeDto
                {
                    employee_id = _info.employee_id,
                    last_name = _info.last_name,
                    first_name = _info.first_name,
                    att_deduction = att_deductionArr.Sum(),
                    overtime = _helper.getHrsAmount(daily_rate, _info.nit_diff, Convert.ToDecimal("15"))
                });
            }
            return Ok(new {
                setEmpDetails
            });
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
                            payroll_id = 64,
                            employee_id = checkInt(worksheet.Cells[row, 1].Text),
                            last_name = worksheet.Cells[row, 2].Text,
                            first_name = worksheet.Cells[row, 3].Text,
                            tardy_hrs = checkDeci(worksheet.Cells[row, 5].Text),
                            undertime_hrs = checkDeci(worksheet.Cells[row, 6].Text),
                            absent_days = checkDeci(worksheet.Cells[row, 7].Text),
                            lwop_days = checkDeci(worksheet.Cells[row, 8].Text),
                            vl_days = checkDeci(worksheet.Cells[row, 9].Text),
                            sl_days = checkDeci(worksheet.Cells[row, 10].Text),
                            otherlv_days = checkDeci(worksheet.Cells[row, 11].Text),
                            nit_diff = checkDeci(worksheet.Cells[row, 12].Text),
                            reg_ot = checkDeci(worksheet.Cells[row, 13].Text),
                            reg_otNp_hr = checkDeci(worksheet.Cells[row, 14].Text),

                            legal_hol_ot = checkDeci(worksheet.Cells[row, 15].Text),
                            legal_hol_ot_1 = checkDeci(worksheet.Cells[row, 16].Text),
                            legal_hol_ot_2 = checkDeci(worksheet.Cells[row, 17].Text),
                            legal_hol_ot_3 = checkDeci(worksheet.Cells[row, 18].Text),
                            legal_hol_rd = checkDeci(worksheet.Cells[row, 19].Text),
                            legal_hol_rd_1 = checkDeci(worksheet.Cells[row, 20].Text),
                            legal_hol_rd_2 = checkDeci(worksheet.Cells[row, 21].Text),
                            legal_hol_rd_3 = checkDeci(worksheet.Cells[row, 22].Text),
                            specl_hol_ot = checkDeci(worksheet.Cells[row, 23].Text),
                            specl_hol_ot_1 = checkDeci(worksheet.Cells[row, 24].Text),
                            specl_hol_ot_2 = checkDeci(worksheet.Cells[row, 25].Text),
                            specl_hol_ot_3 = checkDeci(worksheet.Cells[row, 26].Text),
                            specl_hol_rd = checkDeci(worksheet.Cells[row, 27].Text),
                            specl_hol_rd_1 = checkDeci(worksheet.Cells[row, 28].Text),
                            specl_hol_rd_2 = checkDeci(worksheet.Cells[row, 29].Text),
                            specl_hol_rd_3 = checkDeci(worksheet.Cells[row, 30].Text),
                            res_day_ot = checkDeci(worksheet.Cells[row, 31].Text),
                            res_day_ot_1 = checkDeci(worksheet.Cells[row, 32].Text),
                            res_day_ot_2 = checkDeci(worksheet.Cells[row, 33].Text),
                            res_day_ot_3 = checkDeci(worksheet.Cells[row, 34].Text),
                            dbl_hol_ot = checkDeci(worksheet.Cells[row, 35].Text),
                            dbl_hol_ot_1 = checkDeci(worksheet.Cells[row, 36].Text),
                            dbl_hol_ot_2 = checkDeci(worksheet.Cells[row, 37].Text),
                            dbl_hol_ot_3 = checkDeci(worksheet.Cells[row, 38].Text),
                            dbl_hol_rd = checkDeci(worksheet.Cells[row, 39].Text),
                            dbl_hol_rd_1 = checkDeci(worksheet.Cells[row, 40].Text),
                            dbl_hol_rd_2 = checkDeci(worksheet.Cells[row, 41].Text),
                            dbl_hol_rd_3 = checkDeci(worksheet.Cells[row, 42].Text),
                        };
                        _context.TimeAttendances.AddRange(item);
                        _context.SaveChanges();
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

        private int checkInt(string value)
        {
            return (value != "" ? Convert.ToInt32(value) : 0);
        }

        private decimal checkDeci(string value) { 
            return (value != "" ? Convert.ToDecimal(value) : 0);
        }
    }
}
