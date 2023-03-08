using Marten;
using Weasel.Core;
using Weasel.Core.Migrations;
using Weasel.Postgresql.Tables;

namespace MartenCustomStorageTest;

public class CustomTableStorage : FeatureSchemaBase
{
    private const string TableName = "mt_custom_table";
    public const string InsertSql = $"insert into {TableName}(id) values(?)";
    
    private readonly StoreOptions _options;

    public CustomTableStorage(StoreOptions options) : base("custom_table", options.Advanced.Migrator) => _options = options;

    protected override IEnumerable<ISchemaObject> schemaObjects()
    {
        var table = new Table(new DbObjectName(_options.DatabaseSchemaName, TableName));
        table.AddColumn<string>("id").AsPrimaryKey();
        yield return table;
    }
}