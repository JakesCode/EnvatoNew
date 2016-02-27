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
using Newtonsoft.Json;
using System.IO;

namespace EnvatoNew
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            webBrowser1.Url = new Uri(@"https://api.envato.com/authorization?response_type=code&client_id=nvatomate-l23dgtds&redirect_uri=http://market.envato.com/");
        }

        class envatoRefreshResponse
        {
            public string refresh_token { get; set; }
            public string token_type { get; set; }
            public string access_token { get; set; }
            public string expires_in { get; set; }
        }

        private async void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.Url.ToString().Substring(0, 13) == "http://market")
            {
                string returnedEnvatoParam = HttpUtility.ParseQueryString(webBrowser1.Url.Query).Get("code");
                
                //  Get Envato to give us a refresh token //
                using (var client = new HttpClient())
                {
                    var values = new Dictionary<string, string>
                {
                    { "grant_type", "authorization_code" },
                    { "code", returnedEnvatoParam },
                    { "client_id", "nvatomate-l23dgtds" },
                    { "client_secret", "C9ayZqeoWLxbV5zQmhNlgS457WbbQiY2" }
                };
                    var content = new FormUrlEncodedContent(values);
                    var response = await client.PostAsync("https://api.envato.com/token", content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    
                    // Deserialize the JSON response //
                    var JSONOutput = JsonConvert.DeserializeObject<envatoRefreshResponse>(responseString);
                    File.WriteAllText(@"envatoResponse.nmate", JSONOutput.refresh_token);
                }

                this.Dispose();
            }
        }
    }
}
