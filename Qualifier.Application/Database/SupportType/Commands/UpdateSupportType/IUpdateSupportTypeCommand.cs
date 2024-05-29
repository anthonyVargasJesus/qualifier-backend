namespace Qualifier.Application.Database.SupportType.Commands.UpdateSupportType
{
    public interface IUpdateSupportTypeCommand
    {
        Task<Object> Execute(UpdateSupportTypeDto model, int id);
    }
}
//SupportType
//services.AddTransient<IUpdateSupportTypeCommand, UpdateSupportTypeCommand>();
