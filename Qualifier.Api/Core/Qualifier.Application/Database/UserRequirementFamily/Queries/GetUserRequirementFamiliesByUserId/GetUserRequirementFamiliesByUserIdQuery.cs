using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.UserRequirementFamily.Queries.GetUserRequirementFamiliesByUserId
{
    public class GetUserRequirementFamiliesByUserIdQuery : IGetUserRequirementFamiliesByUserIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetUserRequirementFamiliesByUserIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int userId, int standardId)
        {
            try
            {
                var entities = await (from item in _databaseService.UserRequirementFamily
                                      where ((item.isDeleted == null || item.isDeleted == false)
                                      && item.userId == userId && item.standardId == standardId)
                                      select new UserRequirementFamilyEntity
                                      {
                                          requirementId = item.requirementId,
                                      }).ToListAsync();

                BaseResponseDto<GetUserRequirementFamiliesByUserIdDto> baseResponseDto = new BaseResponseDto<GetUserRequirementFamiliesByUserIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetUserRequirementFamiliesByUserIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }
    }
}
