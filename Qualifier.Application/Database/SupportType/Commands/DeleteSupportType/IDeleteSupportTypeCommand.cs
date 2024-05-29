namespace Qualifier.Application.Database.SupportType.Commands.DeleteSupportType
{
    public interface IDeleteSupportTypeCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}
//SupportType
//services.AddTransient<IDeleteSupportTypeCommand, DeleteSupportTypeCommand>();
