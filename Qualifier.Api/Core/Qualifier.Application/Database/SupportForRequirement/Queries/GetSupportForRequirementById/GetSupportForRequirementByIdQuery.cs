using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.SupportForRequirement.Queries.GetSupportForRequirementById
{
    public class GetSupportForRequirementByIdQuery : IGetSupportForRequirementByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetSupportForRequirementByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int supportForRequirementId)
        {
            try
            {
                var entity = await (from item in _databaseService.SupportForRequirement
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.supportForRequirementId == supportForRequirementId)
                                    select new SupportForRequirementEntity()
                                    {
                                        supportForRequirementId = item.supportForRequirementId,
                                        documentationId = item.documentationId,
                                        requirementId = item.requirementId,
                                        standardId = item.standardId,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetSupportForRequirementByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

