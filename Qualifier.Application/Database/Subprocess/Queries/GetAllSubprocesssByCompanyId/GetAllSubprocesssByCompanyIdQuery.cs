using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Subprocess.Queries.GetAllSubprocesssByCompanyId
{
    internal class GetAllSubprocesssByCompanyIdQuery : IGetAllSubprocesssByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllSubprocesssByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from subprocess in _databaseService.Subprocess
                                      join macroprocess in _databaseService.Macroprocess on subprocess.macroprocess equals macroprocess
                                      where ((subprocess.isDeleted == null || subprocess.isDeleted == false) && subprocess.companyId == companyId)
                                      select new SubprocessEntity
                                      {
                                          subprocessId = subprocess.subprocessId,
                                          code = subprocess.code,
                                          name = subprocess.name,
                                          macroprocessId = subprocess.macroprocessId,
                                      }).ToListAsync();

                BaseResponseDto<GetAllSubprocesssByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllSubprocesssByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllSubprocesssByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

