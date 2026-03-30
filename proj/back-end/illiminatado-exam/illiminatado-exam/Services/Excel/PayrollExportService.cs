using ClosedXML.Excel;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Spreadsheet;
using illiminatado_exam.Data;
using illiminatado_exam.Model;
using illiminatado_exam.Services;
using System.IO;

public class PayrollExportService
{
    private readonly AppDbContext _context;
    private readonly Helpers _helper;
    public PayrollExportService(AppDbContext context, Helpers helpers)
    {
        _context = context;
        _helper = helpers;
    }

    public byte[] ExportPayroll(int payrollNo)
    {
        using var workbook = new XLWorkbook();
        int header_cols = 1;
        int rows = 1;
        // ======================
        // SHEET 1 - PAYREG
        // ======================
        var payRegSheet = workbook.Worksheets.Add("PAYREG");
        string[] headers1 =
        {
            "PAYROLL #","PAY DATE","PAY CODE","EMPLOYEE ID#","LAST NAME","FIRST NAME",
            "FILE STATUS","TAX STATUS","MONTHLY SALARY RATE","BASIC SALARY",
            "DAILY RATE","SALARY RATE TYPE","ATT DEDUCTION","OVERTIME"
        };
        payRegSheet.Row(1).Style.Font.Bold = true;
        for (int i = 0; i < headers1.Length; i++)
        {
            payRegSheet.Cell(1, i + 1).Value = headers1[i];
        }
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
                    pr.pay_date,
                    pr.pay_code,
                    pr.salary_rate_type,
                    ta.payroll_id,
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
                }
            ).ToList();
        rows = 2;
        foreach (var item in result)
        {
            payRegSheet.Cell($"A{rows}").Value = item.payroll_id;
            payRegSheet.Cell($"B{rows}").Value = item.pay_date.ToString("M/d/yyyy");
            payRegSheet.Cell($"C{rows}").Value = item.pay_code;
            payRegSheet.Cell($"D{rows}").Value = item.employee_id;
            payRegSheet.Cell($"E{rows}").Value = item.last_name;
            payRegSheet.Cell($"F{rows}").Value = item.first_name;
            payRegSheet.Cell($"G{rows}").Value = item.file_status;
            payRegSheet.Cell($"H{rows}").Value = item.tax_status;
            payRegSheet.Cell($"I{rows}").Value = item.monthly_salary_rate;

            payRegSheet.Cell($"J{rows}").Style.NumberFormat.Format = "#,##0.00";
            payRegSheet.Cell($"J{rows}").Value = Math.Round(item.monthly_salary_rate, 2);
            payRegSheet.Cell($"K{rows}").Value = Math.Round(Convert.ToDecimal(item.monthly_salary_rate) * 12 / 261, 2);
            payRegSheet.Cell($"L{rows}").Value = item.salary_rate_type;

            decimal[] att_deductionArr = {
                        _helper.getHrsAmount(Convert.ToInt32(item.monthly_salary_rate), item.tardy_hrs),
                        _helper.getHrsAmount(Convert.ToInt32(item.monthly_salary_rate), item.undertime_hrs),
                        _helper.getAbsentAmount(Convert.ToInt32(item.monthly_salary_rate), item.absent_days),
                        _helper.getAbsentAmount(Convert.ToInt32(item.monthly_salary_rate), item.lwop_days)
                    };

            payRegSheet.Cell($"M{rows}").Value = att_deductionArr.Sum();
            payRegSheet.Cell($"N{rows}").Value = _helper.getHrsAmount(Convert.ToInt32(item.monthly_salary_rate), item.nit_diff, Convert.ToDecimal("15"));
            rows++;
        }


        header_cols = 1;
        rows = 1;
        // ======================
        // SHEET 2 - OTR
        // ======================
        var fetchOtReg = _context.OtReg
            .Where(x => x.employee_id == payrollNo)
            .Select(x => new
            {
                x.payroll,
                x.pay_date,
                x.pay_code,
                x.employee_id,
                x.last_name,
                x.first_name,
                x.file_status,
                x.ot_code,
                x.created_date,
                x.cost_center,
                x.ot_rate,
                x.ot_hours,
                x.ot_minutes,
                x.ot_amount,
                x.ot_np_hours,
                x.ot_np_minutes,
                x.ot_np_amount,
                x.ot_np2_hours,
                x.ot_np2_minutes,
                x.ot_np2_amount,
                x.ot_and_np_amount,
                x.recur_start,
                x.recur_end,
                x.freq,
                x.remarks,
                x.system_ot_hours,
                x.system_ot_time,
                x.system_np_hours,
                x.system_np_time,
                x.from,
                x.to,
                x.je_cost_center,
            })
            .ToList();
        var otrSheet = workbook.Worksheets.Add("OTR");

        

        string[] headers = new string[]
        {
            "PAYROLL #",
            "PAY DATE",
            "PAY CODE",
            "EMPLOYEE ID#",
            "LAST NAME",
            "FIRST NAME",
            "FILE STATUS",
            "OT CODE",
            "DATE",
            "COST CENTER",
            "OT RATE",
            "OT HOURS",
            "OT MINUTES",
            "OT AMOUNT",
            "OT NP HOURS",
            "OT NP MINUTES",
            "OT NP AMOUNT",
            "OT NP2 HOURS",
            "OT NP2 MINUTES",
            "OT NP2 AMOUNT",
            "OT AND NP AMOUNT",
            "RECUR START",
            "RECUR END",
            "FREQ",
            "REMARKS",
            "SYSTEM OT HOURS",
            "SYSTEM OT TIME",
            "SYSTEM NP HOURS",
            "SYSTEM NP TIME",
            "FROM",
            "TO",
            "JE COST CENTER",
        };
        foreach (var header in headers)
        {
            otrSheet.Cell(rows, header_cols).Value = header;
            otrSheet.Cell(rows, header_cols).Style.Font.Bold = true;

            header_cols++;
        }

        rows = 2;
        
        foreach (var item in fetchOtReg)
        {
            string strDate = "";
            otrSheet.Cell($"A{rows}").Value = item.payroll;
            strDate = item.pay_date.ToString("M/d/yyyy");
            otrSheet.Cell($"B{rows}").Value = strDate;
            otrSheet.Cell($"C{rows}").Value = item.pay_code;
            otrSheet.Cell($"D{rows}").Value = item.employee_id;
            otrSheet.Cell($"E{rows}").Value = item.last_name;
            otrSheet.Cell($"F{rows}").Value = item.first_name;
            otrSheet.Cell($"G{rows}").Value = item.file_status;
            otrSheet.Cell($"H{rows}").Value = item.ot_code;
            strDate = item.created_date.ToString("M/d/yyyy");
            otrSheet.Cell($"I{rows}").Value = strDate;
            otrSheet.Cell($"J{rows}").Value = item.cost_center;
            otrSheet.Cell($"K{rows}").Value = item.ot_rate;
            otrSheet.Cell($"L{rows}").Value = item.ot_hours;
            otrSheet.Cell($"M{rows}").Value = item.ot_minutes;
            otrSheet.Cell($"N{rows}").Value = item.ot_amount;
            otrSheet.Cell($"O{rows}").Value = item.ot_np_hours;
            otrSheet.Cell($"P{rows}").Value = item.ot_np_minutes;
            otrSheet.Cell($"Q{rows}").Value = item.ot_np_amount;
            otrSheet.Cell($"R{rows}").Value = item.ot_np2_hours;
            otrSheet.Cell($"S{rows}").Value = item.ot_np2_minutes;
            otrSheet.Cell($"T{rows}").Value = item.ot_np2_amount;
            otrSheet.Cell($"U{rows}").Value = item.ot_and_np_amount;
            otrSheet.Cell($"V{rows}").Value = item.recur_start;
            otrSheet.Cell($"W{rows}").Value = item.recur_end;
            otrSheet.Cell($"X{rows}").Value = item.freq;
            otrSheet.Cell($"Y{rows}").Value = item.remarks;
            otrSheet.Cell($"Z{rows}").Value = item.system_ot_hours;
            otrSheet.Cell($"AA{rows}").Value = item.system_ot_time;
            otrSheet.Cell($"AB{rows}").Value = item.system_np_hours;
            otrSheet.Cell($"AC{rows}").Value = item.system_np_time;
            strDate = item.from.ToString("M/d/yyyy");
            otrSheet.Cell($"AD{rows}").Value = strDate;
            strDate = item.to.ToString("M/d/yyyy");
            otrSheet.Cell($"AE{rows}").Value = strDate;
            otrSheet.Cell($"AF{rows}").Value = item.je_cost_center;
            rows++;
        }
        // ======================
        // RETURN FILE
        // ======================
        using var stream = new MemoryStream();
        workbook.SaveAs(stream);

        return stream.ToArray();
    }
}