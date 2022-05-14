using RestSharp;
using System;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Dynamic;
using Newtonsoft.Json.Converters;
using SateliteMautic.Model;

namespace SateliteMautic
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var client = new RestClient("https://mautic.clinicametco.com/api/contacts");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Basic bWV0Y29hZG1pbjpNNHV0MWNNZXRjMCoq");
            IRestResponse response = client.Execute(request);

            dynamic config = JsonConvert.DeserializeObject<ExpandoObject>(response.Content.ToString(), new ExpandoObjectConverter());

            Contacto cont = new Contacto();
            bool ok;
            string text = "";
            foreach (var enabledEndpoint in config.contacts)
            {
                cont.Id = enabledEndpoint.Value.fields.all.id;
                text = cont.Id;
                if (enabledEndpoint.Value.fields.all.firstname == null) {
                    cont.Nombre = "";
                }
                else
                {
                    cont.Nombre = enabledEndpoint.Value.fields.all.firstname;
                }
                if (enabledEndpoint.Value.fields.all.lastname == null)
                {
                    cont.Apellido = "";
                }
                else
                {
                    cont.Apellido = enabledEndpoint.Value.fields.all.lastname;
                }
                if (enabledEndpoint.Value.fields.all.mobile == null)
                {
                    cont.Celular = "0";
                }
                else
                {
                    cont.Celular = enabledEndpoint.Value.fields.all.mobile;
                }
                if (enabledEndpoint.Value.fields.all.email == null)
                {
                    cont.email = "";
                }
                else
                {
                    cont.email = enabledEndpoint.Value.fields.all.email;
                }
                cont.Integrado = 0;
                cont.FechaRegistro = DateTime.Now;

                ok = cont.mtd_registrar();

                if (ok)
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\logsapi\Integrado\log.txt", true))
                    {
                        file.WriteLine("Integrado: " + text + " " + DateTime.Now.ToString());
                    }
                }
                else
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\logsapi\Error\log.txt", true))
                    {
                        file.WriteLine("Fallo: " + text + " " + DateTime.Now.ToString());
                    }
                }
            }
        }
    }
}

