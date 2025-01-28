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
            // Initialiseer de GUI-componenten
            wordTextBox = new TextBox { Left = 50, Top = 20, Width = 200 };
            searchButton = new Button { Text = "Zoek", Left = 260, Top = 20, Width = 100 };
            meaningTextBox = new TextBox { Left = 50, Top = 60, Width = 310, Height = 200, Multiline = true, ScrollBars = ScrollBars.Vertical, ReadOnly = true };

            searchButton.Click += new EventHandler(SearchButton_Click);
            wordTextBox.KeyDown += new KeyEventHandler(WordTextBox_KeyDown);
            this.Resize += new EventHandler(Form_Resize);

            Controls.Add(wordTextBox);
            Controls.Add(searchButton);
            Controls.Add(meaningTextBox);

            Text = "Woordenboek";
            Size = new System.Drawing.Size(400, 300);
        }

        private void Form_Resize(object? sender, EventArgs e)
        {
            // Pas de grootte van het tekstvak aan bij het wijzigen van de grootte van het formulier
            meaningTextBox.Width = this.ClientSize.Width - 100;
            meaningTextBox.Height = this.ClientSize.Height - 100;
        }

        private void WordTextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            // Zoek bij het indrukken van de Enter-toets
            if (e.KeyCode == Keys.Enter)
            {
                SearchButton_Click(this, new EventArgs());
                e.SuppressKeyPress = true; // Voorkom het piepgeluid bij het indrukken van de Enter-toets
            }
        }

        private async void SearchButton_Click(object? sender, EventArgs e)
        {
            // Haal de betekenis van het woord op van de API
            string word = wordTextBox.Text;
            string? meaning = await GetMeaningFromApi(word);
            meaningTextBox.Text = meaning ?? "Betekenis niet gevonden.";
        }

        private async Task<string?> GetMeaningFromApi(string word)
        {
            try
            {
                // Maak een verzoek naar de woordenboek-API
                string apiUrl = $"https://api.dictionaryapi.dev/api/v2/entries/en/{word}";
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Parse de JSON-respons om de betekenis te extraheren
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
            // Start de applicatie
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Program());
        }
    }
}
