def is_palindrome(word):
    return word == word[::-1]


input_word = input("Voer een woord in: ")


if is_palindrome(input_word):
    print(f"{input_word} is een palindroom.")
else:
    print(f"{input_word} is geen palindroom.")