using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace illiminatado_exam.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ot_reg",
                columns: table => new
                {
                    payroll = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pay_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    pay_code = table.Column<string>(type: "text", nullable: false),
                    employee_id = table.Column<int>(type: "integer", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    file_status = table.Column<string>(type: "text", nullable: false),
                    ot_code = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    cost_center = table.Column<int>(type: "integer", nullable: false),
                    ot_rate = table.Column<decimal>(type: "numeric", nullable: false),
                    ot_hours = table.Column<decimal>(type: "numeric", nullable: false),
                    ot_minutes = table.Column<int>(type: "integer", nullable: false),
                    ot_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    ot_np_hours = table.Column<decimal>(type: "numeric", nullable: false),
                    ot_np_minutes = table.Column<int>(type: "integer", nullable: false),
                    ot_np_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    ot_np2_hours = table.Column<int>(type: "integer", nullable: false),
                    ot_np2_minutes = table.Column<int>(type: "integer", nullable: false),
                    ot_np2_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    ot_and_np_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    recur_start = table.Column<int>(type: "integer", nullable: false),
                    recur_end = table.Column<int>(type: "integer", nullable: false),
                    freq = table.Column<int>(type: "integer", nullable: false),
                    remarks = table.Column<string>(type: "text", nullable: false),
                    system_ot_hours = table.Column<decimal>(type: "numeric", nullable: false),
                    system_ot_time = table.Column<string>(type: "text", nullable: false),
                    system_np_hours = table.Column<decimal>(type: "numeric", nullable: false),
                    system_np_time = table.Column<string>(type: "text", nullable: false),
                    from = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    to = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    je_cost_center = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ot_reg", x => x.payroll);
                });

            migrationBuilder.CreateTable(
                name: "pay_regs",
                columns: table => new
                {
                    payroll_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pay_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    pay_code = table.Column<string>(type: "text", nullable: false),
                    employee_id = table.Column<int>(type: "integer", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    file_status = table.Column<string>(type: "text", nullable: false),
                    tax_status = table.Column<string>(type: "text", nullable: false),
                    monthly_salary_rate = table.Column<decimal>(type: "numeric", nullable: false),
                    basic_salary = table.Column<decimal>(type: "numeric", nullable: false),
                    daily_rate = table.Column<decimal>(type: "numeric", nullable: false),
                    salary_rate_type = table.Column<string>(type: "text", nullable: false),
                    att_deduction = table.Column<string>(type: "text", nullable: false),
                    overtime = table.Column<string>(type: "text", nullable: false),
                    adjustments = table.Column<string>(type: "text", nullable: false),
                    gross_income = table.Column<decimal>(type: "numeric", nullable: false),
                    employee_sss = table.Column<int>(type: "integer", nullable: false),
                    employee_mcr = table.Column<decimal>(type: "numeric", nullable: false),
                    employee_pag_ibig = table.Column<int>(type: "integer", nullable: false),
                    taxable_income = table.Column<string>(type: "text", nullable: false),
                    witholding_tax = table.Column<decimal>(type: "numeric", nullable: false),
                    net_salary_after_tax = table.Column<decimal>(type: "numeric", nullable: false),
                    other_ntx_income = table.Column<decimal>(type: "numeric", nullable: false),
                    loan_payments = table.Column<decimal>(type: "numeric", nullable: false),
                    deductions = table.Column<decimal>(type: "numeric", nullable: false),
                    net_salary = table.Column<decimal>(type: "numeric", nullable: false),
                    employer_sss = table.Column<int>(type: "integer", nullable: false),
                    employer_mcr = table.Column<decimal>(type: "numeric", nullable: false),
                    employer_ec = table.Column<int>(type: "integer", nullable: false),
                    employer_pag_ibig = table.Column<string>(type: "text", nullable: false),
                    payroll_cost = table.Column<decimal>(type: "numeric", nullable: false),
                    previous_ytd_gross = table.Column<decimal>(type: "numeric", nullable: false),
                    previous_ytd_witholding = table.Column<decimal>(type: "numeric", nullable: false),
                    previous_ytd_sss = table.Column<decimal>(type: "numeric", nullable: false),
                    previous_ytd_mcr = table.Column<decimal>(type: "numeric", nullable: false),
                    previous_ytd_pag_ibig = table.Column<int>(type: "integer", nullable: false),
                    previous_ytd_bon_13th_nt = table.Column<decimal>(type: "numeric", nullable: false),
                    previous_ytd_bon_13th_tx = table.Column<decimal>(type: "numeric", nullable: false),
                    payment_type = table.Column<string>(type: "text", nullable: false),
                    bank_acct = table.Column<string>(type: "text", nullable: false),
                    bank_name = table.Column<string>(type: "text", nullable: false),
                    comment_field = table.Column<int>(type: "integer", nullable: false),
                    error_field = table.Column<string>(type: "text", nullable: false),
                    date_employed = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    date_terminated = table.Column<string>(type: "text", nullable: false),
                    cost_center = table.Column<string>(type: "text", nullable: false),
                    currency = table.Column<string>(type: "text", nullable: false),
                    exchange_rate = table.Column<int>(type: "integer", nullable: false),
                    tax_table = table.Column<string>(type: "text", nullable: false),
                    payment_freq = table.Column<string>(type: "text", nullable: false),
                    mtd_gross = table.Column<decimal>(type: "numeric", nullable: false),
                    mtd_basic = table.Column<decimal>(type: "numeric", nullable: false),
                    mtd_sss_employee = table.Column<int>(type: "integer", nullable: false),
                    mtd_mcr_employee = table.Column<decimal>(type: "numeric", nullable: false),
                    mtd_pag_ibig_employee = table.Column<int>(type: "integer", nullable: false),
                    mtd_sss_employer = table.Column<decimal>(type: "numeric", nullable: false),
                    mtd_mcr_employer = table.Column<decimal>(type: "numeric", nullable: false),
                    mtd_ec_employer = table.Column<decimal>(type: "numeric", nullable: false),
                    mtd_pag_ibig_employer = table.Column<int>(type: "integer", nullable: false),
                    mtd_wh_tax = table.Column<decimal>(type: "numeric", nullable: false),
                    monthly_allow = table.Column<int>(type: "integer", nullable: false),
                    mtd_ntx = table.Column<decimal>(type: "numeric", nullable: false),
                    from = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    to = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pay_regs", x => x.payroll_id);
                });

            migrationBuilder.CreateTable(
                name: "time_attend",
                columns: table => new
                {
                    payroll_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employee_id = table.Column<int>(type: "integer", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    underTime_hrs = table.Column<decimal>(type: "numeric", nullable: false),
                    absent_days = table.Column<decimal>(type: "numeric", nullable: false),
                    lwop_days = table.Column<decimal>(type: "numeric", nullable: false),
                    vl_days = table.Column<decimal>(type: "numeric", nullable: false),
                    sl_days = table.Column<decimal>(type: "numeric", nullable: false),
                    otherlv_days = table.Column<decimal>(type: "numeric", nullable: false),
                    nit_diff = table.Column<decimal>(type: "numeric", nullable: false),
                    reg_ot = table.Column<decimal>(type: "numeric", nullable: false),
                    reg_otNp_hr = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_time_attend", x => x.payroll_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ot_reg");

            migrationBuilder.DropTable(
                name: "pay_regs");

            migrationBuilder.DropTable(
                name: "time_attend");
        }
    }
}
