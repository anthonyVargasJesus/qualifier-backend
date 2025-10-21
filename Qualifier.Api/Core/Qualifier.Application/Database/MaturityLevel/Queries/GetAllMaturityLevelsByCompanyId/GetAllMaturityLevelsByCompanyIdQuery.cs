using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.MaturityLevel.Queries.GetAllMaturityLevelsByCompanyId
{
    internal class GetAllMaturityLevelsByCompanyIdQuery : IGetAllMaturityLevelsByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllMaturityLevelsByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from maturityLevel in _databaseService.MaturityLevel
                                      where ((maturityLevel.isDeleted == null || maturityLevel.isDeleted == false) && maturityLevel.companyId == companyId)
                                      select new MaturityLevelEntity
                                      {
                                          maturityLevelId = maturityLevel.maturityLevelId,
                                          name = maturityLevel.name,
                                          abbreviation = maturityLevel.abbreviation,
                                          value = maturityLevel.value,
                                          color = maturityLevel.color,
                                      })
                                       .OrderBy(e=>e.value)
                                      .ToListAsync();

                BaseResponseDto<GetAllMaturityLevelsByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllMaturityLevelsByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllMaturityLevelsByCompanyIdDto>>(entities.OrderByDescending(x => x.value).ToList());
                return baseResponseDto;
            }
            catch (Exception ex)
            {
                throw ex;
                //return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

