using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;

namespace EnvatoNew
{
    public partial class Form1 : Form
    {
        public class envatoInteraction
        {
            public string refresh_token { get; set; }
            public string access_token { get; set; }
        }

        class refreshResponse
        {
            public string token_type { get; set; }
            public string access_token { get; set; }
            public string expires_in { get; set; }
        }

        public class Account
        {
            public string image { get; set; }
            public string firstname { get; set; }
            public string surname { get; set; }
            public string available_earnings { get; set; }
            public string total_deposits { get; set; }
            public string balance { get; set; }
            public string country { get; set; }
        }
        public class AccountRoot
        {
            public Account account { get; set; }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public envatoInteraction envato = new envatoInteraction();
        private async void Form1_Load(object sender, EventArgs e)
        {
            if(File.ReadAllText(@"envatoResponse.nmate") == "")
            {
                Login loginScreen = new Login();
                loginScreen.ShowDialog();
            }
            // Logged in, got an access token, done! //
            string envatoResponse = File.ReadAllText(@"envatoResponse.nmate");
            envato.refresh_token = envatoResponse;
            // Refresh the access token with the refresh token every time the app starts up //
            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    { "grant_type", "refresh_token" },
                    { "refresh_token", envato.refresh_token },
                    { "client_id", "nvatomate-l23dgtds" },
                    { "client_secret", "C9ayZqeoWLxbV5zQmhNlgS457WbbQiY2" }
                };
                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync("https://api.envato.com/token", content);
                var responseString = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON response //
                var JSONOutput = JsonConvert.DeserializeObject<refreshResponse>(responseString);
                envato.access_token = JSONOutput.access_token;
                
                // Make some calls to populate the UI (with some user info e.g. icon / username) //
                populateUIWithUserInfo();
            }
        }

        public AccountRoot account = new AccountRoot();
        public void populateUIWithUserInfo()
        {
            using (var client = new HttpClient())
            {
                // TODO: Make a GET request and turn into AccountRoot - can't seem to feckin do it //
            }
            // Populate the UI with User Info e.g. username / icon //
            UI_USERNAME.Text = "Welcome back, " + account.account.firstname + ".";
        }
    }
}
