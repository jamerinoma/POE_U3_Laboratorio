namespace GestionElectronicos.Desktop.Formularios
{
    partial class FrmProductoModal
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
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.nudPrecio = new System.Windows.Forms.NumericUpDown();
            this.lblStock = new System.Windows.Forms.Label();
            this.nudStock = new System.Windows.Forms.NumericUpDown();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // lblTitulo
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Location = new System.Drawing.Point(30, 20);
            this.lblTitulo.Size = new System.Drawing.Size(460, 46);
            this.lblTitulo.Text = "Nuevo producto";
            this.lblTitulo.AutoSize = false;
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 19F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(14, 36, 62);
            // lblNombre
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Location = new System.Drawing.Point(30, 84);
            this.lblNombre.Size = new System.Drawing.Size(455, 26);
            this.lblNombre.Text = "Nombre *";
            this.lblNombre.AutoSize = false;
            this.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            // txtNombre
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Location = new System.Drawing.Point(30, 112);
            this.txtNombre.Size = new System.Drawing.Size(450, 33);
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            // lblDescripcion
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Location = new System.Drawing.Point(30, 154);
            this.lblDescripcion.Size = new System.Drawing.Size(455, 26);
            this.lblDescripcion.Text = "Descripción";
            this.lblDescripcion.AutoSize = false;
            this.lblDescripcion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDescripcion.Font = new System.Drawing.Font("Segoe UI", 10F);
            // txtDescripcion
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Location = new System.Drawing.Point(30, 182);
            this.txtDescripcion.Size = new System.Drawing.Size(450, 75);
            this.txtDescripcion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescripcion.Multiline = true;
            // lblPrecio
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Location = new System.Drawing.Point(30, 273);
            this.lblPrecio.Size = new System.Drawing.Size(220, 26);
            this.lblPrecio.Text = "Precio ($) *";
            this.lblPrecio.AutoSize = false;
            this.lblPrecio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPrecio.Font = new System.Drawing.Font("Segoe UI", 10F);
            // nudPrecio
            this.nudPrecio.Name = "nudPrecio";
            this.nudPrecio.Location = new System.Drawing.Point(30, 302);
            this.nudPrecio.Size = new System.Drawing.Size(210, 34);
            this.nudPrecio.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudPrecio.Maximum = new decimal(999999);
            this.nudPrecio.DecimalPlaces = 2;
            this.nudPrecio.Increment = new decimal(1);
            // lblStock
            this.lblStock.Name = "lblStock";
            this.lblStock.Location = new System.Drawing.Point(268, 273);
            this.lblStock.Size = new System.Drawing.Size(210, 26);
            this.lblStock.Text = "Unidades en stock *";
            this.lblStock.AutoSize = false;
            this.lblStock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStock.Font = new System.Drawing.Font("Segoe UI", 10F);
            // nudStock
            this.nudStock.Name = "nudStock";
            this.nudStock.Location = new System.Drawing.Point(268, 302);
            this.nudStock.Size = new System.Drawing.Size(210, 34);
            this.nudStock.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudStock.Maximum = new decimal(999999);
            // btnCancelar
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Location = new System.Drawing.Point(30, 384);
            this.btnCancelar.Size = new System.Drawing.Size(210, 44);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(232, 237, 244);
            this.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(14, 36, 62);
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.UseVisualStyleBackColor = false;
            // btnGuardar
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Location = new System.Drawing.Point(268, 384);
            this.btnGuardar.Size = new System.Drawing.Size(210, 44);
            this.btnGuardar.Text = "Guardar producto";
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(24, 54, 93);
            this.btnGuardar.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.lblPrecio);
            this.Controls.Add(this.nudPrecio);
            this.Controls.Add(this.lblStock);
            this.Controls.Add(this.nudStock);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.ClientSize = new System.Drawing.Size(510, 457);
            this.Name = "FrmProductoModal";
            this.Text = "Nuevo producto";
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
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.NumericUpDown nudPrecio;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.NumericUpDown nudStock;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardar;
    }
}
