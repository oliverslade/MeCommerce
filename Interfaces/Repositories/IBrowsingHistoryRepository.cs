using DataModels;
using System.Collections.Generic;

namespace Interfaces.Repositories
{
    public interface IBrowsingHistoryRepository
    {
        IEnumerable<BrowsingHistory> GetUsersBrowsingHistories(int userId);

        void CreateBrowsingHistoryEntry(BrowsingHistory bhe);
    }
}