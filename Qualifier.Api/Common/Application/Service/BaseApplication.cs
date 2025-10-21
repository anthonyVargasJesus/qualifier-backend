using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Common.Application.Service
{
    public static class BaseApplication
    {
        public static BaseErrorResponseDto getExceptionErrorResponse()
        {
            BaseErrorDto error = new BaseErrorDto(500, "Failed Request", 321);
            List<BaseErrorDto> errors = new List<BaseErrorDto>();
            errors.Add(error);
            BaseErrorResponseDto response = new BaseErrorResponseDto();
            response.errors = errors;
            return response;
        }

        public static BaseErrorResponseDto getApplicationErrorResponse(List<Error> errors)
        {
            BaseErrorResponseDto response = new BaseErrorResponseDto();
            List<BaseErrorDto> responseErrors = new List<BaseErrorDto>();
            foreach (Error error in errors)
            {
                responseErrors.Add(new BaseErrorDto(400, error.message, 123));
            }
            response.errors = responseErrors;
            return response;
        }

        public static BaseErrorResponseDto getApplicationErrorResponseWithTitle(List<Error> errors, string title)
        {
            BaseErrorResponseDto response = new BaseErrorResponseDto();
            List<BaseErrorDto> responseErrors = new List<BaseErrorDto>();
            foreach (Error error in errors)
            {
                BaseErrorDto baseErrorResponse = new BaseErrorDto(400, error.message, 123);
                responseErrors.Add(baseErrorResponse);
            }
            response.title = title;
            response.errors = responseErrors;
            return response;
        }
        public static int ForceException()
        {
            int a = 5;
            int b = 0;
            return a / b;
        }

    }
}
