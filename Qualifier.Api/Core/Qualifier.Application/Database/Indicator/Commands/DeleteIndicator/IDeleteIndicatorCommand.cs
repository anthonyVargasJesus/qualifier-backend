namespace Qualifier.Application.Database.Indicator.Commands.DeleteIndicator
{
    public interface IDeleteIndicatorCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

