using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.Model;
using NPOI.XWPF.UserModel;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Version.Commands.CreateWordDocumento
{
    internal class CreateWordDocumentCommand : ICreateWordDocumentCommand
    {

        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public CreateWordDocumentCommand(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int versionId)
        {
            try
            {

                var version = await (from item in _databaseService.Version
                                     join confidentialityLevel in _databaseService.ConfidentialityLevel on item.confidentialityLevel equals confidentialityLevel
                                     where (item.versionId == versionId)
                                     select new VersionEntity
                                     {
                                         versionId = item.versionId,
                                         number = item.number,
                                         code = item.code,
                                         name = item.name,
                                         date = item.date,
                                         isCurrent = item.isCurrent,
                                         fileName = item.fileName,
                                         confidentialityLevel = new ConfidentialityLevelEntity
                                         {
                                             name = confidentialityLevel.name,
                                         },
                                     }).FirstOrDefaultAsync();

                var creators = await (from creator in _databaseService.Creator
                                      join bt in _databaseService.Personal on creator.personalId equals bt.personalId into _bt
                                      from personal in _bt.DefaultIfEmpty()
                                      join bt2 in _databaseService.Responsible on creator.responsibleId equals bt2.responsibleId into _bt2
                                      from responsible in _bt2.DefaultIfEmpty()
                                      where ((creator.isDeleted == null || creator.isDeleted == false) && creator.versionId == versionId)
                                      select new CreatorEntity
                                      {
                                          personal = (personal != null) ? new PersonalEntity { name = personal.name, firstName = personal.firstName, lastName = personal.lastName } : null,
                                          responsible = (responsible != null) ? new ResponsibleEntity { name = responsible.name } : null,
                                      })
                                        .ToListAsync();

                var reviewers = await (from reviewer in _databaseService.Reviewer
                                       join bt in _databaseService.Personal on reviewer.personalId equals bt.personalId into _bt
                                       from personal in _bt.DefaultIfEmpty()
                                       join bt2 in _databaseService.Responsible on reviewer.responsibleId equals bt2.responsibleId into _bt2
                                       from responsible in _bt2.DefaultIfEmpty()
                                       where ((reviewer.isDeleted == null || reviewer.isDeleted == false) && reviewer.versionId == versionId)
                                       select new ReviewerEntity
                                       {
                                           personal = (personal != null) ? new PersonalEntity { name = personal.name, firstName = personal.firstName, lastName = personal.lastName } : null,
                                           responsible = (responsible != null) ? new ResponsibleEntity { name = responsible.name } : null,
                                       }).ToListAsync();

                var approvers = await (from approver in _databaseService.Approver
                                       join bt in _databaseService.Personal on approver.personalId equals bt.personalId into _bt
                                       from personal in _bt.DefaultIfEmpty()
                                       join bt2 in _databaseService.Responsible on approver.responsibleId equals bt2.responsibleId into _bt2
                                       from responsible in _bt2.DefaultIfEmpty()
                                       where ((approver.isDeleted == null || approver.isDeleted == false) && approver.versionId == versionId)
                                       select new ApproverEntity
                                       {
                                           personal = (personal != null) ? new PersonalEntity { name = personal.name, firstName = personal.firstName, lastName = personal.lastName } : null,
                                           responsible = (responsible != null) ? new ResponsibleEntity { name = responsible.name } : null,
                                       }).ToListAsync();


                var allSections = await (from section in _databaseService.Section
                                         where ((section.isDeleted == null || section.isDeleted == false) && section.versionId == versionId)
                                         select new SectionEntity
                                         {
                                             sectionId = section.sectionId,
                                             numeration = section.numeration,
                                             name = section.name,
                                             level = section.level,
                                             description = section.description,
                                             parentId = section.parentId,
                                         }).ToListAsync();

                const int FIRST_LEVEL = 1;
                const int SECOND_LEVEL = 2;
                const int THIRD_LEVEL = 3;

                var sections = await (from section in _databaseService.Section
                                      where ((section.isDeleted == null || section.isDeleted == false)
                                      && section.versionId == versionId && section.level == FIRST_LEVEL)
                                      select new SectionEntity
                                      {
                                          sectionId = section.sectionId,
                                          numeration = section.numeration,
                                          name = section.name,
                                          description = section.description,
                                          level = section.level,
                                          parentId = section.parentId,
                                      })
                                      .OrderBy(x => x.numeration)
                                      .ToListAsync();

                foreach (var section in sections)
                    if (hasChildren(allSections, section.sectionId, SECOND_LEVEL))
                    {
                        section.children = getChildren(allSections, section.sectionId, SECOND_LEVEL).OrderBy(x => x.numeration).ToList();

                        foreach (var item in section.children)
                            if (hasChildren(allSections, item.sectionId, THIRD_LEVEL))
                                item.children = getChildren(allSections, item.sectionId, THIRD_LEVEL).OrderBy(x => x.numeration).ToList();
                    }

                XWPFDocument wordDoc = new XWPFDocument();

                XWPFHeaderFooterPolicy headerFooterPolicy = wordDoc.GetHeaderFooterPolicy();

                if (headerFooterPolicy == null) headerFooterPolicy = wordDoc.CreateHeaderFooterPolicy();

                // create header and add a test header
                createHeaderTable(headerFooterPolicy);

                if (version != null)
                    createPresentationPage(wordDoc, version);

                createApprovalTable(wordDoc, version, creators, reviewers, approvers);

                if (version != null)
                    createDocumentIndex(wordDoc, version, sections);
                if (version != null)
                    createDocumentContent(wordDoc, version, sections);



                // create footer
                XWPFFooter footer = headerFooterPolicy.CreateFooter(XWPFHeaderFooterPolicy.DEFAULT);

                // add copyright
                var paragraph2 = footer.CreateParagraph();
                paragraph2.Alignment = (ParagraphAlignment.CENTER);
                XWPFRun run2 = paragraph2.CreateRun();
                run2.SetText("© 2024 Devon Software & Services Pvt. Ltd.");




                var ms = new MemoryStream();

                wordDoc.Write(ms);

                byte[] byteStream = ms.ToArray();


                ms = new MemoryStream();
                ms.Write(byteStream, 0, byteStream.Length);
                ms.Position = 0;

                return ms;
            }
            catch (Exception)
            {
              return BaseApplication.getExceptionErrorResponse();
            }
        }

        private void createHeaderTable(XWPFHeaderFooterPolicy headerFooterPolicy)
        {

            // create header and add a test header
            XWPFHeader header = headerFooterPolicy.CreateHeader(XWPFHeaderFooterPolicy.DEFAULT);

            // Create a table
            XWPFTable outerTable = header.CreateTable(2, 4);
            outerTable.SetColumnWidth(0, 2500);
            outerTable.SetColumnWidth(1, 2500);
            outerTable.SetColumnWidth(2, 1300);
            outerTable.SetColumnWidth(3, 1400);



            createTableText(outerTable, 0, 1, "Sistema de Gestión de Seguridad de la Información", 8, true, ParagraphAlignment.CENTER, "ffffff");
            createTableText(outerTable, 1, 1, "Modelo de Gestión documental del SGSI", 8, true, ParagraphAlignment.CENTER, "ffffff");
            //createTableText(outerTable, 1, 0, "VERSIÓN:", 11, true, ParagraphAlignment.LEFT, "b6dde8");
            //createTableText(outerTable, 1, 1, version.number.ToString(), 11, false, ParagraphAlignment.LEFT, "ffffff");
            //createTableText(outerTable, 2, 0, "FECHA DE LA VERSIÓN:", 11, true, ParagraphAlignment.LEFT, "b6dde8");
            //createTableText(outerTable, 2, 1, version.date.ToShortDateString(), 11, false, ParagraphAlignment.LEFT, "ffffff");

            // Establecer el borde para todas las celdas de la tabla
            //foreach (XWPFTableRow row in outerTable.Rows)
            //{
            //    foreach (XWPFTableCell cell in row.GetTableCells())
            //    {
            //        SetCellBorder(cell, "00FF00"); // Bordes verdes
            //    }
            //}

            //XWPFTableCell col1 = outerTable(0);
            //row1.MergeCells(1, 2);

            //XWPFTableUtil
            //XWPFTableRow row2 = outerTable.GetRow(1);
            //row2.MergeCells(1, 2);


            //XWPFTableRow row3 = outerTable.GetRow(2);
            //row3.MergeCells(1, 2);

            //XWPFTableRow row4 = outerTable.GetRow(3);
            //row4.MergeCells(0, 2);


            //XWPFTableRow row7 = outerTable.GetRow(6);
            //row7.Height = 2500;

            //XWPFTableRow row8 = outerTable.GetRow(7);
            //row8.MergeCells(0, 2);

            //XWPFTableRow row9 = outerTable.GetRow(8);
            //row9.MergeCells(1, 2);

            //XWPFTableRow row10 = outerTable.GetRow(9);
            //row10.MergeCells(1, 2);

            // Set Table Layout to fixed
            SetTableLayoutToFixed(outerTable);
        }

        static void SetCellBorder(XWPFTableCell cell, string color)
        {
            var ctTcPr = cell.GetCTTc().AddNewTcPr();

            // Establecer el color del borde para todos los lados
            if (ctTcPr.IsSetShd() == false)
            {
                ctTcPr.AddNewTcBorders();
            }

            var borders = ctTcPr.tcBorders;

            // Configurar el color del borde

            borders.left.val = ST_Border.single;
            borders.left.color = color;

            borders.right.val = ST_Border.single;
            borders.right.color = color;

            borders.top.val = ST_Border.single;
            borders.top.color = color;

            borders.bottom.val = ST_Border.single;
            borders.bottom.color = color;
        }

        bool hasChildren(List<SectionEntity> allSections, int idSection, int level)
        {
            return allSections.Count(x => x.parentId == idSection && x.level == level) > 0;
        }

        List<SectionEntity> getChildren(List<SectionEntity> allSections, int idSection, int level)
        {
            var entities = allSections.Where(x => x.parentId == idSection && x.level == level).ToList();
            return entities;
        }


        private void createPresentationPage(XWPFDocument wordDoc, VersionEntity version)
        {
            addBreakLine(wordDoc, 10);

            createText(wordDoc, "OFICINA NACIONAL DE PROCESOS ELECTORALES", 16, true, ParagraphAlignment.CENTER);
            createText(wordDoc, "ONPE", 16, true, ParagraphAlignment.CENTER);

            addBreakLine(wordDoc, 1);

            createText(wordDoc, version.name, 16, true, ParagraphAlignment.CENTER);
            createText(wordDoc, "V " + version.number.ToString(), 16, true, ParagraphAlignment.CENTER);

        }

        private void createDocumentIndex(XWPFDocument wordDoc, VersionEntity version, List<SectionEntity> sections)
        {
            addBreakPage(wordDoc);

            createText(wordDoc, "CONTENIDO", 16, true, ParagraphAlignment.CENTER);


            foreach (SectionEntity section in sections)
            {
                createText(wordDoc, section.numeration.ToString() + ". " + section.name, 11, true, ParagraphAlignment.LEFT);
                if (section.children != null)
                    foreach (var child1 in section.children)
                    {
                        createText(wordDoc, "     " + section.numeration.ToString() + "." + child1.numeration.ToString() + ". " + child1.name, 11, true, ParagraphAlignment.LEFT);
                    }
            }


        }

        private void createDocumentContent(XWPFDocument wordDoc, VersionEntity version, List<SectionEntity> sections)
        {

            addBreakPage(wordDoc);

            foreach (SectionEntity section in sections)
            {
                addBreakLine(wordDoc, 1);
                createText(wordDoc, section.numeration.ToString() + ". " + section.name, 12, true, ParagraphAlignment.LEFT);
                createText(wordDoc, "  " + section.description, 11, false, ParagraphAlignment.LEFT);

                if (section.children != null)
                    foreach (var child1 in section.children)
                    {
                        addBreakLine(wordDoc, 1);
                        createText(wordDoc, "     " + section.numeration.ToString() + "." + child1.numeration.ToString() + ". " + child1.name, 11, true, ParagraphAlignment.LEFT);
                        createText(wordDoc, "  " + child1.description, 11, false, ParagraphAlignment.LEFT);
                    }
            }


        }

        private void addBreakLine(XWPFDocument wordDoc, int number)
        {
            XWPFParagraph paddingParagraph = wordDoc.CreateParagraph();
            XWPFRun paddingRun = paddingParagraph.CreateRun();

            for (int i = 0; i < number; i++)
                paddingRun.AddBreak(NPOI.XWPF.UserModel.BreakType.TEXTWRAPPING);
        }
        private void addBreakPage(XWPFDocument wordDoc)
        {
            XWPFParagraph paddingParagraph = wordDoc.CreateParagraph();
            XWPFRun paddingRun = paddingParagraph.CreateRun();

            paddingRun.AddBreak(NPOI.XWPF.UserModel.BreakType.PAGE);
        }

        private void createApprovalTable(XWPFDocument wordDoc, VersionEntity? version, List<CreatorEntity> creators, List<ReviewerEntity> reviewers, List<ApproverEntity> approvers)
        {
            addBreakPage(wordDoc);

            if (version == null)
                version = new VersionEntity();

            XWPFParagraph title = wordDoc.CreateParagraph();

            createText(wordDoc, "Aprobación", 11, true, ParagraphAlignment.LEFT);

            // Create a table
            XWPFTable outerTable = wordDoc.CreateTable(10, 3);
            outerTable.SetColumnWidth(0, 3000);
            outerTable.SetColumnWidth(1, 3000);
            outerTable.SetColumnWidth(2, 3000);

            // Add contents to a cell
            createTableText(outerTable, 0, 0, "CÓDIGO:", 11, true, ParagraphAlignment.LEFT, "b6dde8");
            createTableText(outerTable, 0, 1, version.code, 11, true, ParagraphAlignment.LEFT, "ffffff");
            createTableText(outerTable, 1, 0, "VERSIÓN:", 11, true, ParagraphAlignment.LEFT, "b6dde8");
            createTableText(outerTable, 1, 1, version.number.ToString(), 11, false, ParagraphAlignment.LEFT, "ffffff");
            createTableText(outerTable, 2, 0, "FECHA DE LA VERSIÓN:", 11, true, ParagraphAlignment.LEFT, "b6dde8");
            createTableText(outerTable, 2, 1, version.date.ToShortDateString(), 11, false, ParagraphAlignment.LEFT, "ffffff");

            createTableText(outerTable, 4, 0, "CREADO POR:", 11, true, ParagraphAlignment.LEFT, "b6dde8");

            List<string> creatorsBullets = new List<string>();
            foreach (var item in creators)
            {
                if (item.personal != null)
                    creatorsBullets.Add(item.personal.name + " " + item.personal.firstName + " " + item.personal.lastName);
                if (item.responsible != null)
                    creatorsBullets.Add(item.responsible.name);
            }
            List<string> approversBullets = new List<string>();
            foreach (var item in approvers)
            {
                if (item.personal != null)
                    approversBullets.Add(item.personal.name + " " + item.personal.firstName + " " + item.personal.lastName);
                if (item.responsible != null)
                    approversBullets.Add(item.responsible.name);
            }
            List<string> reviewersBullets = new List<string>();
            foreach (var item in reviewers)
            {
                if (item.personal != null)
                    reviewersBullets.Add(item.personal.name + " " + item.personal.firstName + " " + item.personal.lastName);
                if (item.responsible != null)
                    reviewersBullets.Add(item.responsible.name);
            }

            createBulletText(outerTable, 5, 0, 11, false, ParagraphAlignment.LEFT, creatorsBullets);

            createTableText(outerTable, 4, 1, "REVISADO POR:", 11, true, ParagraphAlignment.LEFT, "b6dde8");
            createBulletText(outerTable, 5, 1, 11, false, ParagraphAlignment.LEFT, reviewersBullets);

            createTableText(outerTable, 4, 2, "APROBADO POR:", 11, true, ParagraphAlignment.LEFT, "b6dde8");
            createBulletText(outerTable, 5, 2, 11, false, ParagraphAlignment.LEFT, approversBullets);

            createTableText(outerTable, 8, 0, "NOMBRE DEL ARCHIVO:", 11, true, ParagraphAlignment.LEFT, "b6dde8");
            createTableText(outerTable, 8, 1, version.fileName, 11, false, ParagraphAlignment.LEFT, "ffffff");

            createTableText(outerTable, 9, 0, "NIVEL DE CONFIDENCIALIDAD:", 11, true, ParagraphAlignment.LEFT, "b6dde8");
            createTableText(outerTable, 9, 1, version.confidentialityLevel.name, 11, false, ParagraphAlignment.LEFT, "ffffff");


            XWPFTableRow row1 = outerTable.GetRow(0);
            row1.MergeCells(1, 2);

            XWPFTableRow row2 = outerTable.GetRow(1);
            row2.MergeCells(1, 2);


            XWPFTableRow row3 = outerTable.GetRow(2);
            row3.MergeCells(1, 2);

            XWPFTableRow row4 = outerTable.GetRow(3);
            row4.MergeCells(0, 2);


            XWPFTableRow row7 = outerTable.GetRow(6);
            row7.Height = 2500;

            XWPFTableRow row8 = outerTable.GetRow(7);
            row8.MergeCells(0, 2);

            XWPFTableRow row9 = outerTable.GetRow(8);
            row9.MergeCells(1, 2);

            XWPFTableRow row10 = outerTable.GetRow(9);
            row10.MergeCells(1, 2);

            // Set Table Layout to fixed
            SetTableLayoutToFixed(outerTable);
        }

        public void createTableText(XWPFTable exampleTable, int row, int column, string text, int fontSize, bool isBold, ParagraphAlignment alignment, string color)
        {
            XWPFTableCell cell1 = exampleTable.GetRow(row).GetCell(column);
            cell1.SetColor(color);
            foreach (var paragraph in cell1.Paragraphs)
            {
                paragraph.Alignment = alignment;
                XWPFRun run4 = paragraph.CreateRun();
                run4.SetText(text);
                //run4.SetColor("FF5000");
                run4.FontFamily = "Arial";
                run4.FontSize = fontSize;
                if (isBold)
                    run4.IsBold = true;
            }
        }

        public void createBulletText(XWPFTable exampleTable, int row, int column, int fontSize, bool isBold, ParagraphAlignment alignment, List<string> bulletTexts)
        {
            XWPFTableCell cell1 = exampleTable.GetRow(row).GetCell(column);

            int i = 0;
            foreach (string bulletText in bulletTexts)
            {
                if (i == 0)
                    foreach (var paragraph in cell1.Paragraphs)
                    {
                        paragraph.Alignment = alignment;
                        XWPFRun run = paragraph.CreateRun();
                        run.SetText("• " + bulletText);
                        run.AddBreak();
                        run.FontFamily = "Arial";
                        run.FontSize = fontSize;
                        if (isBold)
                            run.IsBold = true;
                    }
                else
                {
                    var paragraph = cell1.AddParagraph();
                    paragraph.Alignment = alignment;
                    XWPFRun run = paragraph.CreateRun();
                    run.SetText("• " + bulletText);
                    run.AddBreak();
                    run.FontFamily = "Arial";
                    run.FontSize = fontSize;
                    if (isBold)
                        run.IsBold = true;
                }

                i++;
            }

        }


        public void createText(XWPFDocument wordDoc, string text, int fontSize, bool isBold, ParagraphAlignment alignment)
        {
            XWPFParagraph paragraph = wordDoc.CreateParagraph();
            paragraph.Alignment = alignment;
            XWPFRun run4 = paragraph.CreateRun();
            run4.SetText(text);
            //run4.SetColor("FF5000");
            run4.FontFamily = "Arial";
            run4.FontSize = fontSize;
            if (isBold)
                run4.IsBold = true;

        }

        public static void SetTableLayoutToFixed(XWPFTable table)
        {
            CT_TblLayoutType tblLayout1 = table.GetCTTbl().tblPr.AddNewTblLayout();
            tblLayout1.type = ST_TblLayoutType.@fixed;
        }


    }
}
