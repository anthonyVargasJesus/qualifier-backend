

namespace Qualifier.Common.Application.Dto
{
    public class BaseResponseDto<T>
    {
        public List<T> data { get; set; }
        public Object pagination { get; set; }
    }
}
