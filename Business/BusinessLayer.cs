using Common;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BusinessLayer : IBusinessLayer
    {
        private IDataAccessLayer _dataAccessLayer;

        public BusinessLayer(IDataAccessLayer dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;
        }

        public void AddTeam(Team team)
        {
            _dataAccessLayer.AddTeam(team);
        }

        public Team GetTeam(int id)
        {
            return _dataAccessLayer.GetTeam(id);
        }

        public List<SportEvent> GetNearSportEvents()
        {
            return _dataAccessLayer.GetNearSportEvents();
        }

        public int GetUsersMoney(string id)
        {

            return _dataAccessLayer.GetUsersMoney(id);
        }
        public SportEvent GetSportEvent(int id)
        {
            return _dataAccessLayer.GetSportEvent(id);
        }
    }
}
