// Function to count the occurrences of each word in the given text
function countWordOccurrences(text) {
    // Convert text to lowercase and match words using regex
    const words = text.toLowerCase().match(/\b\w+\b/g);
    const wordCount = {};

    // Count each word's occurrences
    words.forEach(word => {
        wordCount[word] = (wordCount[word] || 0) + 1;
    });

    return wordCount;
}

// Function to display the word count results
function displayWordCount(wordCount) {
    const resultDiv = document.getElementById('result');
    resultDiv.innerHTML = ''; // Clear previous results

    let totalWords = 0;
    // Iterate through the word count object and display each word and its count
    for (const [word, count] of Object.entries(wordCount)) {
        totalWords += count;
        const p = document.createElement('p');
        p.textContent = `${word}: ${count}`;
        resultDiv.appendChild(p);
    }

    // Display the total number of words
    const totalP = document.createElement('p');
    totalP.textContent = `Totaal aantal woorden: ${totalWords}`;
    resultDiv.appendChild(totalP);
}

// Example usage: Add event listener to the button to count words when clicked
document.getElementById('countButton').addEventListener('click', () => {
    const text = document.getElementById('textInput').value;
    const wordCount = countWordOccurrences(text);
    displayWordCount(wordCount);
});
