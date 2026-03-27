using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using illiminatado_exam.Data;
using illiminatado_exam.Model;
using System.IO;

public class PayrollExportService
{
    private readonly AppDbContext _context;
    public PayrollExportService(AppDbContext context)
    {
        _context = context;
    }

    //public List<OtReg> GetOtReg(int payrollNo) {
    //    var fetchOtReg = _context.OtReg
    //        .Where(x => x.payroll == payrollNo)
    //        .Select(x => new
    //        {
    //            x.payroll,
    //            x.pay_date,
    //            x.pay_code,
    //            x.employee_id,
    //            x.last_name,
    //            x.first_name,
    //            x.file_status,
    //            x.ot_code,
    //            x.created_date,
    //            x.cost_center,
    //            x.ot_rate,
    //            x.ot_hours,
    //            x.ot_minutes,
    //            x.ot_amount,
    //            x.ot_np_hours,
    //            x.ot_np_minutes,
    //            x.ot_np_amount,
    //            x.ot_np2_hours,
    //            x.ot_np2_minutes,
    //            x.ot_np2_amount,
    //            x.ot_and_np_amount,
    //            x.recur_start,
    //            x.recur_end,
    //            x.freq,
    //            x.remarks,
    //            x.system_ot_hours,
    //            x.system_ot_time,
    //            x.system_np_hours,
    //            x.system_np_time,
    //            x.from,
    //            x.to,
    //            x.je_cost_center,
    //        })
    //        .ToList();
    //    return fetchOtReg;
    //}
    public byte[] ExportPayroll(int payrollNo)
    {
        using var workbook = new XLWorkbook();

        // ======================
        // SHEET 1 - PAYREG
        // ======================
        var payRegSheet = workbook.Worksheets.Add("PAYREG");


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

        int header_cols = 1;
        int rows = 1;

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
        rows = 1;
        //// ======================
        //// SHEET 1 - PAYREG
        //// ======================
        //var payRegSheet = workbook.Worksheets.Add("PAYROLL REG");

        //int header_cols = 1;
        //payRegSheet.Cell(1, header_cols).Value = "Employee No";
        //payRegSheet.Cell(1, header_cols++).Style.Font.Bold = true;

        //payRegSheet.Cell(1, header_cols).Value = "Name";
        //payRegSheet.Cell(1, header_cols++).Style.Font.Bold = true;

        //payRegSheet.Cell(1, header_cols).Value = "Basic Pay";
        //payRegSheet.Cell(1, header_cols).Style.Font.Bold = true;

        //// SAMPLE DATA (replace later with DB query)
        //payRegSheet.Cell(2, 1).Value = "EMP001";
        //payRegSheet.Cell(2, 2).Value = "Juan Dela Cruz";
        //payRegSheet.Cell(2, 3).Value = 15000;

        //// ======================
        //// SHEET 2 - OT
        //// ======================
        //var otSheet = workbook.Worksheets.Add("OVERTIME");

        //otSheet.Cell(1, 1).Value = "Employee No";
        //otSheet.Cell(1, 2).Value = "Hours";
        //otSheet.Cell(1, 3).Value = "Rate";

        //otSheet.Cell(2, 1).Value = "EMP001";
        //otSheet.Cell(2, 2).Value = 5;
        //otSheet.Cell(2, 3).Value = 120;

        // ======================
        // RETURN FILE
        // ======================
        using var stream = new MemoryStream();
        workbook.SaveAs(stream);

        return stream.ToArray();
    }
}