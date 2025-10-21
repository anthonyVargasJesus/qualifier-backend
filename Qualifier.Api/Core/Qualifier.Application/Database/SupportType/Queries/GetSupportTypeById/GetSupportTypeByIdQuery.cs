using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.SupportType.Queries.GetSupportTypeById
{
    public class GetSupportTypeByIdQuery : IGetSupportTypeByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetSupportTypeByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int supportTypeId)
        {
            try
            {
                var entity = await (from item in _databaseService.SupportType
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.supportTypeId == supportTypeId)
                                    select new SupportTypeEntity()
                                    {
                                        supportTypeId = item.supportTypeId,
                                        name = item.name,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetSupportTypeByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

