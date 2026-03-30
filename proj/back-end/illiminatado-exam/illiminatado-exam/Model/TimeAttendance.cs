using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace illiminatado_exam.Model
{
    [Table("time_attend")]
    public class TimeAttendance
    {
        [Key]
        public int id { get; set; }
        public int payroll_id { get; set; }
        public int employee_id { get; set; }
        public string last_name { get; set; } = string.Empty;
        public string first_name { get; set; } = string.Empty;
        public decimal tardy_hrs { get; set; }
        public decimal undertime_hrs { get; set; }
        public decimal absent_days { get; set; }
        public decimal lwop_days { get; set; }
        public decimal vl_days { get; set; }
        public decimal sl_days { get; set; }
        public decimal otherlv_days { get; set; }
        public decimal nit_diff { get; set; }
        public decimal reg_ot { get; set; }
        public decimal reg_otNp_hr { get; set; }

        public decimal legal_hol_ot { get; set; }
        public decimal legal_hol_ot_1 { get; set; }
        public decimal legal_hol_ot_2 { get; set; }
        public decimal legal_hol_ot_3 { get; set; }

        public decimal legal_hol_rd { get; set; }
        public decimal legal_hol_rd_1 { get; set; }
        public decimal legal_hol_rd_2 { get; set; }
        public decimal legal_hol_rd_3 { get; set; }

        public decimal specl_hol_ot { get; set; }
        public decimal specl_hol_ot_1 { get; set; }
        public decimal specl_hol_ot_2 { get; set; }
        public decimal specl_hol_ot_3 { get; set; }

        public decimal specl_hol_rd { get; set; }
        public decimal specl_hol_rd_1 { get; set; }
        public decimal specl_hol_rd_2 { get; set; }
        public decimal specl_hol_rd_3 { get; set; }

        public decimal res_day_ot { get; set; }
        public decimal res_day_ot_1 { get; set; }
        public decimal res_day_ot_2 { get; set; }
        public decimal res_day_ot_3 { get; set; }

        public decimal dbl_hol_ot { get; set; }
        public decimal dbl_hol_ot_1 { get; set; }
        public decimal dbl_hol_ot_2 { get; set; }
        public decimal dbl_hol_ot_3 { get; set; }

        public decimal dbl_hol_rd { get; set; }
        public decimal dbl_hol_rd_1 { get; set; }
        public decimal dbl_hol_rd_2 { get; set; }
        public decimal dbl_hol_rd_3 { get; set; }
    }
}
