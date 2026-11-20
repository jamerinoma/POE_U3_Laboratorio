using GestionElectronicos.Desktop.Modelos;
using GestionElectronicos.Desktop.Servicios;

namespace GestionElectronicos.Desktop.Formularios;

public partial class FrmProductoModal : Form
{
    private int _id;
    public FrmProductoModal()
    {
        InitializeComponent();
        btnGuardar.Click += btnGuardar_Click;
        btnCancelar.Click += (_, _) => { DialogResult = DialogResult.Cancel; Close(); };
        AcceptButton = btnGuardar;
        CancelButton = btnCancelar;
    }
    public FrmProductoModal(Producto? producto) : this()
    {
        if (producto is null) return;
        _id = producto.Id;
        Text = "Editar producto";
        lblTitulo.Text = "Editar producto";
        txtNombre.Text = producto.Nombre;
        txtDescripcion.Text = producto.Descripcion;
        nudPrecio.Value = Math.Clamp(producto.Precio, nudPrecio.Minimum, nudPrecio.Maximum);
        nudStock.Value = Math.Clamp(producto.Stock, nudStock.Minimum, nudStock.Maximum);
    }
    private async void btnGuardar_Click(object? sender, EventArgs e)
    {
        if (txtNombre.Text.Trim().Length < 2)
        { MessageBox.Show("Ingrese un nombre de al menos 2 caracteres."); return; }
        if (nudPrecio.Value <= 0)
        { MessageBox.Show("El precio debe ser mayor que cero."); return; }
        btnGuardar.Enabled = false;
        try
        {
            await ApiCliente.GuardarProductoAsync(_id, new ProductoGuardar(txtNombre.Text.Trim(),
                txtDescripcion.Text.Trim(), nudPrecio.Value, (int)nudStock.Value));
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (ApiException ex) { MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        finally { btnGuardar.Enabled = true; }
    }
}
