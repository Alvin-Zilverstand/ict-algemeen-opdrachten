import requests
import time
import sys
import os

# Class to handle logging to multiple files
class Tee:
    def __init__(self, *files):
        # Initialize with a list of file objects to write to
        self.files = files

    def write(self, obj):
        # Write the given object to all files and flush the output
        for f in self.files:
            f.write(obj)
            f.flush()

    def flush(self):
        # Flush the output for all files
        for f in self.files:
            f.flush()

    def close(self):
        # Close all files
        for f in self.files:
            f.close()

# Function to get the latitude and longitude of a city using OpenWeatherMap API
def get_coordinates(city_name):
    api_key = 'cf2b92cba5cdb89baccb2fe05cacb3a5'  # API key for OpenWeatherMap
    base_url = 'http://api.openweathermap.org/geo/1.0/direct'  # Base URL for geocoding API
    params = {
        'q': city_name,  # City name to query
        'appid': api_key  # API key parameter
    }
    response = requests.get(base_url, params=params)  # Make a GET request to the API
    if response.status_code == 200:
        data = response.json()  # Parse the JSON response
        if data:
            # Return the latitude and longitude of the first result
            return data[0]['lat'], data[0]['lon']
        else:
            # Print an error message if the city is not found
            print(f"City {city_name} not found.")
            return None, None
    else:
        # Print an error message if the API request fails
        print(f"Failed to get coordinates for city {city_name}. Error code: {response.status_code}")
        return None, None

# Function to get the weather data for given coordinates using OpenWeatherMap API
def get_weather(lat, lon, city_name):
    api_key = 'cf2b92cba5cdb89baccb2fe05cacb3a5'  # API key for OpenWeatherMap
    base_url = 'https://api.openweathermap.org/data/2.5/weather'  # Base URL for weather API
    params = {
        'lat': lat,  # Latitude parameter
        'lon': lon,  # Longitude parameter
        'appid': api_key,  # API key parameter
        'units': 'metric'  # Units parameter to get temperature in Celsius
    }

    response = requests.get(base_url, params=params)  # Make a GET request to the API

    if response.status_code == 200:
        data = response.json()  # Parse the JSON response
        # Print the weather information
        print(f"Weather at coordinates ({lat}, {lon}) for city {city_name}:")
        print(f"Temperature: {data['main']['temp']}°C")
        print(f"Weather: {data['weather'][0]['description']}")
    else:
        # Print an error message if the API request fails
        print(f"Failed to get weather data for coordinates ({lat}, {lon}). Error code: {response.status_code}")

# Main function to get city name input from the user and fetch weather data
if __name__ == "__main__":
    log_file_path = os.path.join(os.path.dirname(__file__), 'weather.log')
    if os.path.exists(log_file_path):
        with open(log_file_path, 'a') as log_file:
            log_file.write('\n\n')  # Add 2 empty lines before appending new logs
    with open(log_file_path, 'a') as log_file:
        tee = Tee(sys.stdout, log_file)
        sys.stdout = tee
        city_name = input("Enter city name: ")  # Prompt the user to enter a city name
        lat, lon = get_coordinates(city_name)  # Get the coordinates of the city
        if lat is not None and lon is not None:
            print(f"Coordinates for {city_name}: ({lat}, {lon})")  # Log the coordinates and city name
            get_weather(lat, lon, city_name)  # Get the weather data for the coordinates
        # Sleep for 5 seconds before exiting to ensure all logs are written
        time.sleep(5)
        tee.close()  # Close the Tee object to ensure all files are properly closed