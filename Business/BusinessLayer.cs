using Common;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.ServiceReference1;
using System.Drawing;

namespace Business
{
    public class BusinessLayer : IBusinessLayer
    {
        private IDataAccessLayer _dataAccessLayer;

        public BusinessLayer(IDataAccessLayer dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;
        }
        #region Teams
        public void AddTeam(Team team)
        {
            _dataAccessLayer.AddTeam(team);
        }
        public List<Team> GetAllTeams()
        {
            return _dataAccessLayer.GetAllTeams();
        }

        public Team GetTeam(int id)
        {
            return _dataAccessLayer.GetTeam(id);
        }

        public void EditTeam(Team team)
        {
            _dataAccessLayer.EditTeam(team);
        }
        public void DeleteTeam(int id)
        {
            _dataAccessLayer.DeleteTeam(id);
        }
        #endregion

        #region SportEvents
        public List<SportEvent> GetNearSportEvents()
        {
            return _dataAccessLayer.GetNearSportEvents();
        }

        public SportEvent GetSportEvent(int id)
        {
            return _dataAccessLayer.GetSportEvent(id);
        }

        public void AddSportEvent(SportEvent sportEvent)
        {
            _dataAccessLayer.AddSportEvent(sportEvent);
        }

        public List<SportEvent> GetAllSportEvents()
        {
            return _dataAccessLayer.GetAllSportEvents();
        }

        public void EditEvent(SportEvent sportEvent)
        {
            _dataAccessLayer.EditEvent(sportEvent);
        }
        public void DeleteEvent(int id)
        {
            _dataAccessLayer.DeleteEvent(id);
        }

        public List<SportEvent> GetNearSportEventsByKindOfSport(int kindId)
        {
            return _dataAccessLayer.GetNearSportEventsByKindOfSport(kindId);
        }

        public List<SportEvent> SearchSportEvents(string status, DateTime date, int kindId)
        {
            if (date == new DateTime(1, 1, 1))
                date = new DateTime(2000, 1, 1);
            return _dataAccessLayer.SearchSportEvents(status, date, kindId);
        }
        #endregion

        #region Bets       
        public void SetBets(Rate rate, List<Bet> bets)
        {
            int rateId = _dataAccessLayer.AddRate(rate);
            _dataAccessLayer.AddBets(bets, rateId);
        }

        public void AddBets(List<Bet> bets, int rateId)
        {
            _dataAccessLayer.AddBets(bets, rateId);
        }

        public List<Bet> GetBets(int rateId)
        {
            return _dataAccessLayer.GetBets(rateId);
        }

        public void EditBets(List<Bet> bets)
        {
            foreach (var bet in bets)
            {
                _dataAccessLayer.EditBet(bet);
            }
        }

        public void DeleteBet(int betId)
        {
            _dataAccessLayer.DeleteBet(betId);
        }
        #endregion

        #region Rates    
        public List<Rate> GetUsersRates(string userId)
        {
            var rates = _dataAccessLayer.GetUsersRates(userId);
            foreach (var rate in rates)
            {
                var bets = _dataAccessLayer.GetBets(rate.Id);
                rate.Bets = bets;
            }
            return rates;
        }

        public void DeleteRate(int rateId)
        {
            var bets = _dataAccessLayer.GetBets(rateId);
            foreach(var bet in bets)
            {
                _dataAccessLayer.DeleteBet(bet.Id);
            }
            _dataAccessLayer.DeleteRate(rateId);
        }
        #endregion

        #region Users
        public void GiveUserMoney(string id, int money)
        {
            var userMoney = _dataAccessLayer.GetUsersMoney(id);
            _dataAccessLayer.SetUsersMoney(id, userMoney + money);
        }

        public int GetUsersMoney(string id)
        {

            return _dataAccessLayer.GetUsersMoney(id);
        }

        public void TakeUsersMoney(string userId, int money)
        {
            var userMoney = _dataAccessLayer.GetUsersMoney(userId);
            userMoney -= money;
            _dataAccessLayer.SetUsersMoney(userId, userMoney);
        }

        public List<User> GetAllUsers()
        {
            return _dataAccessLayer.GetAllUsers();
        }
        public void EditUser(User user)
        {
            _dataAccessLayer.EditUser(user);
            SetUserRole(user.Id, user.Role);
        }
        public void DeleteUser(string id)
        {
            _dataAccessLayer.DeleteUser(id);
        }

        public User GetUser(string id)
        {
            User user = _dataAccessLayer.GetUser(id);
            user.Role = GetUsersRole(id);
            return user;
        }
        public string GetUsersRole(string id)
        {
            return _dataAccessLayer.GetUsersRole(id);
        }

        public void SetUserRole(string id, string role)
        {
            switch (role)
            {
                case "Admin": _dataAccessLayer.SetUserRole(id, "3"); break;
                case "Moderator": _dataAccessLayer.SetUserRole(id, "2"); break;
                case "User": _dataAccessLayer.SetUserRole(id, "1"); break;
            }
        }


        #endregion

        #region Kinds
        public KindOfSport GetKindOfSport(int id)
        {
            return _dataAccessLayer.GetKindOfSport(id);
        }
        public List<KindOfSport> GetAllKindsOfSport()
        {
            return _dataAccessLayer.GetAllKindsOfSport();
        }
        #endregion


        public string GetAdvertisement()
        {

            Service1Client client = new Service1Client("BasicHttpBinding_IService1");
            var stream = client.GetImage();
            var img = Image.FromStream(stream);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] imgBytes = ms.ToArray();
            string imgString = Convert.ToBase64String(imgBytes);
            client.Close();
            //return String.Format("<img src=\"data:image/jpg;base64,{0}\">", imgString);
            return imgString;
        }
    }
}