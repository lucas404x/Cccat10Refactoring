using Dapper;
using System.Data;

namespace Cccat10RefactoringAPI.Infra.TypeHandlers;

public class SQLiteGuidTypeHandler : SqlMapper.TypeHandler<Guid>
{
    public override Guid Parse(object value)
    {
        if (value is Guid)
        {
            return (Guid)value;
        }
        return new Guid((string)value);
    }

    public override void SetValue(IDbDataParameter parameter, Guid value)
    {
        parameter.Value = value.ToString();
    }
}
