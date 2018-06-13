using Ext.Net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;


public class PanelFieldUtility : IDisposable
{
    public PanelFieldUtility()
    {
    }

    public void FillPanelswithFields(Panel[] panels, DataTable dtContent, DataTable dtProperty, string IDField, string ValueField, string[] customTitle, string Kod)
    {
        //foreach (Panel panel in panels)
        //{
        //    panel.Items.Clear();
        //    panel.Items.u
        //    //panel.ClearContent();
        //}
        if (panels.Length == 1)
        {
            //destroyFirst(panels[0]);
        }
        for (int i = 0; i < dtProperty.Rows.Count; i++)
        {
            string fieldTitle, fieldType, fieldId, fieldCls = " ", changeFn = null, changeHnd = null;
            Boolean readOnly = false, hidden = false, protcted = false;
            int LabelWidth = 0, flex = 0;
            if (CommonFunctions.ContainColumn("DATA_TYPE", dtProperty))
            {
                fieldType = dtProperty.Rows[i]["DATA_TYPE"].ToString().Trim();
                if (!(new[] { "String", "Integer", "Decimal", "DateTime" }.Contains(fieldType)))
                {
                    fieldType = "String";
                }
            }
            else
            {
                fieldType = "String";
            }
            if (CommonFunctions.ContainColumn("COMPONENT_ID", dtProperty))
            {
                fieldId = dtProperty.Rows[i]["COMPONENT_ID"].ToString().Trim();
                if (string.IsNullOrEmpty(fieldId))
                {
                    fieldId = "Field" + i.ToString();
                }
            }
            else
            {
                fieldId = "EmptyField" + i.ToString();
            }
            if (CommonFunctions.ContainColumn("TITLE", dtProperty))
            {
                fieldTitle = dtProperty.Rows[i]["TITLE"].ToString().Trim();
            }
            else
            {
                fieldTitle = fieldId;
            }

            IDictionary<string, object> obj = new Dictionary<string, object>();
            string[] propertyArray = Regex.Split(dtProperty.Rows[i]["PROPERTIES"].ToString(), ";");
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
            if (obj.Count > 0)
            {
                if (obj.ContainsKey("ReadOnly"))
                {
                    readOnly = true;
                }
                if (obj.ContainsKey("Hidden"))
                {
                    hidden = true;
                }
                if (obj.ContainsKey("Protected"))
                {
                    protcted = true;
                }
                if (obj.ContainsKey("Flex"))
                {
                    flex = CommonFunctions.ConvertToInteger(obj["Flex"].ToString());
                }
                if (obj.ContainsKey("FieldCls"))
                {
                    fieldCls = obj["FieldCls"].ToString();
                }

                if (obj.ContainsKey("ListenersChangeFn"))
                {
                    changeFn = obj["ListenersChangeFn"].ToString().Trim();
                }
                if (obj.ContainsKey("ListenersChangeHandler"))
                {
                    changeHnd = obj["ListenersChangeHandler"].ToString().Trim();
                }
                if (obj.ContainsKey("LabelWidth"))
                {
                    if (!Int32.TryParse(obj["LabelWidth"].ToString(), out LabelWidth))
                    {
                        //hata ver
                    }
                }
                if (obj.ContainsKey("CustomTitlePos"))
                {
                    int CustomTitleArrayID = CommonFunctions.ConvertToInteger(obj["CustomTitlePos"].ToString());
                    if (customTitle.Length > CustomTitleArrayID)
                    {
                        if (!string.IsNullOrEmpty(customTitle[CustomTitleArrayID]))
                        {
                            fieldTitle = customTitle[CustomTitleArrayID];
                        }
                    }
                }
            }
            string idofValue = " ";
            int indexofValue = -1;
            string resultValue = " ";

            indexofValue = CommonFunctions.getIndexOfString(dtContent, IDField, fieldId);
            if (indexofValue == -1)
            {
                if (TryGetId(fieldId, out idofValue))
                {
                    indexofValue = CommonFunctions.getIndexOfString(dtContent, IDField, idofValue);
                }
                else
                {
                    //hata ver bulunamadı
                }
            }
            if (indexofValue > -1)
            {
                resultValue = dtContent.Rows[indexofValue][ValueField].ToString();
            }
            if (!string.IsNullOrEmpty(Kod))
            {
                fieldId += Kod.Trim();
            }
            if (fieldType.ToUpper() == "Integer".ToUpper() || fieldType.ToUpper() == "Decimal".ToUpper())
            {
                if(fieldType.ToUpper() == "Decimal".ToUpper())
                {
                    resultValue = resultValue.Replace(".", ",");
                }
                Ext.Net.NumberField tempNumberField = new Ext.Net.NumberField();
                tempNumberField.ItemID = dtProperty.Rows[i]["ID"].ToString();
                tempNumberField.ID = fieldId;
                tempNumberField.CustomConfig.Add(new ConfigItem("tablePropertyId", dtProperty.Rows[i]["ID"].ToString(), ParameterMode.Value));
                tempNumberField.CustomConfig.Add(new ConfigItem("resetValue", "0", ParameterMode.Value));
                tempNumberField.MinValue = 0;
                if (fieldType == "Integer")
                {
                    tempNumberField.AllowDecimals = false;
                    int outInt = 0;
                    if (Int32.TryParse(resultValue, out outInt))
                    {
                        /*ConfigItem: resetValue*/
                        tempNumberField.CustomConfig[1].Value = resultValue;
                    }
                    else
                    {
                        outInt = 0;
                        //hata ver
                    }
                    tempNumberField.SetValue(outInt);
                }
                else
                {
                    tempNumberField.AllowDecimals = true;
                    tempNumberField.DecimalPrecision = 2;
                    Decimal outDecimal = 0;
                    if (Decimal.TryParse(resultValue, out outDecimal))
                    {
                        /*ConfigItem: resetValue*/
                        tempNumberField.CustomConfig[1].Value = resultValue;
                    }
                    else
                    {
                        outDecimal = 0;
                        //hata ver
                    }
                    tempNumberField.SetValue(outDecimal);
                }
                if (fieldCls != " ")
                {
                    tempNumberField.FieldCls = fieldCls;
                }
                tempNumberField.Step = 1;
                tempNumberField.FieldLabel = fieldTitle;
                if (LabelWidth > 0)
                {
                    tempNumberField.LabelWidth = LabelWidth;
                }
                if (flex > 0)
                {
                    tempNumberField.Flex = flex;
                }
                tempNumberField.ReadOnly = readOnly;
                tempNumberField.Hidden = hidden;
                if (!string.IsNullOrEmpty(changeFn))
                {
                    tempNumberField.Listeners.Change.Fn = changeFn;
                }
                if (!string.IsNullOrEmpty(changeHnd))
                {
                    tempNumberField.Listeners.Change.Handler = changeHnd;
                }
                string[] strArry = new string[1] { "['change']" };
                tempNumberField.ValidateOnChange = true;
                tempNumberField.CheckChangeEvents = strArry;
                if (panels.Length == 1)
                {
                    panels[0].Add(tempNumberField);
                }
                else
                {
                    int panelIndex = searchComponentArrayById(panels, dtProperty.Rows[i]["PARENT_ID"].ToString().Trim());
                    if (panelIndex > -1)
                    {
                        panels[panelIndex].Add(tempNumberField);
                    }
                    else
                    {
                        //Hata yaz
                    }
                }
            }
            else if (fieldType.ToUpper() == "DateTime".ToUpper())
            {
                DateTime dateValue;
                Ext.Net.DateField tempDateField = new Ext.Net.DateField();
                if (resultValue == null || resultValue.Trim().Length == 0)
                {
                    dateValue = new DateTime(1900, 1, 1, 0, 0, 0);
                }
                else
                {
                    try
                    {
                        dateValue = Convert.ToDateTime(resultValue);
                    }
                    catch (Exception ex)
                    {
                        dateValue = new DateTime(1900, 1, 1, 0, 0, 0);
                        //hata ver
                    }
                }
                if (fieldCls != " ")
                {
                    tempDateField.FieldCls = fieldCls;
                }
                tempDateField.ItemID = dtProperty.Rows[i]["ID"].ToString();
                tempDateField.ID = fieldId;
                tempDateField.CustomConfig.Add(new ConfigItem("tablePropertyId", dtProperty.Rows[i]["ID"].ToString(), ParameterMode.Value));
                tempDateField.FieldLabel = fieldTitle;
                if (LabelWidth > 0)
                {
                    tempDateField.LabelWidth = LabelWidth;
                }
                tempDateField.ReadOnly = readOnly;
                tempDateField.Hidden = hidden;
                tempDateField.Value = dateValue;
                if (!string.IsNullOrEmpty(changeFn))
                {
                    tempDateField.Listeners.Change.Fn = changeFn;
                }
                if (!string.IsNullOrEmpty(changeHnd))
                {
                    tempDateField.Listeners.Change.Handler = changeHnd;
                }
                if (flex > 0)
                {
                    tempDateField.Flex = flex;
                }
                if (panels.Length == 1)
                {
                    panels[0].Add(tempDateField);
                }
                else
                {
                    int panelIndex = searchComponentArrayById(panels, dtProperty.Rows[i]["PARENT_ID"].ToString().Trim());
                    if (panelIndex > -1)
                    {
                        panels[panelIndex].Add(tempDateField);
                    }
                    else
                    {
                        //Hata yaz
                    }
                }
            }
            else
            {
                Ext.Net.TextField tempTextField = new Ext.Net.TextField();
                tempTextField.ID = fieldId;
                tempTextField.ItemID = dtProperty.Rows[i]["ID"].ToString();
                tempTextField.CustomConfig.Add(new ConfigItem("tablePropertyId", dtProperty.Rows[i]["ID"].ToString(), ParameterMode.Value));
                tempTextField.FieldLabel = fieldTitle;
                if (LabelWidth > 0)
                {
                    tempTextField.LabelWidth = LabelWidth;
                }
                tempTextField.ReadOnly = readOnly;
                tempTextField.Hidden = hidden;
                tempTextField.Value = resultValue;
                if (!string.IsNullOrEmpty(changeFn))
                {
                    tempTextField.Listeners.Change.Fn = changeFn;
                }
                if (!string.IsNullOrEmpty(changeHnd))
                {
                    tempTextField.Listeners.Change.Handler = changeHnd;
                    string[] strArry = new string[1] { "['change']" };
                    tempTextField.ValidateOnChange = true;
                    tempTextField.CheckChangeEvents = strArry;
                }
                if (fieldCls != " ")
                {
                    tempTextField.FieldCls = fieldCls;
                }
                if (flex > 0)
                {
                    tempTextField.Flex = flex;
                }
                if (panels.Length == 1)
                {
                    panels[0].Add(tempTextField);
                }
                else
                {
                    int panelIndex = searchComponentArrayById(panels, dtProperty.Rows[i]["PARENT_ID"].ToString().Trim());
                    if (panelIndex > -1)
                    {
                        panels[panelIndex].Add(tempTextField);
                    }
                    else
                    {
                        //Hata yaz
                    }
                }
            }
        }
    }

