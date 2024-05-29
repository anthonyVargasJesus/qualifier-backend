namespace Qualifier.Application.Database.UsageClassification.Commands.CreateUsageClassification
{
    public interface ICreateUsageClassificationCommand
    {
        Task<Object> Execute(CreateUsageClassificationDto model);
    }
}

