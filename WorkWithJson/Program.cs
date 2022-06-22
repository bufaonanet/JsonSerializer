using System.Net.Http.Json;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using WorkWithJson.Models;

#region Program


#endregion

#region Methods

static void Serialization()
{
    var weatherForecast = new WeatherForecast
    {
        Date = DateTime.Parse("21/06/2022"),
        TemperatureCelsius = 20,
        Summary = "Quente"
    };


    string jsonString = JsonSerializer.Serialize(weatherForecast);

    Console.WriteLine(jsonString);
    Console.Clear();

    string jsonStringGenerics = JsonSerializer.Serialize<WeatherForecast>(weatherForecast);

    Console.WriteLine(jsonStringGenerics);
    Console.Clear();

    string fileName = "WeatherForecast.json";

    File.WriteAllText(fileName, jsonString);

    Console.WriteLine(jsonString);
    Console.Clear();
}

static async Task SerializationAsync()
{
    var weatherForecast = new WeatherForecast
    {
        Date = DateTime.Parse("2021-12-01"),
        TemperatureCelsius = 25,
        Summary = "Hot"
    };

    // Name of the file where the JSON string is stored
    string fileName = "WeatherForecast.json";
    FileStream createStream = File.Create(fileName);

    // Serialize it
    await JsonSerializer.SerializeAsync(createStream, weatherForecast);

    // Await and dispose the stream 
    await createStream.DisposeAsync();
    Console.Clear();
}

static void SerializationCollections()
{
    // Create a new WeatherForecast object
    var weatherForecast = new WeatherForecast
    {
        Date = DateTime.Parse("2021-12-01"),
        TemperatureCelsius = 17,
        Summary = "Overcast Clouds",
        Pressure = 1018,
        Humidity = 85,
        Coordinates = new Coordinates()
        {
            Lon = -83.9167,
            Lat = 9.8667
        },
        Wind = new Wind()
        {
            Speed = 1.79,
            Degree = 157,
            Gust = 3.58
        },
        SummaryWords = new[] { "Cool", "Windy", "Humid" }
    };

    // Serialize weatherForecast
    string jsonString = JsonSerializer.Serialize(weatherForecast);

    // Print the serialized JSON string
    Console.WriteLine(jsonString);
    Console.Clear();
}

static void SerializationFromText()
{
    // String variable that contains the JSON text
    string jsonString = @"{
   ""Date"":""2021-12-01T00:00:00-06:00"",
   ""TemperatureCelsius"":17,
   ""Summary"":""Overcast Clouds"",
   ""Pressure"":1018,
   ""Humidity"":85,
   ""Coordinates"":{
      ""Lon"":-83.9167,
      ""Lat"":9.8667
   },
   ""Wind"":{
      ""Speed"":1.79,
      ""Degree"":157,
      ""Gust"":3.58
   },
   ""SummaryWords"":[
      ""Cool"",
      ""Windy"",
      ""Humid""
   ]
}";

    // .NET object deserialized from the JSON string
    WeatherForecast? weatherForecast = JsonSerializer.Deserialize<WeatherForecast>(jsonString);

    // Check if object was deserialized successfully
    if (weatherForecast != null)
    {
        //Print a few deserialized properties
        Console.WriteLine($"Date: {weatherForecast.Date}");
        Console.WriteLine($"TemperatureCelsius: {weatherForecast.TemperatureCelsius}");
        Console.WriteLine($"Summary: {weatherForecast.Summary}");
    }
}

static void DeserializationFromFile()
{
    // JSON filename
    string fileName = "WeatherForecast.json";

    // Extract the JSON text from the file
    string jsonString = File.ReadAllText(fileName);

    // Deserialize the JSON text to a WeatherForecast object
    WeatherForecast? weatherForecast = JsonSerializer.Deserialize<WeatherForecast>(jsonString);

    // if weather object was deserialized successfully (we don't want unexpected errors)
    if (weatherForecast != null)
    {
        // Print some deserialized properties
        Console.WriteLine($"Date: {weatherForecast.Date}");
        Console.WriteLine($"TemperatureCelsius: {weatherForecast.TemperatureCelsius}");
        Console.WriteLine($"Summary: {weatherForecast.Summary}");

    }
}

