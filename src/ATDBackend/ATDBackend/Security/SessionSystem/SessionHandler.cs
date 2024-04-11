using ATDBackend.Utils;

namespace ATDBackend.Security.SessionSystem
{
    public static class SessionHandler
    {
        const int ExpireMinutes = 10;
        const int SIDLength = 150;

        private static List<Session> sessions = new List<Session>();

        private static string GenerateSID()
        {
            string sid = string.Empty;
            do
            {
                sid = RandomGenerator.Generate(SIDLength, RandomGenerator.RandomParts.All);

            } while (sessions.Where(x => x.SessionID == sid).FirstOrDefault() != null);

            return sid;
        }

        public static void ExtendSessionExpire(Session session)
        {
            session.Expire = DateTime.Now.AddMinutes(ExpireMinutes);
        }


        public static bool RemoveSession(Session session) => sessions.Remove(session);

        public static Session CreateSession(int UserID)
        {
            Session? existing = GetSessionByUserID(UserID);
            if (existing != null) sessions.Remove(existing);

            Session session = new Session(UserID, GenerateSID());
            ExtendSessionExpire(session);

            sessions.Add(session);

            return session;
        }

        public static Session? GetSessionByUserID(int UserID) => sessions.Where(x => x.UserID == UserID).FirstOrDefault();

        public static Session? GetSessionBySID(string SessionID) => sessions.Where(x => x.SessionID == SessionID).FirstOrDefault();

    }
}
