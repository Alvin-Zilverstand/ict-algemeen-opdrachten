// Genereer een willekeurig doelgetal tussen 1 en 100
const targetNumber = Math.floor(Math.random() * 100) + 1;
let guess = null;

// Functie om de gok van de gebruiker te controleren
function checkGuess() {
    // Haal de waarde van de invoer op en zet deze om naar een geheel getal
    guess = parseInt(document.getElementById("guessInput").value, 10);

    // Controleer of de gok te laag, te hoog of correct is
    if (guess < targetNumber) {
        document.getElementById("result").innerText = "Te laag! Probeer het opnieuw.";
    } else if (guess > targetNumber) {
        document.getElementById("result").innerText = "Te hoog! Probeer het opnieuw.";
    } else if (guess === targetNumber) {
        document.getElementById("result").innerText = "Gefeliciteerd! Je hebt het juiste getal geraden.";
        startConfetti(); // Start het confetti-effect bij een correcte gok
    } else {
        document.getElementById("result").innerText = "Ongeldige invoer. Voer een getal in tussen 1 en 100.";
    }
}

// Functie om het confetti-effect te starten
function startConfetti() {
    // Maak een container voor de confetti
    const confettiContainer = document.createElement('div');
    confettiContainer.id = 'confetti-container';
    document.body.appendChild(confettiContainer);

    // Definieer de kleuren voor de confetti
    const colors = ['#ff0', '#f00', '#0f0', '#00f', '#f0f', '#0ff'];

    // Maak 100 confetti-elementen met willekeurige kleuren en posities
    for (let i = 0; i < 100; i++) {
        const confetti = document.createElement('div');
        confetti.className = 'confetti';
        confetti.style.left = `${Math.random() * 100}vw`;
        confetti.style.backgroundColor = colors[Math.floor(Math.random() * colors.length)];
        confetti.style.animationDelay = `${Math.random() * 2}s`;
        confettiContainer.appendChild(confetti);
    }

    // Verwijder de confetti-container na 5 seconden
    setTimeout(() => {
        document.body.removeChild(confettiContainer);
    }, 5000);
}
