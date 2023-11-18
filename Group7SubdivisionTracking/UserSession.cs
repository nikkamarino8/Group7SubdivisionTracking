using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group7SubdivisionTracking
{
    internal class UserSession_cs
    {
    }
    public static class UserSession
    {

        public static string Email { get; set; }
        public static string Password { get; set; }
        public static string UserType { get; set; }
        public static string FirstName { get; set; }
        public static string UserID { get; set; }
        public static string LastLogin { get; set;}
        public static string LatestVisitorFirstName { get; set; }
        public static string LatestVisitorFullName { get; set; }
        public static string TotalNumberVisitors {  get; set; }
        public static string FrequentVisitorName {  get; set; }
        public static string FrequentlyVisitedR { get; set; }

    }
}
