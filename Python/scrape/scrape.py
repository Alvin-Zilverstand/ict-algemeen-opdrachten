import os
import requests
import time
from bs4 import BeautifulSoup

# Vraag de gebruiker om de URL van de nieuwswebsite
url = input("Voer de URL van de nieuwswebsite in: ")

# Controleer of de URL begint met 'www.' of 'ww2.', zo niet, voeg 'www.' toe
if not (url.startswith('www.') or url.startswith('ww2.')):
    url = 'www.' + url

# Controleer of de URL begint met 'https://', zo niet, voeg het toe
if not url.startswith('https://'):
    url = 'https://' + url

# Haal de inhoud van de webpagina op
try:
    response = requests.get(url)
    response.raise_for_status()  # Raise an HTTPError for bad responses
    webpage = response.content
except requests.exceptions.RequestException as e:
    print(f"Fout bij het ophalen van de webpagina: {e}")
    exit()

# Parse de webpagina met BeautifulSoup
soup = BeautifulSoup(webpage, 'html.parser')

# Zoek alle titels van nieuwsartikelen (pas de selector aan op basis van de HTML-structuur van de website)
titles = soup.find_all('h2')  # Pas deze selector aan indien nodig

# Bepaal het pad van het huidige script
script_dir = os.path.dirname(os.path.abspath(__file__))
# Bepaal het pad van het bestand titels.txt in dezelfde directory
file_path = os.path.join(script_dir, 'titels.txt')

# Controleer of er titels zijn gevonden
if not titles:
    print("Geen titels gevonden. Controleer de HTML-structuur van de website en pas de selector aan.")
else:
    # Open het bestand titels.txt in append-modus
    with open(file_path, 'a', encoding='utf-8') as file:
        # Schrijf de titels van de nieuwsartikelen naar het bestand
        for title in titles:
            file.write(title.get_text(separator=' ') + '\n')

    print(f"De titels zijn toegevoegd aan {file_path}")

time.sleep(2)  # Wacht 5 seconden voordat het script wordt afgesloten