static async Task DeserializationAsync()
{
    string fileName = "WeatherForecast.json";
    FileStream openStream = File.OpenRead(fileName);

    // Deserialize the JSON text to a WeatherForecast object asynchronously
    WeatherForecast? weatherForecast = await JsonSerializer.DeserializeAsync<WeatherForecast>(openStream);

    // Check if object was deserialized successfully
    if (weatherForecast != null)
    {
        //Print a few deserialized properties
        Console.WriteLine($"Date: {weatherForecast.Date}");
        Console.WriteLine($"TemperatureCelsius: {weatherForecast.TemperatureCelsius}");
        Console.WriteLine($"Summary: {weatherForecast.Summary}");
    }
}

static async Task HttpClientExtensionMethods()
{
    //1. Get
    HttpClient client = new HttpClient
    {
        BaseAddress = new Uri("https://jsonplaceholder.typicode.com")
    };

    //Get User 1
    //https://jsonplaceholder.typicode.com/users/1
    UserApi? user = await client.GetFromJsonAsync<UserApi>("users/1");

    if (user != null)
    {
        Console.WriteLine($"Id: {user.Id}");
        Console.WriteLine($"Name: {user.Name}");
        Console.WriteLine($"Username: {user.Username}");
        Console.WriteLine($"Email: {user.Email}");
    }

    Console.Clear();

    // 2. POST
    UserApi newUser = new UserApi()
    {
        Id = 42,
        Name = "Xavier Morera",
        Email = "xavier@youcanprobablyfindthiseasily.com",
        Username = "xmorera"
    };

    // Post a new user
    HttpResponseMessage response = await client.PostAsJsonAsync("users", newUser);

    // Print the response code
    Console.WriteLine($"{(response.IsSuccessStatusCode ? "Success" : "Error")} - {response.StatusCode}");
    Console.Clear();
}

static void JsonSerializerOptions()
{
    var Coordinates = new Coordinates()
    {
        Lon = -83.9167,
        Lat = 9.8667
    };

    var Wind = new Wind()
    {
        Speed = 1.79,
        Degree = 157,
        Gust = 3.58
    };

    // With no options
    Console.WriteLine(JsonSerializer.Serialize(Coordinates));
    Console.Clear();

    // 1.Instantiate a new JsonSerializerOptions class
    // Instantiate a new JsonSerializerOptions class and pass as parameter WriteIndented for pretty printing
    var options = new JsonSerializerOptions { WriteIndented = true };


    // Options passed as the second parameter
    string coordinatesJson = JsonSerializer.Serialize(Coordinates, options);

    // Print the serialized JSON string
    Console.WriteLine(coordinatesJson);
    Console.Clear();

    // 2. Reuse the JsonSerializerOptions class
    // The JsonSerializerOptions can be reused
    string windJson = JsonSerializer.Serialize(Wind, options);

    // Print the serialized JSON string
    Console.WriteLine(windJson);
    Console.Clear();

    // 3. Copy an existing JsonSerializerOptions object
    // Create a new JsonSerializerOptions instance from an existing one 
    var optionsCopy = new JsonSerializerOptions(options);

    // Call serialize and pass the new instance
    string windJsonTwo = JsonSerializer.Serialize(Wind, optionsCopy);

    // Print the serialized JSON string
    Console.WriteLine(windJsonTwo);
    Console.Clear();

    // 4. Use the constructor that creates a new instance with the default options that ASP.NET Core uses for web apps
    //      PropertyNameCaseInsensitive = true
    //      JsonNamingPolicy = CamelCase
    //      NumberHandling = AllowReadingFromString
    var optionsWeb = new JsonSerializerOptions(JsonSerializerDefaults.Web);

    // Call serialize with web defaults
    string webDefaultsJson = JsonSerializer.Serialize(Wind, optionsWeb);

    // Print the serialized JSON string
    Console.WriteLine(webDefaultsJson);
    Console.Clear();
}

