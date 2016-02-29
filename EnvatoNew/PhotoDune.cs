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
            public float rating { get; set; }
            public float count { get; set; }
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
            public float min { get; set; }
            public float max { get; set; }
            public double avg { get; set; }
            public float sum { get; set; }
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
        public Match single_response { get; set; }
        public int page = 9;
        public int start = 0;
        public bool end = false;
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                picturepanel.Controls.Clear();
            }
            catch
            {

            }

            Dictionary<string, string> allInputs = new Dictionary<string, string>();

            allInputs.Add("colors", UI_COLS.Text);
            allInputs.Add("date", UI_DATE.SelectedText.Replace(" ", "-").ToLower());
            allInputs.Add("price_max", UI_PRICE_HIGH.Text);
            allInputs.Add("price_min", UI_PRICE_LOW.Text);
            allInputs.Add("term", UI_SEARCH.Text);
            allInputs.Add("size", UI_SIZE.SelectedText.ToLower());
            allInputs.Add("tags", UI_TAGS.Text);
            allInputs.Add("username", UI_USER.Text);

            string longurl = "https://api.envato.com/v1/discovery/search/search/item";
            Dictionary<string, string> parameters_1 = new Dictionary<string, string>
            {
                {"site", "photodune.net"}
            };

            foreach(KeyValuePair<string, string> value in allInputs)
            {
                if(!(value.Value == ""))
                {
                    parameters_1.Add(value.Key, value.Value);
                }
            }

            // End of populating parameters dictionary //

            var uriBuilder = new UriBuilder(longurl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            foreach (KeyValuePair<string, string> entry in parameters_1)
            {
                query[entry.Key] = entry.Value;
            }
            uriBuilder.Query = query.ToString();
            longurl = uriBuilder.ToString();

            var RESPONSE = "";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + access_token);
                RESPONSE = await client.GetStringAsync(longurl);
            }

            RootObject envato = JsonConvert.DeserializeObject<RootObject>(RESPONSE);

            List<PictureBox> thumbnailPreviews = new List<PictureBox>();
            int count = 1;
            for (int i = start; i < page; i++)
            {
                // Make a new PictureBox //

                PictureBox thumbnail = new PictureBox();
                thumbnail.Width = 70;
                thumbnail.Height = 70;
                if (count == 1)
                {
                    thumbnail.Left = 0;
                    thumbnail.Top = 0;
                }
                if (count == 2)
                {
                    thumbnail.Left = 80;
                    thumbnail.Top = 0;
                }
                if (count == 3)
                {
                    thumbnail.Left = 80+80;
                    thumbnail.Top = 0;
                }
                if (count == 4)
                {
                    thumbnail.Left = 0;
                    thumbnail.Top = 80;
                }
                if (count == 5)
                {
                    thumbnail.Left = 80;
                    thumbnail.Top = 80;
                }
                if (count == 6)
                {
                    thumbnail.Left = 80+80;
                    thumbnail.Top = 80;
                }

                if (count == 7)
                {
                    thumbnail.Left = 0;
                    thumbnail.Top = 80+80;
                }
                if (count == 8)
                {
                    thumbnail.Left = 80;
                    thumbnail.Top = 80+80;
                }
                if (count == 9)
                {
                    thumbnail.Left = 80 + 80;
                    thumbnail.Top = 80+80;
                }

                try
                {
                    thumbnail.ErrorImage = System.Drawing.Image.FromFile(@"Assets\none.png");
                    thumbnail.ImageLocation = envato.matches[i].previews.thumbnail_preview.small_url;
                    thumbnail.Tag = JsonConvert.SerializeObject(envato.matches[i]);
                    thumbnail.Click += (s, es) => {
                        Match singleMatch = JsonConvert.DeserializeObject<Match>(thumbnail.Tag.ToString());
                        single_response = singleMatch;
                        pictureBox2.ImageLocation = singleMatch.previews.thumbnail_preview.large_url;
                        name.Text = singleMatch.description;
                        result_name.Text = singleMatch.author_username;
                        result_name_image.ImageLocation = singleMatch.author_image;
                        result_name_image.Click += (p, ps) => { EnvatoExplorer envato_explorer_window = new EnvatoExplorer(); envato_explorer_window.envato_explorer.Url = new Uri(singleMatch.author_url); envato_explorer_window.ShowDialog(); };
                        label8.Text = singleMatch.number_of_sales.ToString() + " Sales";
                    };
                } catch
                {
                    thumbnail.ImageLocation = @"none.png";
                    end = true;
                }
                

                picturepanel.Controls.Add(thumbnail);
                thumbnailPreviews.Add(thumbnail);

                count++;
            }
        }

        private void Thumbnail_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            EnvatoExplorer envato_explorer_window = new EnvatoExplorer(); envato_explorer_window.envato_explorer.Url = new Uri(single_response.url); envato_explorer_window.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EnvatoExplorer envato_explorer_window = new EnvatoExplorer(); envato_explorer_window.envato_explorer.Url = new Uri("http://photodune.net/cart"); envato_explorer_window.ShowDialog();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pictureBox5.Visible = true;
            start -= 9;
            page -= 9;
            button1_Click(null, null);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if(!end)
            {
                pictureBox4.Visible = true;
                start += 9;
                page += 9;
                button1_Click(null, null);
            } else
            {
                pictureBox5.Visible = false;
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox6_Click_1(object sender, EventArgs e)
        {
            pictureBox5.Visible = true;
            pictureBox4.Visible = false;
            start = 0;
            page = 9;
            button1_Click(null, null);
        }
    }
}
