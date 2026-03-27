using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace illiminatado_exam.Model
{
    [Table("pay_regs")]
    public class PayReg
    {
        [Key]
        public int payroll_id { get; set; }
        public DateTime pay_date { get; set; } = DateTime.UtcNow;
        public string pay_code { get; set; } = string.Empty;
        public int employee_id { get; set; }
        public string last_name { get; set; } = string.Empty;
        public string first_name { get; set; } = string.Empty;
        public string file_status { get; set; } = string.Empty;
        public string tax_status { get; set; } = string.Empty;
        public decimal monthly_salary_rate { get; set; }
        public decimal basic_salary { get; set; }
        public decimal daily_rate { get; set; }
        public string salary_rate_type { get; set; } = string.Empty;
        public string att_deduction { get; set; } = string.Empty;
        public string overtime { get; set; } = string.Empty;
        public string adjustments { get; set; } = string.Empty;
        public decimal gross_income { get; set; }
        public int employee_sss { get; set; }
        public decimal employee_mcr { get; set; }
        public int employee_pag_ibig { get; set; }
        public string taxable_income { get; set; } = string.Empty;
        public decimal witholding_tax { get; set; }
        public decimal net_salary_after_tax { get; set; }
        public decimal other_ntx_income { get; set; }
        public decimal loan_payments { get; set; }
        public decimal deductions { get; set; }
        public decimal net_salary { get; set; }
        public int employer_sss { get; set; }
        public decimal employer_mcr { get; set; }
        public int employer_ec { get; set; }
        public string employer_pag_ibig { get; set; } = string.Empty;
        public decimal payroll_cost { get; set; }
        public decimal previous_ytd_gross { get; set; }
        public decimal previous_ytd_witholding { get; set; }
        public decimal previous_ytd_sss { get; set; }
        public decimal previous_ytd_mcr { get; set; }
        public int previous_ytd_pag_ibig { get; set; }
        public decimal previous_ytd_bon_13th_nt { get; set; }
        public decimal previous_ytd_bon_13th_tx { get; set; }
        public string payment_type { get; set; } = string.Empty;
        public string bank_acct { get; set; } = string.Empty;
        public string bank_name { get; set; } = string.Empty;
        public int comment_field { get; set; }
        public string error_field { get; set; } = string.Empty;
        public DateTime date_employed { get; set; } = DateTime.UtcNow;
        public string date_terminated { get; set; } = string.Empty;
        public string cost_center { get; set; } = string.Empty;
        public string currency { get; set; } = string.Empty;
        public int exchange_rate { get; set; }
        public string tax_table { get; set; } = string.Empty;
        public string payment_freq { get; set; } = string.Empty;
        public decimal mtd_gross { get; set; }
        public decimal mtd_basic { get; set; }
        public int mtd_sss_employee { get; set; }
        public decimal mtd_mcr_employee { get; set; }
        public int mtd_pag_ibig_employee { get; set; }
        public decimal mtd_sss_employer { get; set; }
        public decimal mtd_mcr_employer { get; set; }
        public decimal mtd_ec_employer { get; set; }
        public int mtd_pag_ibig_employer { get; set; }
        public decimal mtd_wh_tax { get; set; }
        public int monthly_allow { get; set; }
        public decimal mtd_ntx { get; set; }
        public DateTime from { get; set; } = DateTime.UtcNow;
        public DateTime to { get; set; } = DateTime.UtcNow;
    }
}
