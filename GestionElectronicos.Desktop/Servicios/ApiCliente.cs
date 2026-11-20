using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using GestionElectronicos.Desktop.Modelos;

namespace GestionElectronicos.Desktop.Servicios;

public sealed class ApiException : Exception
{
    public ApiException(string mensaje) : base(mensaje) { }
}

public static class ApiCliente
{
    private static readonly HttpClient Http = new() { Timeout = TimeSpan.FromSeconds(20) };
    private static readonly JsonSerializerOptions JsonOpciones = new(JsonSerializerDefaults.Web);

    static ApiCliente()
    {
        string ruta = Path.Combine(AppContext.BaseDirectory, "configuracion.json");
        string baseUrl = "http://localhost:5080/";
        if (File.Exists(ruta))
        {
            var configuracion = JsonSerializer.Deserialize<Dictionary<string, string>>(
                File.ReadAllText(ruta), JsonOpciones);
            if (configuracion is not null && configuracion.TryGetValue("ApiUrl", out string? url)
                && Uri.TryCreate(url, UriKind.Absolute, out var uri)) baseUrl = uri.ToString();
        }
        Http.BaseAddress = new Uri(baseUrl.TrimEnd('/') + "/");
    }

    public static async Task<LoginRespuesta> IngresarAsync(string usuario, string clave)
    {
        using var respuesta = await EnviarAsync(() => Http.PostAsJsonAsync(
            "api/auth/login", new { NombreUsuario = usuario, Clave = clave }));
        var login = await respuesta.Content.ReadFromJsonAsync<LoginRespuesta>(JsonOpciones)
            ?? throw new ApiException("La API no devolvió información de sesión.");
        Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", login.Token);
        return login;
    }

    public static async Task CerrarSesionAsync()
    {
        try { using var respuesta = await Http.PostAsync("api/auth/logout", null); }
        catch (HttpRequestException) { }
        catch (TaskCanceledException) { }
        finally { Http.DefaultRequestHeaders.Authorization = null; }
    }

    public static Task<List<Producto>> ProductosAsync() => LeerListaAsync<Producto>("api/productos");
    public static Task<List<Cliente>> ClientesAsync() => LeerListaAsync<Cliente>("api/clientes");
    public static Task<List<VentaListado>> VentasAsync() => LeerListaAsync<VentaListado>("api/ventas");
    public static Task<List<VentasPorProducto>> VendidosAsync(int? producto = null) =>
        LeerListaAsync<VentasPorProducto>("api/reportes/ventas" + (producto is null ? "" : $"?productoId={producto}"));
    public static Task<List<StockReporte>> StockAsync(int? producto = null) =>
        LeerListaAsync<StockReporte>("api/reportes/stock" + (producto is null ? "" : $"?productoId={producto}"));

    private static async Task<List<T>> LeerListaAsync<T>(string ruta)
    {
        using var respuesta = await EnviarAsync(() => Http.GetAsync(ruta));
        return (await respuesta.Content.ReadFromJsonAsync<List<T>>(JsonOpciones)) ?? [];
    }

    public static async Task GuardarProductoAsync(int id, ProductoGuardar dto)
    {
        using var r = await EnviarAsync(() => id == 0
            ? Http.PostAsJsonAsync("api/productos", dto)
            : Http.PutAsJsonAsync($"api/productos/{id}", dto));
    }
    public static async Task BorrarProductoAsync(int id)
    {
        using var r = await EnviarAsync(() => Http.DeleteAsync($"api/productos/{id}"));
    }
    public static async Task GuardarClienteAsync(int id, ClienteGuardar dto)
    {
        using var r = await EnviarAsync(() => id == 0
            ? Http.PostAsJsonAsync("api/clientes", dto)
            : Http.PutAsJsonAsync($"api/clientes/{id}", dto));
    }
    public static async Task BorrarClienteAsync(int id)
    {
        using var r = await EnviarAsync(() => Http.DeleteAsync($"api/clientes/{id}"));
    }
    public static async Task<int> RegistrarVentaAsync(VentaGuardar dto)
    {
        using var respuesta = await EnviarAsync(() => Http.PostAsJsonAsync("api/ventas", dto));
        using var json = await JsonDocument.ParseAsync(await respuesta.Content.ReadAsStreamAsync());
        return json.RootElement.GetProperty("idVenta").GetInt32();
    }
    private static async Task<HttpResponseMessage> EnviarAsync(Func<Task<HttpResponseMessage>> peticion)
    {
        HttpResponseMessage respuesta;
        try { respuesta = await peticion(); }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException)
        { throw new ApiException("No se pudo conectar con la API. Ejecute primero GestionElectronicos.Api (puerto 5080)."); }
        if (respuesta.IsSuccessStatusCode) return respuesta;
        string texto = await respuesta.Content.ReadAsStringAsync();
        string mensaje;
        try
        {
            using var json = JsonDocument.Parse(texto);
            mensaje = json.RootElement.TryGetProperty("mensaje", out var m)
                ? m.GetString() ?? "Error de la API" : texto;
        }
        catch (JsonException) { mensaje = texto; }
        respuesta.Dispose();
        if (respuesta.StatusCode == HttpStatusCode.Unauthorized)
            throw new ApiException("Sesión vencida o acceso no autorizado. Vuelva a iniciar sesión.");
        throw new ApiException(mensaje.Length > 300 ? mensaje[..300] : mensaje);
    }
}
