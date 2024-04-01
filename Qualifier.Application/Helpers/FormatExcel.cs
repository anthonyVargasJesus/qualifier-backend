
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;



namespace Qualifier.Application.Helpers
{
    public static class FormatExcel
    {
        public static void setFormatRequirementItemCell(IWorkbook workbook, ICell cell, int fontSize, bool isCenter)
        {
            var h1Font = workbook.CreateFont();
            h1Font.FontHeightInPoints = (short)fontSize;
            h1Font.IsBold = true;
            ICellStyle boldStyle = workbook.CreateCellStyle();
            boldStyle.BorderBottom = BorderStyle.Thin;
            boldStyle.BorderTop = BorderStyle.Thin;
            boldStyle.BorderLeft = BorderStyle.Thin;
            boldStyle.BorderRight = BorderStyle.Thin;

            boldStyle.BottomBorderColor = IndexedColors.White.Index;

            boldStyle.VerticalAlignment = VerticalAlignment.Center;

            boldStyle.FillForegroundColor = IndexedColors.Grey25Percent.Index;

            boldStyle.FillPattern = FillPattern.SolidForeground;

            if (isCenter)
                boldStyle.Alignment = HorizontalAlignment.Center;
            boldStyle.SetFont(h1Font);
            cell.CellStyle = boldStyle;
        }
        public static void setFormatFirstChildrenRequirementItemCell(IWorkbook workbook, ICell cell, int fontSize, bool isCenter,
            string hexColor, bool foregroundIsWhite, bool isBold, short borderColor, bool isVerticalCenter)
        {
            var h1Font = workbook.CreateFont();
            h1Font.FontHeightInPoints = (short)fontSize;
            if (isBold)
                h1Font.IsBold = true;
            h1Font.FontName = "Century Gothic";

            if (foregroundIsWhite)
                h1Font.Color = IndexedColors.White.Index;
            else
                h1Font.Color = IndexedColors.Black.Index;

            var boldStyle = (XSSFCellStyle)workbook.CreateCellStyle();
            boldStyle.BorderBottom = BorderStyle.Thin;
            boldStyle.BorderTop = BorderStyle.Thin;
            boldStyle.BorderLeft = BorderStyle.Thin;
            boldStyle.BorderRight = BorderStyle.Thin;

            if (isVerticalCenter)
                boldStyle.VerticalAlignment = VerticalAlignment.Center;
            else
                boldStyle.VerticalAlignment = VerticalAlignment.Top;

            // Añadir bordes
            boldStyle.BorderTop = BorderStyle.Thin;
            boldStyle.TopBorderColor = borderColor;
            boldStyle.BorderRight = BorderStyle.Thin;
            boldStyle.RightBorderColor = borderColor;
            boldStyle.BorderBottom = BorderStyle.Thin;
            boldStyle.BottomBorderColor = borderColor;
            boldStyle.BorderLeft = BorderStyle.Thin;
            boldStyle.LeftBorderColor = borderColor;

            var systemColor = System.Drawing.ColorTranslator.FromHtml(hexColor);


            byte[] rgb = new byte[3] { systemColor.R, systemColor.G, systemColor.B };
            XSSFColor color = new XSSFColor(rgb);
            boldStyle.SetFillBackgroundColor(color);
            boldStyle.SetFillForegroundColor(color);
            boldStyle.FillPattern = FillPattern.SolidForeground;

            boldStyle.WrapText = true;

            if (isCenter)
                boldStyle.Alignment = HorizontalAlignment.Center;
            boldStyle.SetFont(h1Font);

            cell.CellStyle = boldStyle;

        }


        public static void setFormatItemCell(IWorkbook workbook, ICell cell)
        {
            var h1Font = workbook.CreateFont();
            h1Font.FontHeightInPoints = (short)11;
            ICellStyle boldStyle = workbook.CreateCellStyle();
            boldStyle.BorderBottom = BorderStyle.Thin;
            boldStyle.BorderTop = BorderStyle.Thin;
            boldStyle.BorderLeft = BorderStyle.Thin;
            boldStyle.BorderRight = BorderStyle.Thin;
            boldStyle.VerticalAlignment = VerticalAlignment.Center;
            boldStyle.SetFont(h1Font);
            cell.CellStyle = boldStyle;
        }

