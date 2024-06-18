namespace Qualifier.Application.Database.OptionInMenu.Queries.GetOptionInMenusByMenuId
{
    public class GetOptionInMenusByMenuIdDto
    {
        public int optionInMenuId { get; set; }
        public int menuId { get; set; }
        public int optionId { get; set; }
        public int order { get; set; }
        public GetOptionInMenusByMenuIdOptionDto? option { get; set; }
    }
    public class GetOptionInMenusByMenuIdOptionDto
    {
        public string name { get; set; }

    }
}

