namespace Qualifier.Application.Database.Company.Commands.UpdateCompany
{
    public interface IUpdateCompanyCommand
    {
        Task<Object> Execute(UpdateCompanyDto model, int id);
    }
}

