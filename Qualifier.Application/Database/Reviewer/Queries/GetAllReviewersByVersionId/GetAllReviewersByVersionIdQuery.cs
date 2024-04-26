using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Office.Interop.Excel;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Reviewer.Queries.GetAllReviewersByVersionId
{
    internal class GetAllReviewersByVersionIdQuery : IGetAllReviewersByVersionIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllReviewersByVersionIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int versionId)
        {
            try
            {

                var entities = await (from reviewer in _databaseService.Reviewer
                                      join bt in _databaseService.Personal on reviewer.personalId equals bt.personalId into _bt
                                      from personal in _bt.DefaultIfEmpty()
                                      join bt2 in _databaseService.Responsible on reviewer.responsibleId equals bt2.responsibleId into _bt2
                                      from responsible in _bt2.DefaultIfEmpty()
                                      where ((reviewer.isDeleted == null || reviewer.isDeleted == false) && reviewer.versionId == versionId)
                                      select new ReviewerEntity
                                      {
                                          reviewerId = reviewer.reviewerId,
                                          personalId = reviewer.personalId,
                                          responsibleId = reviewer.responsibleId,
                                          personal = (personal != null) ? new PersonalEntity { name = personal.name, firstName = personal.firstName, lastName = personal.lastName } : null,
                                          responsible = (responsible != null) ? new ResponsibleEntity { name = responsible.name } : null,
                                      }).ToListAsync();

                BaseResponseDto<GetAllReviewersByVersionIdDto> baseResponseDto = new BaseResponseDto<GetAllReviewersByVersionIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllReviewersByVersionIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

