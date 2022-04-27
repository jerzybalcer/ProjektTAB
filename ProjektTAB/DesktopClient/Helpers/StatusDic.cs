using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopClient.Helpers
{
    static class StatusDic
    {
        static Dictionary<string, string> statusDic = new Dictionary<string, string>(){
            {"Registered", "Zarejestrowane" },
            {"Started", "Rozpoczęte" },
            {"Finished", "Zakończone" },
            {"Unattended", "Nieobecny" },
            {"Cancelled", "Odwołane" },
            {"Failed", "Nieudane" },
            {"Ordered", "Zlecone"},
            {"SuccessfullyExecuted", "Zakończone"},
            {"CancelledByAssistant", "Anulowane"},
            {"AcceptedByManager", "Zaakceptowane"},
            {"RejectedByManager", "Odrzucone"}
            };

        public static string getStatusLabel(string stat)
        {
            return statusDic[stat];
        }
    }
}
