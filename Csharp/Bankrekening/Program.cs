using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

#region Bankrekening Class
class Bankrekening
{
    public string Rekeningnummer { get; }
    private decimal saldo;
    private List<Transactie> transacties;

    public Bankrekening(string rekeningnummer, decimal beginsaldo)
    {
        Rekeningnummer = rekeningnummer;
        saldo = beginsaldo;
        transacties = new List<Transactie>();
    }

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

    public decimal ControleerSaldo()
    {
        return saldo;
    }

    public List<Transactie> GetTransactieGeschiedenis()
    {
        return transacties;
    }
}
#endregion

#region Transactie Class
class Transactie
{
    public decimal Bedrag { get; }
    public string Beschrijving { get; }
    public DateTime Datum { get; }

    public Transactie(decimal bedrag, string beschrijving)
    {
        Bedrag = bedrag;
        Beschrijving = beschrijving;
        Datum = DateTime.Now;
    }
}
#endregion

#region MainForm Class
public class MainForm : Form
{
    private Bankrekening mijnRekening;
    private Label saldoLabel;
    private TextBox bedragTextBox;
    private Button stortenButton;
    private Button opnemenButton;
    private Button transactieGeschiedenisButton;

    public MainForm()
    {
        mijnRekening = new Bankrekening("NL01BANK0123456789", 1000);

        saldoLabel = new Label() 
        { 
            Text = $"Saldo: €{mijnRekening.ControleerSaldo():N2}", 
            Top = 20, 
            Left = 20, 
            Width = 240,
            Font = new Font("Arial", 16, FontStyle.Bold),
            ForeColor = Color.White,
            BackColor = Color.FromArgb(0, 123, 255),
            TextAlign = ContentAlignment.MiddleCenter
        };
        bedragTextBox = new TextBox() 
        { 
            Top = 60, 
            Left = 20, 
            Width = 240,
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
            Width = 240,
            Font = new Font("Arial", 14),
            BackColor = Color.FromArgb(0, 123, 255),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        opnemenButton = new Button() 
        { 
            Text = "Opnemen", 
            Top = 140, 
            Left = 20,
            Width = 240,
            Font = new Font("Arial", 14),
            BackColor = Color.FromArgb(220, 53, 69),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        transactieGeschiedenisButton = new Button() 
        { 
            Text = "Transactiegeschiedenis", 
            Top = 180, 
            Left = 20,
            Width = 240,
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
        Controls.Add(stortenButton);
        Controls.Add(opnemenButton);
        Controls.Add(transactieGeschiedenisButton);

        Text = "Bankrekening Beheer";
        Size = new Size(300, 300);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.White;
    }

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
