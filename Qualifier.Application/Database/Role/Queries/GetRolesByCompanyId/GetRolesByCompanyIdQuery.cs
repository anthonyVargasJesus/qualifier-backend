using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Role.Queries.GetRolesByCompanyId
{
    public class GetRolesByCompanyIdQuery : IGetRolesByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetRolesByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from role in _databaseService.Role
                                      where ((role.isDeleted == null || role.isDeleted == false) && role.companyId == companyId)
                                      && (role.name.ToUpper().Contains(search.ToUpper()))
                                      select new RoleEntity
                                      {
                                          roleId = role.roleId,
                                          name = role.name,
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetRolesByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetRolesByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetRolesByCompanyIdDto>>(entities);
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
            var total = await (from role in _databaseService.Role
                               where ((role.isDeleted == null || role.isDeleted == false) && role.companyId == companyId)
                               && (role.name.ToUpper().Contains(search.ToUpper()))
                               select new RoleEntity
                               {
                                   roleId = role.roleId,
                               }).CountAsync();
            return total;
        }

    }
}

