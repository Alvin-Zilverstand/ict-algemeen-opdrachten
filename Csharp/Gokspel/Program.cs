﻿using System;
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
        // Initialize the random number to guess
        numberToGuess = random.Next(1, 101);

        // Set up the form
        this.Text = "Gokspel";
        this.Size = new System.Drawing.Size(300, 200);

        // Create and add the prompt label
        Label promptLabel = new Label();
        promptLabel.Text = "Raad het getal tussen 1 en 100:";
        promptLabel.Location = new System.Drawing.Point(10, 20);
        promptLabel.AutoSize = true;
        this.Controls.Add(promptLabel);

        // Create and add the input box
        inputBox = new TextBox();
        inputBox.Location = new System.Drawing.Point(10, 50);
        this.Controls.Add(inputBox);

        // Create and add the guess button
        guessButton = new Button();
        guessButton.Text = "Gok";
        guessButton.Location = new System.Drawing.Point(10, 80);
        guessButton.Click += new EventHandler(GuessButton_Click);
        this.Controls.Add(guessButton);

        // Create and add the result label
        resultLabel = new Label();
        resultLabel.Location = new System.Drawing.Point(10, 110);
        resultLabel.AutoSize = true;
        this.Controls.Add(resultLabel);

        // Create and add the restart button (initially hidden)
        restartButton = new Button();
        restartButton.Text = "Opnieuw";
        restartButton.Location = new System.Drawing.Point(10, 140);
        restartButton.Click += new EventHandler(RestartButton_Click);
        restartButton.Visible = false;
        this.Controls.Add(restartButton);

        // Set up the confetti timer
        confettiTimer = new System.Windows.Forms.Timer();
        confettiTimer.Interval = 30;
        confettiTimer.Tick += new EventHandler(ConfettiTimer_Tick);
    }

    private void GuessButton_Click(object sender, EventArgs e)
    {
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
                restartButton.Visible = true; // Show the restart button
            }
        }
        else
        {
            resultLabel.Text = "Ongeldige invoer. Voer een getal in.";
        }
    }

    private void RestartButton_Click(object sender, EventArgs e)
    {
        // Reset the game
        numberToGuess = random.Next(1, 101);
        resultLabel.Text = "";
        inputBox.Text = "";
        confettiList.Clear();
        confettiTimer.Stop();
        restartButton.Visible = false; // Hide the restart button
        this.Invalidate();
    }

    private void StartConfetti()
    {
        // Initialize confetti
        confettiList.Clear();
        for (int i = 0; i < 100; i++)
        {
            confettiList.Add(new Confetti(random, this.ClientSize));
        }
        confettiTimer.Start();
    }

    private void ConfettiTimer_Tick(object sender, EventArgs e)
    {
        // Update confetti positions
        for (int i = 0; i < confettiList.Count; i++)
        {
            confettiList[i].Update();
        }
        this.Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        // Draw confetti
        foreach (var confetti in confettiList)
        {
            e.Graphics.FillEllipse(new SolidBrush(confetti.Color), confetti.Position.X, confetti.Position.Y, confetti.Size, confetti.Size);
        }
    }

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new Program());
    }
}

class Confetti
{
    public PointF Position;
    public float SpeedY;
    public float SpeedX;
    public float Size;
    public Color Color;

    public Confetti(Random random, Size clientSize)
    {
        // Initialize confetti properties
        Position = new PointF(random.Next(clientSize.Width), random.Next(clientSize.Height));
        SpeedY = (float)(random.NextDouble() * 2 + 1);
        SpeedX = (float)(random.NextDouble() * 2 - 1);
        Size = random.Next(5, 10);
        Color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
    }

    public void Update()
    {
        // Update confetti position and apply gravity
        Position = new PointF(Position.X + SpeedX, Position.Y + SpeedY);
        SpeedY += 0.1f; // gravity effect
    }
}
