using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Company.Queries.GetCompanyById
{
    public class GetCompanyByIdQuery : IGetCompanyByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetCompanyByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entity = await (from item in _databaseService.Company
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.companyId == companyId)
                                    select new CompanyEntity()
                                    {
                                        companyId = item.companyId,
                                        name = item.name,
                                        abbreviation = item.abbreviation,
                                        slogan = item.slogan,
                                        logo = item.logo,
                                        address = item.address,
                                        phone = item.phone,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetCompanyByIdDto>(entity);
            }
            catch (Exception)
            {
              return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

