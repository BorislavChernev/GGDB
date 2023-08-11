using GoodGameDatabase.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace GoodGameDatabase.Web.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext dbContext;

        public ChatHub(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task SendMessage(string user, string message, int discussionId)
        {
            string userName = this.Context.User.Identity.Name;
            DateTime timestamp = DateTime.UtcNow;
            var newMessage = new Message
            {
                DiscussionId = discussionId,
                SenderId = Guid.Parse(this.Context.UserIdentifier),
                Content = message,
                Timestamp = DateTime.UtcNow
            };

            await this.dbContext.Messages.AddAsync(newMessage);
            await this.dbContext.SaveChangesAsync();

            Message[] msg = await this.dbContext.Messages.Where(d => d.DiscussionId == discussionId).Include(m => m.Sender).Include(m => m.Discussion).ToArrayAsync();

            await Clients.All.SendAsync("ReceiveMessage", user, message, timestamp.ToString("yyyy-MM-dd HH:mm:ss"));
            //await Clients.Group($"discussion-{discussionId}").SendAsync("ReceiveMessage", userName, message);
        }

        public async Task JoinDiscussionGroup(int discussionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"discussion-{discussionId}");
        }

        public async Task LeaveDiscussionGroup(int discussionId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"discussion-{discussionId}");
        }

        public override async Task OnConnectedAsync()
        {
            // Code to execute when a user connects to the hub
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // Code to execute when a user disconnects from the hub
        }
    }
}
