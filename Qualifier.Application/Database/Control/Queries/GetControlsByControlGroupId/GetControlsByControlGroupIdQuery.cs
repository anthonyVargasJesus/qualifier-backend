using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Control.Queries.GetControlsByControlGroupId
{
    public class GetControlsByControlGroupIdQuery : IGetControlsByControlGroupIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetControlsByControlGroupIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int controlGroupId)
        {
            try
            {
                var entities = await (from control in _databaseService.Control
                                      where ((control.isDeleted == null || control.isDeleted == false) && control.controlGroup.controlGroupId == controlGroupId)
                                      && (control.name.ToUpper().Contains(search.ToUpper()))
                                      select new ControlEntity
                                      {
                                          controlId = control.controlId,
                                          number = control.number,
                                          name = control.name,
                                          description = control.description,
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetControlsByControlGroupIdDto> baseResponseDto = new BaseResponseDto<GetControlsByControlGroupIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetControlsByControlGroupIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, controlGroupId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int controlGroupId)
        {
            var total = await (from control in _databaseService.Control
                               where ((control.isDeleted == null || control.isDeleted == false) && control.controlGroup.controlGroupId == controlGroupId)
                               && (control.name.ToUpper().Contains(search.ToUpper()))
                               select new ControlEntity
                               {
                                   controlId = control.controlId,
                               }).CountAsync();
            return total;
        }

    }
}

