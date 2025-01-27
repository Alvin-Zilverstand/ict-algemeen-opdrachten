using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

// Class representing a bank account
class Bankrekening
{
    public string Rekeningnummer { get; }
    private decimal saldo;
    private List<Transactie> transacties;

    // Constructor to initialize account number and starting balance
    public Bankrekening(string rekeningnummer, decimal beginsaldo)
    {
        Rekeningnummer = rekeningnummer;
        saldo = beginsaldo;
        transacties = new List<Transactie>();
    }

    // Method to deposit money into the account with a description
    public void Storten(decimal bedrag, string beschrijving)
    {
        if (bedrag > 0)
        {
            saldo += bedrag;
            transacties.Add(new Transactie(bedrag, beschrijving));
        }
        else
        {
            throw new ArgumentException("The amount must be positive.");
        }
    }

    // Method to withdraw money from the account with a description
    public void Opnemen(decimal bedrag, string beschrijving)
    {
        if (bedrag > 0 && bedrag <= saldo)
        {
            saldo -= bedrag;
            transacties.Add(new Transactie(-bedrag, beschrijving));
        }
        else
        {
            throw new ArgumentException("Insufficient balance or invalid amount.");
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

// Class representing a transaction
class Transactie
{
    public decimal Bedrag { get; }
    public string Beschrijving { get; }
    public DateTime Datum { get; }

    // Constructor to initialize transaction details
    public Transactie(decimal bedrag, string beschrijving)
    {
        Bedrag = bedrag;
        Beschrijving = beschrijving;
        Datum = DateTime.Now;
    }
}

// Main form for the application
public class MainForm : Form
{
    private Bankrekening mijnRekening;
    private Label saldoLabel;
    private TextBox bedragTextBox;
    private TextBox beschrijvingTextBox;
    private Button stortenButton;
    private Button opnemenButton;
    private Button transactieGeschiedenisButton;

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
            BorderStyle = BorderStyle.FixedSingle,
            PlaceholderText = "Amount"
        };
        beschrijvingTextBox = new TextBox() 
        { 
            Top = 100, 
            Left = 20, 
            Width = 260,
            Font = new Font("Arial", 14),
            BackColor = Color.White,
            ForeColor = Color.Black,
            BorderStyle = BorderStyle.FixedSingle,
            PlaceholderText = "Description"
        };
        stortenButton = new Button() 
        { 
            Text = "Deposit", 
            Top = 140, 
            Left = 20,
            Width = 260,
            Height = 50,
            Font = new Font("Arial", 14),
            BackColor = Color.FromArgb(0, 123, 255),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        opnemenButton = new Button() 
        { 
            Text = "Withdraw", 
            Top = 200, 
            Left = 20,
            Width = 260,
            Height = 50,
            BackColor = Color.FromArgb(0, 123, 255),
            Font = new Font("Arial", 14, FontStyle.Bold),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        transactieGeschiedenisButton = new Button() 
        { 
            Text = "Transaction History", 
            Top = 260, 
            Left = 20,
            Width = 260,
            Height = 50,
            Font = new Font("Arial", 14),
            BackColor = Color.FromArgb(108, 117, 125),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };

        stortenButton.Click += StortenButton_Click;
        opnemenButton.Click += OpnemenButton_Click;
        transactieGeschiedenisButton.Click += TransactieGeschiedenisButton_Click;

        Controls.Add(saldoLabel);
        Controls.Add(bedragTextBox);
        Controls.Add(beschrijvingTextBox);
        Controls.Add(stortenButton);
        Controls.Add(opnemenButton);
        Controls.Add(transactieGeschiedenisButton);

        Text = "Bank Account Management";
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
            string beschrijving = beschrijvingTextBox.Text;
            mijnRekening.Storten(bedrag, beschrijving);
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
            string beschrijving = beschrijvingTextBox.Text;
            mijnRekening.Opnemen(bedrag, beschrijving);
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
        string geschiedenis = "Transaction History:\n";
        geschiedenis += "Date\t\tDescription\t\tAmount\n";
        geschiedenis += "---------------------------------------------\n";
        foreach (var transactie in transactieGeschiedenis)
        {
            geschiedenis += $"{transactie.Datum}\t{transactie.Beschrijving}\t\t{transactie.Bedrag:C}\n";
        }
        MessageBox.Show(geschiedenis);
    }
}

// Main program class
class Program
{
    static void Main()
    {
        var mijnRekening = new Bankrekening("NL01BANK0123456789", 1000);
        mijnRekening.Storten(100.00m, "Initial deposit");
        mijnRekening.Opnemen(50.00m, "ATM withdrawal");

        Console.WriteLine("Transaction History:");
        foreach (var transactie in mijnRekening.GetTransactieGeschiedenis())
        {
            Console.WriteLine($"{transactie.Datum}: {transactie.Beschrijving} - {transactie.Bedrag:C}");
        }

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MainForm());
    }
}
