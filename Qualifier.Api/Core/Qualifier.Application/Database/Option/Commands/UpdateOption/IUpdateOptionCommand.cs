namespace Qualifier.Application.Database.Option.Commands.UpdateOption
{
    public interface IUpdateOptionCommand
    {
        Task<Object> Execute(UpdateOptionDto model, int id);
    }
}

