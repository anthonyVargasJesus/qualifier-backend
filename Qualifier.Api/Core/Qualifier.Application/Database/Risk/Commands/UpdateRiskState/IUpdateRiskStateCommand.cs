using System;

namespace Qualifier.Application.Database.Risk.Commands.UpdateRiskState
{
    public interface IUpdateRiskStateCommand
    {
        Task<Object> Execute(int id, int riskStatusId, int updateUserId);
    }
}
