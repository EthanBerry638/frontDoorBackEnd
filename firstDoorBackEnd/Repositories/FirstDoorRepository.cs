using firstDoorBackEnd.Database;

namespace firstDoorBackEnd.Repositories
{
    public class FirstDoorRepository : IFirstDoorRepository
    {
        private readonly FirstDoorContext _context;

        public FirstDoorRepository(FirstDoorContext context)
        {
            _context = context; 
        }
    }
}
