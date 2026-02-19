using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Owner.Queries.GetOwnersByCompanyId
{
    public class GetOwnersByCompanyIdQuery : IGetOwnersByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetOwnersByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from owner in _databaseService.Owner
                                      where ((owner.isDeleted == null || owner.isDeleted == false) && owner.companyId == companyId)
                                      && (owner.name.ToUpper().Contains(search.ToUpper()))
                                      select new OwnerEntity
                                      {
                                          ownerId = owner.ownerId,
                                          code = owner.code,
                                          name = owner.name,
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetOwnersByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetOwnersByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetOwnersByCompanyIdDto>>(entities);
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
            var total = await (from owner in _databaseService.Owner
                               where ((owner.isDeleted == null || owner.isDeleted == false) && owner.companyId == companyId)
                               && (owner.name.ToUpper().Contains(search.ToUpper()))
                               select new OwnerEntity
                               {
                                   ownerId = owner.ownerId,
                               }).CountAsync();
            return total;
        }

    }
}

