namespace Qualifier.Application.Database.Indicator.Commands.CreateIndicator
{
    public interface ICreateIndicatorCommand
    {
        Task<Object> Execute(CreateIndicatorDto model);
    }
}

