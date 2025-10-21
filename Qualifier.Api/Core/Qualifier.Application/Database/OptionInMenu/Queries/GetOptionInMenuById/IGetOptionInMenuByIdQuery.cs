namespace Qualifier.Application.Database.OptionInMenu.Queries.GetOptionInMenuById
{
    public interface IGetOptionInMenuByIdQuery
    {
        Task<Object> Execute(int optionInMenuId);
    }
}

