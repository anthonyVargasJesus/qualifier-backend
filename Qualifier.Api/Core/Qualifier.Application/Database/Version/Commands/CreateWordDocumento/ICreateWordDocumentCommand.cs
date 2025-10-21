

namespace Qualifier.Application.Database.Version.Commands.CreateWordDocumento
{
    public interface ICreateWordDocumentCommand
    {
        Task<Object> Execute(int versionId);
    }
}
