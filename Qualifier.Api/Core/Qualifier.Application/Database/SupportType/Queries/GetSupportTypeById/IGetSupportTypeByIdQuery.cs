namespace Qualifier.Application.Database.SupportType.Queries.GetSupportTypeById
{
    public interface IGetSupportTypeByIdQuery
    {
        Task<Object> Execute(int supportTypeId);
    }
}
//SupportType
//services.AddTransient<IGetSupportTypeByIdQuery, GetSupportTypeByIdQuery>();
