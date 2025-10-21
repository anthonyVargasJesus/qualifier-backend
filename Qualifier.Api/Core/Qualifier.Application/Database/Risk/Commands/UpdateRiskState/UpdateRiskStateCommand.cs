using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Application.Database.Risk.Commands.UpdateRisk;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Application.Database.Risk.Commands.UpdateRiskState
{
    internal class UpdateRiskStateCommand : IUpdateRiskStateCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IRiskRepository _riskRepository;

        public UpdateRiskStateCommand(IDatabaseService databaseService, IMapper mapper, IRiskRepository riskRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _riskRepository = riskRepository;
        }

        public async Task<Object> Execute(int id, int riskStatusId, int updateUserId)
        {
            try
            {
                await _riskRepository.UpdateRiskStatusId(id, riskStatusId, updateUserId);
                return riskStatusId;
            }
            catch (Exception ex)
            {
                throw ex;
                //return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}
