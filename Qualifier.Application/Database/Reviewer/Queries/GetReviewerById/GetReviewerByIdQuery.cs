using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Reviewer.Queries.GetReviewerById
{
    public class GetReviewerByIdQuery : IGetReviewerByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetReviewerByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int reviewerId)
        {
            try
            {
                var entity = await (from item in _databaseService.Reviewer
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.reviewerId == reviewerId)
                                    select new ReviewerEntity()
                                    {
                                        reviewerId = item.reviewerId,
                                        personalId = item.personalId,
                                        responsibleId = item.responsibleId,
                                        versionId = item.versionId,
                                        documentationId = item.documentationId,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetReviewerByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

