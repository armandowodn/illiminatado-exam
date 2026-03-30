using DocumentFormat.OpenXml.InkML;
using illiminatado_exam.Data;
using illiminatado_exam.Model;

namespace illiminatado_exam.Services.Master
{
    public class EmployeeMasterServices
    {
        private readonly AppDbContext _context;
        public EmployeeMasterServices(AppDbContext context)
        {
            _context = context;
        }
        public bool SaveEmployee(List<EmpMaster> data) {
            try
            {
                _context.EmpMaster.RemoveRange(_context.EmpMaster);
                _context.EmpMaster.AddRange(data);
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
