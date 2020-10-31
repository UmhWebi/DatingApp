using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.SignalR
{
    public class PresenceTracker
    {
        private static readonly Dictionary<string, List<string>> OnlineUsers =
            new Dictionary<string, List<string>>();

        public Task<bool> UserConnected(string username, string connectionId)
        {
            bool isOnline = false;
            lock (OnlineUsers)
            {
                if (OnlineUsers.ContainsKey(username))
                {
                    OnlineUsers[username].Add(connectionId);
                }
                else
                {
                    OnlineUsers.Add(username, new List<string> { connectionId });
                    isOnline = true;
                }
            }

            return Task.FromResult(isOnline);
        }

        public Task<bool> UserDisconnected(string username, string connectionId)
        {
            bool offOnline = false;
            lock (OnlineUsers)
            {
                if (!OnlineUsers.ContainsKey(username))
                {
                    return Task.FromResult(offOnline);
                }

                OnlineUsers[username].Remove(connectionId);

                if (OnlineUsers.Count == 0) {
                    OnlineUsers.Remove(username);
                    offOnline = true;
                }
            }

            return Task.FromResult(offOnline);
        }

        public Task<string[]> GetOnlineUsers() {
            string[] usersOnline;
            lock(OnlineUsers) {
                usersOnline = OnlineUsers.OrderBy(k => k.Key).Select(k => k.Key).ToArray();
            }

            return Task.FromResult(usersOnline);
        }

        public Task<List<string>> GetConnectionsForUser(string username)
        {
            List<string> connectionIds;
            lock(OnlineUsers)
            {
                connectionIds = OnlineUsers.GetValueOrDefault(username);
            }

            return Task.FromResult(connectionIds);
        }
    }
}