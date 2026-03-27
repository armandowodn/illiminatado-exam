using illiminatado_exam.Model;

namespace illiminatado_exam.Data.Seed
{
    public class DbSeeder
    {
        public static void Seed(AppDbContext context) {
            //if (context.PayRegs.Any())
            //    return;
            var savePayReg = new List<PayReg>
            {
                new PayReg
                {
                    pay_date = new DateTime(2026, 3, 27, 0, 0, 0, DateTimeKind.Utc),
                    pay_code = "BROADSPIRE-SEMI",
                    employee_id = 67012,
                    last_name = "GARCIA",
                    first_name = "ANNELIE"
                },
            };
            context.PayRegs.RemoveRange(context.PayRegs);
            context.PayRegs.AddRange(savePayReg);
            context.SaveChanges();

            var saveOtReg = new List<OtReg>
            {
                new OtReg
                {
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
            };
            context.OtReg.RemoveRange(context.OtReg);
            context.OtReg.AddRange(saveOtReg);
            context.SaveChanges();


        }
    }
}
