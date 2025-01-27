const targetNumber = Math.floor(Math.random() * 100) + 1;
let guess = null;

function checkGuess() {
    guess = parseInt(document.getElementById("guessInput").value, 10);

    if (guess < targetNumber) {
        document.getElementById("result").innerText = "Te laag! Probeer het opnieuw.";
    } else if (guess > targetNumber) {
        document.getElementById("result").innerText = "Te hoog! Probeer het opnieuw.";
    } else if (guess === targetNumber) {
        document.getElementById("result").innerText = "Gefeliciteerd! Je hebt het juiste getal geraden.";
        startConfetti();
    } else {
        document.getElementById("result").innerText = "Ongeldige invoer. Voer een getal in tussen 1 en 100.";
    }
}

function startConfetti() {
    const confettiContainer = document.createElement('div');
    confettiContainer.id = 'confetti-container';
    document.body.appendChild(confettiContainer);

    for (let i = 0; i < 100; i++) {
        const confetti = document.createElement('div');
        confetti.className = 'confetti';
        confetti.style.left = `${Math.random() * 100}vw`;
        confetti.style.animationDelay = `${Math.random() * 2}s`;
        confettiContainer.appendChild(confetti);
    }

    setTimeout(() => {
        document.body.removeChild(confettiContainer);
    }, 5000);
}
