const readline = require("readline");

const rl = readline.createInterface({
    input: process.stdin,
    output: process.stdout,
});

function calculator() {
    rl.question("Enter operation (add, subtract, multiply, divide): ", (operation) => {
        rl.question("Enter the first number: ", (first) => {
            const num1 = parseFloat(first);

            rl.question("Enter the second number: ", (second) => {
                const num2 = parseFloat(second);

                let result;

                switch (operation.toLowerCase()) {
                    case "add":
                        result = num1 + num2;
                        break;
                    case "subtract":
                        result = num1 - num2;
                        break;
                    case "multiply":
                        result = num1 * num2;
                        break;
                    case "divide":
                        if (num2 !== 0) {
                            result = num1 / num2;
                        } else {
                            console.error("Error: Division by zero is not allowed.");
                            rl.close();
                            return;
                        }
                        break;
                    default:
                        console.error("Invalid operation. Please enter add, subtract, multiply, or divide.");
                        rl.close();
                        return;
                }

                console.log(`Result: ${result}`);
                rl.close();
            });
        });
    });
}

calculator();
