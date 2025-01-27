def is_prime(num):
    if num <= 1:
        return False
    for i in range(2, int(num**0.5) + 1):
        if num % i == 0:
            return False
    return True

def generate_primes(N):
    primes = []
    for num in range(2, N + 1):
        if is_prime(num):
            primes.append(num)
    return primes

# Vraag de gebruiker om een getal N
N = int(input("Voer een getal N in: "))
priemgetallen = generate_primes(N)
print(f"Priemgetallen tot {N}: {priemgetallen}")