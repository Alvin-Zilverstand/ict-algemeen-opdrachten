using System;

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
            Console.WriteLine($"U heeft {bedrag} gestort. Nieuw saldo: {saldo}");
        }
        else
        {
            Console.WriteLine("Het bedrag moet positief zijn.");
        }
    }

    public void Opnemen(decimal bedrag)
    {
        if (bedrag > 0 && bedrag <= saldo)
        {
            saldo -= bedrag;
            Console.WriteLine($"U heeft {bedrag} opgenomen. Nieuw saldo: {saldo}");
        }
        else
        {
            Console.WriteLine("Onvoldoende saldo of ongeldig bedrag.");
        }
    }

    public void ControleerSaldo()
    {
        Console.WriteLine($"Uw huidige saldo is: {saldo}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Bankrekening mijnRekening = new Bankrekening(1000);

        mijnRekening.ControleerSaldo();
        mijnRekening.Storten(200);
        mijnRekening.Opnemen(150);
        mijnRekening.ControleerSaldo();
    }
}
