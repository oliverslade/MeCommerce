using DataModels;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class BrowsingHistoryRepository
    {
        private readonly MeCommerceDbContext _context = new MeCommerceDbContext();

        public IEnumerable<BrowsingHistory> GetUsersBrowsingHistories(int userId)
        {
            return _context.BrowsingHistory.Where(x => x.UserId == userId);
        }

        public void CreateBrowsingHistoryEntry(BrowsingHistory bhe)
        {
            _context.BrowsingHistory.Add(bhe);
            _context.SaveChanges();
        }
    }
}