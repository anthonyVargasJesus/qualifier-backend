

namespace Qualifier.Common.Application.Dto
{
    public class BaseErrorDto
    {
        public int status { get; set; }
        public string detail { get; set; }
        public int code { get; set; }

        public BaseErrorDto(int status, string detail, int code)
        {
            this.status = status;
            this.detail = detail;
            this.code = code;
        }
    }
}
