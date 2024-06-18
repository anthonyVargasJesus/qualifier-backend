using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.UserState.Queries.GetUserStateById
{
    public class GetUserStateByIdQuery : IGetUserStateByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetUserStateByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int userStateId)
        {
            try
            {
                var entity = await (from item in _databaseService.UserState
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.userStateId == userStateId)
                                    select new UserStateEntity()
                                    {
                                        userStateId = item.userStateId,
                                        name = item.name,
                                        value = item.value,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetUserStateByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

