using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace illiminatado_exam.Model
{
    [Table("ot_reg")]
    public class OtReg
    {
        [Key]
        public int payroll { get; set; }
        public DateTime pay_date { get; set; } = DateTime.UtcNow;
        public string pay_code { get; set; } = string.Empty;
        public int employee_id { get; set; }
        public string last_name { get; set; } = string.Empty;
        public string first_name { get; set; } = string.Empty;
        public string file_status { get; set; } = string.Empty;
        public string ot_code { get; set; } = string.Empty;
        public DateTime created_date { get; set; } = DateTime.UtcNow;
        public int cost_center { get; set; }
        public decimal ot_rate { get; set; }
        public decimal ot_hours { get; set; }
        public int ot_minutes { get; set; }
        public decimal ot_amount { get; set; }
        public decimal ot_np_hours { get; set; }
        public int ot_np_minutes { get; set; }
        public decimal ot_np_amount { get; set; }
        public int ot_np2_hours { get; set; }
        public int ot_np2_minutes { get; set; }
        public decimal ot_np2_amount { get; set; }
        public decimal ot_and_np_amount { get; set; }
        public int recur_start { get; set; }
        public int recur_end { get; set; }
        public int freq { get; set; }
        public string remarks { get; set; } = string.Empty;
        public decimal system_ot_hours { get; set; }
        public string system_ot_time { get; set; } = string.Empty;
        public decimal system_np_hours { get; set; }
        public string system_np_time { get; set; }
        public DateTime from { get; set; } = DateTime.UtcNow;
        public DateTime to { get; set; } = DateTime.UtcNow;
        public int je_cost_center { get; set; }
    }
}
