namespace Qualifier.Application.Database.RiskTreatmentMethod.Commands.DeleteRiskTreatmentMethod
{
    public interface IDeleteRiskTreatmentMethodCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

