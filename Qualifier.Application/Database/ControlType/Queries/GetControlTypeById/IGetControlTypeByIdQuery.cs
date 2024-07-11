namespace Qualifier.Application.Database.ControlType.Queries.GetControlTypeById
{
    public interface IGetControlTypeByIdQuery
    {
        Task<Object> Execute(int controlTypeId);
    }
}

