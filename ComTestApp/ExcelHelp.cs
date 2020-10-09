using ComTestApp.Common;
using ComTestApp.Entitys;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComTestApp
{
    public class ExcelHelp
    {
        
        public void GenerateExcelModel(List<FieldOfInfo> fieldOfInfos,string name)
        {
            bool IsHaveDropDown = false;//是否包含下拉列表
            var workbook = new HSSFWorkbook();//创建Workbook对象
            ISheet sheet = ((HSSFWorkbook)workbook).CreateSheet(name);//创建工作表
            ISheet helpSheet = ((HSSFWorkbook)workbook).CreateSheet("helpSheet");//创建辅助sheet
            IRow row = sheet.CreateRow(0);//创建表头
            int columnIndex = 1;
            foreach (FieldOfInfo item in fieldOfInfos)
            {
                if (item.IsExcelModel)
                {
                    ICell cell = row.CreateCell(columnIndex);//在行中添加一列
                    cell.SetCellValue(item.Notes);//设置列的内容
                    SetCellStyle(workbook, cell);
                    //添加下拉数据
                    if (!item.SelectType.IsEmpty())
                    {
                        IsHaveDropDown = true;
                        
                        SetCellDropdownlist(helpSheet);
                    }
                    columnIndex++;
                }
            }
            string filePath = "d:\\test.xls";
            FileStream fs = new FileStream(filePath, FileMode.Create);
            workbook.Write(fs);
            fs.Close();
        }

        /// <summary>
        /// 设置单元格为下拉框并限制输入值
        /// </summary>
        /// <param name="sheet"></param>
        private void SetCellDropdownlist(ISheet sheet1)
        {
            //设置生成下拉框的行和列
            var cellRegions = new CellRangeAddressList(0, 65535, 0, 0);

            //设置 下拉框内容
            DVConstraint constraint = DVConstraint.CreateExplicitListConstraint(
                new string[] { "itemA", "itemB", "itemC" });
            //绑定下拉框和作用区域，并设置错误提示信息
            HSSFDataValidation dataValidate = new HSSFDataValidation(cellRegions, constraint);
            dataValidate.CreateErrorBox("输入不合法", "请输入下拉列表中的值。");
            dataValidate.ShowPromptBox = true;

            sheet.AddValidationData(dataValidate);
        }

        /// <summary>
        /// 设置单元格样式
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="cell"></param>
        private void SetCellStyle(HSSFWorkbook workbook, ICell cell)
        {
            HSSFCellStyle fCellStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            HSSFFont ffont = (HSSFFont)workbook.CreateFont();
            ffont.FontHeight = 20 * 20;
            ffont.FontName = "微软雅黑";
            ffont.Color = HSSFColor.Red.Index;
            fCellStyle.SetFont(ffont);
            fCellStyle.FillBackgroundColor = HSSFColor.LemonChiffon.Index;
            fCellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;//垂直对齐
            fCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;//水平对齐
            cell.CellStyle = fCellStyle;
        }

    }
}
