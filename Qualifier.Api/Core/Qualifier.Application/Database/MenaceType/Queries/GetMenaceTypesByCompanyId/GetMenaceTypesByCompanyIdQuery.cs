using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.MenaceType.Queries.GetMenaceTypesByCompanyId
{
    public class GetMenaceTypesByCompanyIdQuery : IGetMenaceTypesByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetMenaceTypesByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from menaceType in _databaseService.MenaceType
                                      where ((menaceType.isDeleted == null || menaceType.isDeleted == false) && menaceType.companyId == companyId)
                                      && (menaceType.name.ToUpper().Contains(search.ToUpper()))
                                      select new MenaceTypeEntity
                                      {
                                          menaceTypeId = menaceType.menaceTypeId,
                                          name = menaceType.name,
                                          companyId = menaceType.companyId,
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetMenaceTypesByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetMenaceTypesByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetMenaceTypesByCompanyIdDto>>(entities);
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
            var total = await (from menaceType in _databaseService.MenaceType
                               where ((menaceType.isDeleted == null || menaceType.isDeleted == false) && menaceType.companyId == companyId)
                               && (menaceType.name.ToUpper().Contains(search.ToUpper()))
                               select new MenaceTypeEntity
                               {
                                   menaceTypeId = menaceType.menaceTypeId,
                               }).CountAsync();
            return total;
        }

    }
}

