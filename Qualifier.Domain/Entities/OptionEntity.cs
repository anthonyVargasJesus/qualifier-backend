using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
	public class OptionEntity : BaseEntity
	{
		public int optionId { get; set; }
		public string? name { get; set; }
		public string? image { get; set; }
		public string? url { get; set; }
		public bool isMobile { get; set; }

	}
}
