using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.UserState.Queries.GetAllUserStatesByCompanyId
{
    internal class GetAllUserStatesByCompanyIdQuery : IGetAllUserStatesByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllUserStatesByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from userState in _databaseService.UserState
                                      where ((userState.isDeleted == null || userState.isDeleted == false) && userState.companyId == companyId)
                                      select new UserStateEntity
                                      {
                                          userStateId = userState.userStateId,
                                          name = userState.name,
                                          value = userState.value,
                                          companyId = userState.companyId,
                                      }).ToListAsync();

                BaseResponseDto<GetAllUserStatesByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllUserStatesByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllUserStatesByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

