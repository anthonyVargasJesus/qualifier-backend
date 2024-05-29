namespace Qualifier.Application.Database.SupportType.Commands.CreateSupportType
{
public interface ICreateSupportTypeCommand
{
Task<Object> Execute(CreateSupportTypeDto model);
}
}
//SupportType
//services.AddTransient<ICreateSupportTypeCommand, CreateSupportTypeCommand>();
