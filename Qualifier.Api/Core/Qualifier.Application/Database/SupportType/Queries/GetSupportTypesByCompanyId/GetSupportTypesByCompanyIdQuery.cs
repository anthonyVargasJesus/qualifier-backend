using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.SupportType.Queries.GetSupportTypesByCompanyId
{
    public class GetSupportTypesByCompanyIdQuery : IGetSupportTypesByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetSupportTypesByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from supportType in _databaseService.SupportType
                                      where ((supportType.isDeleted == null || supportType.isDeleted == false) && supportType.companyId == companyId)
                                      && (supportType.name.ToUpper().Contains(search.ToUpper()))
                                      select new SupportTypeEntity
                                      {
                                          supportTypeId = supportType.supportTypeId,
                                          name = supportType.name,
                                          companyId = supportType.companyId,
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetSupportTypesByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetSupportTypesByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetSupportTypesByCompanyIdDto>>(entities);
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
            var total = await (from supportType in _databaseService.SupportType
                               where ((supportType.isDeleted == null || supportType.isDeleted == false) && supportType.companyId == companyId)
                               && (supportType.name.ToUpper().Contains(search.ToUpper()))
                               select new SupportTypeEntity
                               {
                                   supportTypeId = supportType.supportTypeId,
                               }).CountAsync();
            return total;
        }

    }
}

