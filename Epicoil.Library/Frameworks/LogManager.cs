using System;
using System.Diagnostics;

namespace Epicoil.Library.Frameworks
{
    public static class LogManager
    {
        private static readonly string logname = "UCC Epicor external application Log";

        public static void WriteEntry(Exception er)
        {
            if (!EventLog.SourceExists(logname))
            {
                EventLog.CreateEventSource(logname, logname);
            }

            EventLog.WriteEntry(logname,
                                er.Message + Environment.NewLine
                                + "------------------------------------------------------------" + Environment.NewLine
                                + er.StackTrace,
                                System.Diagnostics.EventLogEntryType.Error);
        }
    }
}
