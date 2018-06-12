<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Demo.aspx.cs" Inherits="Promoto.Demo" %>

<!DOCTYPE html>


<script runat="server">
    
    public class MyModel
    {
        public static object Model = new
        {
            model = "model1"
        };
    }
</script>

<script type="text/jscript">
    var onEditComplete = function (editor, context) {

    }

    var deneme = function () {
        value = ortalama / netkazanc;
        return Ext.util.Format.number(value, '0,000.00');
    };
    var deneme2 = function (data) {
        return data.Ortalama / data.NetKazanc;
    };
    var deneme3 = function (data) {
        if (data.id > 0 && App.store2.count() > 0) {
            return App.store2.getAt(data.id - 1).get('adet') / 2;
        }
        else
        {
            return data.adet;
        }
    };
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <ext:Store runat="server" ID="store1">
        <Model>
            <ext:Model runat="server" Name="model1">
                <Fields>
                    <ext:ModelField Name="id" Type="Int"/>
                    <ext:ModelField Name="Ortalama" />
                    <ext:ModelField Name="NetKazanc" />
                    <ext:ModelField Name="KarOran"/>
                    <ext:ModelField Name="Adet" />
                    <ext:ModelField Name="AdetToplam" />
                    <ext:ModelField Name="Yuzde"/>
                    <ext:ModelField Name="KBOrtalama"/>
                    <ext:ModelField Name="StrPlanArtis"/>
                    <ext:ModelField Name="PersonelAdet"/>
                    <ext:ModelField Name="PersAdYuzde"/>
                    <ext:ModelField Name="OngorulenKBOrt" />
                </Fields>
            </ext:Model>
        </Model>
    </ext:Store>
    <ext:Store runat="server" ID="store2">
        <Model>
            <ext:Model runat="server" Name="model2">
                <Fields>
                    <%--<ext:ModelField Name="id" Type="Int"/>
                    <ext:ModelField Name="Ortalama" />
                    <ext:ModelField Name="NetKazanc" />
                    <ext:ModelField Name="KarOran"/>
                    <ext:ModelField Name="Adet" />
                    <ext:ModelField Name="AdetToplam" />
                    <ext:ModelField Name="Yuzde"/>
                    <ext:ModelField Name="KBOrtalama"/>
                    <ext:ModelField Name="StrPlanArtis"/>
                    <ext:ModelField Name="PersonelAdet"/>
                    <ext:ModelField Name="PersAdYuzde"/>
                    <ext:ModelField Name="OngorulenKBOrt" />--%>
                   <%-- <ext:ModelField Name="dnm">
                        <Calculate Fn="deneme3"/>
                    </ext:ModelField>--%>
                    <ext:ModelField Name="Ay" />
                    <ext:ModelField Name="PersonelAdetYuzde" />
                    <ext:ModelField Name="USOKisiSayi"/>
                    <ext:ModelField Name="StratejikKBOrtalama" />
                    <ext:ModelField Name="StratejikToplamTutar" />
                    <ext:ModelField Name="OngorulenKBOrtalama" />
                    <ext:ModelField Name="OngorulenKBBakiye" />
                    <ext:ModelField Name="StratejikKarOran" />
                    <ext:ModelField Name="StratejikNetKazanc" />
                    <ext:ModelField Name="BeklentiKarOran" />
                    <ext:ModelField Name="BeklentiNetKazanc" />
                </Fields>
            </ext:Model>
        </Model>
    </ext:Store>
    <ext:ResourceManager runat="server" Theme="Triton" DisableViewState="true"/>
    <form id="form1" runat="server">
        <ext:Container runat="server" Scrollable="Both" ViewModel="<%# MyModel.Model %>"
                    AutoDataBind="true" > 
            <LayoutConfig>
                <ext:VBoxLayoutConfig Align="Stretch" />
            </LayoutConfig>
            <Items>  
                <ext:GridPanel
                    ID ="GridPanel1"
                    runat="server"
                    Layout="TableLayout"
                    Scrollable="Both"
                    StoreID="store1">
                    <ColumnModel>
                        <Columns>
                            <ext:Column runat="server" Text="Ortalama" DataIndex="Ortalama" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000.00');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                            <ext:Column runat="server" Text="Net Kazanç" DataIndex="NetKazanc" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000.00');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                            <ext:Column runat="server" Text="Kar Oran" DataIndex="KarOran" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000.00');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                            <ext:Column runat="server" Text="Adet" DataIndex="Adet" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                            <ext:Column runat="server" Text="Adet Toplam" DataIndex="AdetToplam" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                            <ext:Column runat="server" Text="Yüzde" DataIndex="Yuzde" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000.00');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                            <ext:Column runat="server" Text="KB Ortalama" DataIndex="KBOrtalama" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000.00');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                        </Columns>
                    </ColumnModel>
                    <Plugins>
                       <ext:CellEditing runat="server">
                           <Listeners>
                               <Edit Fn="onEditComplete" />
                           </Listeners>
                       </ext:CellEditing>
                   </Plugins>
                </ext:GridPanel>
                <ext:Panel runat="server">
                    <Items>
                        <ext:TextField runat="server" ID="StrPlanArtis" FieldLabel="StrPlanArtis: " BindString="{store1.StrPlanArtis}"></ext:TextField>
                        <ext:TextField runat="server" ID="PersonelAdet" FieldLabel="PersonelAdet: " BindString="{store1.PersonelAdet}"></ext:TextField>
                        <ext:TextField runat="server" ID="PersAdYuzde" FieldLabel="PersAdYuzde: " BindString="{store1.PersAdYuzde}"></ext:TextField>
                        <ext:TextField runat="server" ID="OngorulenKBOrt" FieldLabel="OngorulenKBOrt: " BindString="{store1.OngorulenKBOrt}"></ext:TextField>
                    </Items>
                </ext:Panel>
                <ext:GridPanel
                    ID ="Panel0"
                    runat="server"
                    Layout="TableLayout"
                    Scrollable="Both"
                    StoreID="store2">
                    <ColumnModel>
                        <Columns>
                            <%--<ext:Column runat="server" Text="Ortalama" DataIndex="Ortalama" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000.00');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                            <ext:Column runat="server" Text="Net Kazanç" DataIndex="NetKazanc" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000.00');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                            <ext:Column runat="server" Text="Kar Oran" DataIndex="KarOran" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000.00');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                            <ext:Column runat="server" Text="Adet" DataIndex="Adet" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                            <ext:Column runat="server" Text="Adet Toplam" DataIndex="AdetToplam" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                            <ext:Column runat="server" Text="Yüzde" DataIndex="Yuzde" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000.00');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                            <ext:Column runat="server" Text="KB Ortalama" DataIndex="KBOrtalama" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000.00');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>--%>
                            <%--<ext:Column runat="server" Text="StrPlanArtis" DataIndex="StrPlanArtis" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000.00');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                            <ext:Column runat="server" Text="PersonelAdet" DataIndex="PersonelAdet" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                            <ext:Column runat="server" Text="PersAdYuzde" DataIndex="PersAdYuzde" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000.00');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                            <ext:Column runat="server" Text="OngorulenKBOrt" DataIndex="OngorulenKBOrt" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000.00');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>--%>
                            <ext:Column runat="server" Text="Ay" DataIndex="Ay" Flex="1"/>
                            <ext:Column runat="server" Text="PersonelAdetYuzde" DataIndex="PersonelAdetYuzde" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000.00');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                            <ext:Column runat="server" Text="USOKisiSayi" DataIndex="USOKisiSayi" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000.00');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                            <ext:Column runat="server" Text="StratejikKBOrtalama" DataIndex="StratejikKBOrtalama" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000.00');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                            <ext:Column runat="server" Text="StratejikToplamTutar" DataIndex="StratejikToplamTutar" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000.00');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                            <ext:Column runat="server" Text="OngorulenKBOrtalama" DataIndex="OngorulenKBOrtalama" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000.00');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                            <ext:Column runat="server" Text="OngorulenKBBakiye" DataIndex="OngorulenKBBakiye" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000.00');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                            <ext:Column runat="server" Text="StratejikKarOran" DataIndex="StratejikKarOran" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0.000000');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                            <ext:Column runat="server" Text="StratejikNetKazanc" DataIndex="StratejikNetKazanc" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000.00');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                            <ext:Column runat="server" Text="BeklentiKarOran" DataIndex="BeklentiKarOran" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0.000000');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                            <ext:Column runat="server" Text="BeklentiNetKazanc" DataIndex="BeklentiNetKazanc" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000.00');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                        <%--<Columns>
                            <ext:Column runat="server" Text="Deneme" DataIndex="dnm" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000.00');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>--%>
                        </Columns>
                    </ColumnModel>
                    <Plugins>
                       <ext:CellEditing runat="server">
                           <Listeners>
                               <Edit Fn="onEditComplete" />
                           </Listeners>
                       </ext:CellEditing>
                   </Plugins>
                </ext:GridPanel>
            </Items>
        </ext:Container>
    </form>
</body>
</html>
