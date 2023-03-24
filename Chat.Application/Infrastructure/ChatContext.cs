using Microsoft.EntityFrameworkCore;

namespace Chat.Application.Infrastructure
{
    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions<ChatContext> opt) : base(opt) { }
    }
}
