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
        private ListBox meaningListBox;
        private static readonly HttpClient client = new HttpClient();

        public Program()
        {
            wordTextBox = new TextBox { Left = 50, Top = 20, Width = 200 };
            searchButton = new Button { Text = "Search", Left = 260, Top = 20, Width = 100 };
            meaningListBox = new ListBox { Left = 50, Top = 60, Width = 310, Height = 200 };

            searchButton.Click += new EventHandler(SearchButton_Click);
            wordTextBox.KeyDown += new KeyEventHandler(WordTextBox_KeyDown);

            Controls.Add(wordTextBox);
            Controls.Add(searchButton);
            Controls.Add(meaningListBox);

            Text = "Dictionary";
            Size = new System.Drawing.Size(400, 300);
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
            meaningListBox.Items.Clear();
            meaningListBox.Items.Add(meaning ?? "Meaning not found.");
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
