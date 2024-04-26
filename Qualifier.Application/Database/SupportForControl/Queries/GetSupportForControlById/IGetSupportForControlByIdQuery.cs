namespace Qualifier.Application.Database.SupportForControl.Queries.GetSupportForControlById
{
    public interface IGetSupportForControlByIdQuery
    {
        Task<Object> Execute(int supportForControlId);
    }
}

