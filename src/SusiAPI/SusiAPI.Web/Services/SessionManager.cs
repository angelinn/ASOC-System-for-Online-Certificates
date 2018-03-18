using SusiAPICommon.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SusiAPI.Web.Services
{
    public class SessionEntry
    {
        public SusiSession Session { get; set; }
        public StudentInfo StudentInfo { get; set; } 
        public DateTime Expiration { get; set; }
    }

    public class SessionManager
    {
        private ConcurrentDictionary<string, SessionEntry> clients = new ConcurrentDictionary<string, SessionEntry>();
        private Timer timer;

        public SessionManager()
        {
            timer = new Timer((e) => Cleanup(), null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
        }

        public void AddSession(string username, SusiSession service)
        {
            clients[username] = new SessionEntry
            {
                Session = service,
                Expiration = DateTime.Now.AddMinutes(5)
            };
        }

        public bool TryGetSession(string username, out SessionEntry session)
        {
            session = null;
            if (!clients.ContainsKey(username))
                return false;

            session = clients[username];
            return true;
        }

        private void Cleanup()
        {
            foreach (var tuple in clients)
            {
                if (DateTime.Now > tuple.Value.Expiration)
                    clients.Remove(tuple.Key, out SessionEntry value);
            }
        }
    }
}
