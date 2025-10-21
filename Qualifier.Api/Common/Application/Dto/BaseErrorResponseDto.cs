

namespace Qualifier.Common.Application.Dto
{
    public class BaseErrorResponseDto
    {
        public string title { get; set; }
        public List<BaseErrorDto> errors { get; set; } = new List<BaseErrorDto>();
    }
}
