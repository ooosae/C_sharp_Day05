# Day 05 – Bootcamp .NET
### Per aspera ad astra

# Contents
1. [Chapter I](#chapter-i) \
	[General Rules](#general-rules)
2. [Chapter II](#chapter-ii) \
	[Rules of the Day](#rules-of-the-day)
3. [Chapter III](#chapter-iii) \
	[Intro](#intro)
4. [Chapter IV](#chapter-iv) \
	[Exercise 00 – Configuration](#exercise-00-configuration)
5. [Chapter V](#chapter-v) \
  [Exercise 01 – Interfaces](#exercise-01-interfaces) 
6. [Chapter VI](#chapter-vi) \
  [Exercise 02 – Space of the day](#exercise-02-space-of-the-day) 
7. [Chapter VII](#chapter-vii) \
  [Exercise 03 – Get me some stars](#exercise-03-get-me-some-stars)

# Chapter I 

## General Rules
- Make sure you have [the .NET 5 SDK](<https://dotnet.microsoft.com/download>) installed on your computer and use it.
- Remember, your code will be read! Pay special attention to the design of your code and the naming of variables. Adhere to commonly accepted [C# Coding Conventions](<https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions>).
- Choose your own IDE that is convenient for you.
- The program must be able to run from the dotnet command line.
- Each of the exercise contains examples of input and output. The solution should use them as the correct format.
- At the beginning of each task, there is a list of allowed language constructs.
- If you find the problem difficult to solve, ask questions to other piscine participants, the Internet, Google or go to StackOverflow.
- Avoid **hard coding** and **"magic numbers"**.
- You demonstrate the complete solution, the correct result of the program is just one of the ways to check its correct operation. Therefore, when it is necessary to obtain a certain output as a result of the work of your programs, it is forbidden to show a pre-calculated result.
- Pay special attention to the terms highlighted in **bold** font: their study will be useful to you both in performing the current task, and in your future career of a .NET developer.
- Have fun :)


# Chapter II

##  Rules of the Day
- Use a console application created based on a standard .NET SDK template.
- Use **var**.
- The name of the solution and its project (and its separate catalog) is d_{xx}, where xx are the digits of the current day. The names of the projects are specified in the exercise.
- To format the output data, use the en-GB culture: N2 for the output of monetary amounts, d for dates.

# Chapter III
## Intro

We are all a bit romantic. We’re interested and admired by the boundless expanses of the starry sky and the mysterious space unknown. But is it really the unknown? Because, sometimes, the particles of the boundless space are very, very close to the Earth. And space can also be photographed.

NASA has a set of [API](<https://api.nasa.gov/>)s (**Application Programming Interface**) with a variety of information about the expanses of our Universe. There, in the “Browse APIs” tab, you can find an impressive list of use cases. So why don’t we try to look at the space with the help of programming?

Poyekhali! Let's go! ©

In today's exercises, you will need to implement access to the NASA API methods, get data about the objects of interest to us and output them in the required format.

## What's new

- Application Configuration
- API, REST
- HTTP requests, idempotency
- Async/Await, TAP
- Splitting application into projects
- Covariance and contravariance

## Project structure:
```
d05/
     d05.Nasa/
           Apod/
                Models/
                      MediaOfToday.cs
                ApodClient.cs
           INasaClient.cs
     d05.Host/
           Program.cs
           appsettings.json
```

# Chapter IV
## Exercise 00 – Configuration

To start working with the **API**, you must get an access key, **API Key**: this can be done on the “Generate API Key" tab. We will use it in our methods, but it is not safe to store keys and passwords (and other sensitive data) right in the application code. Remember this as the truth! 

Install [Microsoft.Extensions.Configuration.Json](<https://www.nuget.org/packages/Microsoft.Extensions.Configuration.Json/6.0.0-preview.4.21253.7>) and output the key to the appsettings.json configuration file:

```
{
   "ApiKey": "{ApiKey}"
}

```
In order to be able to publish your developments in Nuget and reuse them in future, divide the solution code into two projects: the *d05.Nasa* class library, which will be responsible for the logic of receiving data, and the *d05.Host* console application, which will use the received data and deal with its output.

> Check yourself after implementation: the *d05.Nasa* project should not have links to the application configuration libraries (**Microsoft.Extensions.Configuration** and **Microsoft.Extensions.Configuration.Json**). The *d05.Host* project should not use the **System.Net.Http** and **System.Text.Json** libraries. The responsibility is divided. The **coupling** of the projects is loose.

In the *d05.Host* console application, you only need the *appsettings.json* configuration and the entry point - the *Main* method of the *Program.cs* class, which will load the configuration, read the input and respond with the requested information.

The *d05.Nasa* library will be responsible for communication with space and contain client implementations for each of the exercises.


# Chapter V
## Exercise 01 – Interfaces

Create an *INasaClient* interface with the *GetAsync* data acquisition method. Whatever we get from NASA, it will be responsible for this.

Make it so that the input and output parameters in this method can be different for each of the implementations. **In** and **out** keywords will help you, they will enable implementations to set the types of input and output parameters themselves. Such an interface is called **contravariant** and **covariant**, respectively: *INasaClient<in TIn, out TOut> with the method TOut GetAsync(TIn input)*.

This is necessary to comply with the **dependency inversion principle**. 

Create three different implementations of the interface with different **in** and **out** parameters (as you wish).


# Chapter VI
## Exercise 02 - Space of the day

“We are just an advanced breed of monkeys on a minor planet of a very average star. But we can understand the Universe. That makes us something very special.”

**― Stephen Hawking**

Consider "APOD (Astronomy Picture of the Day)" - a resource that returns a collection of space photos (or videos) of the day, one of the most popular NASA resources. According to them "It has the popular appeal of a Justin Bieber video”.  Your objective is to output the first N photos of the day to the console. N is set from the console.

You can find the documentation for the “APOD " API by going to the [API](<https://api.nasa.gov/>) -> Browse APIs -> APOD website. Here you can read about the parameters, the link for the GET request and see an example of the returned data.

Based on the example request provided on the website, create the *MediaOfToday* class. The structure of the required data:

```
{
      copyright: "Ignacio Diaz Bobillo",
      date: "2021-06-03",
      explanation: "Globular star cluster Omega Centauri, also known as NGC 5139, is some 15,000 light-years away. The cluster is packed with about 10 million stars much older than the Sun within a volume about 150 light-years in diameter. It's the largest and brightest of 200 or so known globular clusters that roam the halo of our Milky Way galaxy. Though most star clusters consist of stars with the same age and composition, the enigmatic Omega Cen exhibits the presence of different stellar populations with a spread of ages and chemical abundances. In fact, Omega Cen may be the remnant core of a small galaxy merging with the Milky Way. Omega Centauri's red giant stars (with a yellowish hue) are easy to pick out in this sharp, color telescopic view.",
      title: "Millions of Stars in Omega Centauri",
      url: "https://apod.nasa.gov/apod/image/2106/OmegaCent_LRGB_final1_1024.jpg"
}

```
# Chapter VII
## Exercise 03 – Get me some stars

Implement the *ApodClient* class:

```
public class ApodClient : INasaClient<int, Task<MediaOfToday[]>>
```

Implement the *GetAsync* method, it should output the first N elements. Make the input parameter the number of results (**in** type). The method will return a collection of *MediaOfToday* objects. Note that the **async** methods return a **Task**.

Implement an **HTTP GET** request to the NASA API using **HttpClient** and deserialize the response to the *MediaOfToday* list. 

If the request was not executed successfully (a successful response has [StatusCode == 200 (OK)](https://docs.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode?view=net-5.0), you need to output its contents to the console.

Since HTTP requests are an **I/O operation**, to prevent the program from blocking while executing a request to a remote resource (API server), class methods containing these requests must be **asynchronous**. The **async/await** keywords will help you with this.

For a better understanding of what's what, read about [asynchronous breakfasts](<https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/>).

### Naming

Note that asynchronous methods have the Async postfix in the method name. This is good manners when developing libraries. Remember about [C# Coding Conventions](<https://docs.microsoft.com/ru-ru/dotnet/csharp/programming-guide/inside-a-program/coding-conventions>)!

Variables in C# must be **PascalCasing** or **CamelCase**, for parsing use **JsonPropertyName annotations**.

### Input parameters

We will set the number of results from the command line.

|Argument|Type|Description|
|---|---|---|
| ResultCount |int | The number of elements to output |

### Configuration Parameters

|Argument|Type|Description|
|---|---|---|
| ApiKey |string | API key |

### Response format

```
{mediaOfToday.Date}
‘{mediaOfToday.Title}’ by {mediaOfToday.Copyright}
{mediaOfToday.Explanation}
{mediaOfToday.Url}

{mediaOfToday.Date}
‘{mediaOfToday.Title}’
{mediaOfToday.Explanation}
{mediaOfToday.Url}
...
{mediaOfToday.Date}
‘{mediaOfToday.Title}’
{mediaOfToday.Explanation}
{mediaOfToday.Url}.
```

### An error occurred while calling the API method

```
GET “{apiUrl}” returned {StatusCode}:
{Content}
```

### Examples of launching an application from the project folder

```
{
   "ApiKey": "API_KEY"
}

$dotnet run apod 5
19/01/2018
'Clouds in the LMC' by Josep Drudis
An alluring sight in southern skies, the Large Magellanic Cloud (LMC) is seen in this deep and detailed telescopic mosaic. Recorded with broadband and narrowband filters, the scene spans some 5 degrees or 10 full moons. The narrowba
nd filters are designed to transmit only light emitted by hydrogen, and oxygen atoms. Ionized by energetic starlight, the atoms emit their characteristic light as electrons are recaptured and the atoms transition to a lower energy s
tate. As a result, in this image the LMC seems covered with its own clouds of ionized gas surrounding its massive, young stars. Sculpted by the strong stellar winds and ultraviolet radiation, the glowing clouds, dominated by emissio
n from hydrogen, are known as H II (ionized hydrogen) regions. Itself composed of many overlapping H II regions, the Tarantula Nebula is the large star forming region at the left. The largest satellite of our Milky Way Galaxy, the L
MC is about 15,000 light-years across and lies a mere 160,000 light-years away toward the constellation Dorado.
https://apod.nasa.gov/apod/image/1801/LMC_RGBNB-Don-Josep5-Cc1024.jpg

04/04/2008
'Layers in Aureum Chaos'
At first glance these undulating shapes in shades of blue might look like waves on an ocean. Seen here in a false-color image from the Mars Reconnaissance Orbiter's HiRISE camera, they are actually layered rock outcrops found in Aur
eum Chaos. The larger Aureum Chaos region is a chaotic jumble of eroded terrain in the eastern part of Mars' immense canyon Valles Marineris. Distinct layers composing these outcrops could have been laid down by dust or volcanic ash
 settling from the atmosphere, sand carried by martian winds, or sediments deposited on the floor of an ancient lake. This close-up view of the otherwise red planet spans about 4 kilometers, a distance you might walk over flat groun
d in less than an hour.   digg_url = 'http://apod.nasa.gov/apod/ap080404.html'; digg_skin = 'compact';
https://apod.nasa.gov/apod/image/0804/PSP_007006_1765_e800.jpg

31/10/2010
'Halloween and the Ghost Head Nebula'
Halloween's origin is ancient and astronomical.  Since the fifth century BC, Halloween has been celebrated as a cross-quarter day, a day halfway between an equinox (equal day / equal night) and a solstice (minimum day / maximum nigh
t in the northern hemisphere).  With a modern calendar, however, the real cross-quarter day will occur next week.  Another cross-quarter day is Groundhog's Day. Halloween's modern celebration retains historic roots in dressing to sc
are away the spirits of the dead.  Perhaps a fitting tribute to this ancient holiday is this view of the Ghost Head Nebula taken with the Hubble Space Telescope.  Similar to the icon of a fictional ghost, NGC 2080 is actually a star
 forming region in the Large Magellanic Cloud, a satellite galaxy of our own Milky Way Galaxy.  The Ghost Head Nebula spans about 50 light-years and is shown in representative colors.
https://apod.nasa.gov/apod/image/1010/ngc2080_hst.jpg

15/01/1997
'Black Holes Signature From Advective Disks
Research Credit:'
 star. perhaps brighter than allowable from an ADAF onto a neutronservationsws (ADAFs).
https://apod.nasa.gov/apod/image/9701/xraybin_heasarc.gif

13/02/2007
'Vela Supernova Remnant in Visible Light'
The explosion is over but the consequences continue.  About eleven thousand years ago a star in the constellation of Vela could be seen to explode, creating a strange point of light briefly visible to humans living near the beginnin
g of recorded history.  The outer layers of the star crashed into the interstellar medium, driving a shock wave that is still visible today.  A roughly spherical, expanding shock wave is visible in X-rays. The above image captures m
uch of that filamentary and gigantic shock in visible light, spanning almost 100 light years and appearing twenty times the diameter of the full moon. As gas flies away from the detonated star, it decays and reacts with the interste
llar medium, producing light in many different colors and energy bands. Remaining at the center of the Vela Supernova Remnant is a pulsar, a star as dense as nuclear matter that completely rotates more than ten times in a single sec
ond.
https://apod.nasa.gov/apod/image/0702/vela_skyfactory.jpg

{
   "ApiKey": ""
}
```

```
$ dotnet run apod 5
GET
“https://api.nasa.gov/neo/rest/v1/feed?start_date=2015-09-07&end_date=2015-09-08&api_key=” returned Forbidden:
{
  "error": {
    "code": "API_KEY_MISSING",
    "message": "No api_key was supplied. Get one at https://api.nasa.gov:443"
  }
}
```
