using System.ComponentModel;
using GestionElectronicos.Desktop.Modelos;
using GestionElectronicos.Desktop.Servicios;

namespace GestionElectronicos.Desktop.Formularios;

public partial class FrmVentaModal : Form
{
    private readonly BindingList<CarritoItem> _carrito = [];
    public FrmVentaModal()
    {
        InitializeComponent();
        dgvCarrito.AutoGenerateColumns = true;
        dgvCarrito.DataSource = _carrito;
        dgvCarrito.ReadOnly = true;
        dgvCarrito.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvCarrito.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _carrito.ListChanged += (_, _) => CalcularTotal();
        Load += FrmVentaModal_Load;
        btnAgregar.Click += btnAgregar_Click;
        btnQuitar.Click += (_, _) => QuitarSeleccion();
        btnGuardar.Click += btnGuardar_Click;
        btnCancelar.Click += (_, _) => { DialogResult = DialogResult.Cancel; Close(); };
    }
    private async void FrmVentaModal_Load(object? sender, EventArgs e)
    {
        try
        {
            cboCliente.DataSource = await ApiCliente.ClientesAsync();
            cboCliente.DisplayMember = "Etiqueta";
            cboCliente.ValueMember = "Id";
            cboProducto.DataSource = (await ApiCliente.ProductosAsync()).Where(x => x.Stock > 0).ToList();
            cboProducto.DisplayMember = "Etiqueta";
            cboProducto.ValueMember = "Id";
        }
        catch (ApiException ex) { MessageBox.Show(ex.Message, "Error de conexión"); Close(); }
    }
    private void btnAgregar_Click(object? sender, EventArgs e)
    {
        if (cboProducto.SelectedItem is not Producto p) return;
        int cantidad = (int)nudCantidad.Value;
        var existente = _carrito.FirstOrDefault(x => x.IdProducto == p.Id);
        if (cantidad + (existente?.Cantidad ?? 0) > p.Stock)
        { MessageBox.Show($"Existencia disponible: {p.Stock}.", "Stock insuficiente"); return; }
        if (existente is null)
            _carrito.Add(new CarritoItem { IdProducto = p.Id, Producto = p.Nombre,
                Cantidad = cantidad, Precio = p.Precio });
        else
        {
            existente.Cantidad += cantidad;
            dgvCarrito.Refresh();
            CalcularTotal();
        }
    }
    private void QuitarSeleccion()
    {
        if (dgvCarrito.CurrentRow?.DataBoundItem is CarritoItem item) _carrito.Remove(item);
    }
    private void CalcularTotal() => lblTotal.Text = $"TOTAL: ${_carrito.Sum(x => x.Subtotal):N2}";
    private async void btnGuardar_Click(object? sender, EventArgs e)
    {
        if (cboCliente.SelectedItem is not Cliente cliente || _carrito.Count == 0)
        { MessageBox.Show("Seleccione un cliente y agregue productos."); return; }
        btnGuardar.Enabled = false;
        try
        {
            int id = await ApiCliente.RegistrarVentaAsync(new VentaGuardar(cliente.Id,
                _carrito.Select(x => new DetalleVentaEntrada(x.IdProducto, x.Cantidad)).ToList()));
            MessageBox.Show($"Venta #{id} registrada. El inventario fue actualizado.", "Venta exitosa",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (ApiException ex) { MessageBox.Show(ex.Message, "No se pudo registrar", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        finally { btnGuardar.Enabled = true; }
    }
}
