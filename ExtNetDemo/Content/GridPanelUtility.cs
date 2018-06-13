using Ext.Net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;


public class GridPanelUtility : IDisposable
{
    ErrorHandlingUtility ehu = new ErrorHandlingUtility("GridPanelUtility.cs");
    public GridPanelUtility()
    {
    }


    public void FillGridwithList<T>(GridPanel grid, DataTable dtProProperty, List<T> dtContent, Boolean read_only)
    {
        if (dtProProperty == null)
        {
            ehu.InsertError("public void FillGridwithList<T>(GridPanel grid, DataTable dtProProperty, List<T> dtContent, Boolean read_only)", 801, grid.ID + " yüklenirken hata oluştu.", "dtProProperty == null", "");
            return;
        }
        Store store = grid.GetStore();
        if (X.IsAjaxRequest)
        {
            if (store.Model.Count > 0)
            {
                store.Model[0].Fields.Clear();
            }
        }
        for (int i = 0; i < grid.ColumnModel.Columns.Count; i++)
        {
            grid.ColumnModel.Columns.RemoveAt(i);
        }
        int counter = 0;
        string[] ColType = new string[dtProProperty.Rows.Count];
        foreach (DataRow row in dtProProperty.Rows)
        {

            if (row["DATA_TYPE"].ToString().Trim() == "GroupHeader")
            {
                //GroupHeader ise Store'a ModelField olarak eklemeye gerek yok.
            }
            else
            {
                ModelFieldType modelFieldType;
                switch (row["DATA_TYPE"].ToString().Trim())
                {
                    case "DateTime":
                        modelFieldType = ModelFieldType.Date; 
                        ColType[counter] = "D";
                        break;
                    case "Integer":
                        modelFieldType = ModelFieldType.Int;
                        ColType[counter] = "I";
                        break;
                    case "Decimal":
                        modelFieldType = ModelFieldType.Float;
                        ColType[counter] = "F";
                        break;
                    default:
                        modelFieldType = ModelFieldType.String;
                        ColType[counter] = "S";
                        break;
                }
                ModelField field = new ModelField(row["COMPONENT_ID"].ToString().Trim(), modelFieldType);
                field.AllowNull = true;
                field.SubmitEmptyValue = EmptyValue.Null;
                //if(row["COMPONENT_ID"].ToString().Trim() == "KarOran")
                //{
                //    field.Convert.Fn = "Sema2Functions.ConvertKarOran";
                //}
                if (X.IsAjaxRequest)
                {
                    store.AddField(field);
                }
                else
                {
                    store.Model[0].Fields.Add(field);
                }
            }
            #region Column işlemleri

            Column gridCol = new Column { ID = row["ID"].ToString().Trim(), DataIndex = row["COMPONENT_ID"].ToString().Trim(), Text = row["TITLE"].ToString(), Flex = 1, CellWrap = false };
            string columnIdentifier = row["ID"].ToString().Trim() + "|" + row["COMPONENT_ID"].ToString().Trim() + "|" + row["TITLE"].ToString() + "|";

            gridCol.CustomConfig.Add(new ConfigItem("StoreModelType", ColType[counter], ParameterMode.Value));

            /* coloumn'a geç */
            string[] propertyArray = Regex.Split(row["PROPERTIES"].ToString(), ";");
            IDictionary<string, object> obj = new Dictionary<string, object>();
            foreach (string s in propertyArray)
            {
                if (s.Contains(":"))
                {
                    string[] substring = Regex.Split(s, ":");
                    string temp1 = substring[0];
                    string temp2 = substring[1];
                    obj.Add(temp1, temp2);
                }
                else
                {
                    obj.Add(s, true);
                }
            }

            if (obj.ContainsKey("CustomConfig"))
            {
                string configName;
                string configValue;
                string[] configArray = Regex.Split(obj["CustomConfig"].ToString().Trim(), "->");
                if(configArray.Length==2)
                {
                    configName = configArray[0].Trim();
                    configValue = configArray[1].Trim();
                    gridCol.CustomConfig.Add(new ConfigItem(configName, configValue, ParameterMode.Value));
                }
                else
                {
                    ehu.InsertError("", 8010, "CustomConfig hatalı", columnIdentifier + "obj[CustomConfig:]" + obj["CustomConfig"].ToString());
                }
            }
            if (obj.ContainsKey("Hidden"))
            {
                gridCol.Hidden = true;
            }
            else
            {
                if (obj.ContainsKey("Editable"))
                {
                    if (!read_only)
                    {
                        Boolean focus = false;
                        if (obj.ContainsKey("SelectOnFocus"))
                        {
                            focus = true;
                        }
                        if (ColType[counter] == "I")
                        {
                            NumberField numberfield = new NumberField();
                            if (focus) numberfield.SelectOnFocus = true;
                            gridCol.Editor.Add(numberfield);
                        }
                        else if (ColType[counter] == "F")
                        {
                            NumberField numberfield = new NumberField();
                            if (focus) numberfield.SelectOnFocus = true;
                            if (obj.ContainsKey("Step"))
                            {
                                double step = double.Parse(obj["Step"].ToString());
                                numberfield.Step = step;
                            }
                            if (obj.ContainsKey("DecimalPrecision"))
                            {
                                int precision = CommonFunctions.ConvertToInteger(obj["DecimalPrecision"].ToString());
                                numberfield.DecimalPrecision = (precision < 0 ? 2: precision);
                            }
                            gridCol.Editor.Add(numberfield);
                        }
                        else if (ColType[counter] == "D")
                        {
                            DateField datefield = new DateField();
                            if (focus) datefield.SelectOnFocus = true;
                            gridCol.Editor.Add(datefield);
                        }
                        else if (ColType[counter] == "S")
                        {
                            TextField textfield = new TextField();
                            if (focus) textfield.SelectOnFocus = true;
                            gridCol.Editor.Add(textfield);
                        }
                        else
                        {
                            TextField textfield = new TextField();
                            if (focus) textfield.SelectOnFocus = true;
                            gridCol.Editor.Add(textfield);
                        }
                    }
                }
                if (obj.ContainsKey("Renderer"))
                {
                    gridCol.Renderer.Fn = obj["Renderer"].ToString().Trim();
                }
                else if (ColType[counter] == "F")
                {
                    gridCol.Renderer.Fn = "Ext.util.Format.numberRenderer('0,000.00')";
                }
                else if (ColType[counter] == "I")
                {
                    gridCol.Renderer.Fn = "Ext.util.Format.numberRenderer('0,000')";
                }
                else if (ColType[counter] == "D")
                {
                    gridCol.Renderer.Fn = "Ext.util.Format.dateRenderer('d.m.Y H:i:s')";
                }
                if (obj.ContainsKey("Flex"))
                {
                    int flexVal = 1;
                    if(!Int32.TryParse(obj["Flex"].ToString(), out flexVal))
                    {
                        ehu.InsertError("",8011,"Flex nümerik değil", columnIdentifier + "obj[Flex:]" + obj["Flex"].ToString());
                    }
                    gridCol.Flex = flexVal;
                }
                if (obj.ContainsKey("CellWrap"))
                {
                    gridCol.CellWrap = true;
                }
                if (obj.ContainsKey("SummaryType"))
                {
                    if (counter == 0)
                    {
                        gridCol.SummaryRenderer.Fn = "return ('Toplam:');";
                    }
                    else if (obj["SummaryType"].ToString().Trim() == "Sum")
                    {
                        gridCol.SummaryType = SummaryType.Sum;
                        if (ColType[counter] == "I")
                        {
                            gridCol.SummaryRenderer.Fn = "Ext.util.Format.numberRenderer('0,000')";
                        }
                        else if (ColType[counter] == "F")
                        {
                            gridCol.SummaryRenderer.Fn = "Ext.util.Format.numberRenderer('0,000.00')";
                        }
                    }
                    else if (obj["SummaryType"].ToString().Trim() == "Max")
                    {
                        gridCol.SummaryType = SummaryType.Max;
                        if (ColType[counter] == "I")
                        {
                            gridCol.SummaryRenderer.Fn = "Ext.util.Format.numberRenderer('0,000')";
                        }
                        else if (ColType[counter] == "F")
                        {
                            gridCol.SummaryRenderer.Fn = "Ext.util.Format.numberRenderer('0,000.00')";
                        }
                    }
                    else
                    {
                        gridCol.SummaryType = SummaryType.None;
                    }
                }
                else if (obj.ContainsKey("CustomSummaryType"))
                {
                    gridCol.CustomSummaryType = obj["CustomSummaryType"].ToString().Trim();
                    if (row["ID"].ToString().Trim() == "16")
                    {
                        gridCol.TdCls = "task";
                    }
                }
                else if (obj.ContainsKey("Filter"))
                {
                    if (obj["Filter"].ToString().Trim() == "ListFilter")
                    {
                        ListFilter listfilter = new ListFilter();
                        if (obj.ContainsKey("ListFilterLabel"))
                        {
                            listfilter.LabelField = obj["ListFilterLabel"].ToString();
                        }
                        if (obj.ContainsKey("ListFilterContent"))
                        {
                            string tmp = obj["ListFilterContent"].ToString();
                            string[] stringSeparators = new string[] { "\"" };
                            string[] options = tmp.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                            List<string> optionsList = new List<string>(options);
                            foreach (string s in options)
                            {
                                if (s == ",")
                                {
                                    optionsList.Remove(",");
                                }
                            }
                            listfilter.Options = optionsList.ToArray();
                        }
                        gridCol.Filter.Add(listfilter);
                    }
                    else if (obj["Filter"].ToString().Trim() == "StringFilter")
                    {
                        StringFilter stringfilter = new StringFilter();
                        gridCol.Filter.Add(stringfilter);
                    }
                }
            }
            if (obj.ContainsKey("Sort"))
            {
                store.Sort(new DataSorter() { Property = row["COMPONENT_ID"].ToString().Trim(), Direction = Ext.Net.SortDirection.ASC });
            }
            if (obj.ContainsKey("GroupField"))
            {
                store.GroupField = row["COMPONENT_ID"].ToString().Trim();
            }
            gridCol.ToolTip = row["TITLE"].ToString();

            /*en alt seviye ise direk ekle*/
            if (row["RecursionLevel"].ToString().Trim() == "0")
            {
                grid.ColumnModel.Columns.Add(gridCol);
            }
            else
            {
                /*değilse parent bulup ekle*/
                Predicate<ColumnBase> predicate = x => x.DataIndex == row["PARENT_ID"].ToString().Trim();
                int indexFound = grid.ColumnModel.Columns.FindIndex(predicate);
                if (indexFound == -1)
                {
                    ehu.InsertError("", 8019, "CustomConfig hatalı", columnIdentifier + "obj[CustomConfig:]" + obj["CustomConfig"].ToString());
                }
                else
                {
                    grid.ColumnModel.Columns[indexFound].Columns.Add(gridCol);
                }
            }

            #endregion

            if (X.IsAjaxRequest)
            {
                grid.Reconfigure();
            }
            counter++;
        }
        try
        {
            store.RebuildMeta();
            store.DataSource = dtContent;
            store.DataBind();
        }
        catch(Exception ex)
        {
            ehu.InsertError(ex, grid.ID + " veri bağlanamadı", 8019);
        }

        if (X.IsAjaxRequest)
        {
            grid.Reconfigure();
        }
    }

