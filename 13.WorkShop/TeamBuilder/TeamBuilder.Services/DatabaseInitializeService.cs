using System;
using TeamBuilder.Data;

namespace TeamBuilder.Services
{
    public class DatabaseInitializeService
    {
        private readonly TeamBuilderContext db;

        public DatabaseInitializeService(TeamBuilderContext db)
        {
            this.db = db;
        }

        public void DatabaseInitialize()
        {
            this.db.Database.EnsureDeleted();
            this.db.Database.EnsureCreated();

        }
    }
}
