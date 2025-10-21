namespace Qualifier.Application.Database.MenaceType.Commands.CreateMenaceType
{
    public interface ICreateMenaceTypeCommand
    {
        Task<Object> Execute(CreateMenaceTypeDto model);
    }
}

