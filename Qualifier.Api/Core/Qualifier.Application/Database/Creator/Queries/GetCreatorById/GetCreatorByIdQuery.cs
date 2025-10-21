using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Creator.Queries.GetCreatorById
{
    public class GetCreatorByIdQuery : IGetCreatorByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetCreatorByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int creatorId)
        {
            try
            {
                var entity = await (from item in _databaseService.Creator
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.creatorId == creatorId)
                                    select new CreatorEntity()
                                    {
                                        creatorId = item.creatorId,
                                        personalId = item.personalId,
                                        responsibleId = item.responsibleId,
                                        versionId = item.versionId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetCreatorByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

