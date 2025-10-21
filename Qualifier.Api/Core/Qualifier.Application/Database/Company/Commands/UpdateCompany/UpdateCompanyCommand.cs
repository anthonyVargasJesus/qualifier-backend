using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.Company.Commands.UpdateCompany
{
    public class UpdateCompanyCommand : IUpdateCompanyCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;

        public UpdateCompanyCommand(IDatabaseService databaseService, IMapper mapper, ICompanyRepository companyRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _companyRepository = companyRepository;
        }

        public async Task<Object> Execute(UpdateCompanyDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _companyRepository.Update(id, _mapper.Map<CompanyEntity>(model));

                return model;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        private async Task<Notification> existsValidationAsync(int id)
        {
            Notification notification = new Notification();
            int total = await _databaseService.Company.CountAsync(item => item.companyId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateCompanyDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

