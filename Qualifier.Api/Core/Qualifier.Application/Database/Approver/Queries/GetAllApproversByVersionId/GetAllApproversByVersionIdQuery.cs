using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Approver.Queries.GetAllApproversByVersionId
{
    internal class GetAllApproversByVersionIdQuery : IGetAllApproversByVersionIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllApproversByVersionIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int versionId)
        {
            try
            {
                var entities = await (from approver in _databaseService.Approver
                                      join bt in _databaseService.Personal on approver.personalId equals bt.personalId into _bt
                                      from personal in _bt.DefaultIfEmpty()
                                      join bt2 in _databaseService.Responsible on approver.responsibleId equals bt2.responsibleId into _bt2
                                      from responsible in _bt2.DefaultIfEmpty()
                                      where ((approver.isDeleted == null || approver.isDeleted == false) && approver.versionId == versionId)
                                      select new ApproverEntity
                                      {
                                          approverId = approver.approverId,
                                          personalId = approver.personalId,
                                          responsibleId = approver.responsibleId,
                                          personal = (personal != null) ? new PersonalEntity { name = personal.name, firstName = personal.firstName, lastName = personal.lastName } : null,
                                          responsible = (responsible != null) ? new ResponsibleEntity { name = responsible.name } : null,
                                      }).ToListAsync();

                BaseResponseDto<GetAllApproversByVersionIdDto> baseResponseDto = new BaseResponseDto<GetAllApproversByVersionIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllApproversByVersionIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

