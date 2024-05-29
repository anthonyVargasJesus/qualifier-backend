namespace Qualifier.Application.Database.ActiveType.Commands.CreateActiveType
{
    public interface ICreateActiveTypeCommand
    {
        Task<Object> Execute(CreateActiveTypeDto model);
    }
}

