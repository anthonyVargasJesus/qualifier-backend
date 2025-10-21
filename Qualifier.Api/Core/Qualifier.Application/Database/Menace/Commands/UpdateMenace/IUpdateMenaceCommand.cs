namespace Qualifier.Application.Database.Menace.Commands.UpdateMenace
{
    public interface IUpdateMenaceCommand
    {
        Task<Object> Execute(UpdateMenaceDto model, int id);
    }
}

