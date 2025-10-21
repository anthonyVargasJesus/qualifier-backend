using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Menace.Queries.GetMenacesByCompanyId
{
    public class GetMenacesByCompanyIdQuery : IGetMenacesByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetMenacesByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from menace in _databaseService.Menace
                                      join menaceType in _databaseService.MenaceType on menace.menaceType equals menaceType
                                      where ((menace.isDeleted == null || menace.isDeleted == false) && menace.companyId == companyId)
                                      && (menace.name.ToUpper().Contains(search.ToUpper()))
                                      select new MenaceEntity
                                      {
                                          menaceId = menace.menaceId,
                                          menaceTypeId = menace.menaceTypeId,
                                          name = menace.name,
                                          companyId = menace.companyId,
                                          menaceType = new MenaceTypeEntity
                                          {
                                              name = menaceType.name,
                                          },
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetMenacesByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetMenacesByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetMenacesByCompanyIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, companyId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int companyId)
        {
            var total = await (from menace in _databaseService.Menace
                               join menaceType in _databaseService.MenaceType on menace.menaceType equals menaceType
                               where ((menace.isDeleted == null || menace.isDeleted == false) && menace.companyId == companyId)
                               && (menace.name.ToUpper().Contains(search.ToUpper()))
                               select new MenaceEntity
                               {
                                   menaceId = menace.menaceId,
                               }).CountAsync();
            return total;
        }

    }
}

