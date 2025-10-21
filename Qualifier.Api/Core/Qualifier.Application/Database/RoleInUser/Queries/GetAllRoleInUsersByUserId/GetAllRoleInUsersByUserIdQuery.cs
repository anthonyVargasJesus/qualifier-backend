using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.RoleInUser.Queries.GetAllRoleInUsersByUserId
{
    internal class GetAllRoleInUsersByUserIdQuery : IGetAllRoleInUsersByUserIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllRoleInUsersByUserIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int userId)
        {
            try
            {
                var entities = await (from roleInUser in _databaseService.RoleInUser
                                      join role in _databaseService.Role on roleInUser.role equals role
                                      where ((roleInUser.isDeleted == null || roleInUser.isDeleted == false) && roleInUser.userId == userId)
                                      select new RoleInUserEntity
                                      {
                                          roleInUserId = roleInUser.roleInUserId,
                                          roleId = roleInUser.roleId,
                                      }).ToListAsync();

                BaseResponseDto<GetAllRoleInUsersByUserIdDto> baseResponseDto = new BaseResponseDto<GetAllRoleInUsersByUserIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllRoleInUsersByUserIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

