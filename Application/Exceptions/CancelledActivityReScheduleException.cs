﻿namespace Application.Exceptions
{
    public class CancelledActivityReScheduleException : Exception
    {
        public CancelledActivityReScheduleException()
            : base("Cannot ReSchedule a Cancelled Activity")
        {

        }
    }
}
