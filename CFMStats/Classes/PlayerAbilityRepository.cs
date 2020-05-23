using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CFMStats.Classes
{
    public class PlayerAbilityRepository : Dictionary<int, PlayerAbility>
    {
        public PlayerAbilityRepository GetPlayerAbilities(int playerId)
        {
            var collection = new PlayerAbilityRepository();
            var sp = new StoredProc
            {
                Name = "[GetPlayerAbilities]", DataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString, ParameterSet = new SqlCommand()
            };

            sp.ParameterSet.Parameters.AddWithValue("@playerId", playerId);

            var ds = StoredProc.ShowMeTheData(sp);

            if (ds.Tables.Count == 0)
            {
                return collection;
            }

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                var a = new PlayerAbility
                {
                    Id = Helper.IntegerNull(item["signatureLogoId"]),
                    OvrThreshold = Helper.IntegerNull(item["OvrThreshold"]),
                    IsEmpty = Helper.BooleanNull(item["isEmpty"]),
                    IsLocked = Helper.BooleanNull(item["isLocked"]),
                    Title = Helper.StringNull(item["SignatureTitle"]),
                    Description = Helper.StringNull(item["SignatureDescription"]),
                    ActivationDescription = Helper.StringNull(item["SignatureActivationDescription"]),
                    DeactivationDescription = Helper.StringNull(item["SignatureDeactivationDescription"])
                };

                collection.Add(a.Id, a);
            }

            return collection;
        }
    }
}