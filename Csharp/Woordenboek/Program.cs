using System;
using Microsoft.Data.SqlClient; // Update this line
using System.Windows.Forms;
using System.Drawing;

namespace Woordenboek
{
    public class Program : Form
    {
        private TextBox wordTextBox;
        private Button searchButton;
        private Label meaningLabel;

        public Program()
        {
            wordTextBox = new TextBox { Left = 50, Top = 20, Width = 200 };
            searchButton = new Button { Text = "Zoek", Left = 260, Top = 20, Width = 100 };
            meaningLabel = new Label { Left = 50, Top = 60, Width = 310 };

            searchButton.Click += new EventHandler(SearchButton_Click);

            Controls.Add(wordTextBox);
            Controls.Add(searchButton);
            Controls.Add(meaningLabel);

            Text = "Woordenboek";
            Size = new System.Drawing.Size(400, 150);
        }

        private void SearchButton_Click(object? sender, EventArgs e)
        {
            string word = wordTextBox.Text;
            string? meaning = GetMeaningFromDatabase(word);
            meaningLabel.Text = meaning ?? "Betekenis niet gevonden.";
        }

        private string? GetMeaningFromDatabase(string word)
        {
            string connectionString = "Server=your_server_name;Database=your_database_name;User Id=your_username;Password=your_password;";
            string query = "SELECT Meaning FROM Dictionary WHERE Word = @word";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@word", word);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return reader["Meaning"].ToString();
                }
                else
                {
                    return null;
                }
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
