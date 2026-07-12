using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.Breach.Commands.CreateBreach;
using Qualifier.Application.Database.Breach.Commands.DeleteBreach;
using Qualifier.Application.Database.Breach.Commands.UpdateBreach;
using Qualifier.Application.Database.Breach.Queries.GetBreachAgingReport;
using Qualifier.Application.Database.Breach.Queries.GetBreachById;
using Qualifier.Application.Database.Breach.Queries.GetBreachsByEvaluationId;
using Qualifier.Application.Database.Breach.Queries.GetBreachSeverityReport;
using Qualifier.Application.Database.Breach.Queries.GetRisksIdentification;

namespace Qualifier.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
public class BreachController(
    IGetRisksIdentificationQuery risksIdentificationQuery,
    IGetBreachsByEvaluationIdQuery getByEvaluationQuery,
    IGetBreachByIdQuery getByIdQuery,
    IGetBreachSeverityReportQuery severityReportQuery,
    IGetBreachAgingReportQuery agingReportQuery,
    ICreateBreachCommand createCommand,
    IUpdateBreachCommand updateCommand,
    IDeleteBreachCommand deleteCommand
) : ApiBaseController
{
    [HttpGet("severity-report")]
    public async Task<IActionResult> GetSeverityReport()
    {
        if (CompanyId == 0) return CompanyRequiredError();
        var res = await severityReportQuery.Execute(CompanyId);
        return ProcessResponse(res);
    }

    // Reporte "Antigüedad de brechas abiertas": buckets de días desde la creación
    // de cada brecha, para que el dueño del requisito priorice las más viejas.
    [HttpGet("aging-report")]
    public async Task<IActionResult> GetAgingReport()
    {
        if (CompanyId == 0) return CompanyRequiredError();
        var res = await agingReportQuery.Execute(CompanyId);
        return ProcessResponse(res);
    }

    [HttpGet("RisksIdentification")]
    public async Task<IActionResult> GetRisksIdentification(int skip, int pageSize, string? search)
    {
        var res = await risksIdentificationQuery.Execute(skip, pageSize, search ?? string.Empty, CompanyId);
        return ProcessResponse(res);
    }

    [HttpGet]
    public async Task<IActionResult> Get(int skip, int pageSize, string? search, int evaluationId)
    {
        var res = await getByEvaluationQuery.Execute(skip, pageSize, search ?? string.Empty, evaluationId, CompanyId);
        return ProcessResponse(res);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var res = await getByIdQuery.Execute(id);
        return ProcessResponse(res, wrapWithData: true);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBreachDto model)
    {
        model.creationUserId = UserId;
        model.companyId = CompanyId;

        var res = await createCommand.Execute(model);
        return ProcessResponse(res, wrapWithData: true);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateBreachDto model)
    {
        model.updateUserId = UserId;

        var res = await updateCommand.Execute(model, id);
        return ProcessResponse(res, wrapWithData: true);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var res = await deleteCommand.Execute(id, UserId);
        return ProcessResponse(res, wrapWithData: true);
    }
}