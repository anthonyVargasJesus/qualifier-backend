using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Section.Queries.GetSectionById
{
    public class GetSectionByIdQuery : IGetSectionByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetSectionByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int sectionId)
        {
            try
            {
                var entity = await (from item in _databaseService.Section
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.sectionId == sectionId)
                                    select new SectionEntity()
                                    {
                                        sectionId = item.sectionId,
                                        numeration = item.numeration,
                                        name = item.name,
                                        description = item.description,
                                        level = item.level,
                                        parentId = item.parentId,
                                        documentationId = item.documentationId,
                                        versionId = item.versionId,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetSectionByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

