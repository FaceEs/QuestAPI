using Microsoft.EntityFrameworkCore;
using QuestAPI.Core.Data.Models.Item;
using QuestAPI.Core.Data.Models.Player;
using QuestAPI.Core.Data.Models.PlayerQuest;
using QuestAPI.Core.Data.Models.Quest;

namespace QuestAPI.Web.Data
{
    public class QuestDbContext : DbContext
    {
        public DbSet<ItemEntry> Items { get; set; }
        public DbSet<PlayerEntry> Players { get; set; }
        public DbSet<PlayerQuestEntry> PlayerQuests { get; set; }
        public DbSet<QuestEntry> Quests { get; set; }
        public QuestDbContext()
        {

        }
        public QuestDbContext(DbContextOptions<QuestDbContext> options):base(options)
        {
        }
    }
}
