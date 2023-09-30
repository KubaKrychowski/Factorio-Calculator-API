/*

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Uitilies

public static class TestUtilities
{
    public static void InitializeDbForTests(DbContext db)
    {
        db.Messages.AddRange(GetSeedingMessages());
        db.SaveChanges();
    }

    public static void ReinitializeDbForTests<TEntity>(DbContext db)
    {
        db.Set<TEntity>(db.Message);
        InitializeDbForTests(db);
    }

    public static List<Message> GetSeedingMessages()
    {
        return new List<Message>()
        {
            new Message(){ Text = "TEST RECORD: You're standing on my scarf." },
            new Message(){ Text = "TEST RECORD: Would you like a jelly baby?" },
            new Message(){ Text = "TEST RECORD: To the rational mind, " +
                "nothing is inexplicable; only unexplained." }
        };
    }
    // </snippet1>
}*/