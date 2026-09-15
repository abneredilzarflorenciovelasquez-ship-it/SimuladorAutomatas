namespace SimuladorAutomatas.Formularios
{
    partial class FrmMinimizacionAFD
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
            this.panelTabla = new System.Windows.Forms.Panel();
            this.panelAFDMinimizado = new System.Windows.Forms.Panel();
            this.txtProcedimientoMin = new System.Windows.Forms.RichTextBox();
            this.btnValidarAFDMin = new System.Windows.Forms.Button();
            this.btnSimularAFDMin = new System.Windows.Forms.Button();
            this.txtCadenaAFDMin = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblResultadoAFDMin = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelTabla.SuspendLayout();
            this.panelAFDMinimizado.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTabla
            // 
            this.panelTabla.Controls.Add(this.txtProcedimientoMin);
            this.panelTabla.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelTabla.Location = new System.Drawing.Point(0, 0);
            this.panelTabla.Name = "panelTabla";
            this.panelTabla.Size = new System.Drawing.Size(573, 623);
            this.panelTabla.TabIndex = 0;
            // 
            // panelAFDMinimizado
            // 
            this.panelAFDMinimizado.Controls.Add(this.label2);
            this.panelAFDMinimizado.Controls.Add(this.lblResultadoAFDMin);
            this.panelAFDMinimizado.Controls.Add(this.label1);
            this.panelAFDMinimizado.Controls.Add(this.txtCadenaAFDMin);
            this.panelAFDMinimizado.Controls.Add(this.btnSimularAFDMin);
            this.panelAFDMinimizado.Controls.Add(this.btnValidarAFDMin);
            this.panelAFDMinimizado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAFDMinimizado.Location = new System.Drawing.Point(573, 0);
            this.panelAFDMinimizado.Name = "panelAFDMinimizado";
            this.panelAFDMinimizado.Size = new System.Drawing.Size(646, 623);
            this.panelAFDMinimizado.TabIndex = 1;
            // 
            // txtProcedimientoMin
            // 
            this.txtProcedimientoMin.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtProcedimientoMin.Location = new System.Drawing.Point(0, 473);
            this.txtProcedimientoMin.Name = "txtProcedimientoMin";
            this.txtProcedimientoMin.ReadOnly = true;
            this.txtProcedimientoMin.Size = new System.Drawing.Size(573, 150);
            this.txtProcedimientoMin.TabIndex = 0;
            this.txtProcedimientoMin.Text = "";
            // 
            // btnValidarAFDMin
            // 
            this.btnValidarAFDMin.Location = new System.Drawing.Point(12, 421);
            this.btnValidarAFDMin.Name = "btnValidarAFDMin";
            this.btnValidarAFDMin.Size = new System.Drawing.Size(101, 23);
            this.btnValidarAFDMin.TabIndex = 0;
            this.btnValidarAFDMin.Text = "Validar AFD";
            this.btnValidarAFDMin.UseVisualStyleBackColor = true;
            // 
            // btnSimularAFDMin
            // 
            this.btnSimularAFDMin.Location = new System.Drawing.Point(12, 450);
            this.btnSimularAFDMin.Name = "btnSimularAFDMin";
            this.btnSimularAFDMin.Size = new System.Drawing.Size(101, 23);
            this.btnSimularAFDMin.TabIndex = 1;
            this.btnSimularAFDMin.Text = "Simular AFD";
            this.btnSimularAFDMin.UseVisualStyleBackColor = true;
            // 
            // txtCadenaAFDMin
            // 
            this.txtCadenaAFDMin.Location = new System.Drawing.Point(12, 532);
            this.txtCadenaAFDMin.Name = "txtCadenaAFDMin";
            this.txtCadenaAFDMin.Size = new System.Drawing.Size(101, 22);
            this.txtCadenaAFDMin.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 509);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Cadena:";
            // 
            // lblResultadoAFDMin
            // 
            this.lblResultadoAFDMin.AutoSize = true;
            this.lblResultadoAFDMin.Location = new System.Drawing.Point(29, 580);
            this.lblResultadoAFDMin.Name = "lblResultadoAFDMin";
            this.lblResultadoAFDMin.Size = new System.Drawing.Size(72, 16);
            this.lblResultadoAFDMin.TabIndex = 4;
            this.lblResultadoAFDMin.Text = "Resultado:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(227, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "AFD  MINIMIZADO";
            // 
            // FrmMinimizacionAFD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1219, 623);
            this.Controls.Add(this.panelAFDMinimizado);
            this.Controls.Add(this.panelTabla);
            this.Name = "FrmMinimizacionAFD";
            this.Text = "FrmMinimizacionAFD";
            this.Load += new System.EventHandler(this.FrmMinimizacionAFD_Load);
            this.panelTabla.ResumeLayout(false);
            this.panelAFDMinimizado.ResumeLayout(false);
            this.panelAFDMinimizado.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTabla;
        private System.Windows.Forms.RichTextBox txtProcedimientoMin;
        private System.Windows.Forms.Panel panelAFDMinimizado;
        private System.Windows.Forms.Label lblResultadoAFDMin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCadenaAFDMin;
        private System.Windows.Forms.Button btnSimularAFDMin;
        private System.Windows.Forms.Button btnValidarAFDMin;
        private System.Windows.Forms.Label label2;
    }
}