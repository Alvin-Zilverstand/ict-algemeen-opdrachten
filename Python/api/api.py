import requests
import time
import sys
import os

# Klasse om te loggen naar meerdere bestanden
class Tee:
    def __init__(self, *files):
        # Initialiseer met een lijst van bestandobjecten om naar te schrijven
        self.files = files

    def write(self, obj):
        # Schrijf het gegeven object naar alle bestanden en flush de output
        for f in self.files:
            f.write(obj)
            f.flush()

    def flush(self):
        # Flush de output voor alle bestanden
        for f in self.files:
            f.flush()

    def close(self):
        # Sluit alle bestanden
        for f in self.files:
            f.close()

# Functie om de breedte- en lengtegraad van een stad op te halen met de OpenWeatherMap API
def get_coordinates(city_name):
    api_key = 'cf2b92cba5cdb89baccb2fe05cacb3a5'  # API-sleutel voor OpenWeatherMap
    base_url = 'http://api.openweathermap.org/geo/1.0/direct'  # Basis-URL voor geocoding API
    params = {
        'q': city_name,  # Stadnaam om te queryen
        'appid': api_key  # API-sleutel parameter
    }
    response = requests.get(base_url, params=params)  # Maak een GET-verzoek naar de API
    if response.status_code == 200:
        data = response.json()  # Parse de JSON-respons
        if data:
            # Retourneer de breedte- en lengtegraad van het eerste resultaat
            return data[0]['lat'], data[0]['lon']
        else:
            # Print een foutmelding als de stad niet gevonden is
            print(f"Stad {city_name} niet gevonden.")
            return None, None
    else:
        # Print een foutmelding als het API-verzoek mislukt
        print(f"Kon de coördinaten voor stad {city_name} niet ophalen. Foutcode: {response.status_code}")
        return None, None

# Functie om de weersgegevens voor gegeven coördinaten op te halen met de OpenWeatherMap API
def get_weather(lat, lon, city_name):
    api_key = 'cf2b92cba5cdb89baccb2fe05cacb3a5'  # API-sleutel voor OpenWeatherMap
    base_url = 'https://api.openweathermap.org/data/2.5/weather'  # Basis-URL voor weer API
    params = {
        'lat': lat,  # Breedtegraad parameter
        'lon': lon,  # Lengtegraad parameter
        'appid': api_key,  # API-sleutel parameter
        'units': 'metric'  # Eenheden parameter om temperatuur in Celsius te krijgen
    }

    response = requests.get(base_url, params=params)  # Maak een GET-verzoek naar de API

    if response.status_code == 200:
        data = response.json()  # Parse de JSON-respons
        # Print de weersinformatie
        print(f"Weer op coördinaten ({lat}, {lon}) in {city_name}:")
        print(f"Temperatuur: {data['main']['temp']}°C")
        print(f"Weer: {data['weather'][0]['description']}")
    else:
        # Print een foutmelding als het API-verzoek mislukt
        print(f"Kon de weersgegevens voor coördinaten ({lat}, {lon}) niet ophalen. Foutcode: {response.status_code}")

# Hoofdfunctie om de stadnaam van de gebruiker te vragen en de weersgegevens op te halen
if __name__ == "__main__":
    log_file_path = os.path.join(os.path.dirname(__file__), 'weather.log')
    if os.path.exists(log_file_path):
        with open(log_file_path, 'a') as log_file:
            log_file.write('\n')  # Voeg 2 lege regels toe voordat nieuwe logs worden toegevoegd
    with open(log_file_path, 'a') as log_file:
        city_name = input("Voer de naam van de stad in: ")  # Vraag de gebruiker om een stadnaam in te voeren
        lat, lon = get_coordinates(city_name)  # Haal de coördinaten van de stad op
        if lat is not None and lon is not None:
            original_stdout = sys.stdout  # Sla de originele stdout op
            tee = Tee(original_stdout, log_file)
            sys.stdout = tee  # Redirect stdout naar zowel terminal als logbestand
            get_weather(lat, lon, city_name)  # Haal de weersgegevens voor de coördinaten op
            sys.stdout = original_stdout  # Reset stdout naar origineel
        # Wacht 5 seconden voordat het script wordt afgesloten om ervoor te zorgen dat alle logs zijn geschreven
        time.sleep(5)
        tee.close()  # Sluit het Tee-object om ervoor te zorgen dat alle bestanden correct worden gesloten