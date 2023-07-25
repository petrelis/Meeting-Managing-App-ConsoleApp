using System;
using System.Collections.ObjectModel;
using HelperClass;

namespace MeetingsManagingConsoleApp
{
    internal class Program
    {
        private List<Meeting> _meetingList { get; set; }

        static void Main(string[] args)
        {
            var meetingsList = new MeetingList();

            Console.Write("Enter your username: ");
            string username = MiscFunctions.GetNotNullStringFromReadLine("username");

            Console.WriteLine("Type \"help\" for commands");

            while(true)
            {
                Console.Write("Enter a command: ");
                string input = Console.ReadLine();

                switch(input)
                {
                    case "help":
                        Console.WriteLine("\nadd - create a new meeting\n" +
                            "addp - add participants to owned meetings\n" +
                            "removep - remove participants from owned meetings\n" +
                            "remove - delete one of your own meetings\n" +
                            "listall - display all meetings\n" +
                            "search - search for meetings based on attributes\n" +
                            "exit - stops the app\n");
                        break;

                    case "clear":
                        Console.Clear();
                        break;

                    case "add":
                        Meeting newMeeting = Meeting.AddNewMeeting(username);
                        meetingsList.AddMeeting(newMeeting);

                        break;

                    case "addp":
                        Console.Clear();
                        var ownedMeetingsAddp = meetingsList.GetOwnedMeetings(username);
                        if (ownedMeetingsAddp.Count > 0)
                        {
                            meetingsList.DisplayMeetingListDetails(ownedMeetingsAddp);
                            Console.WriteLine("Which meeting do you want to add people to (enter index): ");
                            int addpIndex = MiscFunctions.GetIntFromReadLine() - 1;
                            int addedIndex = meetingsList.AddPeople(addpIndex);
                            Console.Clear();
                            Console.WriteLine($"{addedIndex} Participants added");
                        }
                        break;

                    case "removep":
                        Console.Clear();
                        var ownedMeetingsRemovep = meetingsList.GetOwnedMeetings(username);
                        if (ownedMeetingsRemovep.Count > 0)
                        {
                            meetingsList.DisplayMeetingListDetails(ownedMeetingsRemovep);
                            Console.WriteLine("Which meeting do you want to remove people from (enter index): ");
                            int removepIndex = MiscFunctions.GetIntFromReadLine() - 1;
                            int removedIndex = meetingsList.RemovePeople(removepIndex);
                            Console.Clear();
                            Console.WriteLine($"{removedIndex} Participants removed");
                        }
                        break;

                    case "listall":
                        Console.Clear();
                        meetingsList.DisplayAllMeetings();
                        break;

                    case "remove":
                        Console.Clear();
                        var ownedMeetingsRemove = meetingsList.GetOwnedMeetings(username);
                        if (ownedMeetingsRemove.Count > 0)
                        {
                            meetingsList.DisplayMeetingListDetails(ownedMeetingsRemove);
                            Console.Write("Which meeting do you want to delete (enter index): ");
                            int removeIndex = MiscFunctions.GetIntFromReadLine() - 1;
                            var removeMeeting = ownedMeetingsRemove[removeIndex];
                            meetingsList.DeleteMeeting(removeMeeting);
                            meetingsList.CsMeetingsToJson();
                            Console.Clear();
                            Console.WriteLine("Meeting deleted");
                        }
                        else Console.WriteLine("You don't own any meetings, so you can't delete any");
                        break;



                    case "search":
                        var searchArg = String.Empty;
                        while (searchArg != "name" && searchArg != "desc" && searchArg != "resp" && searchArg != "cat" && searchArg != "type" && searchArg != "atte")
                        {
                            Console.Clear();
                            Console.Write("Enter one of the following meeting attribute filters:" +
                                "\nname, desc, resp, cat, or type: ");
                            searchArg = Console.ReadLine();
                        }
                        meetingsList.DisplayMatchingMeetings(searchArg);
                        break;

                    case "exit":
                        return;

                    default:
                        Console.Clear();
                        Console.WriteLine("Not a valid command");
                        Console.WriteLine("Type \"help\" for commands");
                        break;


                }
            }
        }
    }
}