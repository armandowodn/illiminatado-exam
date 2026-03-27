namespace illiminatado_exam.Model.Upload
{
    public class TimeAttendance
    {
        public int payroll_id { get; set; }
        public int employee_id { get; set; }
        public string last_name { get; set; } = string.Empty;
        public string first_name { get; set; } = string.Empty;
        public decimal underTime_hrs { get; set; }
        public decimal absent_days { get; set; }
        public decimal lwop_days { get; set; }
        public decimal vl_days { get; set; }
        public decimal sl_days { get; set; }
        public decimal otherlv_days { get; set; }
        public decimal nit_diff { get; set; }
        public decimal reg_ot { get; set; }
        public decimal reg_otNp_hr { get; set; }
    }
}
