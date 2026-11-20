namespace GestionElectronicos.Desktop.Formularios
{
    partial class FrmClienteModal
    {
        private System.ComponentModel.IContainer? components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing) components?.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblApellido = new System.Windows.Forms.Label();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.lblCorreo = new System.Windows.Forms.Label();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // lblTitulo
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Location = new System.Drawing.Point(30, 20);
            this.lblTitulo.Size = new System.Drawing.Size(450, 43);
            this.lblTitulo.Text = "Cliente";
            this.lblTitulo.AutoSize = false;
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 19F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(14, 36, 62);
            // lblNombre
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Location = new System.Drawing.Point(30, 82);
            this.lblNombre.Size = new System.Drawing.Size(450, 24);
            this.lblNombre.Text = "Nombre *";
            this.lblNombre.AutoSize = false;
            this.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            // txtNombre
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Location = new System.Drawing.Point(30, 107);
            this.txtNombre.Size = new System.Drawing.Size(440, 32);
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            // lblApellido
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.Location = new System.Drawing.Point(30, 146);
            this.lblApellido.Size = new System.Drawing.Size(450, 24);
            this.lblApellido.Text = "Apellido *";
            this.lblApellido.AutoSize = false;
            this.lblApellido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblApellido.Font = new System.Drawing.Font("Segoe UI", 10F);
            // txtApellido
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Location = new System.Drawing.Point(30, 171);
            this.txtApellido.Size = new System.Drawing.Size(440, 32);
            this.txtApellido.Font = new System.Drawing.Font("Segoe UI", 10F);
            // lblTelefono
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Location = new System.Drawing.Point(30, 210);
            this.lblTelefono.Size = new System.Drawing.Size(450, 24);
            this.lblTelefono.Text = "Teléfono";
            this.lblTelefono.AutoSize = false;
            this.lblTelefono.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTelefono.Font = new System.Drawing.Font("Segoe UI", 10F);
            // txtTelefono
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Location = new System.Drawing.Point(30, 235);
            this.txtTelefono.Size = new System.Drawing.Size(440, 32);
            this.txtTelefono.Font = new System.Drawing.Font("Segoe UI", 10F);
            // lblCorreo
            this.lblCorreo.Name = "lblCorreo";
            this.lblCorreo.Location = new System.Drawing.Point(30, 274);
            this.lblCorreo.Size = new System.Drawing.Size(450, 24);
            this.lblCorreo.Text = "Correo electrónico";
            this.lblCorreo.AutoSize = false;
            this.lblCorreo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCorreo.Font = new System.Drawing.Font("Segoe UI", 10F);
            // txtCorreo
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.Location = new System.Drawing.Point(30, 299);
            this.txtCorreo.Size = new System.Drawing.Size(440, 32);
            this.txtCorreo.Font = new System.Drawing.Font("Segoe UI", 10F);
            // btnCancelar
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Location = new System.Drawing.Point(30, 369);
            this.btnCancelar.Size = new System.Drawing.Size(210, 42);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(232, 237, 244);
            this.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(14, 36, 62);
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.UseVisualStyleBackColor = false;
            // btnGuardar
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Location = new System.Drawing.Point(260, 369);
            this.btnGuardar.Size = new System.Drawing.Size(210, 42);
            this.btnGuardar.Text = "Guardar cliente";
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(24, 54, 93);
            this.btnGuardar.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblApellido);
            this.Controls.Add(this.txtApellido);
            this.Controls.Add(this.lblTelefono);
            this.Controls.Add(this.txtTelefono);
            this.Controls.Add(this.lblCorreo);
            this.Controls.Add(this.txtCorreo);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.ClientSize = new System.Drawing.Size(500, 444);
            this.Name = "FrmClienteModal";
            this.Text = "Cliente";
            this.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label lblCorreo;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardar;
    }
}
