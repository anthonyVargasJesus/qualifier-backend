using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.SupportForControl.Queries.GetSupportForControlsByDocumentationId
{
    public class GetSupportForControlsByDocumentationIdQuery : IGetSupportForControlsByDocumentationIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetSupportForControlsByDocumentationIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int documentationId)
        {
            try
            {
                var entities = await (from supportForControl in _databaseService.SupportForControl
                                      join control in _databaseService.Control on supportForControl.control equals control
                                      where ((supportForControl.isDeleted == null || supportForControl.isDeleted == false) && supportForControl.documentationId == documentationId)
                                      && (control.name.ToUpper().Contains(search.ToUpper()))
                                      select new SupportForControlEntity
                                      {
                                          supportForControlId = supportForControl.supportForControlId,
                                          documentationId = supportForControl.documentationId,
                                          controlId = supportForControl.controlId,
                                          control = new ControlEntity
                                          {
                                              number = control.number,
                                              name = control.name,
                                          },
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetSupportForControlsByDocumentationIdDto> baseResponseDto = new BaseResponseDto<GetSupportForControlsByDocumentationIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetSupportForControlsByDocumentationIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, documentationId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int documentationId)
        {
            var total = await (from supportForControl in _databaseService.SupportForControl
                               join control in _databaseService.Control on supportForControl.control equals control
                               where ((supportForControl.isDeleted == null || supportForControl.isDeleted == false) && supportForControl.documentationId == documentationId)
                               && (control.name.ToUpper().Contains(search.ToUpper()))
                               select new SupportForControlEntity
                               {
                                   supportForControlId = supportForControl.supportForControlId,
                               }).CountAsync();
            return total;
        }

    }
}

