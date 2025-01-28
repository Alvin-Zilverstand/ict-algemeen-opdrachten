import os
import requests
import time
from bs4 import BeautifulSoup

url = input("Voer de URL van de nieuwswebsite in: ")

if not (url.startswith('www.') or url.startswith('ww2.')):
    url = 'www.' + url

if not url.startswith('https://'):
    url = 'https://' + url

try:
    response = requests.get(url)
    response.raise_for_status()
    webpage = response.content
except requests.exceptions.RequestException as e:
    print(f"Fout bij het ophalen van de webpagina: {e}")
    exit()

soup = BeautifulSoup(webpage, 'html.parser')

titles = soup.find_all('h2')

script_dir = os.path.dirname(os.path.abspath(__file__))
file_path = os.path.join(script_dir, 'titels.txt')

if not titles:
    print("Geen titels gevonden. Controleer de HTML-structuur van de website en pas de selector aan.")
else:
    with open(file_path, 'a', encoding='utf-8') as file:
        for title in titles:
            file.write(title.get_text(separator=' ') + '\n')

    print(f"De titels zijn toegevoegd aan {file_path}")

time.sleep(2)