        public static void setFormatHeaderCell(IWorkbook workbook, ICell cell)
        {
            var h1Font = workbook.CreateFont();
            h1Font.FontHeightInPoints = (short)10;
            h1Font.IsBold = true;
            h1Font.Color = IndexedColors.White.Index;
            h1Font.FontName = "Calibri";
            ICellStyle boldStyle = workbook.CreateCellStyle();
            boldStyle.BorderBottom = BorderStyle.Thin;
            boldStyle.BorderTop = BorderStyle.Thin;
            boldStyle.BorderLeft = BorderStyle.Thin;
            boldStyle.BorderRight = BorderStyle.Thin;
            boldStyle.FillForegroundColor = IndexedColors.RoyalBlue.Index;
            boldStyle.FillPattern = FillPattern.SolidForeground;
            boldStyle.LeftBorderColor = IndexedColors.Red.Index;
            boldStyle.Alignment = HorizontalAlignment.Center;
            boldStyle.VerticalAlignment = VerticalAlignment.Center;
            boldStyle.SetFont(h1Font);
            boldStyle.WrapText = true;
            cell.CellStyle = boldStyle;
        }

        public static void setFormatColumnCell(IWorkbook workbook, ICell cell)
        {
            var h1Font = workbook.CreateFont();
            h1Font.FontHeightInPoints = (short)11;
            ICellStyle boldStyle = workbook.CreateCellStyle();
            h1Font.Boldweight = (short)FontBoldWeight.Bold;
            boldStyle.BorderBottom = BorderStyle.Thin;
            boldStyle.BorderTop = BorderStyle.Thin;
            boldStyle.BorderLeft = BorderStyle.Thin;
            boldStyle.BorderRight = BorderStyle.Thin;
            boldStyle.Alignment = HorizontalAlignment.Center;

            boldStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
            boldStyle.SetFont(h1Font);
            cell.CellStyle = boldStyle;
        }

        public static void setFormatColumnCellVertical(IWorkbook workbook, ICell cell)
        {
            var h1Font = workbook.CreateFont();
            h1Font.FontHeightInPoints = (short)11;
            ICellStyle boldStyle = workbook.CreateCellStyle();
            h1Font.Boldweight = (short)FontBoldWeight.Bold;
            boldStyle.BorderBottom = BorderStyle.Thin;
            boldStyle.BorderTop = BorderStyle.Thin;
            boldStyle.BorderLeft = BorderStyle.Thin;
            boldStyle.BorderRight = BorderStyle.Thin;
            boldStyle.Alignment = HorizontalAlignment.Center;
            boldStyle.VerticalAlignment = VerticalAlignment.Bottom;
            boldStyle.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
            boldStyle.Rotation = 90;
            boldStyle.SetFont(h1Font);
            cell.CellStyle = boldStyle;
        }


        public static void setFormatTitleCellBold(IWorkbook workbook, ICell cell, int fontSize)
        {
            var h1Font = workbook.CreateFont();
            h1Font.FontHeightInPoints = (short)fontSize;
            h1Font.IsBold = true;
            h1Font.FontName = "Century Gothic";
            h1Font.Underline = FontUnderlineType.Single;
            ICellStyle boldStyle = workbook.CreateCellStyle();
            boldStyle.VerticalAlignment = VerticalAlignment.Center;
            boldStyle.SetFont(h1Font);
            cell.CellStyle = boldStyle;
        }

        public static void setFormatSubTitleCell(IWorkbook workbook, ICell cell)
        {
            var h1Font = workbook.CreateFont();
            h1Font.FontHeightInPoints = (short)12;
            ICellStyle boldStyle = workbook.CreateCellStyle();
            boldStyle.SetFont(h1Font);
            cell.CellStyle = boldStyle;
        }

        public static void setFormatHeaderCellBold(IWorkbook workbook, ICell cell)
        {
            var h1Font = workbook.CreateFont();
            h1Font.FontHeightInPoints = (short)11;
            h1Font.IsBold = true;
            h1Font.FontName = "Calibri";
            ICellStyle boldStyle = workbook.CreateCellStyle();
            boldStyle.SetFont(h1Font);
            boldStyle.BorderBottom = BorderStyle.Thin;
            boldStyle.BorderTop = BorderStyle.Thin;
            boldStyle.BorderLeft = BorderStyle.Thin;
            boldStyle.BorderRight = BorderStyle.Thin;
            cell.CellStyle = boldStyle;
        }


        public static void setFormatFooterCell(IWorkbook workbook, ICell cell)
        {
            var h1Font = workbook.CreateFont();
            h1Font.FontHeightInPoints = (short)11;
            ICellStyle boldStyle = workbook.CreateCellStyle();
            boldStyle.SetFont(h1Font);
            cell.CellStyle = boldStyle;
        }

        public static void setFormatFooterCellBold(IWorkbook workbook, ICell cell)
        {
            var h1Font = workbook.CreateFont();
            h1Font.FontHeightInPoints = (short)11;
            h1Font.Boldweight = (short)FontBoldWeight.Bold;
            ICellStyle boldStyle = workbook.CreateCellStyle();
            boldStyle.SetFont(h1Font);
            cell.CellStyle = boldStyle;
        }

    }
}
