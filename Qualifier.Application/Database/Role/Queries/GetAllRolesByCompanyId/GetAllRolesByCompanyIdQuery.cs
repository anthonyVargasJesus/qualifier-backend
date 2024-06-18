using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Role.Queries.GetAllRolesByCompanyId
{
    internal class GetAllRolesByCompanyIdQuery : IGetAllRolesByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllRolesByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from role in _databaseService.Role
                                      where ((role.isDeleted == null || role.isDeleted == false) && role.companyId == companyId)
                                      select new RoleEntity
                                      {
                                          roleId = role.roleId,
                                          name = role.name,
                                      }).ToListAsync();

                BaseResponseDto<GetAllRolesByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllRolesByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllRolesByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

