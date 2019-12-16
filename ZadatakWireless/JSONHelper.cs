using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using Newtonsoft.Json.Linq;
using ZadatakWireless.Models;

namespace ZadatakWireless
{
    public class JSONHelper
    {
        public static string jsonFile = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["JSON_Source_path"]);

        /// <summary>
        /// Vraca id iz JSON-a /Poslednji + 1
        /// Radi dodavanja novog zapisa u JSON
        /// </summary>
        /// <returns>Poslednji id proizvoda + 1</returns>
        public static string mainJSONObject = "proizvodi";
        public static int returnNextId()
        {
            var j = File.ReadAllText(jsonFile);
            var jObject = JObject.Parse(j);
            JArray proizvodArray = (JArray)jObject["proizvodi"];
            return proizvodArray.Count + 1;
        }

        #region comboboxDataSource
        public List<Kategorija> getKategorije()
        {
            var list = new List<Kategorija>();

            var json = File.ReadAllText(jsonFile);
            try
            {
                var jObject = JObject.Parse(json);

                if (jObject != null)
                {
                    JArray experiencesArrary = (JArray)jObject["kategorije"];
                    if (experiencesArrary != null)
                    {
                        foreach (var item in experiencesArrary)
                        {
                            Kategorija kategorija = new Kategorija();
                            kategorija.Id = int.Parse(item["id"].ToString());
                            kategorija.Naziv = item["naziv"].ToString();
                            list.Add(kategorija);
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
        public List<Dobavljac> getDobavljace()
        {
            var list = new List<Dobavljac>();

            var json = File.ReadAllText(jsonFile);
            try
            {
                var jObject = JObject.Parse(json);
                if (jObject != null)
                {
                    JArray experiencesArrary = (JArray)jObject["dobavljac"];
                    if (experiencesArrary != null)
                    {
                        foreach (var item in experiencesArrary)
                        {
                            var dobavljac = new Dobavljac();
                            dobavljac.Id = int.Parse(item["id"].ToString());
                            dobavljac.Naziv = item["naziv"].ToString();
                            list.Add(dobavljac);
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
        public List<Proizvodjac> getProizvodjac()
        {
            var list = new List<Proizvodjac>();

            var json = File.ReadAllText(jsonFile);
            try
            {
                var jObject = JObject.Parse(json);
                if (jObject != null)
                {
                    JArray experiencesArrary = (JArray)jObject["proizvodjac"];
                    if (experiencesArrary != null)
                    {
                        foreach (var item in experiencesArrary)
                        {
                            var proizvodjac = new Proizvodjac();
                            proizvodjac.Id = int.Parse(item["id"].ToString());
                            proizvodjac.Naziv = item["naziv"].ToString();
                            list.Add(proizvodjac);
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
        #endregion

    }
}