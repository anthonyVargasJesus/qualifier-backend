using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.User.Queries.GetAllUsersByCompanyId
{
    internal class GetAllUsersByCompanyIdQuery : IGetAllUsersByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllUsersByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from user in _databaseService.User
                                      where ((user.isDeleted == null || user.isDeleted == false) && user.companyId == companyId)
                                      select new UserEntity
                                      {
                                          userId = user.userId,
                                          name = user.name,
                                          middleName = user.middleName,
                                          firstName = user.firstName,
                                          lastName = user.lastName,
                                          email = user.email,
                                          phone = user.phone,
                                          password = user.password,
                                          userStateId = user.userStateId,
                                          image = user.image,
                                          documentNumber = user.documentNumber,
                                          companyId = user.companyId,
                                      }).ToListAsync();

                BaseResponseDto<GetAllUsersByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllUsersByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllUsersByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

