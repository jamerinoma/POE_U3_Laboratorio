using GestionElectronicos.Api.Modelos;
using GestionElectronicos.Api.Repositorios;
using GestionElectronicos.Api.Servicios;
using Microsoft.Data.SqlClient;
using RepoDb;

GlobalConfiguration.Setup().UseSqlServer();
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5080");
builder.Services.AddSingleton<Sesiones>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(RepositorioGenerico<>));
builder.Services.AddScoped<Autenticacion>();
builder.Services.AddScoped<CatalogosServicio>();
builder.Services.AddScoped<VentasServicio>();
var app = builder.Build();

app.UseExceptionHandler(manejador => manejador.Run(async ctx =>
{
    var error = ctx.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;
    bool relacionado = error is SqlException sql && sql.Number == 547;
    bool duplicado = error is SqlException sql2 && sql2.Number is 2601 or 2627;
    ctx.Response.StatusCode = relacionado || duplicado ? 409 : 500;
    string mensaje = relacionado ? "No se puede eliminar: existen ventas relacionadas con este registro."
        : duplicado ? "Ya existe un registro con esa información única."
        : "Error del servidor. Revise la conexión SQL y los mensajes de la API.";
    await ctx.Response.WriteAsJsonAsync(new ErrorDto(mensaje));
}));

app.MapGet("/health", () => Results.Ok(new { estado = "API disponible", tecnologia = ".NET 10 + RepoDB" }));
app.MapPost("/api/auth/login", (Credenciales datos, Autenticacion servicio) =>
{
    var acceso = servicio.Ingresar(datos);
    return acceso is null ? (IResult)Results.Json(new ErrorDto("Usuario o contraseña incorrectos."), statusCode: 401)
        : Results.Ok(acceso);
});
app.MapPost("/api/auth/logout", (HttpContext ctx, Sesiones sesiones) =>
{
    sesiones.Cerrar(ctx);
    return Results.Ok();
});

app.MapGet("/api/productos", (HttpContext ctx, Sesiones sesiones, CatalogosServicio cat) =>
    sesiones.Obtener(ctx) is null ? (IResult)Results.Unauthorized() : Results.Ok(cat.ListarProductos()));
app.MapPost("/api/productos", (HttpContext ctx, Sesiones sesiones, CatalogosServicio cat, ProductoEntrada dato) =>
{
    if (sesiones.Obtener(ctx) is null) return (IResult)Results.Unauthorized();
    var (ok, mensaje) = cat.GuardarProducto(0, dato);
    return ok ? (IResult)Results.Ok(new { mensaje }) : Results.BadRequest(new ErrorDto(mensaje));
});
app.MapPut("/api/productos/{id:int}", (HttpContext ctx, Sesiones sesiones, CatalogosServicio cat, int id, ProductoEntrada dato) =>
{
    if (sesiones.Obtener(ctx) is null) return (IResult)Results.Unauthorized();
    var (ok, mensaje) = cat.GuardarProducto(id, dato);
    return ok ? (IResult)Results.Ok(new { mensaje }) : Results.BadRequest(new ErrorDto(mensaje));
});
app.MapDelete("/api/productos/{id:int}", (HttpContext ctx, Sesiones sesiones, IRepository<Productos> r, int id) =>
{
    if (sesiones.Obtener(ctx) is null) return (IResult)Results.Unauthorized();
    return r.Eliminar(id) == 1 ? (IResult)Results.Ok() : Results.NotFound(new ErrorDto("No se encontró el producto."));
});

app.MapGet("/api/clientes", (HttpContext ctx, Sesiones sesiones, CatalogosServicio cat) =>
    sesiones.Obtener(ctx) is null ? (IResult)Results.Unauthorized() : Results.Ok(cat.ListarClientes()));
app.MapPost("/api/clientes", (HttpContext ctx, Sesiones sesiones, CatalogosServicio cat, ClienteEntrada dato) =>
{
    if (sesiones.Obtener(ctx) is null) return (IResult)Results.Unauthorized();
    var (ok, mensaje) = cat.GuardarCliente(0, dato);
    return ok ? (IResult)Results.Ok(new { mensaje }) : Results.BadRequest(new ErrorDto(mensaje));
});
app.MapPut("/api/clientes/{id:int}", (HttpContext ctx, Sesiones sesiones, CatalogosServicio cat, int id, ClienteEntrada dato) =>
{
    if (sesiones.Obtener(ctx) is null) return (IResult)Results.Unauthorized();
    var (ok, mensaje) = cat.GuardarCliente(id, dato);
    return ok ? (IResult)Results.Ok(new { mensaje }) : Results.BadRequest(new ErrorDto(mensaje));
});
app.MapDelete("/api/clientes/{id:int}", (HttpContext ctx, Sesiones sesiones, IRepository<Clientes> r, int id) =>
{
    if (sesiones.Obtener(ctx) is null) return (IResult)Results.Unauthorized();
    return r.Eliminar(id) == 1 ? (IResult)Results.Ok() : Results.NotFound(new ErrorDto("No se encontró el cliente."));
});

app.MapGet("/api/ventas", (HttpContext ctx, Sesiones sesiones, VentasServicio svc) =>
    sesiones.Obtener(ctx) is null ? (IResult)Results.Unauthorized() : Results.Ok(svc.Historial()));
app.MapPost("/api/ventas", (HttpContext ctx, Sesiones sesiones, VentasServicio svc, NuevaVenta venta) =>
{
    var sesion = sesiones.Obtener(ctx);
    if (sesion is null) return (IResult)Results.Unauthorized();
    try { return (IResult)Results.Ok(new { idVenta = svc.Registrar(venta, sesion.IdEmpleado) }); }
    catch (ArgumentException ex) { return Results.BadRequest(new ErrorDto(ex.Message)); }
});
app.MapGet("/api/reportes/ventas", (HttpContext ctx, Sesiones sesiones, VentasServicio svc, int? productoId) =>
    sesiones.Obtener(ctx) is null ? (IResult)Results.Unauthorized() : Results.Ok(svc.ProductosVendidos(productoId)));
app.MapGet("/api/reportes/stock", (HttpContext ctx, Sesiones sesiones, VentasServicio svc, int? productoId) =>
    sesiones.Obtener(ctx) is null ? (IResult)Results.Unauthorized() : Results.Ok(svc.Inventario(productoId)));

app.Run();
