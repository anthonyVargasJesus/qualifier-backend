namespace Qualifier.Application.Database.SupportForRequirement.Commands.CreateSupportForRequirement
{
    public interface ICreateSupportForRequirementCommand
    {
        Task<Object> Execute(CreateSupportForRequirementDto model);
    }
}

