namespace Qualifier.Application.Database.UserRequirementFamily.Commands.SetUserRequirementFamilies
{
    public interface ISetUserRequirementFamiliesCommand
    {
        Task<Object> Execute(SetUserRequirementFamiliesDto model);
    }
}
