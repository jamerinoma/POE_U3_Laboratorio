namespace GestionElectronicos.Desktop.Formularios
{
    partial class FrmLogin
    {
        private System.ComponentModel.IContainer? components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing) components?.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblBanner = new Label();
            lblTitulo = new Label();
            lblUsuario = new Label();
            txtUsuario = new TextBox();
            lblClave = new Label();
            txtClave = new TextBox();
            chkMostrar = new CheckBox();
            btnIngresar = new Button();
            lblMensaje = new Label();
            SuspendLayout();
            // 
            // lblBanner
            // 
            lblBanner.BackColor = Color.FromArgb(14, 36, 62);
            lblBanner.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            lblBanner.ForeColor = Color.FromArgb(255, 255, 255);
            lblBanner.Location = new Point(0, 0);
            lblBanner.Name = "lblBanner";
            lblBanner.Size = new Size(470, 92);
            lblBanner.TabIndex = 0;
            lblBanner.Text = "GESTIÓN DE VENTAS";
            lblBanner.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTitulo
            // 
            lblTitulo.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(14, 36, 62);
            lblTitulo.Location = new Point(42, 106);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(382, 44);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "Iniciar sesión";
            lblTitulo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblUsuario
            // 
            lblUsuario.Font = new Font("Segoe UI", 10F);
            lblUsuario.ForeColor = Color.FromArgb(31, 52, 77);
            lblUsuario.Location = new Point(42, 165);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(382, 27);
            lblUsuario.TabIndex = 2;
            lblUsuario.Text = "Nombre de usuario";
            lblUsuario.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtUsuario
            // 
            txtUsuario.Font = new Font("Segoe UI", 10F);
            txtUsuario.Location = new Point(42, 195);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(382, 25);
            txtUsuario.TabIndex = 3;
            // 
            // lblClave
            // 
            lblClave.Font = new Font("Segoe UI", 10F);
            lblClave.ForeColor = Color.FromArgb(31, 52, 77);
            lblClave.Location = new Point(42, 245);
            lblClave.Name = "lblClave";
            lblClave.Size = new Size(382, 27);
            lblClave.TabIndex = 4;
            lblClave.Text = "Contraseña";
            lblClave.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtClave
            // 
            txtClave.Font = new Font("Segoe UI", 10F);
            txtClave.Location = new Point(42, 272);
            txtClave.Name = "txtClave";
            txtClave.Size = new Size(382, 25);
            txtClave.TabIndex = 5;
            txtClave.UseSystemPasswordChar = true;
            // 
            // chkMostrar
            // 
            chkMostrar.Font = new Font("Segoe UI", 10F);
            chkMostrar.Location = new Point(42, 316);
            chkMostrar.Name = "chkMostrar";
            chkMostrar.Size = new Size(180, 28);
            chkMostrar.TabIndex = 6;
            chkMostrar.Text = "Mostrar contraseña";
            // 
            // btnIngresar
            // 
            btnIngresar.BackColor = Color.FromArgb(24, 54, 93);
            btnIngresar.FlatAppearance.BorderSize = 0;
            btnIngresar.FlatStyle = FlatStyle.Flat;
            btnIngresar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnIngresar.ForeColor = Color.FromArgb(255, 255, 255);
            btnIngresar.Location = new Point(42, 362);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(382, 45);
            btnIngresar.TabIndex = 7;
            btnIngresar.Text = "INGRESAR";
            btnIngresar.UseVisualStyleBackColor = false;
            // 
            // lblMensaje
            // 
            lblMensaje.Font = new Font("Segoe UI", 9F);
            lblMensaje.ForeColor = Color.FromArgb(198, 40, 40);
            lblMensaje.Location = new Point(42, 417);
            lblMensaje.Name = "lblMensaje";
            lblMensaje.Size = new Size(382, 50);
            lblMensaje.TabIndex = 8;
            lblMensaje.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // FrmLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 255, 255);
            ClientSize = new Size(470, 487);
            Controls.Add(lblBanner);
            Controls.Add(lblTitulo);
            Controls.Add(lblUsuario);
            Controls.Add(txtUsuario);
            Controls.Add(lblClave);
            Controls.Add(txtClave);
            Controls.Add(chkMostrar);
            Controls.Add(btnIngresar);
            Controls.Add(lblMensaje);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema de Gestión de Ventas | Login";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label lblBanner;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblClave;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.CheckBox chkMostrar;
        private System.Windows.Forms.Button btnIngresar;
        private System.Windows.Forms.Label lblMensaje;
    }
}
