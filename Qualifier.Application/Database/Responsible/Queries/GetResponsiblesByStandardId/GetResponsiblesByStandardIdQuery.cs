using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Responsible.Queries.GetResponsiblesByStandardId
{
    public class GetResponsiblesByStandardIdQuery : IGetResponsiblesByStandardIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetResponsiblesByStandardIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int standardId)
        {
            try
            {
                var entities = await (from responsible in _databaseService.Responsible
                                      where ((responsible.isDeleted == null || responsible.isDeleted == false) && responsible.standardId == standardId)
                                      && (responsible.name.ToUpper().Contains(search.ToUpper()))
                                      select new ResponsibleEntity
                                      {
                                          responsibleId = responsible.responsibleId,
                                          name = responsible.name,
                                          description = responsible.description,
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetResponsiblesByStandardIdDto> baseResponseDto = new BaseResponseDto<GetResponsiblesByStandardIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetResponsiblesByStandardIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, standardId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int standardId)
        {
            var total = await (from responsible in _databaseService.Responsible
                               where ((responsible.isDeleted == null || responsible.isDeleted == false) && responsible.standardId == standardId)
                               && (responsible.name.ToUpper().Contains(search.ToUpper()))
                               select new ResponsibleEntity
                               {
                                   responsibleId = responsible.responsibleId,
                               }).CountAsync();
            return total;
        }

    }
}

