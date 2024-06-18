using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Option.Queries.GetAllOptionsByCompanyId
{
    internal class GetAllOptionsByCompanyIdQuery : IGetAllOptionsByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllOptionsByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from option in _databaseService.Option
                                      where ((option.isDeleted == null || option.isDeleted == false) && option.companyId == companyId)
                                      select new OptionEntity
                                      {
                                          optionId = option.optionId,
                                          name = option.name,
                                          image = option.image,
                                          url = option.url,
                                          isMobile = option.isMobile,
                                          companyId = option.companyId,
                                      }).OrderBy(option => option.name)
                                      .ToListAsync();

                BaseResponseDto<GetAllOptionsByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllOptionsByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllOptionsByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

