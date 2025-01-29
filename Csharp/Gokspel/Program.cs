using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

class Program : Form
{
    private Random random = new Random();
    private int numberToGuess;
    private TextBox inputBox;
    private Label resultLabel;
    private Button guessButton;
    private Button restartButton;
    private System.Windows.Forms.Timer confettiTimer;
    private List<Confetti> confettiList = new List<Confetti>();

    public Program()
    {
        // Initialiseer het te raden getal
        numberToGuess = random.Next(1, 101);

        // Stel het formulier in
        this.Text = "Gokspel";
        this.Size = new System.Drawing.Size(300, 200);

        // Maak en voeg het promptlabel toe
        Label promptLabel = new Label();
        promptLabel.Text = "Raad het getal tussen 1 en 100:";
        promptLabel.Location = new System.Drawing.Point(10, 20);
        promptLabel.AutoSize = true;
        this.Controls.Add(promptLabel);

        // Maak en voeg het invoervak toe
        inputBox = new TextBox();
        inputBox.Location = new System.Drawing.Point(10, 50);
        this.Controls.Add(inputBox);

        // Maak en voeg de gokknop toe
        guessButton = new Button();
        guessButton.Text = "Gok";
        guessButton.Location = new System.Drawing.Point(10, 80);
        guessButton.Click += new EventHandler(GuessButton_Click);
        this.Controls.Add(guessButton);

        // Maak en voeg het resultaatlabel toe
        resultLabel = new Label();
        resultLabel.Location = new System.Drawing.Point(10, 110);
        resultLabel.AutoSize = true;
        this.Controls.Add(resultLabel);

        // Maak en voeg de herstartknop toe (aanvankelijk verborgen)
        restartButton = new Button();
        restartButton.Text = "Opnieuw";
        restartButton.Location = new System.Drawing.Point(10, 140);
        restartButton.Click += new EventHandler(RestartButton_Click);
        restartButton.Visible = false;
        this.Controls.Add(restartButton);

        // Stel de confetti-timer in
        confettiTimer = new System.Windows.Forms.Timer();
        confettiTimer.Interval = 30;
        confettiTimer.Tick += new EventHandler(ConfettiTimer_Tick);
    }

    private void GuessButton_Click(object sender, EventArgs e)
    {
        // Verwerk de gok van de gebruiker
        int userGuess;
        if (int.TryParse(inputBox.Text, out userGuess))
        {
            if (userGuess < numberToGuess)
            {
                resultLabel.Text = "Te laag! Probeer opnieuw.";
            }
            else if (userGuess > numberToGuess)
            {
                resultLabel.Text = "Te hoog! Probeer opnieuw.";
            }
            else
            {
                resultLabel.Text = "Gefeliciteerd! Je hebt het juiste getal geraden.";
                StartConfetti();
                restartButton.Visible = true; // Toon de herstartknop
            }
        }
        else
        {
            resultLabel.Text = "Ongeldige invoer. Voer een getal in.";
        }
    }

    private void RestartButton_Click(object sender, EventArgs e)
    {
        // Reset het spel
        numberToGuess = random.Next(1, 101);
        resultLabel.Text = "";
        inputBox.Text = "";
        confettiList.Clear();
        confettiTimer.Stop();
        restartButton.Visible = false; // Verberg de herstartknop
        this.Invalidate();
    }

    private void StartConfetti()
    {
        // Initialiseer confetti
        confettiList.Clear();
        for (int i = 0; i < 100; i++)
        {
            confettiList.Add(new Confetti(random, this.ClientSize));
        }
        confettiTimer.Start();
    }

    private void ConfettiTimer_Tick(object sender, EventArgs e)
    {
        // Update de posities van de confetti
        for (int i = 0; i < confettiList.Count; i++)
        {
            confettiList[i].Update();
        }
        this.Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        // Teken de confetti
        foreach (var confetti in confettiList)
        {
            e.Graphics.FillEllipse(new SolidBrush(confetti.Color), confetti.Position.X, confetti.Position.Y, confetti.Size, confetti.Size);
        }
    }

    [STAThread]
    static void Main()
    {
        // Start de applicatie
        Application.EnableVisualStyles();
        Application.Run(new Program());
    }
}

class Confetti
{
    public Point Position;
    public float SpeedY;
    public float SpeedX;
    public float Size;
    public Color Color;

    public Confetti(Random random, Size clientSize)
    {
        // Initialiseer de eigenschappen van de confetti
        Position = new Point(random.Next(clientSize.Width), random.Next(clientSize.Height));
        SpeedY = (float)(random.NextDouble() * 2 + 1);
        SpeedX = (float)(random.NextDouble() * 2 - 1);
        Size = random.Next(5, 10);
        Color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
    }

    public void Update()
    {
        // Update de positie van de confetti en pas zwaartekracht toe
        Position = new Point((int)(Position.X + SpeedX), (int)(Position.Y + SpeedY));
        SpeedY += 0.1f; // zwaartekracht effect
    }
}
