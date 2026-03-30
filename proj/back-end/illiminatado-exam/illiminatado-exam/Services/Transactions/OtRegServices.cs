using illiminatado_exam.Data;
using illiminatado_exam.Model;

namespace illiminatado_exam.Services.Transactions
{
    public class OtRegServices
    {
        private readonly AppDbContext _context;
        public OtRegServices(AppDbContext context)
        {
            _context = context;
        }

        public bool SaveOtReg(List<OtReg> data)
        {
            try
            {
                _context.OtReg.RemoveRange(_context.OtReg);
                _context.OtReg.AddRange(data);
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
