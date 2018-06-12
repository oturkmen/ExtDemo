<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Demo2.aspx.cs" Inherits="Promoto.Demo2" %>

<!DOCTYPE html>


<script runat="server">

</script>

<script type="text/jscript">
    var onEditComplete = function (editor, context) {

    }

    var deneme = function () {
        value = ortalama / netkazanc;
        return Ext.util.Format.number(value, '0,000.00');
    };
    var deneme2 = function (data) {
        return data.ortalama / data.netkazanc;
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
    <ext:Store runat="server" ID="store2">
        <Model>
            <ext:Model runat="server" Name="model2">
                <Fields>
                    <ext:ModelField Name="id" Type="Int"/>
                    <ext:ModelField Name="ortalama" />
                    <ext:ModelField Name="netkazanc" />
                    <ext:ModelField Name="karoran">
                        <Calculate Fn="deneme2"/>
                    </ext:ModelField>
                    <ext:ModelField Name="adet" />
                    <ext:ModelField Name="adettoplam" />
                    <ext:ModelField Name="yuzde">
                        <Calculate Handler="return data.adet * 100 / data.adettoplam; "/>
                    </ext:ModelField>
                    <ext:ModelField Name="kbortalama">
                        <Calculate Handler="return data.ortalama * 100 / data.adet; "/>
                    </ext:ModelField>
                    <ext:ModelField Name="dnm">
                        <Calculate Fn="deneme3"/>
                    </ext:ModelField>
                    <ext:ModelField Name="dnm2" ServerMapping="deneme5"/>
                </Fields>
            </ext:Model>
        </Model>
    </ext:Store>
    <ext:ResourceManager runat="server" Theme="Triton" DisableViewState="true"/>
    <form id="form1" runat="server">
        <ext:Container runat="server" Scrollable="Both"
                    AutoDataBind="true" > 
            <LayoutConfig>
                <ext:VBoxLayoutConfig Align="Stretch" />
            </LayoutConfig>
            <Items>  
                <ext:GridPanel
                    ID ="Panel0"
                    runat="server"
                    Layout="TableLayout"
                    Scrollable="Both"
                    StoreID="store2">
                    <ColumnModel>
                        <Columns>
                            <ext:Column runat="server" Text="Ortalama" DataIndex="ortalama" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000.00');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                        </Columns>
                        <Columns>
                            <ext:Column runat="server" Text="Net Kazanç" DataIndex="netkazanc" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000.00');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                        </Columns>
                        <Columns>
                            <ext:Column runat="server" Text="Kar Oran" DataIndex="karoran" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000.00');"/>
                               <%-- <Renderer Handler="deneme"/>--%>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                        </Columns>
                        <Columns>
                            <ext:Column runat="server" Text="Adet" DataIndex="adet" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                        </Columns>
                        <Columns>
                            <ext:Column runat="server" Text="Adet Toplam" DataIndex="adettoplam" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                        </Columns>
                        <Columns>
                            <ext:Column runat="server" Text="Yüzde" DataIndex="yuzde" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000.00');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                        </Columns>
                        <Columns>
                            <ext:Column runat="server" Text="KB Ortalama" DataIndex="kbortalama" Flex="1">
                                <Renderer Handler="return Ext.util.Format.number(value, '0,000.00');"/>
                               <Editor>
                                   <ext:NumberField runat="server" />
                               </Editor>
                           </ext:Column>
                        </Columns>
                        <Columns>
                            <ext:Column runat="server" Text="Deneme" DataIndex="dnm" Flex="1">
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
            </Items>
        </ext:Container>
    </form>
</body>
</html>
