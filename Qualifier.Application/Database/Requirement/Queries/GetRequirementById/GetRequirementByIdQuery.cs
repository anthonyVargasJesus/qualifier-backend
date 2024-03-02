using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Requirement.Queries.GetRequirementById
{
    public class GetRequirementByIdQuery : IGetRequirementByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetRequirementByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int requirementId)
        {
            try
            {
                var entity = await (from item in _databaseService.Requirement
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.requirementId == requirementId)
                                    select new RequirementEntity()
                                    {
                                        requirementId = item.requirementId,
                                        numeration = item.numeration,
                                        name = item.name,
                                        description = item.description,
                                        standardId = item.standardId,
                                        level = item.level,
                                        parentId = item.parentId,
                                        isEvaluable = item.isEvaluable,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetRequirementByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

