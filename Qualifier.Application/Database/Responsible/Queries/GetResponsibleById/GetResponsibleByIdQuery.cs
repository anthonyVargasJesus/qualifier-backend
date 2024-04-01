using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Responsible.Queries.GetResponsibleById
{
    public class GetResponsibleByIdQuery : IGetResponsibleByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetResponsibleByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int responsibleId)
        {
            try
            {
                var entity = await (from item in _databaseService.Responsible
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.responsibleId == responsibleId)
                                    select new ResponsibleEntity()
                                    {
                                        responsibleId = item.responsibleId,
                                        name = item.name,
                                        description = item.description,
                                        standardId = item.standardId,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetResponsibleByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

