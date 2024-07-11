namespace Qualifier.Application.Database.Menace.Commands.CreateMenace
{
    public interface ICreateMenaceCommand
    {
        Task<Object> Execute(CreateMenaceDto model);
    }
}

