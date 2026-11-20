using GestionElectronicos.Desktop.Modelos;
using GestionElectronicos.Desktop.Servicios;

namespace GestionElectronicos.Desktop.Formularios;

public partial class FrmClienteModal : Form
{
    private int _id;
    public FrmClienteModal()
    {
        InitializeComponent();
        btnGuardar.Click += btnGuardar_Click;
        btnCancelar.Click += (_, _) => { DialogResult = DialogResult.Cancel; Close(); };
        AcceptButton = btnGuardar;
        CancelButton = btnCancelar;
    }
    public FrmClienteModal(Cliente? cliente) : this()
    {
        if (cliente is null) return;
        _id = cliente.Id;
        Text = "Editar cliente";
        lblTitulo.Text = "Editar cliente";
        txtNombre.Text = cliente.Nombre;
        txtApellido.Text = cliente.Apellido;
        txtTelefono.Text = cliente.Telefono;
        txtCorreo.Text = cliente.Email;
    }
    private async void btnGuardar_Click(object? sender, EventArgs e)
    {
        if (txtNombre.Text.Trim().Length < 2 || txtApellido.Text.Trim().Length < 2)
        { MessageBox.Show("Ingrese nombre y apellido válidos."); return; }
        btnGuardar.Enabled = false;
        try
        {
            await ApiCliente.GuardarClienteAsync(_id, new ClienteGuardar(txtNombre.Text.Trim(),
                txtApellido.Text.Trim(), txtTelefono.Text.Trim(), txtCorreo.Text.Trim()));
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (ApiException ex) { MessageBox.Show(ex.Message, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        finally { btnGuardar.Enabled = true; }
    }
}
