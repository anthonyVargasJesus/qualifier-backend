namespace Qualifier.Application.Database.ActiveType.Commands.UpdateActiveType
{
    public interface IUpdateActiveTypeCommand
    {
        Task<Object> Execute(UpdateActiveTypeDto model, int id);
    }
}

