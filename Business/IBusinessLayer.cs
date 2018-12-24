using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IBusinessLayer
    {
        void AddTeam(Team team);
        Team GetTeam(int id);
        List<SportEvent> GetNearSportEvents();
        int GetUsersMoney(string id);
        SportEvent GetSportEvent(int id);
    }
}
