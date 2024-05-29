namespace Qualifier.Application.Database.UsageClassification.Commands.DeleteUsageClassification
{
    public interface IDeleteUsageClassificationCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

