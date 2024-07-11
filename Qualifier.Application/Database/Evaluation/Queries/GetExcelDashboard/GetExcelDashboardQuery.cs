using AutoMapper;
using DocumentFormat.OpenXml.Drawing;
using Microsoft.EntityFrameworkCore;
using NPOI.HSSF.UserModel;
using NPOI.OpenXmlFormats;
using NPOI.OpenXmlFormats.Dml;
using NPOI.OpenXmlFormats.Dml.Chart;
using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.SS.UserModel.Charts;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using NPOI.XSSF.UserModel.Charts;
using Qualifier.Application.Helpers;
using Qualifier.Domain.Entities;
using System.Drawing;
using IndexedColors = NPOI.SS.UserModel.IndexedColors;


namespace Qualifier.Application.Database.Evaluation.Queries.GetExcelDashboard
{
    public class GetExcelDashboardQuery : IGetExcelDashboardQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetExcelDashboardQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int standardId, int evaluationId, int companyId)
        {
            try
            {

                DateTime inicio = new DateTime(2024, 04, 30);

                DateTime primero = inicio.AddDays(25);


                DateTime segundo = inicio.AddDays(50);

                DateTime tercero = inicio.AddDays(75);

                Console.WriteLine(primero);
                Console.WriteLine(segundo);
                Console.WriteLine(tercero);


                var ms = new MemoryStream();

                IWorkbook workbook;
                workbook = new XSSFWorkbook();

                var standard = await (from item in _databaseService.Standard
                                      where ((item.isDeleted == null || item.isDeleted == false) && item.standardId == standardId)
                                      select new StandardEntity()
                                      {
                                          name = item.name,
                                      }).FirstOrDefaultAsync();

                await createRequirementsSheet(workbook, standardId, evaluationId);
                await createControlsSheet(workbook, standardId, evaluationId);
                await createChartsSheet(workbook, standardId, evaluationId, companyId, standard);

                workbook.Write(ms, true);

                byte[] byteStream = ms.ToArray();


                ms = new MemoryStream();
                ms.Write(byteStream, 0, byteStream.Length);
                ms.Position = 0;

                return ms;

            }
            catch (Exception ex)
            {
                throw ex;
                //return BaseApplication.getExceptionErrorResponse();
            }
        }

        async Task createRequirementsSheet(IWorkbook workbook, int standardId, int evaluationId)
        {

            var standard = await (from item in _databaseService.Standard
                                  where ((item.isDeleted == null || item.isDeleted == false) && item.standardId == standardId)
                                  select new StandardEntity()
                                  {
                                      name = item.name,
                                  }).FirstOrDefaultAsync();

            var requirements = await (from requirement in _databaseService.Requirement
                                      where ((requirement.isDeleted == null || requirement.isDeleted == false) && requirement.standardId == standardId)
                                      select new RequirementEntity
                                      {
                                          requirementId = requirement.requirementId,
                                          numeration = requirement.numeration,
                                          name = requirement.name,
                                          description = requirement.description,
                                          level = requirement.level,
                                          parentId = requirement.parentId,
                                      }).ToListAsync();

            var standardEntity = new StandardEntity();
            standardEntity.setRequirementsWithChildren(requirements);

            var evaluations = await (from requirementEvaluation in _databaseService.RequirementEvaluation
                                     join requirement in _databaseService.Requirement on requirementEvaluation.requirement equals requirement
                                     join maturityLevel in _databaseService.MaturityLevel on requirementEvaluation.maturityLevel equals maturityLevel
                                     join responsible in _databaseService.Responsible on requirementEvaluation.responsible equals responsible
                                     where ((requirementEvaluation.isDeleted == null || requirementEvaluation.isDeleted == false) && requirementEvaluation.evaluationId == evaluationId)
                                     select new RequirementEvaluationEntity
                                     {
                                         requirementEvaluationId = requirementEvaluation.requirementEvaluationId,
                                         maturityLevelId = requirementEvaluation.maturityLevelId,
                                         value = requirementEvaluation.value,
                                         requirementId = requirementEvaluation.requirementId,
                                         responsibleId = requirementEvaluation.responsibleId,
                                         justification = requirementEvaluation.justification,
                                         improvementActions = requirementEvaluation.improvementActions,
                                         requirement = new RequirementEntity
                                         {
                                             requirementId = requirement.requirementId,
                                             name = requirement.name,
                                         },
                                         maturityLevel = new MaturityLevelEntity
                                         {
                                             abbreviation = maturityLevel.abbreviation,
                                             color = maturityLevel.color,
                                         },
                                         responsible = new ResponsibleEntity
                                         {
                                             name = responsible.name,
                                         },

                                     }).ToListAsync();


            var referenceDocumentations = await (from referenceDocumentation in _databaseService.ReferenceDocumentation
                                                 where ((referenceDocumentation.isDeleted == null || referenceDocumentation.isDeleted == false) && referenceDocumentation.evaluationId == evaluationId)
                                                 select new ReferenceDocumentationEntity
                                                 {
                                                     documentationId = referenceDocumentation.documentationId,
                                                     name = referenceDocumentation.name,
                                                     requirementEvaluationId = referenceDocumentation.requirementEvaluationId,
                                                 }).ToListAsync();

            standardEntity.setReferenceDocumentationToEvaluations(evaluations, referenceDocumentations);

            standardEntity.setEvaluationsToRequirements(evaluations);


            string standardName = "";
            if (standard != null)
                standardName = standard.name;

            ISheet excelSheet = workbook.CreateSheet(standardName);

            string title = standardName + " - ANÁLISIS DE BRECHAS INICIAL";

            excelSheet.SetColumnWidth(0, 2700);
            excelSheet.SetColumnWidth(1, 20000);
            excelSheet.SetColumnWidth(2, 3500);
            excelSheet.SetColumnWidth(3, 9000);
            excelSheet.SetColumnWidth(4, 9000);
            excelSheet.SetColumnWidth(5, 15000);
            excelSheet.SetColumnWidth(6, 40000);

            IRow row0 = excelSheet.CreateRow(0);
            setCell(workbook, row0, 0, title, 28);
            row0.Height = 1300;

            //Merge the cell
            CellRangeAddress region = new CellRangeAddress(0, 0, 0, 3);
            excelSheet.AddMergedRegion(region);

            int rowIndex = 1;

            IRow headerRow = excelSheet.CreateRow(rowIndex);
            headerRow.Height = 900;

            short whiteBorder = IndexedColors.White.Index;
            short grayBorder = IndexedColors.Grey25Percent.Index;

            string headerColor = "#187bc5";
            setFormatRequirementItemCell(workbook, headerRow, 0, "Cláusula", 11, true, headerColor, true, true, whiteBorder, true);
            setFormatRequirementItemCell(workbook, headerRow, 1, "Título del requerimiento", 11, true, headerColor, true, true, whiteBorder, true);
            setFormatRequirementItemCell(workbook, headerRow, 2, "Calificación", 11, true, headerColor, true, true, whiteBorder, true);
            setFormatRequirementItemCell(workbook, headerRow, 3, "Responsable del requerimiento", 11, true, headerColor, true, true, whiteBorder, true);
            setFormatRequirementItemCell(workbook, headerRow, 4, "Documentación de referencia", 11, true, headerColor, true, true, whiteBorder, true);
            setFormatRequirementItemCell(workbook, headerRow, 5, "Justificación de la aplicabilidad o de la no aplicabilidad", 11, true, headerColor, true, true, whiteBorder, true);
            setFormatRequirementItemCell(workbook, headerRow, 6, "Acciones de mejora", 11, true, headerColor, true, true, whiteBorder, true);

            rowIndex = rowIndex + 1;
            foreach (RequirementEntity requirement in standardEntity.requirements)
            {
                IRow row = excelSheet.CreateRow(rowIndex);
                row.Height = 650;
                string headerRequirementColor = "#d9d9d9";
                setFormatRequirementItemCell(workbook, row, 0, requirement.numerationToShow, 24, true, headerRequirementColor, false, true, grayBorder, true);
                setFormatRequirementItemCell(workbook, row, 1, requirement.name, 16, false, headerRequirementColor, false, true, grayBorder, true);
                setFormatRequirementItemCell(workbook, row, 2, "", 12, false, headerRequirementColor, false, true, grayBorder, true);
                setFormatRequirementItemCell(workbook, row, 3, "", 12, false, headerRequirementColor, false, true, grayBorder, true);
                setFormatRequirementItemCell(workbook, row, 4, "", 12, false, headerRequirementColor, false, true, grayBorder, true);
                setFormatRequirementItemCell(workbook, row, 5, "", 12, false, headerRequirementColor, false, true, grayBorder, true);
                setFormatRequirementItemCell(workbook, row, 6, "", 12, false, headerRequirementColor, false, true, grayBorder, true);

                CellRangeAddress cellRegion = new CellRangeAddress(rowIndex, rowIndex, 1, 6);
                excelSheet.AddMergedRegion(cellRegion);

                rowIndex = rowIndex + 1;

                if (requirement.children != null)
                {
                    foreach (RequirementEntity child in requirement.children)
                    {
                        IRow childrenRow = excelSheet.CreateRow(rowIndex);
                        childrenRow.Height = 650;
                        string headerChildColor = "#eeece1";
                        setFormatRequirementItemCell(workbook, childrenRow, 0, child.numerationToShow, 14, true, headerChildColor, false, true, grayBorder, true);
                        setFormatRequirementItemCell(workbook, childrenRow, 1, child.name, 12, false, headerChildColor, false, true, grayBorder, true);
                        setFormatRequirementItemCell(workbook, childrenRow, 2, "", 12, false, headerChildColor, false, true, grayBorder, true);
                        setFormatRequirementItemCell(workbook, childrenRow, 3, "", 12, false, headerChildColor, false, true, grayBorder, true);
                        setFormatRequirementItemCell(workbook, childrenRow, 4, "", 12, false, headerChildColor, false, true, grayBorder, true);
                        setFormatRequirementItemCell(workbook, childrenRow, 5, "", 12, false, headerChildColor, false, true, grayBorder, true);
                        setFormatRequirementItemCell(workbook, childrenRow, 6, "", 12, false, headerChildColor, false, true, grayBorder, true);

                        CellRangeAddress childCellRegion = new CellRangeAddress(rowIndex, rowIndex, 1, 6);
                        excelSheet.AddMergedRegion(childCellRegion);

                        rowIndex++;


                        if (child.children != null)
                            foreach (RequirementEntity child2 in child.children)
                            {
                                if (child2.requirementEvaluations != null)
                                    foreach (RequirementEvaluationEntity requirementEvaluation in child2.requirementEvaluations)
                                    {
                                        IRow children2Row = excelSheet.CreateRow(rowIndex);
                                        children2Row.Height = 1500;
                                        string headerChild2Color = "#ffffff";
                                        setFormatRequirementItemCell(workbook, children2Row, 0, child2.numerationToShow, 12, true, headerChild2Color, false, false, grayBorder, true);
                                        setFormatRequirementItemCell(workbook, children2Row, 1, child2.name, 10, false, headerChild2Color, false, false, grayBorder, true);

                                        string maturityLevelName = "";
                                        string maturityLevelColor = "#FFFFFF";

                                        if (requirementEvaluation.maturityLevel != null)
                                            if (requirementEvaluation.maturityLevel.abbreviation != null)
                                                maturityLevelName = requirementEvaluation.maturityLevel.abbreviation;

                                        if (requirementEvaluation.maturityLevel != null)
                                            if (requirementEvaluation.maturityLevel.color != null)
                                                maturityLevelColor = requirementEvaluation.maturityLevel.color;

                                        setFormatRequirementItemCell(workbook, children2Row, 2, maturityLevelName, 16, true, maturityLevelColor, true, true, grayBorder, true);

                                        setFormatRequirementItemCell(workbook, children2Row, 2, maturityLevelName, 16, true, maturityLevelColor, true, true, grayBorder, true);

                                        string responsible = "";
                                        if (requirementEvaluation.responsible != null)
                                            responsible = requirementEvaluation.responsible.name;



                                        setFormatRequirementItemCell(workbook, children2Row, 3, responsible, 10, true, headerChild2Color, false, false, grayBorder, true);

                                        string referenceDocumentation = "";

                                        int i = 0;
                                        if (requirementEvaluation.referenceDocumentations != null)
                                            foreach (ReferenceDocumentationEntity references in requirementEvaluation.referenceDocumentations)
                                            {
                                                referenceDocumentation += "  " + (i + 1).ToString() + ". " + references.name + Environment.NewLine;
                                                i++;
                                            }

                                        setFormatRequirementItemCell(workbook, children2Row, 4, referenceDocumentation, 10, false, headerChild2Color, false, false, grayBorder, false);
                                        setFormatRequirementItemCell(workbook, children2Row, 5, requirementEvaluation.justification, 10, false, headerChild2Color, false, false, grayBorder, false);
                                        setFormatRequirementItemCell(workbook, children2Row, 6, requirementEvaluation.improvementActions, 10, false, headerChild2Color, false, false, grayBorder, false);
                                    }

                                rowIndex++;
                            }



                    }
                }

            }

            rowIndex++;
        }



        async Task createControlsSheet(IWorkbook workbook, int standardId, int evaluationId)
        {

            var groups = await (from item in _databaseService.ControlGroup
                                where ((item.isDeleted == null || item.isDeleted == false) && item.standardId == standardId)
                                select new ControlGroupEntity
                                {
                                    controlGroupId = item.controlGroupId,
                                    number = item.number,
                                    name = item.name,
                                }).OrderBy(x => x.number)
                            .ToListAsync();

            var controls = await (from item in _databaseService.Control
                                  where ((item.isDeleted == null || item.isDeleted == false) && item.standardId == standardId)
                                  select new ControlEntity
                                  {
                                      controlId = item.controlId,
                                      controlGroupId = item.controlGroupId,
                                      number = item.number,
                                      name = item.name,
                                  }).OrderBy(x => x.number)
                              .ToListAsync();



            var evaluations = await (from controlEvaluation in _databaseService.ControlEvaluation
                                     join control in _databaseService.Control on controlEvaluation.control equals control
                                     join maturityLevel in _databaseService.MaturityLevel on controlEvaluation.maturityLevel equals maturityLevel
                                     join responsible in _databaseService.Responsible on controlEvaluation.responsible equals responsible
                                     where ((controlEvaluation.isDeleted == null || controlEvaluation.isDeleted == false) && controlEvaluation.evaluationId == evaluationId)
                                     select new ControlEvaluationEntity
                                     {
                                         controlEvaluationId = controlEvaluation.controlEvaluationId,
                                         maturityLevelId = controlEvaluation.maturityLevelId,
                                         value = controlEvaluation.value,
                                         controlId = controlEvaluation.controlId,
                                         responsibleId = controlEvaluation.responsibleId,
                                         justification = controlEvaluation.justification,
                                         improvementActions = controlEvaluation.improvementActions,
                                         control = new ControlEntity
                                         {
                                             controlId = control.controlId,
                                             number = control.number,
                                             name = control.name,
                                         },
                                         maturityLevel = new MaturityLevelEntity
                                         {
                                             abbreviation = maturityLevel.abbreviation,
                                             color = maturityLevel.color,
                                         },
                                         responsible = new ResponsibleEntity
                                         {
                                             name = responsible.name,
                                         },

                                     }).ToListAsync();


            foreach (ControlEntity item in controls)
                item.setEvaluations(evaluations);

            setControlsWithGroup(groups, controls);


            var referenceDocumentations = await (from referenceDocumentation in _databaseService.ReferenceDocumentation
                                                 where ((referenceDocumentation.isDeleted == null || referenceDocumentation.isDeleted == false) && referenceDocumentation.evaluationId == evaluationId)
                                                 select new ReferenceDocumentationEntity
                                                 {
                                                     documentationId = referenceDocumentation.documentationId,
                                                     name = referenceDocumentation.name,
                                                     requirementEvaluationId = referenceDocumentation.requirementEvaluationId,
                                                 }).ToListAsync();

            setControlReferenceDocumentationToEvaluations(evaluations, referenceDocumentations);

            //standardEntity.setEvaluationsToRequirements(evaluations);


            ISheet excelSheet = workbook.CreateSheet("Anexo - A");

            string title = "ANEXO A - EVALUACIÓN DE CONTROLES";

            excelSheet.SetColumnWidth(0, 2700);
            excelSheet.SetColumnWidth(1, 20000);
            excelSheet.SetColumnWidth(2, 3500);
            excelSheet.SetColumnWidth(3, 9000);
            excelSheet.SetColumnWidth(4, 9000);
            excelSheet.SetColumnWidth(5, 15000);
            excelSheet.SetColumnWidth(6, 40000);

            IRow row0 = excelSheet.CreateRow(0);
            setCell(workbook, row0, 0, title, 28);
            row0.Height = 1300;

            //Merge the cell
            CellRangeAddress region = new CellRangeAddress(0, 0, 0, 3);
            excelSheet.AddMergedRegion(region);

            int rowIndex = 1;

            IRow headerRow = excelSheet.CreateRow(rowIndex);
            headerRow.Height = 900;

            short whiteBorder = IndexedColors.White.Index;
            short grayBorder = IndexedColors.Grey25Percent.Index;

            string headerColor = "#187bc5";
            setFormatRequirementItemCell(workbook, headerRow, 0, "Cláusula", 11, true, headerColor, true, true, whiteBorder, true);
            setFormatRequirementItemCell(workbook, headerRow, 1, "Título del requerimiento", 11, true, headerColor, true, true, whiteBorder, true);
            setFormatRequirementItemCell(workbook, headerRow, 2, "Calificación", 11, true, headerColor, true, true, whiteBorder, true);
            setFormatRequirementItemCell(workbook, headerRow, 3, "Responsable del requerimiento", 11, true, headerColor, true, true, whiteBorder, true);
            setFormatRequirementItemCell(workbook, headerRow, 4, "Documentación de referencia", 11, true, headerColor, true, true, whiteBorder, true);
            setFormatRequirementItemCell(workbook, headerRow, 5, "Justificación de la aplicabilidad o de la no aplicabilidad", 11, true, headerColor, true, true, whiteBorder, true);
            setFormatRequirementItemCell(workbook, headerRow, 6, "Acciones de mejora", 11, true, headerColor, true, true, whiteBorder, true);

            rowIndex = rowIndex + 1;


            foreach (ControlGroupEntity group in groups)
            {
                IRow childrenRow = excelSheet.CreateRow(rowIndex);
                childrenRow.Height = 650;
                string headerChildColor = "#eeece1";
                setFormatRequirementItemCell(workbook, childrenRow, 0, group.number.ToString(), 14, true, headerChildColor, false, true, grayBorder, true);
                setFormatRequirementItemCell(workbook, childrenRow, 1, group.name, 12, false, headerChildColor, false, true, grayBorder, true);
                setFormatRequirementItemCell(workbook, childrenRow, 2, "", 12, false, headerChildColor, false, true, grayBorder, true);
                setFormatRequirementItemCell(workbook, childrenRow, 3, "", 12, false, headerChildColor, false, true, grayBorder, true);
                setFormatRequirementItemCell(workbook, childrenRow, 4, "", 12, false, headerChildColor, false, true, grayBorder, true);
                setFormatRequirementItemCell(workbook, childrenRow, 5, "", 12, false, headerChildColor, false, true, grayBorder, true);
                setFormatRequirementItemCell(workbook, childrenRow, 6, "", 12, false, headerChildColor, false, true, grayBorder, true);

                CellRangeAddress childCellRegion = new CellRangeAddress(rowIndex, rowIndex, 1, 6);
                excelSheet.AddMergedRegion(childCellRegion);

                rowIndex++;


                if (group.controls != null)
                    foreach (ControlEntity control in group.controls)
                    {
                        if (control.controlEvaluations != null)
                            foreach (ControlEvaluationEntity controlEvaluation in control.controlEvaluations)
                            {
                                IRow children2Row = excelSheet.CreateRow(rowIndex);
                                children2Row.Height = 1400;
                                string headerChild2Color = "#ffffff";
                                setFormatRequirementItemCell(workbook, children2Row, 0, control.number.ToString(), 12, true, headerChild2Color, false, false, grayBorder, true);
                                setFormatRequirementItemCell(workbook, children2Row, 1, control.name, 10, false, headerChild2Color, false, false, grayBorder, true);

                                string maturityLevelName = "";
                                string maturityLevelColor = "#FFFFFF";

                                if (controlEvaluation.maturityLevel != null)
                                    if (controlEvaluation.maturityLevel.abbreviation != null)
                                        maturityLevelName = controlEvaluation.maturityLevel.abbreviation;

                                if (controlEvaluation.maturityLevel != null)
                                    if (controlEvaluation.maturityLevel.color != null)
                                        maturityLevelColor = controlEvaluation.maturityLevel.color;

                                setFormatRequirementItemCell(workbook, children2Row, 2, maturityLevelName, 16, true, maturityLevelColor, true, true, grayBorder, true);

                                string responsible = "";
                                if (controlEvaluation.responsible != null)
                                    responsible = controlEvaluation.responsible.name;

                                setFormatRequirementItemCell(workbook, children2Row, 3, responsible, 10, true, headerChild2Color, false, false, grayBorder, true);

                                string referenceDocumentation = "";

                                int i = 0;

                                if (controlEvaluation.referenceDocumentations != null)
                                    foreach (ReferenceDocumentationEntity references in controlEvaluation.referenceDocumentations)
                                    {
                                        referenceDocumentation += "  " + (i + 1).ToString() + ". " + references.name + Environment.NewLine;
                                        i++;
                                    }

                                setFormatRequirementItemCell(workbook, children2Row, 4, referenceDocumentation, 10, false, headerChild2Color, false, false, grayBorder, false);
                                setFormatRequirementItemCell(workbook, children2Row, 5, controlEvaluation.justification, 10, false, headerChild2Color, false, false, grayBorder, false);
                                setFormatRequirementItemCell(workbook, children2Row, 6, controlEvaluation.improvementActions, 10, false, headerChild2Color, false, false, grayBorder, false);
                            }

                        rowIndex++;
                    }



            }




            rowIndex++;
        }


        async Task createChartsSheet(IWorkbook workbook, int standardId, int evaluationId, int companyId, StandardEntity? standard)
        {

            var requirements = await (from requirement in _databaseService.Requirement
                                      where ((requirement.isDeleted == null || requirement.isDeleted == false) && requirement.standardId == standardId)
                                      select new RequirementEntity
                                      {
                                          requirementId = requirement.requirementId,
                                          numeration = requirement.numeration,
                                          name = requirement.name,
                                          description = requirement.description,
                                          level = requirement.level,
                                          parentId = requirement.parentId,
                                      }).ToListAsync();
            var maturityLevels = await (from maturityLevel in _databaseService.MaturityLevel
                                         where ((maturityLevel.isDeleted == null || maturityLevel.isDeleted == false) && maturityLevel.companyId == companyId)
                                         select new MaturityLevelEntity
                                         {
                                             maturityLevelId = maturityLevel.maturityLevelId,
                                             name = maturityLevel.name,
                                             abbreviation = maturityLevel.abbreviation,
                                             value = maturityLevel.value,
                                             color = maturityLevel.color,
                                             factor = maturityLevel.factor,
                                         }).OrderByDescending(x => x.value).ToListAsync();

            var evaluations = await (from requirementEvaluation in _databaseService.RequirementEvaluation
                                     join requirement in _databaseService.Requirement on requirementEvaluation.requirement equals requirement
                                     join maturityLevel in _databaseService.MaturityLevel on requirementEvaluation.maturityLevel equals maturityLevel
                                     join responsible in _databaseService.Responsible on requirementEvaluation.responsible equals responsible
                                     where ((requirementEvaluation.isDeleted == null || requirementEvaluation.isDeleted == false) && requirementEvaluation.evaluationId == evaluationId)
                                     select new RequirementEvaluationEntity
                                     {
                                         requirementEvaluationId = requirementEvaluation.requirementEvaluationId,
                                         maturityLevelId = requirementEvaluation.maturityLevelId,
                                         value = requirementEvaluation.value,
                                         requirementId = requirementEvaluation.requirementId,
                                         responsibleId = requirementEvaluation.responsibleId,
                                         justification = requirementEvaluation.justification,
                                         improvementActions = requirementEvaluation.improvementActions,
                                         requirement = new RequirementEntity
                                         {
                                             requirementId = requirement.requirementId,
                                             name = requirement.name,
                                         },
                                         maturityLevel = new MaturityLevelEntity
                                         {
                                             abbreviation = maturityLevel.abbreviation,
                                             color = maturityLevel.color,
                                         },
                                         responsible = new ResponsibleEntity
                                         {
                                             name = responsible.name,
                                         },

                                     }).ToListAsync();

            var indicators = await (from indicator in _databaseService.Indicator
                                    where ((indicator.isDeleted == null || indicator.isDeleted == false) && indicator.companyId == companyId)
                                    select new IndicatorEntity
                                    {
                                        indicatorId = indicator.indicatorId,
                                        name = indicator.name,
                                        description = indicator.description,
                                        abbreviation = indicator.abbreviation,
                                        minimum = indicator.minimum,
                                        maximum = indicator.maximum,
                                        color = indicator.color,
                                    }).ToListAsync();

            var standardEntity = new StandardEntity();
            standardEntity.setRequirementsWithChildren(requirements);

            standardEntity.setTotalValuesToRequirements(evaluations, maturityLevels);

            standardEntity.setTotalValueInMaturityLevels(maturityLevels);

            decimal totalValuexEvaluation = standardEntity.getTotalValuexEvaluation();

            standardEntity.setIndicators(indicators);

            standardEntity.setPercentInMaturityLevels(maturityLevels, totalValuexEvaluation);

            string standardName = "";
            if (standard != null)
                standardName = standard.name;

            string title = "RESUMEN RESULTADOS DEL ESTADO DE LOS REQUERIMIENTOS DE LA " + standardName;

            ISheet excelSheet = workbook.CreateSheet("Indicadores");

            int initRow = 1;

            IRow row0 = excelSheet.CreateRow(initRow);
            row0.Height = 1300;

            setCell(workbook, row0, 2, title, 18);

            //Merge the cell
            //CellRangeAddress region = new CellRangeAddress(0, 0, 1, 3);
            //excelSheet.AddMergedRegion(region);

            initRow = initRow + 1;
            int endingRow = initRow + 12;

            XSSFDrawing drawing = (XSSFDrawing) excelSheet.CreateDrawingPatriarch();
            XSSFClientAnchor anchor = (XSSFClientAnchor)drawing.CreateAnchor(0, 0, 0, 0, 2, initRow, 5, endingRow);
            CreatePieChart(excelSheet, drawing, anchor, "", maturityLevels);

            XSSFDrawing drawing2 = (XSSFDrawing)excelSheet.CreateDrawingPatriarch();
            XSSFClientAnchor anchor2 = (XSSFClientAnchor)drawing.CreateAnchor(0, 0, 0, 0, 6, initRow, 11, endingRow);

            CreateColumnChart(excelSheet, drawing2, anchor2, maturityLevels, standardEntity.requirements);

            excelSheet.SetColumnWidth(0, 2700);
            excelSheet.SetColumnWidth(1, 2700);
            excelSheet.SetColumnWidth(2, 9000);

            int rowIndex = endingRow+1;
            int initColumnIndex = 2;

            IRow headerRow = excelSheet.CreateRow(rowIndex);
            headerRow.Height = 900;

            short whiteBorder = IndexedColors.White.Index;
            short grayBorder = IndexedColors.Grey25Percent.Index;

            int firstRow = 0;
            int firstColumn = 0;
            int lastRow = 0;
            int lastColumn = 0;

            string headerColor = "#010000";
            setFormatRequirementItemCell(workbook, headerRow, initColumnIndex, "Requerimientos", 11, true, headerColor, true, true, whiteBorder, true);

            int columnIndex = 2;
            foreach(MaturityLevelEntity maturityLevel in maturityLevels)
            {
                setFormatRequirementItemCell(workbook, headerRow, columnIndex+1, (maturityLevel.name== null)?"": maturityLevel.name, 10, true, (maturityLevel.color == null) ? "" : maturityLevel.color, true, true, whiteBorder, true);
                excelSheet.SetColumnWidth(columnIndex + 1, 3500);
                columnIndex++;
            }
            setFormatRequirementItemCell(workbook, headerRow, columnIndex+1, "Calificación", 11, true, headerColor, true, true, whiteBorder, true);
            excelSheet.SetColumnWidth(columnIndex + 1, 7500);

            rowIndex = rowIndex + 1;

            int firstBarChartRowIndex = rowIndex;
            foreach (RequirementEntity requirement in standardEntity.requirements)
            {
                IRow row = excelSheet.CreateRow(rowIndex);
                //row.Height = 650;
                string headerRequirementColor = "#FFFFFF";
              
                setFormatRequirementItemCell(workbook, row, initColumnIndex, "Cláusula " + requirement.numerationToShow + " - " + requirement.name, 11, false, headerRequirementColor, false, false, grayBorder, true);

                int j = 3;
                foreach (MaturityLevelEntity maturityLevel in requirement.maturityLevels)
                {
                    setFormatRequirementItemCell(workbook, row, j, maturityLevel.value.ToString(), 11, true, headerRequirementColor, false, false, grayBorder, true);
                    j++;
                }

                setFormatRequirementItemCell(workbook, row, j, (requirement.indicator == null)?"": requirement.indicator.name , 11, false, headerRequirementColor, false, true, grayBorder, true);

                rowIndex++;
            }
            int lastBarChartRowIndex = rowIndex-1;


            firstRow = rowIndex;
            firstColumn = 1;
            lastRow = rowIndex;


            IRow footerRow = excelSheet.CreateRow(rowIndex);
            //footerRow.Height = 900;
            setFormatRequirementItemCell(workbook, footerRow, initColumnIndex, "Total", 11, true, headerColor, true, true, whiteBorder, true);
            int columnIndex2 = 2;
            foreach (MaturityLevelEntity maturityLevel in maturityLevels)
            {
                setFormatRequirementItemCell(workbook, footerRow, columnIndex2 + 1, maturityLevel.value.ToString() , 12, true, (maturityLevel.color == null) ? "" : maturityLevel.color, true, true, whiteBorder, true);
                columnIndex2++;
            }

            lastColumn = columnIndex2;

            setFormatRequirementItemCell(workbook, footerRow, columnIndex2 + 1, totalValuexEvaluation.ToString(), 12, true, headerColor, true, true, whiteBorder, true);

            //init Controlesssss

            await setControlDashboardSheet(workbook, excelSheet, standardId, evaluationId, companyId, standard, rowIndex + 3);

        }



        async Task setControlDashboardSheet(IWorkbook workbook, ISheet excelSheet, int standardId, int evaluationId, int companyId, StandardEntity? standard, int initRow)
        {

            var maturityLevels = await(from maturityLevel in _databaseService.MaturityLevel
                                       where ((maturityLevel.isDeleted == null || maturityLevel.isDeleted == false) && maturityLevel.companyId == companyId)
                                       select new MaturityLevelEntity
                                       {
                                           maturityLevelId = maturityLevel.maturityLevelId,
                                           name = maturityLevel.name,
                                           abbreviation = maturityLevel.abbreviation,
                                           value = maturityLevel.value,
                                           color = maturityLevel.color,
                                           factor = maturityLevel.factor,
                                       }).OrderByDescending(x => x.value).ToListAsync();

            var indicators = await(from indicator in _databaseService.Indicator
                                   where ((indicator.isDeleted == null || indicator.isDeleted == false) && indicator.companyId == companyId)
                                   select new IndicatorEntity
                                   {
                                       indicatorId = indicator.indicatorId,
                                       name = indicator.name,
                                       description = indicator.description,
                                       abbreviation = indicator.abbreviation,
                                       minimum = indicator.minimum,
                                       maximum = indicator.maximum,
                                       color = indicator.color,
                                   }).ToListAsync();

            var controlGroups = await(from controlGroup in _databaseService.ControlGroup
                                      where ((controlGroup.isDeleted == null || controlGroup.isDeleted == false) && controlGroup.standardId == standardId)
                                      select new ControlGroupEntity
                                      {
                                          controlGroupId = controlGroup.controlGroupId,
                                          number = controlGroup.number,
                                          name = controlGroup.name,
                                      }).ToListAsync();


            var controlEvaluations = await(from controlEvaluation in _databaseService.ControlEvaluation
                                           join control in _databaseService.Control on controlEvaluation.control equals control
                                           where ((controlEvaluation.isDeleted == null || controlEvaluation.isDeleted == false) && controlEvaluation.evaluationId == evaluationId)
                                           select new ControlEvaluationEntity
                                           {
                                               maturityLevelId = controlEvaluation.maturityLevelId,
                                               value = controlEvaluation.value,
                                               controlGroupId = controlEvaluation.control.controlGroupId,
                                           }).ToListAsync();


            var standardEntity = new StandardEntity();
            standardEntity.controlsGroups = controlGroups;
            standardEntity.setTotalValuesToControls(controlEvaluations, maturityLevels);

            standardEntity.setTotalValueControlsInMaturityLevels(maturityLevels);

            decimal totalValuexEvaluation = standardEntity.getTotalControlValuexEvaluation();

            standardEntity.setControlIndicators(indicators);

            standardEntity.setPercentControlsInMaturityLevels(maturityLevels, totalValuexEvaluation);

            //List<PieControlDashboardControlGroup> pieControlDashboardControlGroups = standardEntity.getPieControlChartDashboard(maturityLevels);

            //List<string> colors = standardEntity.getColors(maturityLevels);

            //List<BartVerticalControlDashboard> bartVerticalControlDashboard = standardEntity.getControlBarChartDashboard();

            string standardName = "";
            if (standard != null)
                standardName = standard.name;

            string title = "RESUMEN DE RESULTADOS DEL ESTADO DE LOS CONTROLES DEL ANEXO A DE LA " + standardName;


            IRow row0 = excelSheet.CreateRow(initRow);
            row0.Height = 1300;

            setCell(workbook, row0, 2, title, 18);

            //Merge the cell
            //CellRangeAddress region = new CellRangeAddress(0, 0, 1, 3);
            //excelSheet.AddMergedRegion(region);

            initRow = initRow + 1;
            int endingRow = initRow + 12;

            XSSFDrawing drawing = (XSSFDrawing)excelSheet.CreateDrawingPatriarch();
            XSSFClientAnchor anchor = (XSSFClientAnchor)drawing.CreateAnchor(0, 0, 0, 0, 2, initRow, 5, endingRow);
            CreateControlPieChart(excelSheet, drawing, anchor, "", maturityLevels);

            XSSFDrawing drawing2 = (XSSFDrawing)excelSheet.CreateDrawingPatriarch();
            XSSFClientAnchor anchor2 = (XSSFClientAnchor)drawing.CreateAnchor(0, 0, 0, 0, 6, initRow, 11, endingRow);

            CreateControlColumnChart(excelSheet, drawing2, anchor2, maturityLevels, standardEntity.controlsGroups);

            excelSheet.SetColumnWidth(0, 2700);
            excelSheet.SetColumnWidth(1, 2700);
            excelSheet.SetColumnWidth(2, 9000);

            int rowIndex = endingRow + 1;
            int initColumnIndex = 2;

            IRow headerRow = excelSheet.CreateRow(rowIndex);
            headerRow.Height = 900;

            short whiteBorder = IndexedColors.White.Index;
            short grayBorder = IndexedColors.Grey25Percent.Index;

            int lastColumn = 0;

            string headerColor = "#010000";
            setFormatRequirementItemCell(workbook, headerRow, initColumnIndex, "Controles", 11, true, headerColor, true, true, whiteBorder, true);

            int columnIndex = 2;
            foreach (MaturityLevelEntity maturityLevel in maturityLevels)
            {
                setFormatRequirementItemCell(workbook, headerRow, columnIndex + 1, (maturityLevel.name == null) ? "" : maturityLevel.name, 10, true, (maturityLevel.color == null) ? "" : maturityLevel.color, true, true, whiteBorder, true);
                excelSheet.SetColumnWidth(columnIndex + 1, 3500);
                columnIndex++;
            }
            setFormatRequirementItemCell(workbook, headerRow, columnIndex + 1, "Calificación", 11, true, headerColor, true, true, whiteBorder, true);
            excelSheet.SetColumnWidth(columnIndex + 1, 7500);

            rowIndex = rowIndex + 1;

            int firstBarChartRowIndex = rowIndex;
            foreach (ControlGroupEntity group in standardEntity.controlsGroups)
            {
                IRow row = excelSheet.CreateRow(rowIndex);
                //row.Height = 650;
                string headerRequirementColor = "#FFFFFF";

                setFormatRequirementItemCell(workbook, row, initColumnIndex, "Anexo " + group.number.ToString() + " - " + group.name, 11, false, headerRequirementColor, false, false, grayBorder, true);

                int j = 3;
                foreach (MaturityLevelEntity maturityLevel in group.maturityLevels)
                {
                    setFormatRequirementItemCell(workbook, row, j, maturityLevel.value.ToString(), 11, true, headerRequirementColor, false, false, grayBorder, true);
                    j++;
                }

                setFormatRequirementItemCell(workbook, row, j, (group.indicator == null) ? "" : group.indicator.name, 11, false, headerRequirementColor, false, true, grayBorder, true);

                rowIndex++;
            }
            int lastBarChartRowIndex = rowIndex - 1;

            IRow footerRow = excelSheet.CreateRow(rowIndex);
            setFormatRequirementItemCell(workbook, footerRow, initColumnIndex, "Total", 11, true, headerColor, true, true, whiteBorder, true);
            int columnIndex2 = 2;
            foreach (MaturityLevelEntity maturityLevel in maturityLevels)
            {
                setFormatRequirementItemCell(workbook, footerRow, columnIndex2 + 1, maturityLevel.value.ToString(), 12, true, (maturityLevel.color == null) ? "" : maturityLevel.color, true, true, whiteBorder, true);
                columnIndex2++;
            }

            lastColumn = columnIndex2;

            setFormatRequirementItemCell(workbook, footerRow, columnIndex2 + 1, totalValuexEvaluation.ToString(), 12, true, headerColor, true, true, whiteBorder, true);




        }


        private void setFormatRequirementItemCell(IWorkbook workbook, IRow row, int columnIndex, string text, int fontSize,
            bool isCenter, string hexColor, bool foregroundIsWhite, bool isBold, short borderColor, bool isVerticalCenter)
        {
            ICell cell = row.CreateCell(columnIndex);
            cell.SetCellValue(text);
            FormatExcel.setFormatFirstChildrenRequirementItemCell(workbook, cell, fontSize, isCenter, hexColor, foregroundIsWhite, isBold, borderColor, isVerticalCenter);
        }


        private void setItemCell(IWorkbook workbook, IRow row, int rowIndex, int columnIndex, string text)
        {
            ICell cell = row.CreateCell(columnIndex);
            cell.SetCellValue(text);
            FormatExcel.setFormatItemCell(workbook, cell);
        }

        private void setHeaderCell(IWorkbook workbook, IRow row, int rowIndex, int columnIndex, string text)
        {
            ICell cell = row.CreateCell(columnIndex);
            cell.SetCellValue(text);
            FormatExcel.setFormatHeaderCell(workbook, cell);


        }

        private void setCell(IWorkbook workbook, IRow row, int columnIndex, string text, int fontsize)
        {
            ICell cell = row.CreateCell(columnIndex);
            cell.SetCellValue(text);
            FormatExcel.setFormatTitleCellBold(workbook, cell, fontsize);
        }


        public void setControlsWithGroup(List<ControlGroupEntity> groups, List<ControlEntity> controls)
        {
            foreach (var group in groups)
            {
                group.controls = controls.Where(x => x.controlGroupId == group.controlGroupId).OrderBy(x => x.number).ToList();
                setNumeration(group.controls, group.number);
            }

        }

        public void setControlReferenceDocumentationToEvaluations(List<ControlEvaluationEntity> evaluations, List<ReferenceDocumentationEntity> referenceDocumentations)
        {
            foreach (ControlEvaluationEntity item in evaluations)
                item.referenceDocumentations = referenceDocumentations.Where(x => x.controlEvaluationId == item.controlEvaluationId).ToList();
        }

        private void setNumeration(List<ControlEntity> controls, int parentNumber)
        {
            foreach (ControlEntity item in controls)
                item.numerationToShow = parentNumber.ToString() + "." + item.number.ToString();
        }


        private static void CreatePieChart(ISheet sheet, IDrawing drawing, IClientAnchor anchor, string serieTitle, List<MaturityLevelEntity> maturityLevels)
        {
            XSSFChart chart = (XSSFChart)drawing.CreateChart(anchor);

            IPieChartData<string, double> pieChartData = chart.ChartDataFactory.CreatePieChartData<string, double>();
            IChartLegend legend = chart.GetOrCreateLegend();
            legend.Position = LegendPosition.Right;

            List<string> categories = new List<string>();
            List<double> values = new List<double>();
            foreach (MaturityLevelEntity maturityLevel in maturityLevels)
            {
                categories.Add((maturityLevel.name == null) ? "" : maturityLevel.name);
                values.Add((double)maturityLevel.value);
            }

            IChartDataSource<string> categoryAxis = DataSources.FromArray(categories.ToArray());
            IChartDataSource<double> valueAxis = DataSources.FromArray(values.ToArray());
            
            var serie = pieChartData.AddSeries(categoryAxis, valueAxis);

            serie.SetTitle(serieTitle);

            chart.Plot(pieChartData);

            var ser = chart.GetCTChart().plotArea.pieChart.ElementAt(0);

            foreach(var item in ser.ser)
            {
                for (int i = 0; i <= item.dPt.Count() - 1; i++)
                {
                    var shapeProperties = item.dPt[i].spPr;

                    CT_SolidColorFillProperties solidFill = shapeProperties.AddNewSolidFill();
                    CT_SchemeColor schemeColor = new CT_SchemeColor();

                    string hexColor = "";
                   if (maturityLevels.Count >= i)
                       hexColor = maturityLevels.ElementAt(i).color;

                    var systemColor = System.Drawing.ColorTranslator.FromHtml(hexColor);

                    var color = solidFill.AddNewSrgbClr();
                    color.val = new byte[] { systemColor.R, systemColor.G, systemColor.B };

                }
            }

        }

        private static void CreateControlPieChart(ISheet sheet, IDrawing drawing, IClientAnchor anchor, string serieTitle, List<MaturityLevelEntity> maturityLevels)
        {
            XSSFChart chart = (XSSFChart)drawing.CreateChart(anchor);

            IPieChartData<string, double> pieChartData = chart.ChartDataFactory.CreatePieChartData<string, double>();
            IChartLegend legend = chart.GetOrCreateLegend();
            legend.Position = LegendPosition.Right;

            List<string> categories = new List<string>();
            List<double> values = new List<double>();
            foreach (MaturityLevelEntity maturityLevel in maturityLevels)
            {
                categories.Add((maturityLevel.name == null) ? "" : maturityLevel.name);
                values.Add((double)maturityLevel.value);
            }

            IChartDataSource<string> categoryAxis = DataSources.FromArray(categories.ToArray());
            IChartDataSource<double> valueAxis = DataSources.FromArray(values.ToArray());

            var serie = pieChartData.AddSeries(categoryAxis, valueAxis);

            serie.SetTitle(serieTitle);

            chart.Plot(pieChartData);

            var ser = chart.GetCTChart().plotArea.pieChart.ElementAt(0);

            foreach (var item in ser.ser)
            {
                for (int i = 0; i <= item.dPt.Count() - 1; i++)
                {
                    var shapeProperties = item.dPt[i].spPr;

                    CT_SolidColorFillProperties solidFill = shapeProperties.AddNewSolidFill();
                    CT_SchemeColor schemeColor = new CT_SchemeColor();

                    string hexColor = "";
                    if (maturityLevels.Count >= i)
                        hexColor = maturityLevels.ElementAt(i).color;

                    var systemColor = System.Drawing.ColorTranslator.FromHtml(hexColor);

                    var color = solidFill.AddNewSrgbClr();
                    color.val = new byte[] { systemColor.R, systemColor.G, systemColor.B };

                }
            }

        }

        private static void CreateBarChart(ISheet sheet, IDrawing drawing, IClientAnchor anchor, List<MaturityLevelEntity> maturityLevels, List<RequirementEntity> requirements)
        {
            XSSFChart chart = (XSSFChart)drawing.CreateChart(anchor);

            IBarChartData<string, double> barChartData = chart.ChartDataFactory.CreateBarChartData<string, double>();
            IChartLegend legend = chart.GetOrCreateLegend();
            legend.Position = LegendPosition.Right;

            // Configurar el eje de las categorías
            var categoryAxis = chart.ChartAxisFactory.CreateCategoryAxis(AxisPosition.Bottom);
            //categoryAxis.Orientation = AxisOrientation.MinToMax;
            

            //IChartAxis bottomAxis = chart.ChartAxisFactory.CreateCategoryAxis(AxisPosition.Bottom);
            //bottomAxis.MajorTickMark = AxisTickMark.None;


            var valueAxis = chart.ChartAxisFactory.CreateValueAxis(AxisPosition.Left);
            //valueAxis.Orientation = AxisOrientation.MinToMax;


            //IValueAxis leftAxis = chart.ChartAxisFactory.CreateValueAxis(AxisPosition.Left);
            //leftAxis.Crosses = AxisCrosses.Min;
            //leftAxis.SetCrossBetween(AxisCrossBetween.Between);

            List<string> categories = new List<string>();
            foreach (var requirement in requirements)
                categories.Add(requirement.name);

             foreach (MaturityLevelEntity maturityLevel in maturityLevels)
            {    
                    List<double> data = new List<double>();
                    foreach (var requirement in requirements)
                        foreach (var item in requirement.maturityLevels.Where(x=>x.maturityLevelId == maturityLevel.maturityLevelId).ToList())
                            data.Add((double)item.value);

                    IChartDataSource<double> valueAxis2 = DataSources.FromArray(data.ToArray());
                    IChartDataSource<string> categoryAxis2 = DataSources.FromArray(categories.ToArray());
                    var serie = barChartData.AddSeries(categoryAxis2, valueAxis2);
                    serie.SetTitle((maturityLevel.name == null) ? "" : maturityLevel.name);
            }

          
            chart.Plot(barChartData, categoryAxis, valueAxis);

          


            var barChart = chart.GetCTChart().plotArea.barChart.ElementAt(0);

            int i = 0;
           foreach (CT_BarSer item in barChart.ser)
            {

                //item.AddNewSpPr().AddNewSolidFill().AddNewSrgbClr().Val = "FF0000"; // Rojo

                var shapeProperties = new NPOI.OpenXmlFormats.Dml.Chart.CT_ShapeProperties();

                CT_SolidColorFillProperties solidFill = shapeProperties.AddNewSolidFill();
                CT_SchemeColor schemeColor = new CT_SchemeColor();

                string hexColor = "";
                if (maturityLevels.Count >= i)
                    hexColor = maturityLevels.ElementAt(i).color;

                var systemColor = System.Drawing.ColorTranslator.FromHtml(hexColor);

                var color = solidFill.AddNewSrgbClr();
                color.val = new byte[] { systemColor.R, systemColor.G, systemColor.B };
                i++;

                item.spPr = shapeProperties;
            }

            //var barGrouping = chart.GetCTChart().plotArea.barChart.ElementAt(0);
            barChart.grouping.val = ST_BarGrouping.stacked;

        }

        private static void CreateColumnChart(ISheet sheet, IDrawing drawing, IClientAnchor anchor, List<MaturityLevelEntity> maturityLevels, List<RequirementEntity> requirements)
        {
            XSSFChart chart = (XSSFChart)drawing.CreateChart(anchor);

            IColumnChartData<string, double> barChartData = chart.ChartDataFactory.CreateColumnChartData<string, double>();
            IChartLegend legend = chart.GetOrCreateLegend();
            legend.Position = LegendPosition.Right;

            var categoryAxis = chart.ChartAxisFactory.CreateCategoryAxis(AxisPosition.Top);
            categoryAxis.Orientation = AxisOrientation.MinToMax;

            IValueAxis leftAxis = chart.ChartAxisFactory.CreateValueAxis(AxisPosition.Left);
            leftAxis.Crosses = AxisCrosses.Min;
            leftAxis.SetCrossBetween(AxisCrossBetween.Between);

            List<string> categories = new List<string>();
            foreach (var requirement in requirements)
                categories.Add(requirement.name);

            foreach (MaturityLevelEntity maturityLevel in maturityLevels)
            {
                List<double> data = new List<double>();
                foreach (var requirement in requirements)
                    foreach (var item in requirement.maturityLevels.Where(x => x.maturityLevelId == maturityLevel.maturityLevelId).ToList())
                        data.Add((double)item.value);

                IChartDataSource<double> valueAxis2 = DataSources.FromArray(data.ToArray());
                IChartDataSource<string> categoryAxis2 = DataSources.FromArray(categories.ToArray());
                var serie = barChartData.AddSeries(categoryAxis2, valueAxis2);
                serie.SetTitle((maturityLevel.name == null) ? "" : maturityLevel.name);
            }


            chart.Plot(barChartData, categoryAxis, leftAxis);




            var barChart = chart.GetCTChart().plotArea.barChart.ElementAt(0);

            int i = 0;
            foreach (CT_BarSer item in barChart.ser)
            {

                //item.AddNewSpPr().AddNewSolidFill().AddNewSrgbClr().Val = "FF0000"; // Rojo

                var shapeProperties = new NPOI.OpenXmlFormats.Dml.Chart.CT_ShapeProperties();

                CT_SolidColorFillProperties solidFill = shapeProperties.AddNewSolidFill();
                CT_SchemeColor schemeColor = new CT_SchemeColor();

                string hexColor = "";
                if (maturityLevels.Count >= i)
                    hexColor = maturityLevels.ElementAt(i).color;

                var systemColor = System.Drawing.ColorTranslator.FromHtml(hexColor);

                var color = solidFill.AddNewSrgbClr();
                color.val = new byte[] { systemColor.R, systemColor.G, systemColor.B };
                i++;

                item.spPr = shapeProperties;
            }

            //var barGrouping = chart.GetCTChart().plotArea.barChart.ElementAt(0);
            //barChart.grouping.val = ST_BarGrouping.clustered;

        }

        private static void CreateControlColumnChart(ISheet sheet, IDrawing drawing, IClientAnchor anchor, List<MaturityLevelEntity> maturityLevels, List<ControlGroupEntity> groups)
        {
            XSSFChart chart = (XSSFChart)drawing.CreateChart(anchor);

            IColumnChartData<string, double> barChartData = chart.ChartDataFactory.CreateColumnChartData<string, double>();
            IChartLegend legend = chart.GetOrCreateLegend();
            legend.Position = LegendPosition.Right;

            var categoryAxis = chart.ChartAxisFactory.CreateCategoryAxis(AxisPosition.Top);
            categoryAxis.Orientation = AxisOrientation.MinToMax;

            IValueAxis leftAxis = chart.ChartAxisFactory.CreateValueAxis(AxisPosition.Left);
            leftAxis.Crosses = AxisCrosses.Min;
            leftAxis.SetCrossBetween(AxisCrossBetween.Between);

            List<string> categories = new List<string>();
            foreach (var group in groups)
                categories.Add(group.name);

            foreach (MaturityLevelEntity maturityLevel in maturityLevels)
            {
                List<double> data = new List<double>();
                foreach (var group in groups)
                    foreach (var item in group.maturityLevels.Where(x => x.maturityLevelId == maturityLevel.maturityLevelId).ToList())
                        data.Add((double)item.value);

                IChartDataSource<double> valueAxis2 = DataSources.FromArray(data.ToArray());
                IChartDataSource<string> categoryAxis2 = DataSources.FromArray(categories.ToArray());
                var serie = barChartData.AddSeries(categoryAxis2, valueAxis2);
                serie.SetTitle((maturityLevel.name == null) ? "" : maturityLevel.name);
            }


            chart.Plot(barChartData, categoryAxis, leftAxis);

            var barChart = chart.GetCTChart().plotArea.barChart.ElementAt(0);

            int i = 0;
            foreach (CT_BarSer item in barChart.ser)
            {

                var shapeProperties = new NPOI.OpenXmlFormats.Dml.Chart.CT_ShapeProperties();

                CT_SolidColorFillProperties solidFill = shapeProperties.AddNewSolidFill();
                CT_SchemeColor schemeColor = new CT_SchemeColor();

                string hexColor = "";
                if (maturityLevels.Count >= i)
                    hexColor = maturityLevels.ElementAt(i).color;

                var systemColor = System.Drawing.ColorTranslator.FromHtml(hexColor);

                var color = solidFill.AddNewSrgbClr();
                color.val = new byte[] { systemColor.R, systemColor.G, systemColor.B };
                i++;

                item.spPr = shapeProperties;
            }

            //var barGrouping = chart.GetCTChart().plotArea.barChart.ElementAt(0);
            //barChart.grouping.val = ST_BarGrouping.clustered;

        }


    }
}
