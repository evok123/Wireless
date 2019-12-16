<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pocetna.aspx.cs" Inherits="ZadatakWireless._Default" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>Database:</legend>
    <asp:GridView ID="gvProizvodi" runat="server" Width="100%" OnSelectedIndexChanged="gvProizvodi_OnSelectedIndexChanged" DataSourceID="dsProizvodi" AutoGenerateColumns="False" DataKeyNames="id" AllowPaging="True">
        <Columns>
            <asp:CommandField ButtonType="Button" SelectText="Izmeni"  ShowSelectButton="True"></asp:CommandField>
            <asp:BoundField DataField="id"  HeaderText="id" Visible="False" ReadOnly="True" InsertVisible="False" SortExpression="id"></asp:BoundField>
            <asp:BoundField DataField="naziv" HeaderText="Naziv" SortExpression="naziv"></asp:BoundField>
            <asp:BoundField DataField="opis" HeaderText="Opis" SortExpression="opis"></asp:BoundField> 
            <asp:BoundField DataField="kategorija" HeaderText="Kategorija" SortExpression="kategorija"></asp:BoundField>
            <asp:BoundField DataField="proizvodjac" HeaderText="Proizvodjač" SortExpression="proizvodjac"></asp:BoundField>
            <asp:BoundField DataField="dobavljac" HeaderText="Dobavljač" SortExpression="dobavljac"></asp:BoundField>
            <asp:BoundField DataField="cena" HeaderText="Cena" SortExpression="cena"></asp:BoundField>
        </Columns>
    </asp:GridView>
    </fieldset>
    <asp:SqlDataSource runat="server" ID="dsProizvodi" ConnectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\WM.mdf;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT p.*,k.naziv kategorija,pr.naziv as proizvodjac,d.naziv as dobavljac FROM Proizvod p
                                                LEFT JOIN kategorija k ON p.kategorija_id = k.id 
LEFT JOIN proizvodjac pr ON pr.id = p.proizvodjac_id 
LEFT JOIN dobavljac d on d.id = p.dobavljac_id"></asp:SqlDataSource>
    <fieldset>
    <legend>JSON:</legend>
    <asp:GridView Visible="True" ID="gvProizvodiJson" Width="100%"  DataKeyNames="id" runat="server" OnSelectedIndexChanged="gvProizvodiJson_OnSelectedIndexChanged" AutoGenerateColumns="False"  AllowPaging="True" DataSourceID="dsJsonProizvodi">
        <Columns>
            <asp:CommandField ButtonType="Button" SelectText="Izmeni" ShowSelectButton="True"></asp:CommandField>
            <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="False"></asp:BoundField>
            <asp:BoundField DataField="Naziv" HeaderText="Naziv" SortExpression="Naziv"></asp:BoundField>
            <asp:BoundField DataField="Opis" HeaderText="Opis" SortExpression="Opis"></asp:BoundField>            
            <asp:BoundField DataField="Kategorija.naziv" HeaderText="Kategorija" SortExpression="Kategorija"></asp:BoundField>
            <asp:BoundField DataField="Proizvodjac.naziv" HeaderText="Proizvodjač" SortExpression="Proizvodjac"></asp:BoundField>
            <asp:BoundField DataField="Dobavljac.naziv" HeaderText="Dobavljač" SortExpression="Dobavljac"></asp:BoundField>
            <asp:BoundField DataField="Cena" HeaderText="Cena" SortExpression="Cena"></asp:BoundField>
        </Columns>
    </asp:GridView>
    </fieldset>
    <asp:ObjectDataSource runat="server" DataObjectTypeName="Models.Proizvod+Models.Kategorija+Models.Dobavljac+Models.Proizvodjac" ID="dsJsonProizvodi" SelectMethod="getProizvodiJSON" TypeName="ZadatakWireless.Logic.ProductController"></asp:ObjectDataSource>
</asp:Content>
