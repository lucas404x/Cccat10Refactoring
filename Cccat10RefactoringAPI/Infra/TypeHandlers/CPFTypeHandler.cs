using Cccat10RefactoringDomain.ValueObjects;
using Dapper;
using System.Data;

namespace Cccat10RefactoringAPI.Infra.TypeHandlers;

public class CPFTypeHandler : SqlMapper.TypeHandler<CPF>
{
    public override CPF Parse(object value)
    {
        return value is CPF ? (CPF)value : new CPF((string)value);
    }

    public override void SetValue(IDbDataParameter parameter, CPF value)
    {
        parameter.Value = value.ToString();
    }
}
