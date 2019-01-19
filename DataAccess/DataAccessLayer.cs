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
                        teams.Add(new Team()
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            CountryName = (string)reader["CountryName"]
                        });
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
            UpdateEventsStatus();
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
                command.Parameters.AddWithValue("@kindOfSportId", sportEvent.KindOfSport.Id);
                command.Parameters.AddWithValue("@teamId1", sportEvent.FirstTeam.Id);
                command.Parameters.AddWithValue("@teamId2", sportEvent.SecondTeam.Id);
                command.Parameters.AddWithValue("@coef1", sportEvent.FirstCoeficient);
                command.Parameters.AddWithValue("@coef2", sportEvent.SecondCoeficient);
                command.Parameters.AddWithValue("@coef3", sportEvent.ThirdCoeficient);
                command.Parameters.AddWithValue("@coef4", sportEvent.FourthCoeficient);
                command.Parameters.AddWithValue("@status", sportEvent.Status);
                command.ExecuteNonQuery();
            }
        }

        public SportEvent GetSportEvent(int id)
        {
            UpdateEventsStatus();
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
                    sportEvent = new SportEvent()
                    {
                        Id = id,
                        DateTime = (DateTime)reader["Date"],
                        FirstTeam = GetTeam((int)reader["FirstTeamId"]),
                        SecondTeam = GetTeam((int)reader["SecondTeamId"]),
                        KindOfSport = GetKindOfSport((int)reader["KindOfSportId"]),
                        FirstCoeficient = (double)reader["FirstCoeficient"],
                        SecondCoeficient = (double)reader["SecondCoeficient"],
                        ThirdCoeficient = (double)reader["ThirdCoeficient"],
                        FourthCoeficient = (double)reader["FourthCoeficient"],
                        Status = (string)reader["Status"]
                    };
                }
                reader.Close();
            }
            return sportEvent;
        }

        public List<SportEvent> GetAllSportEvents()
        {
            UpdateEventsStatus();
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
                        sportEvents.Add(new SportEvent()
                        {
                            Id = (int)reader["Id"],
                            DateTime = (DateTime)reader["Date"],
                            FirstTeam = GetTeam((int)reader["FirstTeamId"]),
                            SecondTeam = GetTeam((int)reader["SecondTeamId"]),
                            KindOfSport = GetKindOfSport((int)reader["KindOfSportId"]),
                            FirstCoeficient = (double)reader["FirstCoeficient"],
                            SecondCoeficient = (double)reader["SecondCoeficient"],
                            ThirdCoeficient = (double)reader["ThirdCoeficient"],
                            FourthCoeficient = (double)reader["FourthCoeficient"],
                            Status = (string)reader["Status"]
                        });
                    }
                }
                reader.Close();
            }
            return sportEvents;
        }

        public List<SportEvent> GetNearSportEvents()
        {
            UpdateEventsStatus();
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
                        sportEvents.Add(new SportEvent()
                        {
                            Id = (int)reader["Id"],
                            DateTime = (DateTime)reader["Date"],
                            FirstTeam = GetTeam((int)reader["FirstTeamId"]),
                            SecondTeam = GetTeam((int)reader["SecondTeamId"]),
                            KindOfSport = GetKindOfSport((int)reader["KindOfSportId"]),
                            Status = (string)reader["Status"]
                        });
                    }
                }
                reader.Close();
            }
            return sportEvents;
        }

        public List<SportEvent> GetNearSportEventsByKindOfSport(int kindId)
        {
            UpdateEventsStatus();
            List<SportEvent> sportEvents = new List<SportEvent>();
            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    CommandText = "GetNearSportEventsByKindOfSport",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = connection
                };
                command.Parameters.AddWithValue("@Id", kindId);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        sportEvents.Add(new SportEvent()
                        {
                            Id = (int)reader["Id"],
                            DateTime = (DateTime)reader["Date"],
                            FirstTeam = GetTeam((int)reader["FirstTeamId"]),
                            SecondTeam = GetTeam((int)reader["SecondTeamId"]),
                            KindOfSport = GetKindOfSport((int)reader["KindOfSportId"]),
                            FirstCoeficient = (double)reader["FirstCoeficient"],
                            SecondCoeficient = (double)reader["SecondCoeficient"],
                            ThirdCoeficient = (double)reader["ThirdCoeficient"],
                            FourthCoeficient = (double)reader["FourthCoeficient"],
                            Status = (string)reader["Status"]
                        });
                    }
                }
                reader.Close();
            }
            return sportEvents;
        }

        public List<SportEvent> SearchSportEvents(string status, DateTime date, int kindId)
        {            
            List<SportEvent> sportEvents = new List<SportEvent>();
            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    CommandText = "SearchSportEvents",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = connection
                };
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@date", date);
                command.Parameters.AddWithValue("@kindId", kindId);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        sportEvents.Add(new SportEvent()
                        {
                            Id = (int)reader["Id"],
                            DateTime = (DateTime)reader["Date"],
                            FirstTeam = GetTeam((int)reader["FirstTeamId"]),
                            SecondTeam = GetTeam((int)reader["SecondTeamId"]),
                            KindOfSport = GetKindOfSport((int)reader["KindOfSportId"])
                        });
                    }
                }
                reader.Close();
            }
            return sportEvents;
        }

        public void EditEvent(SportEvent sportEvent)
        {
            UpdateEventsStatus();
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
                command.Parameters.AddWithValue("@status", sportEvent.Status);
                command.Parameters.AddWithValue("@kindOfSportId", sportEvent.KindOfSport.Id);
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

        public void UpdateEventsStatus()
        {
            var comingEvents = SearchSportEvents("Coming", new DateTime(2000, 1, 1), 0);
            foreach (var comingEvent in comingEvents)
            {
                var time = comingEvent.DateTime - DateTime.Now;
                if (time < new TimeSpan() && time > new TimeSpan(-2, 0, 0))
                    UpdateEventStatus(comingEvent.Id, "Going");
                if (time < new TimeSpan(-2, 0, 0))
                    UpdateEventStatus(comingEvent.Id, "Passed");
            }

            var goingEvents = SearchSportEvents("Going", new DateTime(2000, 1, 1), 0);
            foreach (var goingEvent in goingEvents)
            {
                if (goingEvent.DateTime - DateTime.Now < new TimeSpan(-2, 0, 0))
                    UpdateEventStatus(goingEvent.Id, "Passed");
            }
        }

        public void UpdateEventStatus(int id, string status)
        {
            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    CommandText = "UpdateEventStatus",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = connection
                };
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@status", status);
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

                        Users.Add(new User()
                        {
                            Id = id,
                            Email = (string)reader["Email"],
                            Password = (string)reader["PasswordHash"],
                            Money = (int)reader["Money"],
                            Role = GetUsersRole(id)
                        });
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

        public void SetUserRole(string id, string roleId)
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
                        bets.Add(new Bet()
                        {
                            Id = (int)reader["Id"],
                            ResultType = ((string)reader["ResultType"]).Trim(),
                            ResultValue = ((string)reader["ResultValue"]).Trim(),
                            Money = (int)reader["Money"]
                        });
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

        #region kinds
        public KindOfSport GetKindOfSport(int id)
        {
            KindOfSport Kind = new KindOfSport() { Id = id };

            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    CommandText = "GetKindOfSport",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = connection
                };
                command.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {
                    reader.Read();
                    Kind.Name = (string)reader["Name"];
                }
                reader.Close();
            }
            return Kind;
        }

        public List<KindOfSport> GetAllKindsOfSport()
        {
            List<KindOfSport> kinds = new List<KindOfSport>();
            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    CommandText = "GetAllKindsOfSport",
                    CommandType = System.Data.CommandType.StoredProcedure,
                    Connection = connection
                };
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        kinds.Add(new KindOfSport()
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"]
                        });
                    }
                }

                // Update database events

                reader.Close();
            }
            return kinds;
        }

        #endregion

    }
}