static void CaseInsensitiveMatching()
{
    // JSON object with a description instead of Summary
    string jsonString = @"{
  ""date"":""2021-12-01T00:00:00-07:00"",
  ""temperaturecelsius"":7,
  ""feelslike"":3.14,
  ""description"":""overcast clouds"",
  ""pressure"":1003,
  ""humidity"":79,
  ""coord"": 
  {
    ""lon"":48.75,
    ""lat"":8.24
  },
  ""wind"":
  {
    ""speed"":11.32,
    ""deg"":200,
    ""gust"":17.49
  },
  ""keywords"":
  [
    ""Chill"",
    ""Windy""
  ],
  ""country"":""DE"",
  ""city"":""Baden-Baden""
}";

    // 1.Deserialize with default options, namely case sensitive matching
    // Deserialize JSON
    WeatherForecast? weatherForecast = JsonSerializer.Deserialize<WeatherForecast>(jsonString);

    // Check if object was deserialized
    if (weatherForecast != null)
    {
        // Print some deserialized properties
        Console.WriteLine($"Date: {weatherForecast.Date}");
        // Casing is different: "temperaturecelsius" in JSON vs. "TemperatureCelsius" in .NET
        Console.WriteLine($"TemperatureCelsius: {weatherForecast.TemperatureCelsius}");
        Console.WriteLine($"Summary: {weatherForecast.Summary}");
    }

    // 2. Create a JsonSerializerOptions object with case-insensitive property names matching
    var options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

    // Deserialize, passing the PropertyNameCaseInsensitive option
    WeatherForecast? weatherForecastCaseInsensitive = JsonSerializer.Deserialize<WeatherForecast>(jsonString, options);

    // Check if object was deserialized
    if (weatherForecastCaseInsensitive != null)
    {
        // Print some deserialized properties
        Console.WriteLine($"Date: {weatherForecastCaseInsensitive.Date}");
        Console.WriteLine($"TemperatureCelsius: {weatherForecastCaseInsensitive.TemperatureCelsius}");
        Console.WriteLine($"Summary: {weatherForecastCaseInsensitive.Summary}");
    }
}

static void CustomizePropertyNamesAndValues()
{
    string jsonString = @"{
  ""date"":""2021-12-01T00:00:00-07:00"",
  ""temperaturecelsius"":7,
  ""feelslike"":3.14,
  ""description"":""overcast clouds"",
  ""TemperatureRanges"": {
    ""ColdMinTemp"": 20,
    ""HotMinTemp"": 40
  }
}";

    // Serialize weatherForecast 
    WeatherForecastProperty? weatherForecast = JsonSerializer.Deserialize<WeatherForecastProperty>(jsonString);

    // 1. Customize individual property names
    // If nothing is printed out, go and uncomment [JsonPropertyName("description")] in the WeatherForecast2 class
    if (weatherForecast != null)
    {
        Console.WriteLine($"Description: {weatherForecast.Summary}");
    }
    Console.Clear();

    // 2. Converting all property names to camelCase
    // Without any additional options
    JsonSerializerOptions optionsIndented = new JsonSerializerOptions
    {
        WriteIndented = true
    };

    string weatherjson = JsonSerializer.Serialize(weatherForecast, optionsIndented);
    Console.WriteLine(weatherjson);

    // With PropertyNamingPolicy set to JsonNamingPolicy.CamelCase
    JsonSerializerOptions optionsPropertyNamingPolicy = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    string weatherJson = JsonSerializer.Serialize(weatherForecast, optionsPropertyNamingPolicy);
    Console.WriteLine(weatherJson);
    Console.Clear();

    // 3. Use a custom JSON property naming policy
    // Created a policy (UpperCaseNamingPolicy.cs) to upper case properties
    JsonSerializerOptions optionsCustomPolicy = new JsonSerializerOptions
    {
        PropertyNamingPolicy = new UpperCaseNamingPolicy(),
        WriteIndented = true
    };
    string jsonCustomPolicy = JsonSerializer.Serialize(weatherForecast, optionsCustomPolicy);
    Console.WriteLine(jsonCustomPolicy);
    Console.Clear();

    // 4. Converting dictionary keys to camel case
    JsonSerializerOptions optionsDictKeyPolicyPolicy = new JsonSerializerOptions
    {
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };
    string jsonDictKeyPolicy = JsonSerializer.Serialize(weatherForecast, optionsDictKeyPolicyPolicy);
    Console.WriteLine(jsonDictKeyPolicy);
    Console.Clear();

    // 5. Enums as strings
    var weatherWithEnums = new WeatherForecastProperty
    {
        Date = DateTime.Parse("2021-12-01"),
        TemperatureCelsius = 25,
        Summary = "Hot",
        FeelsLike = Feels.Hot
    };

    string jsonEnum = JsonSerializer.Serialize(weatherWithEnums, optionsIndented);
    Console.WriteLine(jsonEnum);

    var optionsEnums = new JsonSerializerOptions
    {
        WriteIndented = true,
        Converters =
    {
        new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
    }
    };

    string jsonEnums = JsonSerializer.Serialize(weatherForecast, optionsEnums);
    Console.WriteLine(jsonEnums);
    Console.Clear();
}

