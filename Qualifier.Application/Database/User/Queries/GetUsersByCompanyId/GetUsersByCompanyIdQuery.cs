using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.User.Queries.GetUsersByCompanyId
{
    public class GetUsersByCompanyIdQuery : IGetUsersByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetUsersByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from user in _databaseService.User
                                      join userState in _databaseService.UserState on user.userState equals userState
                                      where ((user.isDeleted == null || user.isDeleted == false) && user.companyId == companyId)
                                      && (user.name.ToUpper().Contains(search.ToUpper()) || user.firstName.ToUpper().Contains(search.ToUpper())
                                      || user.lastName.ToUpper().Contains(search.ToUpper()))
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
                                          userState = new UserStateEntity
                                          {
                                              name = userState.name,
                                          },
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetUsersByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetUsersByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetUsersByCompanyIdDto>>(entities);
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
            var total = await (from user in _databaseService.User
                               where ((user.isDeleted == null || user.isDeleted == false) && user.companyId == companyId)
                                && (user.name.ToUpper().Contains(search.ToUpper()) || user.firstName.ToUpper().Contains(search.ToUpper())
                                      || user.lastName.ToUpper().Contains(search.ToUpper()))
                               select new UserEntity
                               {
                                   userId = user.userId,
                               }).CountAsync();
            return total;
        }

    }
}

