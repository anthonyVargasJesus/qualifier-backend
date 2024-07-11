namespace Qualifier.Application.Database.MenaceType.Commands.UpdateMenaceType
{
    public interface IUpdateMenaceTypeCommand
    {
        Task<Object> Execute(UpdateMenaceTypeDto model, int id);
    }
}

