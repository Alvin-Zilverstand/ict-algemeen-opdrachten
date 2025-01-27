using System;
using System.Windows.Forms;

class Bankrekening
{
    private decimal saldo;

    public Bankrekening(decimal beginsaldo)
    {
        saldo = beginsaldo;
    }

    public void Storten(decimal bedrag)
    {
        if (bedrag > 0)
        {
            saldo += bedrag;
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
}

public class MainForm : Form
{
    private Bankrekening mijnRekening;
    private Label saldoLabel;
    private TextBox bedragTextBox;
    private Button stortenButton;
    private Button opnemenButton;

    public MainForm()
    {
        mijnRekening = new Bankrekening(1000);

        saldoLabel = new Label() { Text = $"Saldo: {mijnRekening.ControleerSaldo()}", Top = 20, Left = 20, Width = 200 };
        bedragTextBox = new TextBox() { Top = 50, Left = 20, Width = 200 };
        stortenButton = new Button() { Text = "Storten", Top = 80, Left = 20 };
        opnemenButton = new Button() { Text = "Opnemen", Top = 110, Left = 20 };

        stortenButton.Click += StortenButton_Click;
        opnemenButton.Click += OpnemenButton_Click;

        Controls.Add(saldoLabel);
        Controls.Add(bedragTextBox);
        Controls.Add(stortenButton);
        Controls.Add(opnemenButton);
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
}

class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new MainForm());
    }
}
