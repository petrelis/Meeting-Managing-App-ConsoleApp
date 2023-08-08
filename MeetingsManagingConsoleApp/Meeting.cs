using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelperLibrary;

namespace MeetingsManagingConsoleApp
{
    public class Meeting
    {
        public enum MeetingCategory
        {
            Sales,
            Dev,
            Management
        }
        public enum MeetingType
        {
            InPerson,
            OnLine
        }

        public string Name { get; set; }
        public string ResponsiblePerson { get; set; }
        public string Description { get; set; }
        public MeetingCategory Category { get; set; }
        public MeetingType Type { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public List<string> Participants { get; set; }


        public Meeting(string Name, string ResponsiblePerson, string Description, int CategoryIndex, int TypeIndex, DateTimeOffset StartDate, DateTimeOffset EndDate, List<string> Participants)
        {
            this.Name = Name;
            this.ResponsiblePerson = ResponsiblePerson;
            this.Description = Description;
            this.Category = (MeetingCategory)CategoryIndex;
            this.Type = (MeetingType)TypeIndex;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.Participants = Participants;
        }

        public static Meeting AddNewMeeting(String username)
        {
            Console.Clear();
            Console.WriteLine("Meeting Name: ");
            var name = ConsoleInputOutputFunctions.GetNotNullStringFromReadLine("name");
            var responsiblePerson = username;
            Console.WriteLine("Description: ");
            var description = Console.ReadLine();
            var categoryIndex = GetCategoryIndex();
            var typeIndex = GetTypeIndex();
            var startDate = DateFunctions.GetStartDateTime();
            var endDate = DateFunctions.GetEndDateTime(startDate);
            var participants = new List<string>();

            var newMeeting = new Meeting(name,
                responsiblePerson,
                description,
                categoryIndex,
                typeIndex,
                startDate,
                endDate,
                participants);

            return newMeeting;
        }

        public static int GetCategoryIndex()
        {
            Console.WriteLine("\nAvailable Categories:");
            Console.WriteLine($"\t[{(int) MeetingCategory.Sales}] {MeetingCategory.Sales}\n" +
                $"\t[{(int) MeetingCategory.Dev}] {MeetingCategory.Dev}\n" +
                $"\t[{(int) MeetingCategory.Management}] {MeetingCategory.Management}");
            Console.Write("Choose the index of this Meeting's Category: ");

            var enumCount = ((int)Enum.GetValues(typeof(MeetingCategory)).Cast<MeetingCategory>().Max());
            return ConsoleInputOutputFunctions.GetEnumIndex(enumCount);
        }

        public static int GetTypeIndex()
        {
            Console.WriteLine("\nAvailable Types:");
            Console.WriteLine($"\t[{(int) MeetingType.InPerson}] {MeetingType.InPerson}\n" +
                $"\t[{(int) MeetingType.OnLine}] {MeetingType.OnLine}\n");
            Console.Write("Choose the index of this Meeting's Type: ");

            var enumCount = ((int)Enum.GetValues(typeof(MeetingType)).Cast<MeetingType>().Max());
            return ConsoleInputOutputFunctions.GetEnumIndex(enumCount);
        }
    }
}
