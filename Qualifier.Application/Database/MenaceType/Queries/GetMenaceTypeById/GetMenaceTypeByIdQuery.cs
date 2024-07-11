using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.MenaceType.Queries.GetMenaceTypeById
{
    public class GetMenaceTypeByIdQuery : IGetMenaceTypeByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetMenaceTypeByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int menaceTypeId)
        {
            try
            {
                var entity = await (from item in _databaseService.MenaceType
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.menaceTypeId == menaceTypeId)
                                    select new MenaceTypeEntity()
                                    {
                                        menaceTypeId = item.menaceTypeId,
                                        name = item.name,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetMenaceTypeByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

