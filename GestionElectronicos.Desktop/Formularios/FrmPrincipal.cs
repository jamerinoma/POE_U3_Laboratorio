using GestionElectronicos.Desktop.Modelos;
using GestionElectronicos.Desktop.Servicios;

namespace GestionElectronicos.Desktop.Formularios;

public partial class FrmPrincipal : Form
{
    private LoginRespuesta? _sesion;
    private sealed record OpcionFiltro(int Id, string Etiqueta);

    public FrmPrincipal()
    {
        InitializeComponent();
        ConfigurarGrid(dgvProductos);
        ConfigurarGrid(dgvClientes);
        ConfigurarGrid(dgvVentas);
        ConfigurarGrid(dgvVendidos);
        ConfigurarGrid(dgvStock);
        Load += FrmPrincipal_Load;
        mnuProductos.Click += (_, _) => tabs.SelectedTab = tabProductos;
        mnuClientes.Click += (_, _) => tabs.SelectedTab = tabClientes;
        mnuVentas.Click += (_, _) => tabs.SelectedTab = tabVentas;
        mnuReportes.Click += (_, _) => tabs.SelectedTab = tabReportes;
        mnuSalir.Click += async (_, _) => { await CerrarSesionAsync(); };
        btnNuevoProducto.Click += async (_, _) => await AbrirProductoAsync(null);
        btnEditarProducto.Click += async (_, _) => await AbrirProductoAsync(Seleccionado<Producto>(dgvProductos));
        btnBorrarProducto.Click += async (_, _) => await BorrarProductoAsync();
        btnActualizarProductos.Click += async (_, _) => await CargarProductosAsync();
        dgvProductos.CellDoubleClick += async (_, e) => { if (e.RowIndex >= 0) await AbrirProductoAsync(Seleccionado<Producto>(dgvProductos)); };
        btnNuevoCliente.Click += async (_, _) => await AbrirClienteAsync(null);
        btnEditarCliente.Click += async (_, _) => await AbrirClienteAsync(Seleccionado<Cliente>(dgvClientes));
        btnBorrarCliente.Click += async (_, _) => await BorrarClienteAsync();
        btnActualizarClientes.Click += async (_, _) => await CargarClientesAsync();
        dgvClientes.CellDoubleClick += async (_, e) => { if (e.RowIndex >= 0) await AbrirClienteAsync(Seleccionado<Cliente>(dgvClientes)); };
        btnNuevaVenta.Click += async (_, _) => await AbrirVentaAsync();
        btnActualizarVentas.Click += async (_, _) => await CargarVentasAsync();
        btnConsultar.Click += async (_, _) =>
            await CargarReportesAsync(generarPdf: true);
        tabs.SelectedIndexChanged += async (_, _) =>
        {
            if (_sesion is null) return;
            if (tabs.SelectedTab == tabProductos) await CargarProductosAsync();
            else if (tabs.SelectedTab == tabClientes) await CargarClientesAsync();
            else if (tabs.SelectedTab == tabVentas) await CargarVentasAsync();
            else if (tabs.SelectedTab == tabReportes) await CargarReportesAsync();
        };
    }
    public FrmPrincipal(LoginRespuesta sesion) : this()
    {
        _sesion = sesion;
        lblSesion.Text = $"Sesión: {sesion.Nombre}, Usuario: {sesion.NombreUsuario}";
    }
    private static void ConfigurarGrid(DataGridView tabla)
    {
        tabla.ReadOnly = true;
        tabla.AllowUserToAddRows = false;
        tabla.AllowUserToDeleteRows = false;
        tabla.MultiSelect = false;
        tabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        tabla.BackgroundColor = Color.White;
        tabla.RowHeadersVisible = false;
        tabla.AutoGenerateColumns = true;
        tabla.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 42, 71);
        tabla.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        tabla.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        tabla.EnableHeadersVisualStyles = false;
    }
    private async void FrmPrincipal_Load(object? sender, EventArgs e)
    {
        if (_sesion is null) return;
        await CargarProductosAsync();
        await CargarClientesAsync();
        await CargarVentasAsync();
        await CargarOpcionesFiltroAsync();
        await CargarReportesAsync();
    }
    private static T? Seleccionado<T>(DataGridView tabla) where T : class =>
        tabla.CurrentRow?.DataBoundItem as T;
    private async Task CargarProductosAsync() => await SeguroAsync(async () =>
    {
        dgvProductos.DataSource = await ApiCliente.ProductosAsync();
        if (dgvProductos.Columns["Etiqueta"] is { } col) col.Visible = false;
    });
    private async Task CargarClientesAsync() => await SeguroAsync(async () =>
    {
        dgvClientes.DataSource = await ApiCliente.ClientesAsync();
        if (dgvClientes.Columns["Etiqueta"] is { } col) col.Visible = false;
    });
    private async Task CargarVentasAsync() => await SeguroAsync(async () =>
    {
        dgvVentas.DataSource = await ApiCliente.VentasAsync();
    });
    private async Task CargarOpcionesFiltroAsync()
    {
        await SeguroAsync(async () =>
        {
            var productos = await ApiCliente.ProductosAsync();
            var opciones = new List<OpcionFiltro> { new(0, "Todos los productos") };
            opciones.AddRange(productos.Select(x => new OpcionFiltro(x.Id, x.Nombre)));
            cboFiltro.DataSource = opciones;
            cboFiltro.DisplayMember = "Etiqueta";
            cboFiltro.ValueMember = "Id";
            cboFiltro.SelectedIndex = 0;
        });
    }
    private async Task CargarReportesAsync(
        bool generarPdf = false) => await SeguroAsync(async () =>
    {
        int seleccionado = (cboFiltro.SelectedItem as OpcionFiltro)?.Id ?? 0;
        int? id = seleccionado == 0 ? null : seleccionado;
        var ventas = await ApiCliente.VendidosAsync(id);
        var stock = await ApiCliente.StockAsync(id);
        dgvVendidos.DataSource = ventas;
        dgvStock.DataSource = stock;
        lblResumen.Text = $"Unidades vendidas: {ventas.Sum(x => x.Unidades)}   |   Ventas: ${ventas.Sum(x => x.Monto):N2}   |   Productos críticos: {stock.Count(x => x.Stock <= 5)}";
        // BONUS: semáforo visual, rojo <=5; ámbar 6..10; verde >10.
        foreach (DataGridViewRow fila in dgvStock.Rows)
        {
            if (fila.DataBoundItem is not StockReporte item) continue;
            fila.DefaultCellStyle.BackColor = item.Stock <= 5 ? Color.MistyRose
                : item.Stock <= 10 ? Color.LemonChiffon : Color.Honeydew;
            fila.DefaultCellStyle.ForeColor = item.Stock <= 5 ? Color.DarkRed : Color.FromArgb(25, 42, 60);
        }
        if (generarPdf)
        {
            string filtro = (cboFiltro.SelectedItem as OpcionFiltro)?
                .Etiqueta ?? "Todos los productos";

            ReportePdfServicio.Generar(ventas, stock, filtro);
        }
    });
    private async Task AbrirProductoAsync(Producto? producto)
    {
        using var modal = new FrmProductoModal(producto);
        if (modal.ShowDialog(this) != DialogResult.OK) return;
        await CargarProductosAsync();
        await CargarOpcionesFiltroAsync();
        await CargarReportesAsync();
    }
    private async Task BorrarProductoAsync()
    {
        var p = Seleccionado<Producto>(dgvProductos);
        if (p is null) { MessageBox.Show("Seleccione un producto."); return; }
        if (MessageBox.Show($"¿Eliminar '{p.Nombre}'?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        await SeguroAsync(async () =>
        {
            await ApiCliente.BorrarProductoAsync(p.Id);
            await CargarProductosAsync(); await CargarOpcionesFiltroAsync();
        });
    }
    private async Task AbrirClienteAsync(Cliente? cliente)
    {
        using var modal = new FrmClienteModal(cliente);
        if (modal.ShowDialog(this) != DialogResult.OK) return;
        await CargarClientesAsync();
    }
    private async Task BorrarClienteAsync()
    {
        var cliente = Seleccionado<Cliente>(dgvClientes);
        if (cliente is null) { MessageBox.Show("Seleccione un cliente."); return; }
        if (MessageBox.Show($"¿Eliminar '{cliente.Etiqueta}'?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
        await SeguroAsync(async () => { await ApiCliente.BorrarClienteAsync(cliente.Id); await CargarClientesAsync(); });
    }
    private async Task AbrirVentaAsync()
    {
        using var modal = new FrmVentaModal();
        if (modal.ShowDialog(this) != DialogResult.OK) return;
        await CargarVentasAsync(); await CargarProductosAsync();
        await CargarReportesAsync();
    }
    private async Task CerrarSesionAsync()
    {
        await ApiCliente.CerrarSesionAsync();
        Close();
    }
    private static async Task SeguroAsync(Func<Task> accion)
    {
        try { await accion(); }
        catch (ApiException ex) { MessageBox.Show(ex.Message, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        catch (Exception ex) { MessageBox.Show("Ocurrió un problema: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }
}
