namespace Qualifier.Application.Database.RiskTreatment.Commands.DeleteRiskTreatment
{
    public interface IDeleteRiskTreatmentCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

