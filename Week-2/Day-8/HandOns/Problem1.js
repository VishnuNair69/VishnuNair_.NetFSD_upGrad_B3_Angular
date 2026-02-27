const cities = ["London", "New York", "Tokyo"];

const getWeatherAsync = async () => {
  try {
    const res = await fetch("https://jsonplaceholder.typicode.com/users");

    if (!res.ok) {
      throw new Error("Failed to fetch data");
    }

    const data = await res.json();

    cities.forEach((city, index) => {
      const report = `
City: ${city}
Temperature: ${20 + index}°C
Weather: clear sky
------------------------
`;
      console.log(report);
    });

  } catch (error) {
    console.error("Error:", error.message);
  }
};

getWeatherAsync();