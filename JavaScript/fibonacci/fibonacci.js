// Function to generate the first N numbers of the Fibonacci sequence
function generateFibonacci(n) {
    if (n <= 0) return []; // Return an empty array if n is less than or equal to 0
    if (n === 1) return [0]; // Return [0] if n is 1
    if (n === 2) return [0, 1]; // Return [0, 1] if n is 2

    const fib = [0, 1]; // Initialize the array with the first two Fibonacci numbers
    for (let i = 2; i < n; i++) {
        fib.push(fib[i - 1] + fib[i - 2]); // Calculate the next Fibonacci number and add it to the array
    }
    return fib; // Return the array containing the first N Fibonacci numbers
}

// Function to display the Fibonacci sequence on the webpage
function displayFibonacci() {
    const n = parseInt(document.getElementById('fibonacciInput').value); // Get the input value and convert it to an integer
    const result = generateFibonacci(n); // Generate the Fibonacci sequence
    document.getElementById('fibonacciResult').innerText = result.join(', '); // Display the result on the webpage
}

// Add an event listener to the button to call the displayFibonacci function when clicked
document.getElementById('generateButton').addEventListener('click', displayFibonacci);