static void ConfigureOrder()
{
    // Create a new WeatherForecast object
    var weatherForecast = new WeatherForecastOrder
    {
        Date = DateTime.Parse("2021-12-01"),
        TemperatureCelsius = 7,
        Summary = "Overcast Clouds",
        Pressure = 1003,
        Humidity = 79,
        Coordinates = new Coordinates()
        {
            Lon = -83.9167,
            Lat = 9.8667
        },
        Wind = new Wind()
        {
            Speed = 11.32,
            Degree = 200,
            Gust = 17.49
        },
        SummaryWords = new[] { "Chill", "Windy" },
        WeatherCondition = WeatherCondition.Snow
    };

    // No additional options specify order
    var options = new JsonSerializerOptions
    {
        WriteIndented = true,
    };

    // Serialize weatherForecast, order is determined via attributes
    string jsonString = JsonSerializer.Serialize(weatherForecast, options);

    File.WriteAllText("weather.json", jsonString);
    Console.Clear();
}

static void IgnoreProperties()
{
    // Create a new WeatherForecast object
    var weatherForecast = new WeatherForecastIgnore
    {
        Date = DateTime.Parse("2021-12-01"),
        TemperatureCelsius = 7,
        Summary = "Overcast Clouds",
        Pressure = 1003,
        Humidity = 79,
        Coordinates = new Coordinates()
        {
            Lon = -83.9167,
            Lat = 9.8667
        },
        Wind = new Wind()
        {
            Speed = 11.32,
            Degree = 200,
            Gust = 17.49
        },
        SummaryWords = new[] { "Chill", "Windy" },
        WeatherCondition = WeatherCondition.Snow,
        WeatherLogo = "carvedrock.png",
        RequestTime = 42
    };

    // 1. Ignore individual properties
    // Shows null values but ignores properties with [JsonIgnore]
    var options = new JsonSerializerOptions
    {
        WriteIndented = true
    };

    string jsonString = JsonSerializer.Serialize(weatherForecast, options);
    Console.WriteLine(jsonString);
    Console.Clear();

    // 2. Ignore read-only properties
    options = new JsonSerializerOptions
    {
        IgnoreReadOnlyProperties = true,
        WriteIndented = true
    };
    jsonString = JsonSerializer.Serialize(weatherForecast, options);
    Console.WriteLine(jsonString);
    Console.Clear();

    // 3. Ignore null values
    options = new JsonSerializerOptions
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    jsonString = JsonSerializer.Serialize(weatherForecast, options);
    Console.WriteLine(jsonString);
    Console.Clear();

    // 4. Ignore default values
    options = new()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
    };

    jsonString = JsonSerializer.Serialize(weatherForecast, options);
    Console.WriteLine(jsonString);
    Console.Clear();
}

