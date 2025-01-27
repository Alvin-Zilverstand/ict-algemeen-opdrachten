using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace DictionaryApp
{
    public class Program : Form
    {
        private TextBox wordTextBox;
        private Button searchButton;
        private TextBox meaningTextBox;
        private static readonly HttpClient client = new HttpClient();

        public Program()
        {
            wordTextBox = new TextBox { Left = 50, Top = 20, Width = 200 };
            searchButton = new Button { Text = "Search", Left = 260, Top = 20, Width = 100 };
            meaningTextBox = new TextBox { Left = 50, Top = 60, Width = 310, Height = 200, Multiline = true, ScrollBars = ScrollBars.Vertical, ReadOnly = true };

            searchButton.Click += new EventHandler(SearchButton_Click);
            wordTextBox.KeyDown += new KeyEventHandler(WordTextBox_KeyDown);
            this.Resize += new EventHandler(Form_Resize);

            Controls.Add(wordTextBox);
            Controls.Add(searchButton);
            Controls.Add(meaningTextBox);

            Text = "Dictionary";
            Size = new System.Drawing.Size(400, 300);
        }

        private void Form_Resize(object? sender, EventArgs e)
        {
            meaningTextBox.Width = this.ClientSize.Width - 100;
            meaningTextBox.Height = this.ClientSize.Height - 100;
        }

        private void WordTextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchButton_Click(this, new EventArgs());
                e.SuppressKeyPress = true; // Prevent the beep sound on Enter key press
            }
        }

        private async void SearchButton_Click(object? sender, EventArgs e)
        {
            string word = wordTextBox.Text;
            string? meaning = await GetMeaningFromApi(word);
            meaningTextBox.Text = meaning ?? "Meaning not found.";
        }

        private async Task<string?> GetMeaningFromApi(string word)
        {
            try
            {
                string apiUrl = $"https://api.dictionaryapi.dev/api/v2/entries/en/{word}";
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Parse the responseBody to extract the meaning
                // This is a simplified example, you may need to adjust the parsing based on the actual API response format
                var json = System.Text.Json.JsonDocument.Parse(responseBody);
                var meaning = json.RootElement[0].GetProperty("meanings")[0].GetProperty("definitions")[0].GetProperty("definition").GetString();
                return meaning;
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Program());
        }
    }
}