    public DataTable getColumns(string ComponentID)
    {
        DataAccess dataAccess = new DataAccess();
        SqlParameter[] sqlparamArrayKol = new SqlParameter[4] { new SqlParameter("@InputComponent", ComponentID), new SqlParameter("@InputTypeof", "col"), new SqlParameter("@MinRecursionLevel", "0"), new SqlParameter("@MaxRecursionLevel", "9999") };
        DataTable returnTable = new DataTable();
        try
        {
            returnTable = dataAccess.getSQL("getProProperties", sqlparamArrayKol);
        }
        catch (Exception ex)
        {
            ehu.InsertError(ex, "Kolon getirme hatası (" + ComponentID + ")", 8027, "");
        }
        return returnTable;
    }

    public void FillGridwithDatatable(GridPanel grid, DataTable dtProProperty, DataTable dtContent, Boolean read_only)
    {
        List<object> listObj = new List<object>();
        IDictionary<string, object> objDic = null;
        if (dtContent == null)
        {
            objDic = new Dictionary<string, object>();
            for (int i = 0; i < dtProProperty.Rows.Count; i++)
            {
                objDic.Add(dtProProperty.Rows[i][4].ToString(), null);
            }
        }
        else
        {
            for (int i = 0; i < dtContent.Rows.Count; i++)
            {
                DataRow dr = dtContent.Rows[i];
                objDic = new Dictionary<string, object>();
                for (int j = 0; j < dtContent.Columns.Count; j++)
                {
                    objDic.Add(dtContent.Columns[j].ColumnName, dr[j]);
                }
                listObj.Add(objDic);
            }
        }
        FillGridwithList<object>(grid, dtProProperty, listObj, read_only);
    }
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}