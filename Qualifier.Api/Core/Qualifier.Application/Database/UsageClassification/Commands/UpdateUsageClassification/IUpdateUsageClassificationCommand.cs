namespace Qualifier.Application.Database.UsageClassification.Commands.UpdateUsageClassification
{
    public interface IUpdateUsageClassificationCommand
    {
        Task<Object> Execute(UpdateUsageClassificationDto model, int id);
    }
}

