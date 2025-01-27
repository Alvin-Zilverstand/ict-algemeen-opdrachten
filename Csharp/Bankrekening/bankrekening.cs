using System;
using System.Collections.Generic;

public class Bankrekening
{
    private decimal saldo;
    private string rekeningnummer;
    private List<string> transactieGeschiedenis;

    public Bankrekening(string rekeningnummer, decimal beginsaldo)
    {
        this.rekeningnummer = rekeningnummer;
        saldo = beginsaldo;
        transactieGeschiedenis = new List<string>();
        transactieGeschiedenis.Add($"Rekening geopend met beginsaldo: €{beginsaldo}");
    }

    public void Storten(decimal bedrag)
    {
        if (bedrag > 0)
        {
            saldo += bedrag;
            transactieGeschiedenis.Add($"€{bedrag} gestort. Nieuw saldo: €{saldo}");
            Console.WriteLine($"€{bedrag} gestort. Nieuw saldo: €{saldo}");
        }
        else
        {
            Console.WriteLine("Bedrag moet positief zijn om te storten.");
        }
    }

    public void Opnemen(decimal bedrag)
    {
        if (bedrag > 0 && bedrag <= saldo)
        {
            saldo -= bedrag;
            transactieGeschiedenis.Add($"€{bedrag} opgenomen. Nieuw saldo: €{saldo}");
            Console.WriteLine($"€{bedrag} opgenomen. Nieuw saldo: €{saldo}");
        }
        else
        {
            Console.WriteLine("Onvoldoende saldo of ongeldig bedrag.");
        }
    }

    public decimal ControleerSaldo()
    {
        return saldo;
    }

    public void BekijkTransactieGeschiedenis()
    {
        Console.WriteLine("Transactiegeschiedenis:");
        foreach (var transactie in transactieGeschiedenis)
        {
            Console.WriteLine(transactie);
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Bankrekening mijnRekening = new Bankrekening("NL01BANK0123456789", 1000);
        mijnRekening.Storten(500);
        mijnRekening.Opnemen(200);
        Console.WriteLine($"Huidig saldo: €{mijnRekening.ControleerSaldo()}");
        mijnRekening.BekijkTransactieGeschiedenis();
    }
}
