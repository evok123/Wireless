using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZadatakWireless.Models;


namespace ZadatakWireless
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.LoadComplete += Page_LoadComplete;
        }
        private void Page_LoadComplete(object sender, EventArgs e)
        {
           
            if (Request.QueryString["S"] == "JSON")
            {
                cbKategorija.DataSourceID = "dsKategorijaJSON";
                cbDobavljac.DataSourceID = "dsDobavljacJSON";
                cbProizvodjac.DataSourceID = "dsProizvodjacJSON";
            }
            if (Request.QueryString["ID"] != null)
            {
                
                btnSave.Text = "Sačuvaj izmene";
                if (Request.QueryString["S"] == "DB")
                {
                    //DATABASE
                    var controller = new ZadatakWireless.Logic.ProductController();
                    var proizvod = controller.getProizvodDB(int.Parse(Request.QueryString["ID"].ToString()));
                    tbNaziv.Text = proizvod.Naziv;
                    tbOpis.Text = proizvod.Opis;
                    tbCena.Text = proizvod.Cena.ToString();
                    cbKategorija.SelectedValue = proizvod.Kategorija.Id.ToString();
                    cbDobavljac.SelectedValue = proizvod.Dobavljac.Id.ToString();
                    cbProizvodjac.SelectedValue = proizvod.Proizvodjac.Id.ToString();
                }
                else
                {
                    var controller = new ZadatakWireless.Logic.ProductController();
                    var proizvod = controller.getProizvodByIdJSON(int.Parse(Request.QueryString["ID"].ToString()));
                    tbNaziv.Text = proizvod.Naziv;
                    tbOpis.Text = proizvod.Opis;
                    tbCena.Text = proizvod.Cena.ToString();
                    cbKategorija.SelectedValue = proizvod.Kategorija.Id.ToString();
                    cbDobavljac.SelectedValue = proizvod.Dobavljac.Id.ToString();
                    cbProizvodjac.SelectedValue = proizvod.Proizvodjac.Id.ToString();
                    //JSON
                }
            }

        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            var controller = new ZadatakWireless.Logic.ProductController();
            var proizvod = new Proizvod();
            var kategorija = new Kategorija();
            var dobavljac = new Dobavljac();
            var proizvodjac = new Proizvodjac();
            proizvod.Naziv = tbNaziv.Text;
            proizvod.Opis = tbOpis.Text;
            kategorija.Id= int.Parse(cbKategorija.SelectedValue);
            kategorija.Naziv = cbKategorija.SelectedItem.Text;
            proizvodjac.Id = int.Parse(cbProizvodjac.SelectedValue);
            proizvodjac.Naziv = cbProizvodjac.SelectedItem.Text;
            dobavljac.Id = int.Parse(cbDobavljac.SelectedValue);
            dobavljac.Naziv = cbDobavljac.SelectedItem.Text;
            proizvod.Kategorija = kategorija;
            proizvod.Dobavljac = dobavljac;
            proizvod.Proizvodjac = proizvodjac;
            proizvod.Cena = double.Parse(tbCena.Text);
            //INSERT
            if (Request.QueryString["ID"] == null)
            {
                if (Request.QueryString["S"] == "JSON")
                {
                   var insert =  controller.addProizvodJSON(proizvod);
                    if (insert)
                    {
                        Response.Redirect("~/Pocetna.aspx");
                    }
                    else
                    {

                    }
                }
                else
                {
                    var insert = controller.addProizvod(proizvod);
                    if (insert)
                    {
                        Response.Redirect("~/Pocetna.aspx");
                    }
                    else
                    {
                        //showError
                    }
                }
               
            }
            else
            {
                if (Request.QueryString["S"] == "JSON")
                {
                    proizvod.Id = int.Parse(Request.QueryString["ID"]);
                    var update =  controller.updateProizvodJSON(proizvod);
                    if (update)
                    {
                        Response.Redirect("~/Pocetna.aspx");
                    }
                    else
                    {

                    }
                }
                else
                {
                    proizvod.Id = int.Parse(Request.QueryString["ID"]);
                    var update = controller.updateProizvod(proizvod);
                    if (update)
                    {
                        Response.Redirect("~/Pocetna.aspx");
                    }
                    else
                    {

                    }
                }

            
            }
        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
           Response.Redirect("Pocetna.aspx");
        }
    }
}