    private int searchComponentArrayById(Panel[] panels, string searchId)
    {
        int foundIndex = -1;
        for (int i = 0; i < panels.Length; i++)
        {
            if (panels[i].ID == searchId)
            {
                foundIndex = i;
                break;
            }
        }
        return foundIndex;
    }

    public void destroyFirst(Panel panel)
    {
        //Predicate<AbstractComponent> predicate = x => x.ID != " ";
        //X.GetCmp<Ext.Net.Panel>(panel.ID).Items.RemoveAll(predicate);

        //X.GetCmp<Ext.Net.Panel>(panel.ID).RemoveAll();

        int itemCount = X.GetCmp<Ext.Net.Panel>(panel.ID).Items.Count;
        if (itemCount > 0)
        {
            for (int i = 0; i < itemCount; i++)
            {
                var item = X.GetCmp<Ext.Net.Panel>(panel.ID).Items.First();
                X.GetCmp<Ext.Net.Panel>(panel.ID).Items.Remove(item);
            }
        }
    }

    //public void FillPanelswithFields(Panel panel, DataTable dtContent, DataTable dtProperty, string SearchPanelId, string IDField, string ValueField)
    //{
    //    Panel[] panels = new Panel[1] { panel };
    //    FillPanelswithFields(panels, dtContent, dtProperty, IDField, ValueField);
    //}

