using DocumentFormat.OpenXml.InkML;
using illiminatado_exam.Model;
using illiminatado_exam.Services.Master;
using illiminatado_exam.Services.Transactions;

namespace illiminatado_exam.Data.Seed
{
    public class DbSeeder
    {
        private readonly EmployeeMasterServices _employeeService;
        private readonly PayRegServices _payRegService;
        private readonly OtRegServices _otRegService;
        public DbSeeder(EmployeeMasterServices employeeService, PayRegServices payRegService, OtRegServices otRegService)
        {
            _employeeService = employeeService;
            _payRegService = payRegService;
            _otRegService = otRegService;
        }
        public static void Seed(AppDbContext context) {
            seed_EmpMaster(context);
            seed_PayReg(context);
            seed_OtReg(context);
        }
        private static void seed_EmpMaster(AppDbContext context) {
            var save = new List<EmpMaster>
            {
                new EmpMaster
                {
                    employee_id = 85403,
                    last_name = "ABUY",
                    first_name =  "JESSEN",
                    file_status = "EMPLOYEE",
                    tax_status = "S",
                    daily_rate = 0
                },
                new EmpMaster
                {
                    employee_id = 85206,
                    last_name = "MAGNO",
                    first_name =  "CLARKE",
                    file_status = "EMPLOYEE",
                    tax_status = "S",
                    daily_rate = 0
                },
            };
            var service = new EmployeeMasterServices(context);
            service.SaveEmployee(save);
        }
        private static void seed_PayReg(AppDbContext context)
        {
            var save = new List<PayReg>
            {
                new PayReg
                {
                    pay_date = new DateTime(2020, 3, 31, 0, 0, 0, DateTimeKind.Utc),
                    pay_code = "BROADSPIRE-SEMI",
                    employee_id = 85206,
                    salary_rate_type = "PER MONTH",
                    monthly_salary_rate = 22000
                },
                new PayReg
                {
                    pay_date = new DateTime(2020, 3, 31, 0, 0, 0, DateTimeKind.Utc),
                    pay_code = "BROADSPIRE-SEMI",
                    salary_rate_type = "PER MONTH",
                    employee_id = 85403,
                    monthly_salary_rate = 21292
                },
            };
            var service = new PayRegServices(context);
            service.SavePayReg(save);
        }
        private static void seed_OtReg(AppDbContext context)
        {
            var save = new List<OtReg>
            {
                new OtReg
                {
                    payroll = 64,
                    pay_date = new DateTime(2026, 3, 27, 0, 0, 0, DateTimeKind.Utc),
                    pay_code = "BROADSPIRE-SEMI",
                    employee_id = 67008,
                    last_name = "GARCIA",
                    first_name = "ANNELIE",
                    file_status = "EMPLOYEE",
                    ot_code = "NITE-DIFF",
                    created_date = new DateTime(2026, 3, 27, 0, 0, 0, DateTimeKind.Utc),
                    cost_center = 5035,
                    ot_rate = 0.15m,
                    ot_hours = 80m,
                    ot_minutes = 0,
                    ot_amount = 3426.7m,
                    ot_np_hours = 0,
                    ot_np_minutes = 0,
                    ot_np_amount = 0,
                    ot_np2_hours = 0,
                    ot_np2_minutes = 0,
                    ot_np2_amount = 0,
                    ot_and_np_amount = 3426.7m,
                    recur_start = 0,
                    recur_end = 0,
                    freq = 0,
                    remarks = "UPLOADED FROM EXCEL",
                    system_ot_hours = 80m,
                    system_ot_time = "00080:00000",
                    system_np_hours = 0m,
                    system_np_time = "00080:00000",
                    from = new DateTime(2026, 3, 27, 0, 0, 0, DateTimeKind.Utc),
                    to = new DateTime(2026, 3, 27, 0, 0, 0, DateTimeKind.Utc),
                    je_cost_center = 5035,
                },
                new OtReg
                {
                    payroll = 64,
                    pay_date = new DateTime(2026, 3, 27, 0, 0, 0, DateTimeKind.Utc),
                    pay_code = "BROADSPIRE-SEMI",
                    employee_id = 85206,
                    last_name = "MAGNO",
                    first_name = "CLARKE",
                    file_status = "EMPLOYEE",
                    ot_code = "NITE-DIFF",
                    created_date = new DateTime(2026, 3, 27, 0, 0, 0, DateTimeKind.Utc),
                    cost_center = 5035,
                    ot_rate = 0.15m,
                    ot_hours = 80m,
                    ot_minutes = 0,
                    ot_amount = 3426.7m,
                    ot_np_hours = 0,
                    ot_np_minutes = 0,
                    ot_np_amount = 0,
                    ot_np2_hours = 0,
                    ot_np2_minutes = 0,
                    ot_np2_amount = 0,
                    ot_and_np_amount = 3426.7m,
                    recur_start = 0,
                    recur_end = 0,
                    freq = 0,
                    remarks = "UPLOADED FROM EXCEL",
                    system_ot_hours = 80m,
                    system_ot_time = "00080:00000",
                    system_np_hours = 0m,
                    system_np_time = "00080:00000",
                    from = new DateTime(2026, 3, 27, 0, 0, 0, DateTimeKind.Utc),
                    to = new DateTime(2026, 3, 27, 0, 0, 0, DateTimeKind.Utc),
                    je_cost_center = 5035,
                },
            };
            var service = new OtRegServices(context);
            service.SaveOtReg(save);
        }
    }
}
