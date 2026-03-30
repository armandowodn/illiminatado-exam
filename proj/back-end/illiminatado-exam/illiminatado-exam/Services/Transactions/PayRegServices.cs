using illiminatado_exam.Data;
using illiminatado_exam.Model;

namespace illiminatado_exam.Services.Transactions
{
    public class PayRegServices
    {
        private readonly AppDbContext _context;
        public PayRegServices(AppDbContext context)
        {
            _context = context;
        }

        public bool SavePayReg(List<PayReg> data)
        {
            try
            {
                _context.PayRegs.RemoveRange(_context.PayRegs);
                _context.PayRegs.AddRange(data);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
