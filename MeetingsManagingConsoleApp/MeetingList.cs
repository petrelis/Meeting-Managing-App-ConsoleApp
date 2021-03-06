using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MeetingsManagingConsoleApp
{
    public class MeetingList
    {
        private List<Meeting> _meetingList { get; set; } = new List<Meeting>(JsonMeetingsToCs());

        static List<Meeting> JsonMeetingsToCs()
        {
            string fileName = @"C:\Users\visti\Desktop\MeetingsManagingConsoleApp\MeetingsManagingConsoleApp\Meetings.json";
            if (File.Exists(fileName) && new FileInfo(fileName).Length > 1)
            {
                string justText = File.ReadAllText(fileName);

                var meetings = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Meeting>>(justText);

                return meetings;
            }
            else 
                return new List<Meeting>();
        }

        private void DisplayMeetingDetails(Meeting meeting)
        {
            Console.WriteLine(""+
            $"Name: {meeting.Name} \n" +
            $"Responsible Person: {meeting.ResponsiblePerson} \n" +
            $"Description: {meeting.Description} \n" +
            $"Category: {meeting.Category} \n" +
            $"Type: {meeting.Type} \n" +
            $"StartDate: {meeting.StartDate} \n" +
            $"EndDate: {meeting.EndDate} \n" +
            "Participants:");

            Console.WriteLine("[");
            foreach(string participant in meeting.Participants)
            {
                Console.WriteLine(participant);
            }
            Console.WriteLine("]");

        }

        public void CsMeetingsToJson()
        {
            string filePath = @"C:\Users\visti\Desktop\MeetingsManagingConsoleApp\MeetingsManagingConsoleApp\Meetings.json";

            var jsonMeetingList = String.Empty;

            if (File.Exists(filePath))
            {
                int currentIndex = 1;
                var jsonMeeting = String.Empty;
                foreach (Meeting meeting in _meetingList)
                {
                    if(currentIndex == _meetingList.Count)
                        jsonMeeting = Newtonsoft.Json.JsonConvert.SerializeObject(meeting, Newtonsoft.Json.Formatting.Indented);
                    else
                        jsonMeeting = Newtonsoft.Json.JsonConvert.SerializeObject(meeting, Newtonsoft.Json.Formatting.Indented) +",\n";
                    jsonMeetingList += jsonMeeting;
                    currentIndex++;
                }
                jsonMeetingList = string.Concat("[\n" + jsonMeetingList + "\n]");
                File.WriteAllText(filePath, jsonMeetingList); 
            }

            
        }

        public void DisplayMeetingListDetails(List<Meeting> meetings)
        {
            int i = 0;
            foreach (var meeting in meetings)
            {
                i++;
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine($"index: {i} ");
                DisplayMeetingDetails(meeting);
                Console.WriteLine("--------------------------------------------");
            }
        }

        internal int AddPeople(int addpIndex)
        {
            string input = String.Empty;
            Console.WriteLine("Enter participant names one by one, type \"c\" to stop");
            int addedIndex = 0;
            while(true)
            {
                input = Console.ReadLine();
                if (input == "c")
                    break;
                if (!_meetingList[addpIndex].Participants.Contains(input))
                {
                    _meetingList[addpIndex].Participants.Add(input);
                    addedIndex++;
                    Console.WriteLine("Person added");
                }
                else
                    Console.WriteLine("Person is already in the meeting");
                
            }
            CsMeetingsToJson();
            return addedIndex;
        }

        internal int RemovePeople(int removepIndex)
        {
            string input = String.Empty;
            Console.WriteLine("Enter participant names one by one, type \"c\" to stop");
            int removedIndex = 0;
            while (true)
            {
                input = Console.ReadLine();
                if (input == "c")
                    break;
                if (input != _meetingList[removepIndex].ResponsiblePerson
                    && _meetingList[removepIndex].Participants.Contains(input))
                {
                    _meetingList[removepIndex].Participants.Remove(input);
                    removedIndex++;
                }
                else Console.WriteLine("Can't remove");
            }
            CsMeetingsToJson();
            return removedIndex;
        }

        public bool AddMeeting(Meeting meeting)
        {
            _meetingList.Add(meeting);
            CsMeetingsToJson();
            return true;
        }

        public void DeleteMeeting(Meeting meeting)
        {
            _meetingList.Remove(meeting);
            CsMeetingsToJson();
        }

        public void DisplayMeeting(string name)
        {
            var meeting = _meetingList.FirstOrDefault(m => m.Name == name);
            if (meeting == null)
            {
                Console.WriteLine("Meeting not found");
            }
            else
            {
                DisplayMeetingDetails(meeting);
            }
        }

        public void DisplayAllMeetings()
        {
            if (_meetingList.Count > 0)
                DisplayMeetingListDetails(_meetingList);
            else
                Console.WriteLine("No meetings found");
        }

        public List<Meeting> DisplayGetOwnedMeetings(string responsiblePerson)
        {
            var ownedMeetings = _meetingList.Where(m => m.ResponsiblePerson.Contains(responsiblePerson)).ToList();
            DisplayMeetingListDetails(ownedMeetings);
            return ownedMeetings;
        }

        public void DisplayMatchingMeetings(string searchArg)
        {
            var matchingMeetings = new List<Meeting>();
            switch (searchArg)
            {
                case "name":
                    Console.Write("enter name: ");
                    string name = Console.ReadLine();
                    matchingMeetings = _meetingList.Where(m => m.Name.Contains(name)).ToList();
                    break;
                case "desc":
                    Console.Write("enter description: ");
                    string description = Console.ReadLine();
                    matchingMeetings = _meetingList.Where(m => m.Description.Contains(description)).ToList();
                    break;
                case "resp":
                    Console.Write("enter responsible person's name: ");
                    string resPerson = Console.ReadLine();
                    matchingMeetings = _meetingList.Where(m => m.ResponsiblePerson.Contains(resPerson)).ToList();
                    break;
                case "cat":
                    Console.Write("enter category: ");
                    string category = Console.ReadLine();
                    matchingMeetings = _meetingList.Where(m => m.Category.Contains(category)).ToList();
                    break;
                case "type":
                    Console.Write("enter type: ");
                    string type = Console.ReadLine();
                    matchingMeetings = _meetingList.Where(m => m.Type.Contains(type)).ToList();
                    break;
                case "atte":
                    Console.WriteLine("Enter the minimum amount of attendees");
                    var minPeople = Int32.Parse(Console.ReadLine());
                    matchingMeetings = _meetingList.Where(m => m.Participants.Count >= minPeople).ToList();
                    break;
            }
                
            if (matchingMeetings.Count > 0)
                DisplayMeetingListDetails(matchingMeetings);
            else
                Console.WriteLine("No matching meetings found");
        }
    }
}
