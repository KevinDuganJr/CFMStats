using System.Collections.Generic;

namespace CFMStats.Classes
{
    public class Stage
    {
        public int Id { get; set; }
        public string SeasonType { get; set; }
    }

    public class Stages : Dictionary<int, Stage>
    {
        public Stages AllStages()
        {
            var items = new Stages();
            var item = new Stage();

            item = new Stage(); item.Id = 0; item.SeasonType = "Preseason"; items.Add(item.Id, item);
            item = new Stage(); item.Id = 1; item.SeasonType = "Regular"; items.Add(item.Id, item);
            item = new Stage(); item.Id = 2; item.SeasonType = "Off Season"; items.Add(item.Id, item);
            item = new Stage(); item.Id = 4; item.SeasonType = "Post Season"; items.Add(item.Id, item);

            return items;
        }
    }
}
