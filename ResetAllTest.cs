using Marten;
using Xunit;

namespace MartenCustomStorageTest;

public class ResetAllTest
{
    [Fact]
    public async Task WhenCustomTableIsUsedInABatchWithOtherDocumentResetAllShouldWork()
    {
        var store = DocumentStore.For(_ =>
        {
            _.Connection("HOST=localhost; DATABASE='postgres'; USER ID='postgres'; PASSWORD='Password12!'");
            _.Storage.Add<CustomTableStorage>();
        });
        await store.Schema.ApplyAllConfiguredChangesToDatabaseAsync();
        await using var session = store.LightweightSession();
        session.QueueSqlCommand(CustomTableStorage.InsertSql, Guid.NewGuid().ToString());
        session.Insert(new User(Guid.NewGuid().ToString(), "John Doe"));
        await session.SaveChangesAsync();
        await store.Advanced.ResetAllData();
    }
}