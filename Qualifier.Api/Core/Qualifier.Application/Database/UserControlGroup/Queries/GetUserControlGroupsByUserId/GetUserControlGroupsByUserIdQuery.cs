using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.UserControlGroup.Queries.GetUserControlGroupsByUserId
{
    public class GetUserControlGroupsByUserIdQuery : IGetUserControlGroupsByUserIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetUserControlGroupsByUserIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int userId, int standardId)
        {
            try
            {
                var entities = await (from item in _databaseService.UserControlGroup
                                      where ((item.isDeleted == null || item.isDeleted == false)
                                      && item.userId == userId && item.standardId == standardId)
                                      select new UserControlGroupEntity
                                      {
                                          controlGroupId = item.controlGroupId,
                                      }).ToListAsync();

                BaseResponseDto<GetUserControlGroupsByUserIdDto> baseResponseDto = new BaseResponseDto<GetUserControlGroupsByUserIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetUserControlGroupsByUserIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }
    }
}
