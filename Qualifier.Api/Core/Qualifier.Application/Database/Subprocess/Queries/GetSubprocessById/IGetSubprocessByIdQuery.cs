namespace Qualifier.Application.Database.Subprocess.Queries.GetSubprocessById
{
    public interface IGetSubprocessByIdQuery
    {
        Task<Object> Execute(int subprocessId);
    }
}

