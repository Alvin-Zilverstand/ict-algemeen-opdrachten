using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

#region Bankrekening Class
// Class representing a bank account
class Bankrekening
{
    public string Rekeningnummer { get; } // Account number
    private decimal saldo; // Account balance
    private List<Transactie> transacties; // List of transactions

    // Constructor to initialize account number and starting balance
    public Bankrekening(string rekeningnummer, decimal beginsaldo)
    {
        Rekeningnummer = rekeningnummer;
        saldo = beginsaldo;
        transacties = new List<Transactie>();
    }

    // Method to deposit money into the account
    public void Storten(decimal bedrag)
    {
        if (bedrag > 0)
        {
            saldo += bedrag;
            transacties.Add(new Transactie(bedrag, "Storting"));
        }
        else
        {
            throw new ArgumentException("Het bedrag moet positief zijn.");
        }
    }

    // Method to withdraw money from the account
    public void Opnemen(decimal bedrag)
    {
        if (bedrag > 0 && bedrag <= saldo)
        {
            saldo -= bedrag;
            transacties.Add(new Transactie(-bedrag, "Opname"));
        }
        else
        {
            throw new ArgumentException("Onvoldoende saldo of ongeldig bedrag.");
        }
    }

    // Method to check the current balance
    public decimal ControleerSaldo()
    {
        return saldo;
    }

    // Method to get the transaction history
    public List<Transactie> GetTransactieGeschiedenis()
    {
        return transacties;
    }
}
#endregion

#region Transactie Class
// Class representing a transaction
class Transactie
{
    public decimal Bedrag { get; } // Transaction amount
    public string Beschrijving { get; } // Transaction description
    public DateTime Datum { get; } // Transaction date

    // Constructor to initialize transaction details
    public Transactie(decimal bedrag, string beschrijving)
    {
        Bedrag = bedrag;
        Beschrijving = beschrijving;
        Datum = DateTime.Now;
    }
}
#endregion

#region MainForm Class
// Main form for the application
public class MainForm : Form
{
    private Bankrekening mijnRekening; // Bank account instance
    private Label saldoLabel; // Label to display balance
    private TextBox bedragTextBox; // TextBox to input amount
    private Button stortenButton; // Button to deposit money
    private Button opnemenButton; // Button to withdraw money
    private Button transactieGeschiedenisButton; // Button to view transaction history

    // Constructor to initialize the form and its controls
    public MainForm()
    {
        mijnRekening = new Bankrekening("NL01BANK0123456789", 1000);

        saldoLabel = new Label() 
        { 
            Text = $"Saldo: €{mijnRekening.ControleerSaldo():N2}", 
            Top = 20, 
            Left = 20, 
            Width = 260,
            Font = new Font("Arial", 16, FontStyle.Bold),
            ForeColor = Color.White,
            BackColor = Color.FromArgb(0, 123, 255),
            TextAlign = ContentAlignment.MiddleCenter
        };
        bedragTextBox = new TextBox() 
        { 
            Top = 60, 
            Left = 20, 
            Width = 260,
            Font = new Font("Arial", 14),
            BackColor = Color.White,
            ForeColor = Color.Black,
            BorderStyle = BorderStyle.FixedSingle
        };
        stortenButton = new Button() 
        { 
            Text = "Storten", 
            Top = 100, 
            Left = 20,
            Width = 260,
            Height = 50, // Adjusted height
            Font = new Font("Arial", 14),
            BackColor = Color.FromArgb(0, 123, 255),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        opnemenButton = new Button() 
        { 
            Text = "Opnemen", 
            Top = 160, 
            Left = 20,
            Width = 260,
            Height = 50, // Adjusted height
            BackColor = Color.FromArgb(0, 123, 255), // Lighter blue
            Font = new Font("Arial", 14, FontStyle.Bold), // Bold and larger font
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        transactieGeschiedenisButton = new Button() 
        { 
            Text = "Transactiegeschiedenis", 
            Top = 220, 
            Left = 20,
            Width = 260,
            Height = 50, // Adjusted height
            Font = new Font("Arial", 14),
            BackColor = Color.FromArgb(108, 117, 125),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };

        // Event handlers for button clicks
        stortenButton.Click += StortenButton_Click;
        opnemenButton.Click += OpnemenButton_Click;
        transactieGeschiedenisButton.Click += TransactieGeschiedenisButton_Click;

        // Add controls to the form
        Controls.Add(saldoLabel);
        Controls.Add(bedragTextBox);
        Controls.Add(stortenButton);
        Controls.Add(opnemenButton);
        Controls.Add(transactieGeschiedenisButton);

        Text = "Bankrekening Beheer";
        Size = new Size(320, 380);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.White;
    }

    // Event handler for deposit button click
    private void StortenButton_Click(object sender, EventArgs e)
    {
        try
        {
            decimal bedrag = decimal.Parse(bedragTextBox.Text);
            mijnRekening.Storten(bedrag);
            saldoLabel.Text = $"Saldo: €{mijnRekening.ControleerSaldo():N2}";
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    // Event handler for withdraw button click
    private void OpnemenButton_Click(object sender, EventArgs e)
    {
        try
        {
            decimal bedrag = decimal.Parse(bedragTextBox.Text);
            mijnRekening.Opnemen(bedrag);
            saldoLabel.Text = $"Saldo: €{mijnRekening.ControleerSaldo():N2}";
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    // Event handler for transaction history button click
    private void TransactieGeschiedenisButton_Click(object sender, EventArgs e)
    {
        var transactieGeschiedenis = mijnRekening.GetTransactieGeschiedenis();
        string geschiedenis = "Transactiegeschiedenis:\n";
        geschiedenis += "Datum\t\tBeschrijving\t\tBedrag\n";
        geschiedenis += "---------------------------------------------\n";
        foreach (var transactie in transactieGeschiedenis)
        {
            geschiedenis += $"{transactie.Datum}\t{transactie.Beschrijving}\t\t{transactie.Bedrag:C}\n";
        }
        MessageBox.Show(geschiedenis);
    }
}
#endregion

#region Program Class
// Main program class
class Program
{
    static void Main()
    {
        var mijnRekening = new Bankrekening("NL01BANK0123456789", 1000);
        mijnRekening.Storten(100.00m);
        mijnRekening.Opnemen(50.00m);

        Console.WriteLine("Transactiegeschiedenis:");
        foreach (var transactie in mijnRekening.GetTransactieGeschiedenis())
        {
            Console.WriteLine($"{transactie.Datum}: {transactie.Beschrijving} - {transactie.Bedrag:C}");
        }

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MainForm());
    }
}
#endregion
