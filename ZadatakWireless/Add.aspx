<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="ZadatakWireless.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div  style="width: 50%; margin: 0 auto; ">
        Naziv:<br/>
   <asp:TextBox Width="100%" ID="tbNaziv" runat="server">
    </asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldNaziv" 
                                runat="server"  ControlToValidate ="tbNaziv"
                                ErrorMessage="Molimo unesite naziv proizvoda"></asp:RequiredFieldValidator>
    <br/>
   
        Opis:<br/>
    <asp:TextBox Width="100%" TextMode="MultiLine" ID="tbOpis" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="validateNaziv" 
                                runat="server"  ControlToValidate ="tbOpis"
                                ErrorMessage="Molimo unesite opis proizvoda">
   
    </asp:RequiredFieldValidator>
    <br/>
    Kategorija:<br/>
        <asp:DropDownList ID="cbKategorija" Width="100%"  runat="server" DataSourceID="dsKategorija" DataTextField="naziv" DataValueField="id"></asp:DropDownList>
    <br/>
    <br/>
    Proizvodjac:<br/>
        <asp:DropDownList ID="cbProizvodjac" Width="100%"  runat="server" DataSourceID="dsProizvodjac" DataTextField="naziv" DataValueField="id"></asp:DropDownList>
    <br/>
    <br/>
        Dobavljač:<br/>
   <asp:DropDownList ID="cbDobavljac" Width="100%"  runat="server" DataSourceID="dsDobavljac" DataTextField="naziv" DataValueField="id"></asp:DropDownList>
    <br/>
    <br/>
        Cena:<br/>
    <asp:TextBox  ID="tbCena" Width="100%"  runat="server"></asp:TextBox> 
    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorCena" 
                                runat="server"  ControlToValidate ="tbCena"
                                ErrorMessage="Molimo unesite cenu proizvoda"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator runat="server" id="rexNumber" controltovalidate="tbCena" validationexpression="[+]?([0-9]*[.])?[0-9]+" errormessage="Cena nije u odgovarajucem formatu" />
    <br/>
    <asp:Button ID="btnSave" Width="50%" OnClick="btnSave_OnClick" runat="server" Text="Sacuvaj" />
    <asp:Button ID="btnCancel" Width="50%" OnClick="btnCancel_OnClick" runat="server" Text="Odustani" />
   <%-- DATASOURCES--%>
    </div>
    <asp:ObjectDataSource runat="server" ID="dsKategorijaJSON" SelectMethod="getKategorije" TypeName="ZadatakWireless.JSONHelper"></asp:ObjectDataSource>
    <asp:ObjectDataSource runat="server" ID="dsDobavljacJSON" SelectMethod="getDobavljace" TypeName="ZadatakWireless.JSONHelper"></asp:ObjectDataSource>
    <asp:ObjectDataSource runat="server" ID="dsProizvodjacJSON" SelectMethod="getProizvodjac" TypeName="ZadatakWireless.JSONHelper"></asp:ObjectDataSource>
    <asp:SqlDataSource runat="server" ID="dsKategorija" ConnectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\WM.mdf;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM kategorija"></asp:SqlDataSource>
    <asp:SqlDataSource runat="server" ID="dsProizvodjac" ConnectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\WM.mdf;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM Proizvodjac"></asp:SqlDataSource>
    <asp:SqlDataSource runat="server" ID="dsDobavljac" ConnectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\WM.mdf;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [Dobavljac]"></asp:SqlDataSource>
    </asp:Content>
