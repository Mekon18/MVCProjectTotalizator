using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IDataAccessLayer
    {
        Team GetTeam(int id);
        void AddTeam(Team team);
        void AddSportEvent(SportEvent sportEvent);
        List<SportEvent> GetNearSportEvents();
        int GetUsersMoney(string id);
        SportEvent GetSportEvent(int id);
    }
}
