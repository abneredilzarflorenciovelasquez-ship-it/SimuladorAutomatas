namespace SimuladorAutomatas.Formularios
{
    partial class FrmVisualizarAFNRegex
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelVisualizacion = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelVisualizacion
            // 
            this.panelVisualizacion.AutoScroll = true;
            this.panelVisualizacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelVisualizacion.Location = new System.Drawing.Point(0, 0);
            this.panelVisualizacion.Name = "panelVisualizacion";
            this.panelVisualizacion.Size = new System.Drawing.Size(896, 465);
            this.panelVisualizacion.TabIndex = 0;
            this.panelVisualizacion.Paint += new System.Windows.Forms.PaintEventHandler(this.panelVisualizacion_Paint);
            // 
            // FrmVisualizarAFNRegex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 465);
            this.Controls.Add(this.panelVisualizacion);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "FrmVisualizarAFNRegex";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visualización del AFN generado";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelVisualizacion;
    }
}