namespace Qualifier.Application.Database.SupportForRequirement.Commands.UpdateSupportForRequirement
{
    public interface IUpdateSupportForRequirementCommand
    {
        Task<Object> Execute(UpdateSupportForRequirementDto model, int id);
    }
}

