using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Control.Queries.GetAllControlsByStandardId
{
    internal class GetAllControlsByStandardIdQuery : IGetAllControlsByStandardIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllControlsByStandardIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int standardId)
        {
            try
            {
                var entities = await (from control in _databaseService.Control
                                      where ((control.isDeleted == null || control.isDeleted == false) && control.standardId == standardId)
                                      select new ControlEntity
                                      {
                                          controlId = control.controlId,
                                          number = control.number,
                                          name = control.name,
                                      }).ToListAsync();

                BaseResponseDto<GetAllControlsByStandardIdDto> baseResponseDto = new BaseResponseDto<GetAllControlsByStandardIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllControlsByStandardIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

