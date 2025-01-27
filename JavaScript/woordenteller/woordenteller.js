function countWordOccurrences(text) {
    const words = text.toLowerCase().match(/\b\w+\b/g);
    const wordCount = {};

    words.forEach(word => {
        wordCount[word] = (wordCount[word] || 0) + 1;
    });

    return wordCount;
}

function displayWordCount(wordCount) {
    const resultDiv = document.getElementById('result');
    resultDiv.innerHTML = ''; // Clear previous results

    let totalWords = 0;
    for (const [word, count] of Object.entries(wordCount)) {
        totalWords += count;
        const p = document.createElement('p');
        p.textContent = `${word}: ${count}`;
        resultDiv.appendChild(p);
    }

    const totalP = document.createElement('p');
    totalP.textContent = `Totaal aantal woorden: ${totalWords}`;
    resultDiv.appendChild(totalP);
}

// Example usage:
document.getElementById('countButton').addEventListener('click', () => {
    const text = document.getElementById('textInput').value;
    const wordCount = countWordOccurrences(text);
    displayWordCount(wordCount);
});
