using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace illiminatado_exam.Model
{
    [Table("tbl_m_employee")]
    public class EmpMaster
    {
        [Key]
        public int id { get; set; }
        public int employee_id { get; set; }
        public string last_name { get; set; } = string.Empty;
        public string first_name { get; set; } = string.Empty;
        public string file_status { get; set; } = string.Empty;
        public string tax_status { get; set; } = string.Empty;
        public decimal daily_rate { get; set; }
    }
}
