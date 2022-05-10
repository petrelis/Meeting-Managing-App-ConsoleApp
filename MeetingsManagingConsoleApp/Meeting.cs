using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsManagingConsoleApp
{
    public class Meeting
    {
        public string Name { get; set; }
        public string ResponsiblePerson { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public List<string> Participants { get; set; }

        public Meeting(string Name, string ResponsiblePerson, string Description, string Category, string Type, DateTimeOffset StartDate, DateTimeOffset EndDate, List<string> Participants)
        {
            this.Name = Name;
            this.ResponsiblePerson = ResponsiblePerson;
            this.Description = Description;
            this.Category = Category;
            this.Type = Type;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.Participants = Participants;
        }
    }
}
