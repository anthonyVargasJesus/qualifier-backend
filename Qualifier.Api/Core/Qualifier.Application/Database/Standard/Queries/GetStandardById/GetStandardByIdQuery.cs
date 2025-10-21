using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Standard.Queries.GetStandardById
{
    public class GetStandardByIdQuery : IGetStandardByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetStandardByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int standardId)
        {
            try
            {
                var entity = await (from item in _databaseService.Standard
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.standardId == standardId)
                                    select new StandardEntity()
                                    {
                                        standardId = item.standardId,
                                        name = item.name,
                                        description = item.description,
                                        parentId = item.parentId,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetStandardByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

