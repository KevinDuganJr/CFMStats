using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace CFMStats.Classes
{
    public class oSeason
    {
        public int ID { get; set; }
        public int Year { get; set; }
    }

    public class oSeasons : Dictionary<int, oSeason>
    {
        public oSeasons getSeasons(int leagueID)
        {
            oSeasons collection = new oSeasons();
            StoredProc SP = new StoredProc();

            SP.Name = "Seasons_select";
            SP.DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SP.ParameterSet = new System.Data.SqlClient.SqlCommand();

            SP.ParameterSet.Parameters.AddWithValue("@leagueId", leagueID);

            DataSet ds = StoredProc.ShowMeTheData(SP);

            if (ds.Tables.Count == 0) { return collection; }

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                oSeason s = new oSeason
                {
                    ID = item.Field<int>("seasonIndex"),
                    Year = item.Field<int>("seasonIndex")
                };
                collection.Add(s.ID, s);
            }


            return collection;
        }


    }
}