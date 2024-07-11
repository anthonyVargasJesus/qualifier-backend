using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Menace.Queries.GetAllMenacesByCompanyId
{
    internal class GetAllMenacesByCompanyIdQuery : IGetAllMenacesByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllMenacesByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from menace in _databaseService.Menace
                                      join menaceType in _databaseService.MenaceType on menace.menaceType equals menaceType
                                      where ((menace.isDeleted == null || menace.isDeleted == false) && menace.companyId == companyId)
                                      select new MenaceEntity
                                      {
                                          menaceId = menace.menaceId,
                                          menaceTypeId = menace.menaceTypeId,
                                          name = menace.name,
                                          companyId = menace.companyId,
                                      }).ToListAsync();

                BaseResponseDto<GetAllMenacesByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllMenacesByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllMenacesByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

