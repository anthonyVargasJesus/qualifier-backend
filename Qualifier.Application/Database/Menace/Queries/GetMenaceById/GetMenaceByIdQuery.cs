using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Menace.Queries.GetMenaceById
{
    public class GetMenaceByIdQuery : IGetMenaceByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetMenaceByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int menaceId)
        {
            try
            {
                var entity = await (from item in _databaseService.Menace
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.menaceId == menaceId)
                                    select new MenaceEntity()
                                    {
                                        menaceId = item.menaceId,
                                        menaceTypeId = item.menaceTypeId,
                                        name = item.name,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetMenaceByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

