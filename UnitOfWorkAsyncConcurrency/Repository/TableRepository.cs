
using Microsoft.EntityFrameworkCore;
using UnitOfWorkAsync.Models;

namespace UnitOfWorkAsync.Repository
{
    public class TableRepository : Repository<Table>
    {
        public TableRepository(DbContext context)
            : base(context)
        {
        }
        /*        
        public int UpdateCourseCredits(DbUpdateConcurrencyException ex)
        {
            return _context.Database.ExecuteSqlCommand("UPDATE Course SET Credits = Credits * {0}", multiplier);
        }
        */
    }
}
