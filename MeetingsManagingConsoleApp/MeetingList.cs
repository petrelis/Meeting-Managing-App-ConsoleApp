﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HelperLibrary;

namespace MeetingsManagingConsoleApp
{
    public class MeetingList
    {
        private List<Meeting> _meetingList { get; set; } = new List<Meeting>(JsonMeetingsToCs());

        static List<Meeting> JsonMeetingsToCs()
        {
            string filePath = FileFunctions.GetFilePathFromBaseDirectory("Meetings.json");
            if (File.Exists(filePath) && new FileInfo(filePath).Length > 1)
            {
                string justText = File.ReadAllText(filePath);

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
            string filePath = FileFunctions.GetFilePathFromBaseDirectory("Meetings.json");

            var jsonMeetingList = String.Empty;

            if (File.Exists(filePath))
            {
                int currentIndex = 1;
                var jsonMeeting = String.Empty;
                foreach (Meeting meeting in _meetingList)
                {
                    if (currentIndex == _meetingList.Count)
                        jsonMeeting = Newtonsoft.Json.JsonConvert.SerializeObject(meeting, Newtonsoft.Json.Formatting.Indented);
                    else
                        jsonMeeting = Newtonsoft.Json.JsonConvert.SerializeObject(meeting, Newtonsoft.Json.Formatting.Indented) + ",\n";
                    jsonMeetingList += jsonMeeting;
                    currentIndex++;
                }
                jsonMeetingList = string.Concat("[\n" + jsonMeetingList + "\n]");
                File.WriteAllText(filePath, jsonMeetingList);
            }
            else Console.WriteLine("Json file does not exist");

            
        }

        public void DisplayMeetingListDetails(List<Meeting> meetings)
        {
            if (meetings.Count == 0) Console.WriteLine("No meetings found");
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
            int peopleAddedCount = 0;
            while(true)
            {
                input = ConsoleInputOutputFunctions.GetNotNullStringFromReadLine("name");
                if (input == "c")
                    break;
                if (!_meetingList[addpIndex].Participants.Contains(input))
                {
                    _meetingList[addpIndex].Participants.Add(input);
                    peopleAddedCount++;
                    Console.WriteLine($"Person added at: {DateTimeOffset.Now}");
                    int i = 1;
                    Meeting currMeeting = _meetingList[addpIndex];
                    foreach(var meeting in _meetingList)
                    {
                        i++;
                        if (meeting.Participants.Contains(input) && i != addpIndex){
                            if (currMeeting.StartDate < meeting.StartDate && currMeeting.EndDate > meeting.StartDate
                                || meeting.StartDate < currMeeting.EndDate && meeting.EndDate > currMeeting.EndDate)
                                Console.WriteLine($"Warning: The person you just added is already in a meeting ({meeting.Name}) that intersects" +
                                    " the one they are being added to right now!");
                        }
                    }
                }
                else
                    Console.WriteLine("Person is already in the meeting");
                
            }
            CsMeetingsToJson();
            return peopleAddedCount;
        }

        internal int RemovePeople(int removepIndex)
        {
            string input = String.Empty;
            Console.WriteLine("Enter participant names one by one, type \"c\" to stop");
            int peopleRemovedCount = 0;
            while (true)
            {
                input = ConsoleInputOutputFunctions.GetNotNullStringFromReadLine("name or \"c\" to stop");
                if (input == "c")
                    break;
                if (input != _meetingList[removepIndex].ResponsiblePerson
                    && _meetingList[removepIndex].Participants.Contains(input))
                {
                    _meetingList[removepIndex].Participants.Remove(input);
                    peopleRemovedCount++;
                }
                else Console.WriteLine("Can't remove");
            }
            CsMeetingsToJson();
            return peopleRemovedCount;
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

        public List<Meeting> GetOwnedMeetings(string responsiblePerson)
        {
            return _meetingList.Where(m => m.ResponsiblePerson.Contains(responsiblePerson)).ToList();
        }

        public void DisplayMatchingMeetings(string searchArg)
        {
            var matchingMeetings = new List<Meeting>();
            switch (searchArg)
            {
                case "name":
                    Console.Write("enter name: ");
                    string name = ConsoleInputOutputFunctions.GetNotNullStringFromReadLine("name");
                    matchingMeetings = _meetingList.Where(m => m.Name.Contains(name)).ToList();
                    break;
                case "desc":
                    Console.Write("enter description: ");
                    string description = Console.ReadLine();
                    matchingMeetings = _meetingList.Where(m => m.Description.Contains(description)).ToList();
                    break;
                case "resp":
                    Console.Write("enter responsible person's name: ");
                    string resPerson = ConsoleInputOutputFunctions.GetNotNullStringFromReadLine("name");
                    matchingMeetings = _meetingList.Where(m => m.ResponsiblePerson.Contains(resPerson)).ToList();
                    break;
                case "cat":
                    int categoryIndex = Meeting.GetCategoryIndex();
                    matchingMeetings = _meetingList.Where(m => m.Category == (Meeting.MeetingCategory)categoryIndex).ToList();
                    break;
                case "type":
                    Console.Write("enter type: ");
                    int typeIndex = Meeting.GetTypeIndex();
                    matchingMeetings = _meetingList.Where(m => m.Type == (Meeting.MeetingType)typeIndex).ToList();
                    break;
                case "atte":
                    Console.WriteLine("Enter the minimum amount of attendees");
                    var minPeople = ConsoleInputOutputFunctions.GetIntFromReadLine();
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
