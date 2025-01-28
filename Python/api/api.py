import requests
import time
import sys
import os

class Tee:
    def __init__(self, *files):
        self.files = files

    def write(self, obj):
        for f in self.files:
            f.write(obj)
            f.flush()

    def flush(self):
        for f in self.files:
            f.flush()

    def close(self):
        for f in self.files:
            f.close()

def get_coordinates(city_name):
    api_key = os.getenv('OPENWEATHERMAP_API_KEY', 'default_api_key')
    base_url = 'http://api.openweathermap.org/geo/1.0/direct'
    params = {
        'q': city_name,
        'appid': api_key
    }
    response = requests.get(base_url, params=params)
    if response.status_code == 200:
        data = response.json()
        if data:
            return data[0]['lat'], data[0]['lon']
        else:
            print(f"Stad {city_name} niet gevonden.")
            return None, None
    else:
        print(f"Kon de coördinaten voor stad {city_name} niet ophalen. Foutcode: {response.status_code}")
        return None, None

def get_weather(lat, lon, city_name):
    api_key = os.getenv('OPENWEATHERMAP_API_KEY', 'default_api_key')
    base_url = 'https://api.openweathermap.org/data/2.5/weather'
    params = {
        'lat': lat,
        'lon': lon,
        'appid': api_key,
        'units': 'metric'
    }

    response = requests.get(base_url, params=params)

    if response.status_code == 200:
        data = response.json()
        print(f"Weer op coördinaten ({lat}, {lon}) in {city_name}:")
        print(f"Temperatuur: {data['main']['temp']}°C")
        print(f"Weer: {data['weather'][0]['description']}")
    else:
        print(f"Kon de weersgegevens voor coördinaten ({lat}, {lon}) niet ophalen. Foutcode: {response.status_code}")

if __name__ == "__main__":
    log_file_path = os.path.join(os.path.dirname(__file__), 'weather.log')
    if os.path.exists(log_file_path):
        with open(log_file_path, 'a') as log_file:
            log_file.write('\n')
    with open(log_file_path, 'a') as log_file:
        city_name = input("Voer de naam van de stad in: ")
        lat, lon = get_coordinates(city_name)
        if lat is not None and lon is not None:
            original_stdout = sys.stdout
            tee = Tee(original_stdout, log_file)
            sys.stdout = tee
            get_weather(lat, lon, city_name)
            sys.stdout = original_stdout
        time.sleep(5)
        tee.close()