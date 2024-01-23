using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CFMStats.Classes;

namespace CFMStats.Services
{
    public class UtilityService
    {
        public Dictionary<string, int> GetExportId(int id)
        {
            var leagueDictionary = new Dictionary<string, int>();

            var export = "";
            var sp = new StoredProc
            {
                Name = "League_select", ParameterSet = new SqlCommand()
            };

            var userId = string.Empty;
            sp.ParameterSet.Parameters.AddWithValue("@ownerUserID", userId);

            var ds = StoredProc.ShowMeTheData(sp);

            if(ds.Tables.Count == 0)

            {
                return leagueDictionary;
            }

            foreach(DataRow item in ds.Tables[0].Rows)
            {
                var leagueId = Helper.IntegerNull(item["id"]);
                var exportId = Helper.StringNull(item["exportId"]).ToLower();

                leagueDictionary.Add(exportId, leagueId);

                //if (id == item.Field<int>("ID"))
                //{
                //    export = item.Field<string>("exportId");
                //    break;
                //}
            }

            return leagueDictionary; //export.ToLower();
        }
    }
}