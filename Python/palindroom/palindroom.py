def is_palindrome(word):
    # Controleer of een woord een palindroom is
    return word == word[::-1]

# Vraag de gebruiker om een woord in te voeren
input_word = input("Voer een woord in: ")

# Controleer of het woord een palindroom is en geef het resultaat weer
if is_palindrome(input_word):
    print(f"{input_word} is een palindroom.")
else:
    print(f"{input_word} is geen palindroom.")