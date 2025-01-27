function generateFibonacci(n) {
    if (n <= 0) return [];
    if (n === 1) return [0];
    if (n === 2) return [0, 1];

    const fib = [0, 1];
    for (let i = 2; i < n; i++) {
        fib.push(fib[i - 1] + fib[i - 2]);
    }
    return fib;
}

function displayFibonacci() {
    const n = parseInt(document.getElementById('fibonacciInput').value);
    const result = generateFibonacci(n);
    document.getElementById('fibonacciResult').innerText = result.join(', ');
}

document.getElementById('generateButton').addEventListener('click', displayFibonacci);
