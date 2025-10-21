using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.MenaceType.Queries.GetAllMenaceTypesByCompanyId
{
    internal class GetAllMenaceTypesByCompanyIdQuery : IGetAllMenaceTypesByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllMenaceTypesByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from menaceType in _databaseService.MenaceType
                                      where ((menaceType.isDeleted == null || menaceType.isDeleted == false) && menaceType.companyId == companyId)
                                      select new MenaceTypeEntity
                                      {
                                          menaceTypeId = menaceType.menaceTypeId,
                                          name = menaceType.name,
                                          companyId = menaceType.companyId,
                                      }).ToListAsync();

                BaseResponseDto<GetAllMenaceTypesByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllMenaceTypesByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllMenaceTypesByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

