using System.Collections.Generic;
using CFMStats.DataContracts;

namespace CFMStats.Classes
{
    public class DevelopmentTraitService : Dictionary<int, DevelopmentTrait>
    {
        public DevelopmentTraitService GetDevelopmentTraits()
        {
            var traits = new DevelopmentTraitService();
            
            var trait = new DevelopmentTrait
            {
                Id = 0, Name = "Normal"
            };
            traits.Add(trait.Id, trait);

            trait = new DevelopmentTrait
            {
                Id = 1, Name = "Star"
            };
            traits.Add(trait.Id, trait);

            trait = new DevelopmentTrait
            {
                Id = 2, Name = "Superstar"
            };
            traits.Add(trait.Id, trait);

            trait = new DevelopmentTrait
            {
                Id = 3, Name = "Superstar X-Factor"
            };
            traits.Add(trait.Id, trait);

            return traits;
        }
    }
}