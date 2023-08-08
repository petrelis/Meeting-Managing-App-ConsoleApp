using Microsoft.VisualStudio.TestTools.UnitTesting;
using MeetingsManagingConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsManagingConsoleApp.Tests
{
    [TestClass()]
    public class MeetingListTests
    {
        [TestMethod]
        public void AddMeetingTest_ProperDatetime_ReturnsTrue()
        {
            //Arrange
            List<string> participants = new List<string>();
            participants.Add("person1");
            participants.Add("person2");

            MeetingList meetingList = new MeetingList();

            var meeting = new Meeting(
                "name",
                "responsiblePerson",
                "description",
                0,
                0,
                new DateTimeOffset(2022, 04, 03, 20, 30, 0, TimeSpan.Zero),
                new DateTimeOffset(2022, 04, 03, 21, 30, 0, TimeSpan.Zero),
                participants);
            
            //Act
            var result = meetingList.AddMeeting(meeting);

            //Assert
            Assert.IsTrue(result);
        }
    }
}