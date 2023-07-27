using Microsoft.EntityFrameworkCore;
using TestApplication.Models;

namespace TestApplication.Database
{
    public class ApplicationContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<SendChan> SendChans { get; set; }
        public DbSet<Topic > Topics { get; set; }
        public DbSet<ChannelSetting> ChannelSettings { get; set; }
        public DbSet<WebcabMessage > WebcabMessages { get; set; }
        public DbSet<TcLogs> TcLogs { get; set; }

        public ApplicationContext(DbContextOptions <ApplicationContext> options) : base(options)
        {
           // Database.EnsureDeleted();   // удаляем бд со старой схемой
            Database.EnsureCreated();   // создаем бд с новой схемой
        }        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Tom", Email = "Tom@mail.com", Phone = "12345" },
                new User { Id = 2, Name = "Alice", Email = "Alice@mail.com", Phone = "54321" },
                new User { Id = 3, Name = "James", Email = "James@mail.com", Phone = "45876" });

            modelBuilder.Entity<Topic>().HasData(
                new Topic { Id = 1, Name = "Topic1" },
                new Topic { Id = 2, Name = "Topic2" },
                new Topic { Id = 3, Name = "Topic3" },
                new Topic { Id = 4, Name = "Topic4" }
                );

            modelBuilder.Entity<SendChan>().HasData(
                new SendChan { Id = 1, Name = "Email" },
                new SendChan { Id = 2, Name = "Webcab" },
                new SendChan { Id = 3, Name = "Telegram" }
                );

            modelBuilder.Entity<ChannelSetting>().HasData(
                new ChannelSetting { Id = 1, UserId = 1, TopicId = 1, SendChanId = 1, Enabled = true },
                new ChannelSetting { Id = 2, UserId = 1, TopicId = 1, SendChanId = 2, Enabled = true },
                new ChannelSetting { Id = 3, UserId = 1, TopicId = 1, SendChanId = 3, Enabled = true },

                new ChannelSetting { Id = 4, UserId = 2, TopicId = 1, SendChanId = 1, Enabled = true },
                new ChannelSetting { Id = 5, UserId = 2, TopicId = 1, SendChanId = 2, Enabled = true },
                new ChannelSetting { Id = 6, UserId = 2, TopicId = 1, SendChanId = 3, Enabled = true },

                new ChannelSetting { Id = 7, UserId = 3, TopicId = 1, SendChanId = 1, Enabled = true },
                new ChannelSetting { Id = 8, UserId = 3, TopicId = 1, SendChanId = 2, Enabled = true },
                new ChannelSetting { Id = 9, UserId = 3, TopicId = 1, SendChanId = 3, Enabled = true },

                new ChannelSetting { Id = 10, UserId = 1, TopicId = 2, SendChanId = 1, Enabled = true },
                new ChannelSetting { Id = 11, UserId = 1, TopicId = 2, SendChanId = 2, Enabled = false },
                new ChannelSetting { Id = 12, UserId = 1, TopicId = 2, SendChanId = 3, Enabled = true },

                new ChannelSetting { Id = 13, UserId = 2, TopicId = 2, SendChanId = 1, Enabled = true },
                new ChannelSetting { Id = 14, UserId = 2, TopicId = 2, SendChanId = 2, Enabled = false },
                new ChannelSetting { Id = 15, UserId = 2, TopicId = 2, SendChanId = 3, Enabled = true },

                new ChannelSetting { Id = 16, UserId = 3, TopicId = 2, SendChanId = 1, Enabled = false },
                new ChannelSetting { Id = 17, UserId = 3, TopicId = 2, SendChanId = 2, Enabled = true },
                new ChannelSetting { Id = 18, UserId = 3, TopicId = 2, SendChanId = 3, Enabled = true },

                new ChannelSetting { Id = 19, UserId = 1, TopicId = 3, SendChanId = 1, Enabled = true },
                new ChannelSetting { Id = 20, UserId = 1, TopicId = 3, SendChanId = 2, Enabled = false },
                new ChannelSetting { Id = 21, UserId = 1, TopicId = 3, SendChanId = 3, Enabled = true },

                new ChannelSetting { Id = 22, UserId = 2, TopicId = 3, SendChanId = 1, Enabled = false },
                new ChannelSetting { Id = 23, UserId = 2, TopicId = 3, SendChanId = 2, Enabled = false },
                new ChannelSetting { Id = 24, UserId = 2, TopicId = 3, SendChanId = 3, Enabled = false },

                new ChannelSetting { Id = 25, UserId = 3, TopicId = 3, SendChanId = 1, Enabled = false },
                new ChannelSetting { Id = 26, UserId = 3, TopicId = 3, SendChanId = 2, Enabled = true },
                new ChannelSetting { Id = 27, UserId = 3, TopicId = 3, SendChanId = 3, Enabled = true }
                );
        }
        
    }
}
