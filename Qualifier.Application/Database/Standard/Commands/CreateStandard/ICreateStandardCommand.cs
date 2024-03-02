namespace Qualifier.Application.Database.Standard.Commands.CreateStandard
{
    public interface ICreateStandardCommand
    {
        Task<Object> Execute(CreateStandardDto model);
    }
}

