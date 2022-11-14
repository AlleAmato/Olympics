
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Olympics.Models;
using Olympics.ViewModels;
using System.Data.SqlClient;

namespace Olympics.Controller
{
    public static class Athletes
    {
        public static string connectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            }
        }

        private static List<Athlete> _athletes;
        public static List<Athlete> GetAll()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                _athletes = new List<Athlete>();
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "SELECT [Id], [IdAthlete], ISNULL([Name],''), ISNULL([Sex],''), ISNULL([Age],0), " +
                        "ISNULL([Height],0), ISNULL([Weight],0), ISNULL([NOC],0), ISNULL([Games],''), ISNULL([Year],0), " +
                        "ISNULL([Season],''), ISNULL([City],''), ISNULL([Sport],''), ISNULL([Event],''), ISNULL([Medal],'') FROM[dbo].[athletes]";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            _athletes.Add(new Athlete
                            {
                                Id = (int)reader.GetInt64(0),
                                IdAthlete = (int) reader.GetInt64(1),
                                Name = reader.GetString(2),
                                Sex = reader.GetString(3),
                                Age= reader.GetInt32(4),
                                Height= reader.GetInt32(5),
                                Weight= reader.GetInt32(6),
                                NOC = reader.GetString(7),
                                Games = reader.GetString(8),
                                Year = reader.GetInt32(9),
                                Season = reader.GetString(10),
                                City = reader.GetString(11),
                                Sport = reader.GetString(12),
                                Event1 = reader.GetString(13),
                                Medal = reader.GetString(14)
                            });
                        }
                    }
                    return _athletes;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return _athletes;
        }

        public static List<string> GetGenders()
        {
            List<string> sessi = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "select distinct sex from athletes ";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sessi.Add(reader.GetString(0));
                        }
                    }
                    return sessi;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public static List<string> GetGames()
        {
            List<string> giochi = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "select distinct games from athletes ";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            giochi.Add(reader.GetString(0));
                        }
                    }
                    return giochi;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return giochi;
        }


        public static List<string> GetSport(string game)
        {
            List<string> sports = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    if (!String.IsNullOrEmpty(game))
                    {
                        cmd.CommandText = "select distinct sport from athletes where games like '"+game+"'"; 
                        //serve a tenere la scelta dell'utente
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                sports.Add(reader.GetString(0));
                            }
                        }
                    }
                    return sports;
                }
                catch (Exception)
                {
                    throw;
                }
            }


            return sports;
        }


        public static List<string> GetEvent1(string game, string sport)
        {
            List<string> eventi = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    if (!String.IsNullOrEmpty(sport) && !String.IsNullOrEmpty(game))
                    {
                        cmd.CommandText = "select distinct event from athletes where games like '" + game + "' and sport like '" + sport + "'";
                        //serve a tenere la scelta dell'utente
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                eventi.Add(reader.GetString(0));
                            }
                        }
                    }
                    return eventi;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return eventi;
        }


        public static List<string> GetMedal()
        {
            List<string> medaglie = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandText = "select distinct medal from athletes where medal is not null ";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            medaglie.Add(reader.GetString(0));
                        }
                    }
                    return medaglie;
                }
                catch (Exception)
                {
                    throw;
                }
            }


            return medaglie;
        }




        public static List<Athlete> FindAll(string name, string sex, string games, string sport, string event1, string medal)//qui le stringhe page e comboSelect
        {
            if (string.IsNullOrWhiteSpace(name)) name = "";
            if (string.IsNullOrWhiteSpace(sex)) sex = "";
            if (string.IsNullOrWhiteSpace(games)) games = "";
            if (string.IsNullOrWhiteSpace(sport)) sport = "";
            if (string.IsNullOrWhiteSpace(event1)) event1 = "";
            if (string.IsNullOrWhiteSpace(medal)) medal = "";
            //return GetAll().FindAll(condizione);

            //1 - Mi creo la lista vuota da restituire
            List<Athlete> retVal = new List<Athlete>();

            //2 - Mi connetto al db e apro la connessione
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    //3 - Compongo la query sql (con i parametri)
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection; 
                    if (name !=" " && sex!="" && games!="" && sport!="" && event1!="") { 
                    cmd.CommandText = "SELECT[Id], [IdAthlete], ISNULL([Name],''), ISNULL([Sex],''), ISNULL([Age],0), " +
                        "ISNULL([Height],0), ISNULL([Weight],0), ISNULL([NOC],0), ISNULL([Games],''), ISNULL([Year],0), " +
                        "ISNULL([Season],''), ISNULL([City],''), ISNULL([Sport],''), ISNULL([Event],''), ISNULL([Medal],'') FROM[dbo].[athletes] ";
                    cmd.CommandText += "WHERE (Name Like @name) ";
                    cmd.CommandText += "and (Sex LIKE @sex) ";
                    cmd.CommandText += "and (Games LIKE @games) ";
                    cmd.CommandText += "and (Sport LIKE @sport) ";
                    cmd.CommandText += "and (Event lIKE @event1) ";
                    cmd.CommandText += "and (Medal LIKE @medal) ";
                    }
                    else if(name != " " && sex != "" && games != "" && sport != "" && event1 == "")
                    {
                        cmd.CommandText = "SELECT[Id], [IdAthlete], ISNULL([Name],''), ISNULL([Sex],''), ISNULL([Age],0), " +
                            "ISNULL([Height],0), ISNULL([Weight],0), ISNULL([NOC],0), ISNULL([Games],''), ISNULL([Year],0), " +
                            "ISNULL([Season],''), ISNULL([City],''), ISNULL([Sport],''), ISNULL([Event],''), ISNULL([Medal],'') FROM[dbo].[athletes] ";
                        cmd.CommandText += "WHERE (Name Like @name) ";
                        cmd.CommandText += "and (Sex LIKE @sex)  ";
                        cmd.CommandText += " and (Games LIKE @games) ";
                        cmd.CommandText += "and (Sport LIKE @sport) ";
                        cmd.CommandText += "and (Medal LIKE @medal) "; }
                    else if (name != " " && sex != "" && games != "" && sport == "" && event1 == "")
                    {
                        cmd.CommandText = "SELECT[Id], [IdAthlete], ISNULL([Name],''), ISNULL([Sex],''), ISNULL([Age],0), " +
                            "ISNULL([Height],0), ISNULL([Weight],0), ISNULL([NOC],0), ISNULL([Games],''), ISNULL([Year],0), " +
                            "ISNULL([Season],''), ISNULL([City],''), ISNULL([Sport],''), ISNULL([Event],''), ISNULL([Medal],'') FROM[dbo].[athletes] ";
                        cmd.CommandText += "WHERE (Name Like @name) ";
                        cmd.CommandText += "and(Sex LIKE @sex) ";
                        cmd.CommandText += "and (Games LIKE @games) ";
                        cmd.CommandText += "and (Medal LIKE @medal) ";
                    }
                    else if (name != " " && sex != "" && games == "" )
                    {
                        cmd.CommandText = "SELECT[Id], [IdAthlete], ISNULL([Name],''), ISNULL([Sex],''), ISNULL([Age],0), " +
                            "ISNULL([Height],0), ISNULL([Weight],0), ISNULL([NOC],0), ISNULL([Games],''), ISNULL([Year],0), " +
                            "ISNULL([Season],''), ISNULL([City],''), ISNULL([Sport],''), ISNULL([Event],''), ISNULL([Medal],'') FROM[dbo].[athletes] ";
                        cmd.CommandText += "WHERE (Name Like @name) ";
                        cmd.CommandText += "and (Sex LIKE @sex) ";
                        cmd.CommandText += "and (Medal LIKE @medal) ";
                    }
                    else if (name != " " && sex == "" && games=="" )
                    {
                        cmd.CommandText = "SELECT[Id], [IdAthlete], ISNULL([Name],''), ISNULL([Sex],''), ISNULL([Age],0), " +
                            "ISNULL([Height],0), ISNULL([Weight],0), ISNULL([NOC],0), ISNULL([Games],''), ISNULL([Year],0), " +
                            "ISNULL([Season],''), ISNULL([City],''), ISNULL([Sport],''), ISNULL([Event],''), ISNULL([Medal],'') FROM[dbo].[athletes] ";
                        cmd.CommandText += "WHERE (Name Like %@name%) ";
                        cmd.CommandText += "and (Medal LIKE @medal) ";
                    }
                    else if (name == " " && sex != "" && games=="")
                    {
                        cmd.CommandText = "SELECT[Id], [IdAthlete], ISNULL([Name],''), ISNULL([Sex],''), ISNULL([Age],0), " +
                            "ISNULL([Height],0), ISNULL([Weight],0), ISNULL([NOC],0), ISNULL([Games],''), ISNULL([Year],0), " +
                            "ISNULL([Season],''), ISNULL([City],''), ISNULL([Sport],''), ISNULL([Event],''), ISNULL([Medal],'') FROM[dbo].[athletes] ";
                        cmd.CommandText += "WHERE Sex LIKE @sex ";
                        cmd.CommandText += "and (Medal LIKE @medal)  ";
                    }
                    else if (name != " " && sex != "" && games == "")
                    {
                        cmd.CommandText = "SELECT[Id], [IdAthlete], ISNULL([Name],''), ISNULL([Sex],''), ISNULL([Age],0), " +
                            "ISNULL([Height],0), ISNULL([Weight],0), ISNULL([NOC],0), ISNULL([Games],''), ISNULL([Year],0), " +
                            "ISNULL([Season],''), ISNULL([City],''), ISNULL([Sport],''), ISNULL([Event],''), ISNULL([Medal],'') FROM[dbo].[athletes] ";
                        cmd.CommandText += "WHERE Sex LIKE @sex ";
                        cmd.CommandText += "and (Medal LIKE @medal)  ";
                        cmd.CommandText += "and (name LIKE @name)  ";
                    }
                    else if (name != " " && sex == "" && games != "" && sport == "" && event1 == "")
                    {
                        cmd.CommandText = "SELECT[Id], [IdAthlete], ISNULL([Name],''), ISNULL([Sex],''), ISNULL([Age],0), " +
                            "ISNULL([Height],0), ISNULL([Weight],0), ISNULL([NOC],0), ISNULL([Games],''), ISNULL([Year],0), " +
                            "ISNULL([Season],''), ISNULL([City],''), ISNULL([Sport],''), ISNULL([Event],''), ISNULL([Medal],'') FROM[dbo].[athletes] ";
                        cmd.CommandText += "WHERE (Name Like @name) ";
                        cmd.CommandText += " and (Games LIKE @games) ";
                        cmd.CommandText += "and (Medal LIKE @medal) ";
                    }
                    else if (name != " " && sex == "" && games != "" && sport != "" && event1 == "")
                    {
                        cmd.CommandText = "SELECT[Id], [IdAthlete], ISNULL([Name],''), ISNULL([Sex],''), ISNULL([Age],0), " +
                            "ISNULL([Height],0), ISNULL([Weight],0), ISNULL([NOC],0), ISNULL([Games],''), ISNULL([Year],0), " +
                            "ISNULL([Season],''), ISNULL([City],''), ISNULL([Sport],''), ISNULL([Event],''), ISNULL([Medal],'') FROM[dbo].[athletes] ";
                        cmd.CommandText += "WHERE (Name Like @name) ";
                        cmd.CommandText += " and (Games LIKE @games) ";
                        cmd.CommandText += "and (Sport LIKE @sport) ";
                        cmd.CommandText += "and (Medal LIKE @medal) ";
                    }
                    else if (name != " " && sex == "" && games != "" && sport != "" && event1 != "")
                    {
                        cmd.CommandText = "SELECT[Id], [IdAthlete], ISNULL([Name],''), ISNULL([Sex],''), ISNULL([Age],0), " +
                            "ISNULL([Height],0), ISNULL([Weight],0), ISNULL([NOC],0), ISNULL([Games],''), ISNULL([Year],0), " +
                            "ISNULL([Season],''), ISNULL([City],''), ISNULL([Sport],''), ISNULL([Event],''), ISNULL([Medal],'') FROM[dbo].[athletes] ";
                        cmd.CommandText += "WHERE (Name Like @name) ";
                        cmd.CommandText += "and (event LIKE @event1)  ";
                        cmd.CommandText += " and (Games LIKE @games) ";
                        cmd.CommandText += "and (Sport LIKE @sport) ";
                        cmd.CommandText += "and (Medal LIKE @medal) ";
                    }
                    else if (name == " " && sex != "" && games != "" && sport == "" && event1 == "")
                    {
                        cmd.CommandText = "SELECT[Id], [IdAthlete], ISNULL([Name],''), ISNULL([Sex],''), ISNULL([Age],0), " +
                            "ISNULL([Height],0), ISNULL([Weight],0), ISNULL([NOC],0), ISNULL([Games],''), ISNULL([Year],0), " +
                            "ISNULL([Season],''), ISNULL([City],''), ISNULL([Sport],''), ISNULL([Event],''), ISNULL([Medal],'') FROM[dbo].[athletes] ";
                        cmd.CommandText += "where (Sex LIKE @sex)  ";
                        cmd.CommandText += " and (Games LIKE @games) ";
                        cmd.CommandText += "and (Medal LIKE @medal) ";
                    }
                    else if (name == " " && sex != "" && games != "" && sport != "" && event1 == "")
                    {
                        cmd.CommandText = "SELECT[Id], [IdAthlete], ISNULL([Name],''), ISNULL([Sex],''), ISNULL([Age],0), " +
                            "ISNULL([Height],0), ISNULL([Weight],0), ISNULL([NOC],0), ISNULL([Games],''), ISNULL([Year],0), " +
                            "ISNULL([Season],''), ISNULL([City],''), ISNULL([Sport],''), ISNULL([Event],''), ISNULL([Medal],'') FROM[dbo].[athletes] ";
                        cmd.CommandText += "where (Sex LIKE @sex)  ";
                        cmd.CommandText += " and (Games LIKE @games) ";
                        cmd.CommandText += "and (Sport LIKE @sport) ";
                        cmd.CommandText += "and (Medal LIKE @medal) ";
                    }
                    else if (name == " " && sex != "" && games != "" && sport != "" && event1 != "")
                    {
                        cmd.CommandText = "SELECT[Id], [IdAthlete], ISNULL([Name],''), ISNULL([Sex],''), ISNULL([Age],0), " +
                            "ISNULL([Height],0), ISNULL([Weight],0), ISNULL([NOC],0), ISNULL([Games],''), ISNULL([Year],0), " +
                            "ISNULL([Season],''), ISNULL([City],''), ISNULL([Sport],''), ISNULL([Event],''), ISNULL([Medal],'') FROM[dbo].[athletes] ";
                        cmd.CommandText += "WHERE (event Like @event1) ";
                        cmd.CommandText += "and (Sex LIKE @sex)  ";
                        cmd.CommandText += " and (Games LIKE @games) ";
                        cmd.CommandText += "and (Sport LIKE @sport) ";
                        cmd.CommandText += "and (Medal LIKE @medal) ";
                    }
                    else if (name == " " && sex == "" && games != "" && sport == "" && event1 == "")
                    {
                        cmd.CommandText = "SELECT[Id], [IdAthlete], ISNULL([Name],''), ISNULL([Sex],''), ISNULL([Age],0), " +
                            "ISNULL([Height],0), ISNULL([Weight],0), ISNULL([NOC],0), ISNULL([Games],''), ISNULL([Year],0), " +
                            "ISNULL([Season],''), ISNULL([City],''), ISNULL([Sport],''), ISNULL([Event],''), ISNULL([Medal],'') FROM[dbo].[athletes] ";
                        cmd.CommandText += " where (Games LIKE @games) ";
                        cmd.CommandText += "and (Medal LIKE @medal) ";
                    }
                    else if (name != " " && sex != "" && games != "" && sport != "" && event1 == "")
                    {
                        cmd.CommandText = "SELECT[Id], [IdAthlete], ISNULL([Name],''), ISNULL([Sex],''), ISNULL([Age],0), " +
                            "ISNULL([Height],0), ISNULL([Weight],0), ISNULL([NOC],0), ISNULL([Games],''), ISNULL([Year],0), " +
                            "ISNULL([Season],''), ISNULL([City],''), ISNULL([Sport],''), ISNULL([Event],''), ISNULL([Medal],'') FROM[dbo].[athletes] ";
                        cmd.CommandText += " where (Games LIKE @games) ";
                        cmd.CommandText += "and (Sport LIKE @sport) ";
                        cmd.CommandText += "and (Medal LIKE @medal) ";
                    }
                    else if (name == " " && sex == "" && games != "" && sport != "" && event1 != "")
                    {
                        cmd.CommandText = "SELECT[Id], [IdAthlete], ISNULL([Name],''), ISNULL([Sex],''), ISNULL([Age],0), " +
                            "ISNULL([Height],0), ISNULL([Weight],0), ISNULL([NOC],0), ISNULL([Games],''), ISNULL([Year],0), " +
                            "ISNULL([Season],''), ISNULL([City],''), ISNULL([Sport],''), ISNULL([Event],''), ISNULL([Medal],'') FROM[dbo].[athletes] ";
                        cmd.CommandText += " where (Games LIKE @games) ";
                        cmd.CommandText += "and (Sport LIKE @sport) ";
                        cmd.CommandText += "and (Medal LIKE @medal) ";
                        cmd.CommandText += "and (event LIKE @event1) ";
                    }else
                    {
                        cmd.CommandText = "SELECT[Id], [IdAthlete], ISNULL([Name],''), ISNULL([Sex],''), ISNULL([Age],0), " +
                            "ISNULL([Height],0), ISNULL([Weight],0), ISNULL([NOC],0), ISNULL([Games],''), ISNULL([Year],0), " +
                            "ISNULL([Season],''), ISNULL([City],''), ISNULL([Sport],''), ISNULL([Event],''), ISNULL([Medal],'') FROM[dbo].[athletes] ";
                    }


                    cmd.Parameters.AddWithValue("@name", $"%{name}%");
                    cmd.Parameters.AddWithValue("@sex", $"{sex}%");
                    cmd.Parameters.AddWithValue("@games", $"{games}");
                    cmd.Parameters.AddWithValue("@sport", $"{sport}");
                    cmd.Parameters.AddWithValue("@event1", $"{event1}");
                    cmd.Parameters.AddWithValue("@medal", $"{medal}");

                    //4 - Ottengo il data reader
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        //5 - per ogni riga della query creo un oggetto User e aggiungo alla lista
                        while (reader.Read())
                        {
                            retVal.Add(new Athlete
                            {
                                Id = (int)reader.GetInt64(0),
                                IdAthlete = (int)reader.GetInt64(1),
                                Name = reader.GetString(2),
                                Sex = reader.GetString(3),
                                Age = reader.GetInt32(4),
                                Height = reader.GetInt32(5),
                                Weight = reader.GetInt32(6),
                                NOC = reader.GetString(7),
                                Games = reader.GetString(8),
                                Year = reader.GetInt32(9),
                                Season = reader.GetString(10),
                                City = reader.GetString(11),
                                Sport = reader.GetString(12),
                                Event1 = reader.GetString(13),
                                Medal = reader.GetString(14)
                            });
                        }
                    }
                    //6 - Restituisco la lista
                    return retVal;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
             private static Athlete ReadToAthlete(SqlDataReader reader)
            {
                return new Athlete
                {
                    Id = (int)reader["Id"],
                    IdAthlete = (int)reader["IdAthlete"],
                    Name = (string)reader["Name"],
                    Sex = (string)reader["Sex"],
                    Age = reader.GetInt32(reader.GetOrdinal("Age")),
                    Height = reader.GetInt32(reader.GetOrdinal("Height")),
                    Weight = reader.GetInt32(reader.GetOrdinal("Weight")),
                    NOC = (string)reader["NOC"],
                    Games = (string)reader["Games"],
                    Year = reader.GetInt32(reader.GetOrdinal("Year")),
                    Season = (string)reader["Season"],
                    City = (string)reader["City"],
                    Sport = (string)reader["Sport"],
                    Event1=(string)reader["Event1"],
                    Medal=(string)reader["Medal"]
                };
            }

            private static SqlParameter[] AthleteToParameters(Athlete a)
            {
                SqlParameter[] sp = new SqlParameter[] {
                new SqlParameter("@id",  a.Id),
                new SqlParameter("@idAthlete",  a.IdAthlete),
                new SqlParameter("@name",  a.Name),
                new SqlParameter("@sex",  a.Sex),
                new SqlParameter("@age",  a.Age),
                new SqlParameter("@height",  a.Height),
                new SqlParameter("@weight",  a.Weight),
                new SqlParameter("@noc",  a.NOC),
                new SqlParameter("@games",  a.Games),
                new SqlParameter("@year",  a.Year),
                new SqlParameter("@season",  a.Season),
                new SqlParameter("@city",  a.City),
                new SqlParameter("@sport",  a.Sport),
                new SqlParameter("@Event1",  a.Event1),
                new SqlParameter("@medal",  a.Medal),

            };
                return sp;
            }

        }
    }

