
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ATDBackend.Security.SessionSystem
{
    public class Session
    {
        public string SessionID { get; private set; }

        public int UserID { get; private set; }

        public DateTime Expire {  get; set; }

        public bool IsExpired => Expire < DateTime.Now;

        public  Session(int UserID, string SessionID)
        {
            this.UserID = UserID;
            this.SessionID = SessionID;
        }

    }
}
