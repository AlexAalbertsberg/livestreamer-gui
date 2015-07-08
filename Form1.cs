using System;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace LivestreamerGUI
{
    public partial class Form1 : Form
    {
        public static string BASE_URL = "https://api.twitch.tv/kraken/streams?game=League+of+Legends";

        public JArray streams;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(BASE_URL).Result;
            if(response.IsSuccessStatusCode)
            {
                string dataObjects = response.Content.ReadAsStringAsync().Result;

                var results = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(dataObjects);

                streams = results.streams;

                foreach(JObject stream in streams)
                {
                    var channel = stream["channel"];
                    var name = channel["name"];

                    listBox1.Items.Add(name);
                }
                
            }
            else
            {
                string jsonError = response.Content.ReadAsStringAsync().Result;
                MessageBox.Show(jsonError);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
