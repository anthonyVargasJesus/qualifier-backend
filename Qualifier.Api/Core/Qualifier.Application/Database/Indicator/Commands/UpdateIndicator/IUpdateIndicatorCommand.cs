namespace Qualifier.Application.Database.Indicator.Commands.UpdateIndicator
{
    public interface IUpdateIndicatorCommand
    {
        Task<Object> Execute(UpdateIndicatorDto model, int id);
    }
}