static void CustomizeCharacterEncoding()
{
    // Create a new WeatherForecast object, look at the word used in Summary
    var weatherForecast = new WeatherForecast
    {
        Date = DateTime.Parse("2021-12-01"),
        TemperatureCelsius = 7,
        Summary = "жарко"
    };

    string fileName = "WeatherForecast.json";

    // 1. Serialize with no encoding options
    // Replaces with \uxxxx where xxxx is the Unicode code of the character
    var options = new JsonSerializerOptions { WriteIndented = true };
    string jsonString = JsonSerializer.Serialize(weatherForecast, options);
    File.WriteAllText(fileName, jsonString);

    // 2. Serialize language character sets
    // Notice usings at the top: System.Text.Encodings.Web and System.Text.Unicode
    options = new JsonSerializerOptions
    {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
        WriteIndented = true
    };

    jsonString = JsonSerializer.Serialize(weatherForecast, options);
    File.WriteAllText(fileName, jsonString);

    // 3. Serialize specific characters
    // Possible to specify which characters can be or not be escaped
    // This example serializes only the first two characters of жарко
    var encoderSettings = new TextEncoderSettings();
    encoderSettings.AllowCharacters('\u0436', '\u0430');
    encoderSettings.AllowRange(UnicodeRanges.BasicLatin);
    options = new JsonSerializerOptions
    {
        Encoder = JavaScriptEncoder.Create(encoderSettings),
        WriteIndented = true
    };

    jsonString = JsonSerializer.Serialize(weatherForecast, options);
    File.WriteAllText(fileName, jsonString);

    // 4. Serialize all characters
    // Possible to indicate to serialize all characters to minimize escaping
    // Use with caution, only when sure that client will use UTF-8 encoding
    options = new JsonSerializerOptions
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };
    jsonString = JsonSerializer.Serialize(weatherForecast, options);
    File.WriteAllText(fileName, jsonString);
    Console.Clear();
}

static void JsonDocument()
{
    string filename = "weather.json";
    // String variable that contains the JSON string
    string jsonString = File.ReadAllText(filename);

    double sum = 0;
    int count = 0;
    TempDataPoint? perfectTemperature;


    // Notice the using. This class utilizes resources from pooled memory... failure to properly dispose this object will result in the memory not being returned to the pool, which will increase GC impact across various parts of the framework.
    using (JsonDocument document = System.Text.Json.JsonDocument.Parse(jsonString))
    {
        JsonElement root = document.RootElement;
        // Get the entire JSON from the root JsonElement
        Console.WriteLine(root.ToString());
        Console.Clear();

        // Get one property (make sure it does exist) 
        JsonElement temp = root.GetProperty("temperaturecelsius");
        Console.WriteLine(temp.ToString());
        Console.Clear();

        // Get a property, which in this particular case is an array and iterate over the JsonElement objects it contains
        JsonElement last7daysElement = root.GetProperty("last7days");
        Console.WriteLine(last7daysElement.ToString());
        Console.Clear();


        foreach (JsonElement measure in last7daysElement.EnumerateArray())
        {
            if (measure.TryGetProperty("temperature", out JsonElement tempElement))
            {
                sum += tempElement.GetDouble();
                count++;
                if (tempElement.GetInt32() == 7)
                {
                    // Use an extension method from JsonSerializer to deserialize a JsonElement
                    perfectTemperature = JsonSerializer.Deserialize<TempDataPoint>(measure);
                    // Instead of doing it manually, as shown below
                    // perfectTemperature = new TempDataPoint{ 
                    //   humidity = measure.GetProperty("humidity").GetInt32(),
                    //   temperature = measure.GetProperty("temperature").GetInt32()               
                    //};
                }
            }
        }
    }

    double average = sum / count;
    Console.WriteLine($"Average temperature : {average}");
    Console.Clear();
}

