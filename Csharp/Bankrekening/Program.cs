using System;
using System.Collections.Generic;
using System.Windows.Forms;

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

        saldoLabel = new Label() { Text = $"Saldo: {mijnRekening.ControleerSaldo()}", Top = 20, Left = 20, Width = 200 };
        bedragTextBox = new TextBox() { Top = 50, Left = 20, Width = 200 };
        stortenButton = new Button() { Text = "Storten", Top = 80, Left = 20 };
        opnemenButton = new Button() { Text = "Opnemen", Top = 110, Left = 20 };
        transactieGeschiedenisButton = new Button() { Text = "Transactiegeschiedenis", Top = 140, Left = 20 };

        stortenButton.Click += StortenButton_Click;
        opnemenButton.Click += OpnemenButton_Click;
        transactieGeschiedenisButton.Click += TransactieGeschiedenisButton_Click;

        Controls.Add(saldoLabel);
        Controls.Add(bedragTextBox);
        Controls.Add(stortenButton);
        Controls.Add(opnemenButton);
        Controls.Add(transactieGeschiedenisButton);
    }

    private void StortenButton_Click(object sender, EventArgs e)
    {
        try
        {
            decimal bedrag = decimal.Parse(bedragTextBox.Text);
            mijnRekening.Storten(bedrag);
            saldoLabel.Text = $"Saldo: {mijnRekening.ControleerSaldo()}";
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
            saldoLabel.Text = $"Saldo: {mijnRekening.ControleerSaldo()}";
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
        foreach (var transactie in transactieGeschiedenis)
        {
            geschiedenis += $"{transactie.Datum}: {transactie.Beschrijving} - {transactie.Bedrag:C}\n";
        }
        MessageBox.Show(geschiedenis);
    }
}

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
