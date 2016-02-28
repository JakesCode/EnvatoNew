using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;
using RestSharp;
using System.Web;

namespace EnvatoNew
{
    public partial class PhotoDune : Form
    {
        public PhotoDune()
        {
            InitializeComponent();
        }

        public class Rating
        {
            public int rating { get; set; }
            public int count { get; set; }
        }

        public class ThumbnailPreview
        {
            public string small_url { get; set; }
            public string large_url { get; set; }
            public int large_width { get; set; }
            public int large_height { get; set; }
        }

        public class IconWithThumbnailPreview
        {
            public string icon_url { get; set; }
            public string thumbnail_url { get; set; }
            public int thumbnail_width { get; set; }
            public int thumbnail_height { get; set; }
        }

        public class IconWithSquarePreview
        {
            public string icon_url { get; set; }
            public string square_url { get; set; }
        }

        public class Length
        {
            public int hours { get; set; }
            public int minutes { get; set; }
            public int seconds { get; set; }
        }

        public class IconWithAudioPreview
        {
            public string icon_url { get; set; }
            public string mp3_url { get; set; }
            public int mp3_id { get; set; }
            public Length length { get; set; }
        }

        public class Previews
        {
            public ThumbnailPreview thumbnail_preview { get; set; }
            public IconWithThumbnailPreview icon_with_thumbnail_preview { get; set; }
            public IconWithSquarePreview icon_with_square_preview { get; set; }
            public IconWithAudioPreview icon_with_audio_preview { get; set; }
        }

        public class Match
        {
            public int id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public string description_html { get; set; }
            public string site { get; set; }
            public string classification { get; set; }
            public string classification_url { get; set; }
            public int? price_cents { get; set; }
            public int number_of_sales { get; set; }
            public string author_username { get; set; }
            public string author_url { get; set; }
            public string author_image { get; set; }
            public string url { get; set; }
            public string summary { get; set; }
            public Rating rating { get; set; }
            public string updated_at { get; set; }
            public string published_at { get; set; }
            public bool trending { get; set; }
            public Previews previews { get; set; }
            public List<object> attributes { get; set; }
            public List<string> tags { get; set; }
        }

        public class Links
        {
            public string next_page_url { get; set; }
            public object prev_page_url { get; set; }
            public string first_page_url { get; set; }
            public string last_page_url { get; set; }
        }

        public class CategoryRootCount
        {
            public object key { get; set; }
            public int count { get; set; }
            public object description { get; set; }
        }

        public class Category
        {
            public string key { get; set; }
            public int count { get; set; }
            public object description { get; set; }
        }

        public class Tag
        {
            public string key { get; set; }
            public int count { get; set; }
            public object description { get; set; }
        }

        public class Rating2
        {
            public string key { get; set; }
            public int count { get; set; }
            public object description { get; set; }
        }

        public class Date
        {
            public string key { get; set; }
            public int count { get; set; }
            public object description { get; set; }
        }

        public class SalesCount
        {
            public string key { get; set; }
            public int count { get; set; }
            public object description { get; set; }
        }

        public class Cost
        {
            public int count { get; set; }
            public int min { get; set; }
            public int max { get; set; }
            public double avg { get; set; }
            public int sum { get; set; }
        }

        public class Aggregations
        {
            public CategoryRootCount category_root_count { get; set; }
            public List<Category> category { get; set; }
            public object platform_root_count { get; set; }
            public object platform { get; set; }
            public object file_formats { get; set; }
            public List<Tag> tags { get; set; }
            public object colors { get; set; }
            public List<Rating2> rating { get; set; }
            public List<Date> date { get; set; }
            public object size { get; set; }
            public List<SalesCount> sales_count { get; set; }
            public Cost cost { get; set; }
            public object length { get; set; }
            public object tempo { get; set; }
            public object alpha { get; set; }
            public object looped { get; set; }
            public object resolution { get; set; }
            public object vocals_in_audio { get; set; }
            public object frame_rate { get; set; }
            public object compatible_with { get; set; }
        }

        public class RootObject
        {
            public int took { get; set; }
            public List<Match> matches { get; set; }
            public bool timed_out { get; set; }
            public int total_hits { get; set; }
            public Links links { get; set; }
            public object author_exists { get; set; }
            public Aggregations aggregations { get; set; }
        }

        private void PhotoDune_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public string access_token { get; set; }
        private async void button1_Click(object sender, EventArgs e)
        {
            string longurl = "https://api.envato.com/v1/discovery/search/search/item";
            Dictionary<string, string> parameters_1 = new Dictionary<string, string>
            {
                {"site", "photodune.net"}
            };

            // End of populating parameters dictionary //

            var uriBuilder = new UriBuilder(longurl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            uriBuilder.Query = query.ToString();
            longurl = uriBuilder.ToString();

            var RESPONSE = "";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + access_token);
                RESPONSE = await client.GetStringAsync(longurl);
            }

            
            MessageBox.Show(Newtonsoft.Json.Linq.JObject.Parse(RESPONSE).First.ToString());
        }
    }
}
