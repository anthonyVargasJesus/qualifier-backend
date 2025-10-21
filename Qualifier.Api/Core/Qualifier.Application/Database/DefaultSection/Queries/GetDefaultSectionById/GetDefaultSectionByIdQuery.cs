using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.DefaultSection.Queries.GetDefaultSectionById
{
    public class GetDefaultSectionByIdQuery : IGetDefaultSectionByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetDefaultSectionByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int defaultSectionId)
        {
            try
            {
                var entity = await (from item in _databaseService.DefaultSection
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.defaultSectionId == defaultSectionId)
                                    select new DefaultSectionEntity()
                                    {
                                        defaultSectionId = item.defaultSectionId,
                                        numeration = item.numeration,
                                        name = item.name,
                                        description = item.description,
                                        level = item.level,
                                        parentId = item.parentId,
                                        documentTypeId = item.documentTypeId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetDefaultSectionByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

