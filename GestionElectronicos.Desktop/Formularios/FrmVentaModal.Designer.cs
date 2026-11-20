namespace GestionElectronicos.Desktop.Formularios
{
    partial class FrmVentaModal
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
            this.lblCliente = new System.Windows.Forms.Label();
            this.cboCliente = new System.Windows.Forms.ComboBox();
            this.lblProducto = new System.Windows.Forms.Label();
            this.cboProducto = new System.Windows.Forms.ComboBox();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.dgvCarrito = new System.Windows.Forms.DataGridView();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // lblTitulo
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Location = new System.Drawing.Point(24, 14);
            this.lblTitulo.Size = new System.Drawing.Size(780, 43);
            this.lblTitulo.Text = "Registrar venta";
            this.lblTitulo.AutoSize = false;
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(14, 36, 62);
            // lblCliente
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Location = new System.Drawing.Point(24, 71);
            this.lblCliente.Size = new System.Drawing.Size(780, 27);
            this.lblCliente.Text = "Cliente";
            this.lblCliente.AutoSize = false;
            this.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCliente.Font = new System.Drawing.Font("Segoe UI", 10F);
            // cboCliente
            this.cboCliente.Name = "cboCliente";
            this.cboCliente.Location = new System.Drawing.Point(24, 100);
            this.cboCliente.Size = new System.Drawing.Size(780, 34);
            this.cboCliente.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            // lblProducto
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Location = new System.Drawing.Point(24, 150);
            this.lblProducto.Size = new System.Drawing.Size(780, 27);
            this.lblProducto.Text = "Producto";
            this.lblProducto.AutoSize = false;
            this.lblProducto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblProducto.Font = new System.Drawing.Font("Segoe UI", 10F);
            // cboProducto
            this.cboProducto.Name = "cboProducto";
            this.cboProducto.Location = new System.Drawing.Point(24, 180);
            this.cboProducto.Size = new System.Drawing.Size(535, 34);
            this.cboProducto.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            // lblCantidad
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Location = new System.Drawing.Point(580, 150);
            this.lblCantidad.Size = new System.Drawing.Size(110, 27);
            this.lblCantidad.Text = "Cantidad";
            this.lblCantidad.AutoSize = false;
            this.lblCantidad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCantidad.Font = new System.Drawing.Font("Segoe UI", 10F);
            // nudCantidad
            this.nudCantidad.Name = "nudCantidad";
            this.nudCantidad.Location = new System.Drawing.Point(580, 180);
            this.nudCantidad.Size = new System.Drawing.Size(95, 34);
            this.nudCantidad.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudCantidad.Maximum = new decimal(10000);
            this.nudCantidad.Minimum = new decimal(1);
            this.nudCantidad.Value = new decimal(1);
            // btnAgregar
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Location = new System.Drawing.Point(690, 178);
            this.btnAgregar.Size = new System.Drawing.Size(115, 37);
            this.btnAgregar.Text = "Añadir";
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(17, 125, 146);
            this.btnAgregar.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.FlatAppearance.BorderSize = 0;
            this.btnAgregar.UseVisualStyleBackColor = false;
            // dgvCarrito
            this.dgvCarrito.Name = "dgvCarrito";
            this.dgvCarrito.Location = new System.Drawing.Point(24, 237);
            this.dgvCarrito.Size = new System.Drawing.Size(780, 262);
            this.dgvCarrito.BackgroundColor = System.Drawing.Color.White;
            this.dgvCarrito.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // btnQuitar
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Location = new System.Drawing.Point(24, 512);
            this.btnQuitar.Size = new System.Drawing.Size(175, 39);
            this.btnQuitar.Text = "Quitar selección";
            this.btnQuitar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnQuitar.BackColor = System.Drawing.Color.FromArgb(232, 237, 244);
            this.btnQuitar.ForeColor = System.Drawing.Color.FromArgb(14, 36, 62);
            this.btnQuitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuitar.FlatAppearance.BorderSize = 0;
            this.btnQuitar.UseVisualStyleBackColor = false;
            // lblTotal
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Location = new System.Drawing.Point(520, 514);
            this.lblTotal.Size = new System.Drawing.Size(285, 36);
            this.lblTotal.Text = "TOTAL: $0.00";
            this.lblTotal.AutoSize = false;
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(14, 36, 62);
            // btnCancelar
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Location = new System.Drawing.Point(445, 565);
            this.btnCancelar.Size = new System.Drawing.Size(170, 42);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(232, 237, 244);
            this.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(14, 36, 62);
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.UseVisualStyleBackColor = false;
            // btnGuardar
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Location = new System.Drawing.Point(634, 565);
            this.btnGuardar.Size = new System.Drawing.Size(170, 42);
            this.btnGuardar.Text = "CONFIRMAR VENTA";
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(24, 54, 93);
            this.btnGuardar.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.cboCliente);
            this.Controls.Add(this.lblProducto);
            this.Controls.Add(this.cboProducto);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.nudCantidad);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.dgvCarrito);
            this.Controls.Add(this.btnQuitar);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.ClientSize = new System.Drawing.Size(830, 638);
            this.Name = "FrmVentaModal";
            this.Text = "Registrar venta";
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
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.ComboBox cboCliente;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.ComboBox cboProducto;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.DataGridView dgvCarrito;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardar;
    }
}