    public Boolean TryGetId(string input, out string result)
    {
        result = " ";
        int index;
        Boolean resultBool = false;
        if (input.ToUpper().Contains("TextField".ToUpper()))
        {
            index = input.ToUpper().IndexOf("TextField".ToUpper());
            if (index >= 0)
            {
                result = input.Remove(index, "TextField".Length);
            }
        }
        else if (input.ToUpper().Contains("NumberField".ToUpper()))
        {
            index = input.ToUpper().IndexOf("NumberField".ToUpper());
            if (index >= 0)
            {
                result = input.Remove(index, "NumberField".Length);
            }
        }
        else if (input.ToUpper().Contains("DateField".ToUpper()))
        {
            index = input.ToUpper().IndexOf("DateField".ToUpper());
            if (index >= 0)
            {
                result = input.Remove(index, "DateField".Length);
            }
        }
        if (result == " ")
        {
            resultBool = false;
        }
        else
        {
            resultBool = true;
        }
        return resultBool;
    }

    public DataTable getComponent(string ComponentID)
    {
        DataAccess dataAccess = new DataAccess();
        SqlParameter[] sqlparamArrayKol = new SqlParameter[4] { new SqlParameter("@InputComponent", ComponentID), new SqlParameter("@InputTypeof", "Field"), new SqlParameter("@MinRecursionLevel", "0"), new SqlParameter("@MaxRecursionLevel", "9999") };
        DataTable returnTable = new DataTable();
        try
        {
            returnTable = dataAccess.getSQL("getProProperties", sqlparamArrayKol);
        }
        catch (Exception ex)
        {
            ErrorHandlingUtility ehu = new ErrorHandlingUtility("PanelFieldUtility.cs");
            ehu.InsertError(ex, "Panel getirme hatası (" + ComponentID + ")", 927, "");
        }
        return returnTable;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}