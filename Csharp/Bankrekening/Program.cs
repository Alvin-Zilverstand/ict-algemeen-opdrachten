using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

class Bankrekening
{
    public string Rekeningnummer { get; }
    private decimal saldo;
    private List<Transactie> transacties;

    public Bankrekening(string rekeningnummer, decimal beginsaldo)
    {
        // Initialiseer rekeningnummer en beginsaldo
        Rekeningnummer = rekeningnummer;
        saldo = beginsaldo;
        transacties = new List<Transactie>();
    }

    public void Storten(decimal bedrag, string beschrijving)
    {
        // Stort geld op de rekening met een beschrijving
        if (bedrag > 0)
        {
            saldo += bedrag;
            transacties.Add(new Transactie(bedrag, beschrijving));
        }
        else
        {
            throw new ArgumentException("Het bedrag moet positief zijn.");
        }
    }

    public void Opnemen(decimal bedrag, string beschrijving)
    {
        // Neem geld op van de rekening met een beschrijving
        if (bedrag > 0 && bedrag <= saldo)
        {
            saldo -= bedrag;
            transacties.Add(new Transactie(-bedrag, beschrijving));
        }
        else
        {
            throw new ArgumentException("Onvoldoende saldo of ongeldig bedrag.");
        }
    }

    public decimal ControleerSaldo()
    {
        // Controleer het huidige saldo
        return saldo;
    }

    public List<Transactie> GetTransactieGeschiedenis()
    {
        // Haal de transactiegeschiedenis op
        return transacties;
    }
}

class Transactie
{
    public decimal Bedrag { get; }
    public string Beschrijving { get; }
    public DateTime Datum { get; }

    public Transactie(decimal bedrag, string beschrijving)
    {
        // Initialiseer transactiegegevens
        Bedrag = bedrag;
        Beschrijving = beschrijving;
        Datum = DateTime.Now;
    }
}

public class MainForm : Form
{
    private Bankrekening mijnRekening;
    private Label saldoLabel;
    private TextBox bedragTextBox;
    private TextBox beschrijvingTextBox;
    private Button stortenButton;
    private Button opnemenButton;
    private Button transactieGeschiedenisButton;

    public MainForm()
    {
        // Initialiseer de GUI-componenten
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
            PlaceholderText = "Bedrag"
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
            PlaceholderText = "Beschrijving"
        };
        stortenButton = new Button() 
        { 
            Text = "Storten", 
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
            Text = "Opnemen", 
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
            Text = "Transactiegeschiedenis", 
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

        Text = "Bankrekening Beheer";
        Size = new Size(320, 380);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.White;
    }

    private void StortenButton_Click(object sender, EventArgs e)
    {
        // Verwerk de storting
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

    private void OpnemenButton_Click(object sender, EventArgs e)
    {
        // Verwerk de opname
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

    private void TransactieGeschiedenisButton_Click(object sender, EventArgs e)
    {
        // Toon de transactiegeschiedenis
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

class Program
{
    static void Main()
    {
        // Start de applicatie
        var mijnRekening = new Bankrekening("NL01BANK0123456789", 1000);
        mijnRekening.Storten(100.00m, "Initial deposit");
        mijnRekening.Opnemen(50.00m, "ATM withdrawal");

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
