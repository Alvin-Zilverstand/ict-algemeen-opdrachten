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
    } else {
        document.getElementById("result").innerText = "Ongeldige invoer. Voer een getal in tussen 1 en 100.";
    }
}
