using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using Common;

namespace DataAccess
{
    public class DataAccessLayer : IDataAccessLayer
    {
        private string _connectingString;
        public DataAccessLayer()
        {
            _connectingString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        #region Teams
        public Team GetTeam(int id)
        {
            Team team = new Team();

            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    CommandText = "GetTeam",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = connection
                };
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {
                    reader.Read();
                    team.Id = (int)reader["Id"];
                    team.Name = (string)reader["Name"];
                    team.CountryName = (string)reader["CountryName"];
                }
                reader.Close();
            }
            return team;
        }

        public List<Team> GetAllTeams()
        {
            List<Team> teams = new List<Team>();
            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    CommandText = "GetAllTeams",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = connection
                };
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        string name = (string)reader["Name"];
                        string country = (string)reader["CountryName"];

                        teams.Add(new Team() { Id = id, Name = name, CountryName = country });
                    }
                }
                reader.Close();
            }
            return teams;
        }

        public void AddTeam(Team team)
        {
            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand
                {
                    CommandText = "AddTeam",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = connection
                };
                command.Parameters.AddWithValue("@name", team.Name);
                command.Parameters.AddWithValue("@countryName", team.CountryName);
                command.ExecuteNonQuery();
            }
        }

        public void EditTeam(Team team)
        {
            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand
                {
                    CommandText = "EditTeam",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = connection
                };
                command.Parameters.AddWithValue("@Id", team.Id);
                command.Parameters.AddWithValue("@name", team.Name);
                command.Parameters.AddWithValue("@countryName", team.CountryName);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteTeam(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand
                {
                    CommandText = "DeleteTeam",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = connection
                };
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        #endregion

        #region SportEvents
        public void AddSportEvent(SportEvent sportEvent)
        {
            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    CommandText = "AddSportEvent",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = connection
                };
                command.Parameters.AddWithValue("@date", sportEvent.DateTime);
                command.Parameters.AddWithValue("@kindOfSport", sportEvent.KindOfSport);
                command.Parameters.AddWithValue("@teamId1", sportEvent.FirstTeam.Id);
                command.Parameters.AddWithValue("@teamId2", sportEvent.SecondTeam.Id);
                command.Parameters.AddWithValue("@coef1", sportEvent.FirstCoeficient);
                command.Parameters.AddWithValue("@coef2", sportEvent.SecondCoeficient);
                command.Parameters.AddWithValue("@coef3", sportEvent.ThirdCoeficient);
                command.Parameters.AddWithValue("@coef4", sportEvent.FourthCoeficient);
                command.ExecuteNonQuery();
            }
        }

        public SportEvent GetSportEvent(int id)
        {
            SportEvent sportEvent = null;
            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    CommandText = "GetSportEvent",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = connection
                };
                command.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {
                    reader.Read();
                    DateTime date = (DateTime)reader["Date"];
                    int firstTeamId = (int)reader["FirstTeamId"];
                    int secondTeamId = (int)reader["SecondTeamId"];
                    string kindOfSport = (string)reader["KindOfSport"];
                    double coef1 = (double)reader["FirstCoeficient"];
                    double coef2 = (double)reader["SecondCoeficient"];
                    double coef3 = (double)reader["ThirdCoeficient"];
                    double coef4 = (double)reader["FourthCoeficient"];

                    Team firstTeam = GetTeam(firstTeamId);
                    Team secondTeam = GetTeam(secondTeamId);

                    sportEvent = new SportEvent()
                    {
                        Id = id,
                        DateTime = date,
                        FirstTeam = firstTeam,
                        SecondTeam = secondTeam,
                        KindOfSport = kindOfSport,
                        FirstCoeficient = coef1,
                        SecondCoeficient = coef2,
                        ThirdCoeficient = coef3,
                        FourthCoeficient = coef4
                    };
                }
                reader.Close();
            }
            return sportEvent;
        }

        public List<SportEvent> GetAllSportEvents()
        {
            List<SportEvent> sportEvents = new List<SportEvent>();
            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    CommandText = "GetAllSportEvents",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = connection
                };
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        DateTime date = (DateTime)reader["Date"];
                        int firstTeamId = (int)reader["FirstTeamId"];
                        int secondTeamId = (int)reader["SecondTeamId"];
                        string kindOfSport = (string)reader["KindOfSport"];
                        double coef1 = (double)reader["FirstCoeficient"];
                        double coef2 = (double)reader["SecondCoeficient"];
                        double coef3 = (double)reader["ThirdCoeficient"];
                        double coef4 = (double)reader["FourthCoeficient"];

                        Team firstTeam = GetTeam(firstTeamId);
                        Team secondTeam = GetTeam(secondTeamId);

                        sportEvents.Add(new SportEvent()
                        {
                            Id = id,
                            DateTime = date,
                            FirstTeam = firstTeam,
                            SecondTeam = secondTeam,
                            KindOfSport = kindOfSport,
                            FirstCoeficient = coef1,
                            SecondCoeficient = coef2,
                            ThirdCoeficient = coef3,
                            FourthCoeficient = coef4
                        });
                    }
                }
                reader.Close();
            }
            return sportEvents;
        }

        public List<SportEvent> GetNearSportEvents()
        {
            List<SportEvent> sportEvents = new List<SportEvent>();
            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    CommandText = "GetNearSportEvents",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = connection
                };
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        DateTime date = (DateTime)reader["Date"];
                        int firstTeamId = (int)reader["FirstTeamId"];
                        int secondTeamId = (int)reader["SecondTeamId"];
                        string kindOfSport = (string)reader["KindOfSport"];

                        Team firstTeam = GetTeam(firstTeamId);
                        Team secondTeam = GetTeam(secondTeamId);

                        sportEvents.Add(new SportEvent() { Id = id, DateTime = date, FirstTeam = firstTeam, SecondTeam = secondTeam, KindOfSport = kindOfSport });
                    }
                }
                reader.Close();
            }
            return sportEvents;
        }

        public void EditEvent(SportEvent sportEvent)
        {
            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    CommandText = "EditEvent",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = connection
                };
                command.Parameters.AddWithValue("@Id", sportEvent.Id);
                command.Parameters.AddWithValue("@date", sportEvent.DateTime);
                command.Parameters.AddWithValue("@kindOfSport", sportEvent.KindOfSport);
                command.Parameters.AddWithValue("@teamId1", sportEvent.FirstTeam.Id);
                command.Parameters.AddWithValue("@teamId2", sportEvent.SecondTeam.Id);
                command.Parameters.AddWithValue("@coef1", sportEvent.FirstCoeficient);
                command.Parameters.AddWithValue("@coef2", sportEvent.SecondCoeficient);
                command.Parameters.AddWithValue("@coef3", sportEvent.ThirdCoeficient);
                command.Parameters.AddWithValue("@coef4", sportEvent.FourthCoeficient);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteEvent(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    CommandText = "DeleteEvent",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = connection
                };
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        #endregion

        #region Users
        public int GetUsersMoney(string id)
        {
            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    CommandText = "GetUsersMoney",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = connection
                };
                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@userId",
                    Value = id
                };
                SqlParameter moneyParam = new SqlParameter
                {
                    ParameterName = "@money",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output
                };
                command.Parameters.AddRange(new[] { idParam, moneyParam });
                command.ExecuteNonQuery();

                return (int)command.Parameters["@money"].Value;
            }
        }

        public void SetUsersMoney(string id, int money)
        {
            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    CommandText = "UpdateUsersMoney",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = connection
                };
                SqlParameter userId = new SqlParameter
                {
                    ParameterName = "@userId",
                    Value = id
                };
                SqlParameter newMoney = new SqlParameter
                {
                    ParameterName = "@newMoney",
                    Value = money
                };
                command.Parameters.AddRange(new[] { userId, newMoney });
                command.ExecuteNonQuery();
            }
        }

        public List<User> GetAllUsers()
        {
            List<User> Users = new List<User>();
            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    CommandText = "GetAllUsers",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = connection
                };
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string id = (string)reader["Id"];
                        string email = (string)reader["Email"];
                        string passwordHash = (string)reader["PasswordHash"];
                        int money = (int)reader["Money"];
                        string role = GetUsersRole(id);

                        Users.Add(new User() { Id = id, Email = email, Password = passwordHash, Money = money, Role = role });
                    }
                }
                reader.Close();
            }
            return Users;
        }

        public void EditUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand
                {
                    CommandText = "EditUser",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = connection
                };
                command.Parameters.AddWithValue("@Id", user.Id);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@passwordHash", user.Password);
                command.Parameters.AddWithValue("@money", user.Money);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteUser(string id)
        {
            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    CommandText = "DeleteUser",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = connection
                };
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        public User GetUser(string id)
        {
            User user = new User() { Id = id };

            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    CommandText = "GetUser",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = connection
                };
                command.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {
                    reader.Read();
                    user.Password = (string)reader["PasswordHash"];
                    user.Email = (string)reader["Email"];
                    user.Money = (int)reader["Money"];
                }
                reader.Close();
            }
            return user;
        }

        public string GetUsersRole(string id)
        {
            string role = "";
            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    CommandText = "GetUsersRole",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = connection
                };
                command.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {
                    reader.Read();
                    role = (string)reader["Name"];
                }
                reader.Close();
            }
            return role;
        }

        public void SetUserRole(string id,string roleId)
        {
            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand
                {
                    CommandText = "SetUserRole",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = connection
                };
                command.Parameters.AddWithValue("@userId", id); 
                command.Parameters.AddWithValue("@roleId", roleId);
                command.ExecuteNonQuery();
            }
        }

        #endregion

        #region Bets
        public void AddBets(List<Bet> bets, int RateId)
        {
            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();
                foreach (var bet in bets)
                {
                    SqlCommand command = new SqlCommand
                    {
                        CommandText = "AddBet",
                        CommandType = System.Data.CommandType.StoredProcedure,
                        Connection = connection
                    };
                    SqlParameter type = new SqlParameter
                    {
                        ParameterName = "@type",
                        Value = bet.ResultType
                    };
                    SqlParameter value = new SqlParameter
                    {
                        ParameterName = "@value",
                        Value = bet.ResultValue
                    };
                    SqlParameter money = new SqlParameter
                    {
                        ParameterName = "@money",
                        Value = bet.Money
                    };
                    SqlParameter rateId = new SqlParameter
                    {
                        ParameterName = "@rateId",
                        Value = RateId
                    };
                    command.Parameters.AddRange(new[] { type, value, money, rateId });
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Bet> GetBets(int rateId)
        {
            List<Bet> bets = new List<Bet>();
            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    CommandText = "GetBets",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = connection
                };
                command.Parameters.AddWithValue("@rateId", rateId);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        string resultType = ((string)reader["ResultType"]).Trim();
                        string resultValue = ((string)reader["ResultValue"]).Trim();
                        int money = (int)reader["Money"];

                        bets.Add(new Bet() { Id = id, ResultType = resultType, ResultValue = resultValue, Money = money });
                    }
                }
                reader.Close();
            }
            return bets;
        }
        #endregion

        #region Rates
        public int AddRate(Rate rate)
        {
            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand
                {
                    CommandText = "AddRate",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = connection
                };
                SqlParameter eventId = new SqlParameter
                {
                    ParameterName = "@eventId",
                    Value = rate.Event.Id
                };
                SqlParameter dateTime = new SqlParameter
                {
                    ParameterName = "@dateTime",
                    Value = rate.DateTime
                };
                SqlParameter userId = new SqlParameter
                {
                    ParameterName = "@userId",
                    Value = rate.UserId
                };
                command.Parameters.AddRange(new[] { eventId, dateTime, userId });
                var id = command.ExecuteScalar();
                return (int)(decimal)id;
            }
        }

        public List<Rate> GetUsersRates(string userId)
        {
            List<Rate> rates = new List<Rate>();
            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    CommandText = "GetUsersRates",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = connection
                };
                command.Parameters.AddWithValue("@userId", userId);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        DateTime date = (DateTime)reader["DateTime"];
                        int eventId = (int)reader["EventId"];

                        SportEvent sportEvent = GetSportEvent(eventId);

                        rates.Add(new Rate() { Id = id, DateTime = date, Event = sportEvent, UserId = userId });
                    }
                }
                reader.Close();
            }
            return rates;
        }
        #endregion

    }
}

//SqlParameter type = new SqlParameter
//{
//    ParameterName = "@type",
//    Value = bet.ResultType
//};
//command.Parameters.AddRange(new[] { type, value, money, rateId });