static void JsonNode()
{
    // Load a couple of JSON texts
    string filenameForecast = "forecast.json";
    string filenameWind = "wind.json";

    string jsonForecast = File.ReadAllText(filenameForecast);
    string jsonWind = File.ReadAllText(filenameWind);

    // 1. Create a JsonNode DOM from a JSON string and make a few changes
    JsonNode? forecastNode = System.Text.Json.Nodes.JsonNode.Parse(jsonForecast);

    // Prepare options for pretty printing JSON
    var options = new JsonSerializerOptions { WriteIndented = true };

    // Print out the JSON object, notice there is no "wind" and description is there (I'll remove it later)
    if (forecastNode != null)
    {
        Console.WriteLine(forecastNode.ToJsonString(options));
    }
    Console.Clear();

    // Cast as JsonObject to get the number of elements
    Console.WriteLine(forecastNode.AsObject().Count());
    Console.Clear();

    // Get the list of all keys included in the object
    List<string> keys = forecastNode.AsObject().Select(child => child.Key).ToList();
    Console.WriteLine(string.Join(", ", keys));
    Console.Clear();

    // Get the value and type from one JsonNode
    JsonNode tempCelsius = forecastNode["temperaturecelsius"];
    Console.WriteLine($"Type={tempCelsius.GetType()}");
    Console.WriteLine($"JSON={tempCelsius.ToJsonString()}");
    Console.Clear();

    // Get a JSON array
    JsonNode keywords = forecastNode["keywords"];
    Console.WriteLine($"Type={keywords.GetType()}");
    Console.WriteLine($"JSON={keywords.ToJsonString()}");

    Console.WriteLine($"Type={keywords[0].GetType()}");
    Console.WriteLine($"JSON={keywords[0].ToJsonString()}");
    Console.Clear();

    // Properties can be chained, which is useful for extracting single values
    decimal coord = (decimal)forecastNode["coord"]["lon"];
    Console.WriteLine($"coord.lon={coord}");
    Console.Clear();

    // JsonNode DOM is mutable, new objects can be added and removed
    // Remove description
    string summary = forecastNode["description"].GetValue<string>();
    forecastNode.AsObject().Remove("description");
    if (forecastNode != null)
    {
        Console.WriteLine(forecastNode.ToJsonString(options));
    }
    Console.Clear();

    // Add summary and wind
    forecastNode.AsObject().Add("summary", summary);

    JsonNode? windNode = System.Text.Json.Nodes.JsonNode.Parse(jsonWind);
    forecastNode.AsObject().Add("wind", windNode);

    if (forecastNode != null)
    {
        Console.WriteLine(forecastNode.ToJsonString(options));
    }
    Console.Clear();

    // 2. Create a new JsonObject using object initializers
    var forecastObject = new JsonObject
    {
        ["date"] = new DateTime(2019, 8, 1),
        ["temperaturecelsius"] = 7,
        ["feelslike"] = 3.14,
        ["description"] = "overcast clouds",
        ["pressure"] = 1003,
        ["humidity"] = 79,
        ["coord"] = new JsonObject()
        {
            //["lon"] = 48.75,
            ["lat"] = 8.24
        },
        ["wind"] = new JsonObject()
        {
            ["speed"] = 11.32,
            ["deg"] = 200,
            ["gust"] = 17.49
        },
        ["keywords"] = new JsonArray("Chill", "Windy"),
        ["country"] = "DE",
        ["city"] = "Baden-Baden"
    };

    // Print the JSON object
    if (forecastObject != null)
    {
        Console.WriteLine(forecastObject.ToJsonString(options));
    }
    Console.Clear();

    // Add an object
    forecastObject["coord"]["lon"] = 48.75;

    // Remove a property
    forecastObject.Remove("keywords");

    // Change the value of a property
    forecastObject["date"] = new DateTime(2022, 1, 1);

    Console.WriteLine(forecastObject.ToJsonString(options));
    Console.Clear();
}

#endregion