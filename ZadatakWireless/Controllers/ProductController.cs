using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Web;
using ZadatakWireless.Models;

namespace ZadatakWireless.Logic
{

    /// <summary>
    /// CRUD OPERACIJE ZA PROIZVOD
    /// </summary>
    public class ProductController
    {
        private string jsonSource = JSONHelper.jsonFile;
        private string ConnectionString = ConfigurationManager.ConnectionStrings["WMConnectionString"].ConnectionString;
        #region DB OP's
        /// <summary>
        /// Funkcija koja vraca listu proizvoda iz baze
        /// </summary>
        /// <returns>List<Proizvod></returns>

        public Proizvod getProizvodDB(int id)
        {
            var proizvod = new Proizvod();
            try
            {
                //Kreiramo konekciju
                using (var connection = new SqlConnection(ConnectionString))
                {
                    //Kreiamo komandu
                    using (var command = connection.CreateCommand())
                    {
                        //Setujemo text komande
                        command.CommandText = @"SELECT p.*,k.naziv as kategorija,pr.naziv as proizvodjac,d.naziv as dobavljac FROM Proizvod p
                                                LEFT JOIN kategorija k ON p.kategorija_id = k.id 
                                                LEFT JOIN proizvodjac pr ON pr.id = p.proizvodjac_id 
                                                LEFT JOIN dobavljac d on d.id = p.dobavljac_id where p.id = @id";
                        command.Parameters.AddWithValue("@id", id);
                        //Otvaramo konekciju
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            //Citamo podatke
                            while (reader.Read())
                            {

                                if (reader.HasRows)
                                {
                                    proizvod.Id = reader.GetInt32(0);
                                    proizvod.Naziv = reader.GetString(1);
                                    proizvod.Opis = reader.GetString(2);
                                    var kategorija = new Kategorija();
                                    kategorija.Id = reader.GetInt32(3);
                                    kategorija.Naziv = reader.GetString(7);
                                    proizvod.Kategorija = kategorija;
                                    var proizvodjac = new Proizvodjac();
                                    proizvodjac.Id = reader.GetInt32(4);
                                    proizvodjac.Naziv = reader.GetString(8);
                                    proizvod.Proizvodjac = proizvodjac;
                                    var dobavljac = new Dobavljac();
                                    dobavljac.Id = reader.GetInt32(5);
                                    dobavljac.Naziv = reader.GetString(9);
                                    proizvod.Dobavljac = dobavljac;
                                    proizvod.Cena = reader.GetDouble(6);

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //Logujemo gresku
                throw;
            }

            return proizvod;
        }
        public bool addProizvod(Proizvod proizvod)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText =
                            "INSERT INTO proizvod VALUES(@naziv,@opis,@kategorija,@proizvodjac,@dobavljac,@cena)";
                        command.Parameters.AddWithValue("@naziv", proizvod.Naziv);
                        command.Parameters.AddWithValue("@opis", proizvod.Opis);
                        command.Parameters.AddWithValue("@kategorija", proizvod.Kategorija.Id);
                        command.Parameters.AddWithValue("@proizvodjac", proizvod.Proizvodjac.Id);
                        command.Parameters.AddWithValue("@dobavljac", proizvod.Dobavljac.Id);
                        command.Parameters.AddWithValue("cena", proizvod.Cena);
                        connection.Open();
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }


        }
        public bool updateProizvod(Proizvod proizvod)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText =
                            "UPDATE proizvod set naziv=@naziv,opis=@opis,kategorija_id=@kategorija,proizvodjac_id=@proizvodjac,dobavljac_id=@dobavljac,cena=@cena WHERE id = @id";
                        command.Parameters.AddWithValue("@naziv", proizvod.Naziv);
                        command.Parameters.AddWithValue("@opis", proizvod.Opis);
                        command.Parameters.AddWithValue("@kategorija", proizvod.Kategorija.Id);
                        command.Parameters.AddWithValue("@proizvodjac", proizvod.Proizvodjac.Id);
                        command.Parameters.AddWithValue("@dobavljac", proizvod.Dobavljac.Id);
                        command.Parameters.AddWithValue("cena", proizvod.Cena);
                        command.Parameters.AddWithValue("@id", proizvod.Id);
                        connection.Open();
                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }


        }
        #endregion
        #region JSON OP's
        public bool addProizvodJSON(Proizvod proizvod)
        {
            var pro = "{ 'id':'" + JSONHelper.returnNextId() + "','naziv': '" + proizvod.Naziv + "'," +
                           "'opis': '" + proizvod.Opis + "','cena':'" + proizvod.Cena + "','kategorija':[{'id':'" + proizvod.Kategorija.Id + "','naziv':'" + proizvod.Kategorija.Naziv + "'}]," +
                           "'proizvodjac':[{'id':'" + proizvod.Proizvodjac.Id + "','naziv':'" + proizvod.Proizvodjac.Naziv + "'}],'dobavljac':[{'id':'" + proizvod.Dobavljac.Id + "','naziv':'" + proizvod.Dobavljac.Naziv + "'}]}";
            try
            {
                var json = File.ReadAllText(JSONHelper.jsonFile);
                var jsonObj = JObject.Parse(json);
                var proizvodiArray = jsonObj.GetValue(JSONHelper.mainJSONObject) as JArray;
                var noviProizvod = JObject.Parse(pro);
                proizvodiArray.Add(noviProizvod);
                jsonObj[JSONHelper.mainJSONObject] = proizvodiArray;
                string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(JSONHelper.jsonFile, newJsonResult);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        public List<Proizvod> getProizvodiJSON()
        {
            var list = new List<Proizvod>();
            var json = File.ReadAllText(JSONHelper.jsonFile);
            try
            {
                var jObject = JObject.Parse(json);
                if (jObject != null)
                {
                    JArray proizvodArray = (JArray)jObject[JSONHelper.mainJSONObject];
                    if (proizvodArray != null)
                    {
                        foreach (var item in proizvodArray)
                        {
                            Proizvod p = new Proizvod();
                            Kategorija kategorija = new Kategorija();
                            var proizvodjac = new Proizvodjac();
                            var dobavljac = new Dobavljac();
                            p.Id = int.Parse(item["id"].ToString());
                            p.Naziv = item["naziv"].ToString();
                            p.Opis = item["opis"].ToString();
                            p.Cena = double.Parse(item["cena"].ToString());
                            foreach (var kat in item["kategorija"])
                            {
                                kategorija.Id = int.Parse(kat["id"].ToString());
                                kategorija.Naziv = kat["naziv"].ToString();
                                p.Kategorija = kategorija;
                            }
                            foreach (var dob in item["dobavljac"])
                            {
                                dobavljac.Id = int.Parse(dob["id"].ToString());
                                dobavljac.Naziv = dob["naziv"].ToString();
                                p.Dobavljac = dobavljac;
                            }
                            foreach (var pro in item["proizvodjac"])
                            {
                                proizvodjac.Id = int.Parse(pro["id"].ToString());
                                proizvodjac.Naziv = pro["naziv"].ToString();
                                p.Proizvodjac = proizvodjac;
                            }
                            list.Add(p);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return list;
        }
        public Proizvod getProizvodByIdJSON(int id)
        {
            var proizvod = new Proizvod();
            var json = File.ReadAllText(JSONHelper.jsonFile);
            try
            {
                var jObject = JObject.Parse(json);
                if (jObject != null)
                {
                    JArray proizvodArray = (JArray)jObject[JSONHelper.mainJSONObject];
                    if (proizvodArray != null)
                    {
                        foreach (var item in proizvodArray.Where(obj => obj["id"].Value<int>() == id))
                        {
                            if (int.Parse(item["id"].ToString()) == id)
                            {
                                Kategorija kategorija = new Kategorija();
                                var proizvodjac = new Proizvodjac();
                                var dobavljac = new Dobavljac();
                                proizvod.Id = int.Parse(item["id"].ToString());
                                proizvod.Naziv = item["naziv"].ToString();
                                proizvod.Opis = item["opis"].ToString();
                                proizvod.Cena = double.Parse(item["cena"].ToString());
                                foreach (var kat in item["kategorija"])
                                {
                                    kategorija.Id = int.Parse(kat["id"].ToString());
                                    kategorija.Naziv = kat["naziv"].ToString();
                                    proizvod.Kategorija = kategorija;
                                }
                                foreach (var dob in item["dobavljac"])
                                {
                                    dobavljac.Id = int.Parse(dob["id"].ToString());
                                    dobavljac.Naziv = dob["naziv"].ToString();
                                    proizvod.Dobavljac = dobavljac;
                                }
                                foreach (var pro in item["proizvodjac"])
                                {
                                    proizvodjac.Id = int.Parse(pro["id"].ToString());
                                    proizvodjac.Naziv = pro["naziv"].ToString();
                                    proizvod.Proizvodjac = proizvodjac;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return proizvod;
        }
        public bool updateProizvodJSON(Proizvod proizvod)
        {
            var json = File.ReadAllText(JSONHelper.jsonFile);
            try
            {
                var jObject = JObject.Parse(json);
                JArray proizvodiArray = (JArray)jObject[JSONHelper.mainJSONObject];

                foreach (var pro in proizvodiArray.Where(obj => obj["id"].Value<int>() == proizvod.Id))
                {
                    pro["naziv"] = proizvod.Naziv;
                    pro["opis"] = proizvod.Opis;
                    pro["cena"] = proizvod.Cena;
                    JArray kategorije = (JArray)pro["kategorija"];
                    kategorije[0]["id"] = proizvod.Kategorija.Id.ToString();
                    kategorije[0]["naziv"] = proizvod.Kategorija.Naziv.ToString();
                    JArray dobavljaci = (JArray)pro["dobavljac"];
                    dobavljaci[0]["id"] = proizvod.Dobavljac.Id.ToString();
                    dobavljaci[0]["naziv"] = proizvod.Dobavljac.Naziv.ToString();
                    JArray proizvodjaci = (JArray)pro["proizvodjac"];
                    proizvodjaci[0]["id"] = proizvod.Proizvodjac.Id.ToString();
                    proizvodjaci[0]["naziv"] = proizvod.Proizvodjac.Naziv.ToString();
                }
                jObject[JSONHelper.mainJSONObject] = proizvodiArray;
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(JSONHelper.jsonFile, output);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}