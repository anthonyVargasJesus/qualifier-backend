using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Owner.Queries.GetOwnerById
{
    public class GetOwnerByIdQuery : IGetOwnerByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetOwnerByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int ownerId)
        {
            try
            {
                var entity = await (from item in _databaseService.Owner
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.ownerId == ownerId)
                                    select new OwnerEntity()
                                    {
                                        ownerId = item.ownerId,
                                        code = item.code,
                                        name = item.name,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetOwnerByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

