using System.Net.Http.Json;
using d05.Nasa.Apod.Models;

namespace d05.Nasa.Apod
{
    public class ApodClient : INasaClient<int, Task<MediaOfToday[]>>
    {
        private readonly string _apiKey;
        private const string ApiUrl = "https://api.nasa.gov/planetary/apod";

        public ApodClient(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<MediaOfToday[]> GetAsync(int resultCount)
        {
            return await GetAsyncInternal(resultCount).ConfigureAwait(false);
        }

        private async Task<MediaOfToday[]> GetAsyncInternal(int resultCount)
        {
            // Создаем новый экземпляр HttpClient, который будет использоваться для выполнения HTTP-запросов
            using (var httpClient = new HttpClient())
            {
                // Формируем URL для запроса, включая API ключ и количество запрашиваемых элементов
                var apiUrlWithKey = $"{ApiUrl}?api_key={_apiKey}&count={resultCount}";

                try
                {
                    // Выполняем асинхронный GET-запрос по указанному URL
                    var response = await httpClient.GetAsync(apiUrlWithKey).ConfigureAwait(false);

                    // Проверяем, успешен ли был запрос
                    if (response.IsSuccessStatusCode)
                    {
                        // Если успешен, читаем содержимое ответа и десериализуем его в массив объектов MediaOfToday
                        var content = await response.Content.ReadFromJsonAsync<MediaOfToday[]>().ConfigureAwait(false);
                        // Возвращаем полученный массив, если он не является null; в противном случае возвращаем пустой массив
                        return content ?? Array.Empty<MediaOfToday>();
                    }
                    else
                    {
                        // Если запрос не был успешным, выводим информацию о коде статуса и содержимом ответа
                        Console.WriteLine($"GET \"{apiUrlWithKey}\" returned {response.StatusCode}:");
                        Console.WriteLine(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                    }
                }
                catch (Exception ex)
                {
                    // Если произошла ошибка в процессе выполнения запроса, выводим сообщение об ошибке
                    Console.WriteLine($"An error occurred while calling the API method: {ex.Message}");
                }

                // Возвращаем пустой массив в случае ошибки
                return Array.Empty<MediaOfToday>();
            }
        }

        public string GetApiKey()
        {
            return _apiKey;
        }
    }
}
