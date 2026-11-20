using GestionElectronicos.Desktop.Servicios;

namespace GestionElectronicos.Desktop.Formularios;

public partial class FrmLogin : Form
{
    public FrmLogin()
    {
        InitializeComponent();
        AcceptButton = btnIngresar;
        btnIngresar.Click += btnIngresar_Click;
        chkMostrar.CheckedChanged += (_, _) => txtClave.UseSystemPasswordChar = !chkMostrar.Checked;
        txtUsuario.TextChanged += (_, _) => lblMensaje.Text = "";
        txtClave.TextChanged += (_, _) => lblMensaje.Text = "";
    }

    private async void btnIngresar_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtUsuario.Text) || txtClave.Text.Length == 0)
        { lblMensaje.Text = "Ingrese usuario y contraseña."; return; }
        btnIngresar.Enabled = false;
        try
        {
            var sesion = await ApiCliente.IngresarAsync(txtUsuario.Text.Trim(), txtClave.Text);
            txtClave.Clear(); lblMensaje.Text = "";
            Hide();
            using (var principal = new FrmPrincipal(sesion)) principal.ShowDialog(this);
            Show();
            txtClave.Focus();
        }
        catch (ApiException ex) { lblMensaje.Text = ex.Message; }
        catch (Exception ex) { lblMensaje.Text = "Error al ingresar: " + ex.Message; }
        finally { btnIngresar.Enabled = true; }
    }
}
