using System;

namespace Com.Ericmas001.Util
{
    public class DateTimeWithZone
    {
        private readonly DateTime m_UtcDateTime;
        private readonly TimeZoneInfo m_TimeZone;

        public DateTimeWithZone(DateTime dateTime, TimeZoneInfo timeZone)
        {
            m_UtcDateTime = TimeZoneInfo.ConvertTimeToUtc(dateTime, timeZone);
            m_TimeZone = timeZone;
        }

        public DateTime UniversalTime { get { return m_UtcDateTime; } }

        public TimeZoneInfo TimeZone { get { return m_TimeZone; } }

        public DateTime LocalTime
        {
            get
            {
                return TimeZoneInfo.ConvertTime(m_UtcDateTime, m_TimeZone);
            }
        }
    }
}