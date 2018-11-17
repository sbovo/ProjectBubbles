using System;

namespace ProjectBubbles.Models
{
    // TODO: In a refactoring task: Remove the dependancy between Item and TableItem.
    public class Item
    {
        /// <summary>
        /// TeamId identifies a team or a group of persons
        /// </summary>
        public string TeamId { get; set; }
        /// <summary>
        /// MeetingDatePlus is stored as string in the international format: YYYY-MM-DD
        /// +
        /// Some string content that can differentiate meetings occuring the same day
        /// </summary>
        public string MeetingDatePlus { get; set; }
        public ParticipationEnum Participation { get; set; }
        public string UserName { get; set; }
        public string Location { get; set; }
        public string Activity { get; set; }
    }

    public enum ParticipationEnum
    {
        Yes,
        No,
        Tentative,
        NotAnsweredYet
    }
}
