namespace SimuladorAutomatas.Formularios
{
    partial class FrmConversionAFD
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
            this.panelProcedimiento = new System.Windows.Forms.Panel();
            this.txtProcedimiento = new System.Windows.Forms.RichTextBox();
            this.panelAFD = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblResultadoAFD = new System.Windows.Forms.Label();
            this.btnSimularAFD = new System.Windows.Forms.Button();
            this.txtCadenaAFD = new System.Windows.Forms.TextBox();
            this.btnValidarAFD = new System.Windows.Forms.Button();
            this.btnMinimizarAFD = new System.Windows.Forms.Button();
            this.panelProcedimiento.SuspendLayout();
            this.panelAFD.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelProcedimiento
            // 
            this.panelProcedimiento.Controls.Add(this.txtProcedimiento);
            this.panelProcedimiento.Location = new System.Drawing.Point(1, -1);
            this.panelProcedimiento.Name = "panelProcedimiento";
            this.panelProcedimiento.Size = new System.Drawing.Size(488, 472);
            this.panelProcedimiento.TabIndex = 0;
            // 
            // txtProcedimiento
            // 
            this.txtProcedimiento.Location = new System.Drawing.Point(3, 3);
            this.txtProcedimiento.Name = "txtProcedimiento";
            this.txtProcedimiento.ReadOnly = true;
            this.txtProcedimiento.Size = new System.Drawing.Size(482, 469);
            this.txtProcedimiento.TabIndex = 0;
            this.txtProcedimiento.Text = "";
            // 
            // panelAFD
            // 
            this.panelAFD.Controls.Add(this.btnMinimizarAFD);
            this.panelAFD.Controls.Add(this.label1);
            this.panelAFD.Controls.Add(this.lblResultadoAFD);
            this.panelAFD.Controls.Add(this.btnSimularAFD);
            this.panelAFD.Controls.Add(this.txtCadenaAFD);
            this.panelAFD.Controls.Add(this.btnValidarAFD);
            this.panelAFD.Location = new System.Drawing.Point(495, -1);
            this.panelAFD.Name = "panelAFD";
            this.panelAFD.Size = new System.Drawing.Size(584, 472);
            this.panelAFD.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 350);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Cadena";
            // 
            // lblResultadoAFD
            // 
            this.lblResultadoAFD.AutoSize = true;
            this.lblResultadoAFD.Location = new System.Drawing.Point(15, 452);
            this.lblResultadoAFD.Name = "lblResultadoAFD";
            this.lblResultadoAFD.Size = new System.Drawing.Size(72, 16);
            this.lblResultadoAFD.TabIndex = 3;
            this.lblResultadoAFD.Text = "Resultado:";
            // 
            // btnSimularAFD
            // 
            this.btnSimularAFD.Location = new System.Drawing.Point(4, 410);
            this.btnSimularAFD.Name = "btnSimularAFD";
            this.btnSimularAFD.Size = new System.Drawing.Size(101, 23);
            this.btnSimularAFD.TabIndex = 2;
            this.btnSimularAFD.Text = "Simular AFD";
            this.btnSimularAFD.UseVisualStyleBackColor = true;
            this.btnSimularAFD.Click += new System.EventHandler(this.btnSimularAFD_Click);
            // 
            // txtCadenaAFD
            // 
            this.txtCadenaAFD.Location = new System.Drawing.Point(5, 369);
            this.txtCadenaAFD.Name = "txtCadenaAFD";
            this.txtCadenaAFD.Size = new System.Drawing.Size(100, 22);
            this.txtCadenaAFD.TabIndex = 1;
            // 
            // btnValidarAFD
            // 
            this.btnValidarAFD.Location = new System.Drawing.Point(-3, 254);
            this.btnValidarAFD.Name = "btnValidarAFD";
            this.btnValidarAFD.Size = new System.Drawing.Size(101, 23);
            this.btnValidarAFD.TabIndex = 0;
            this.btnValidarAFD.Text = "ValidarAFD";
            this.btnValidarAFD.UseVisualStyleBackColor = true;
            this.btnValidarAFD.Click += new System.EventHandler(this.btnValidarAFD_Click);
            // 
            // btnMinimizarAFD
            // 
            this.btnMinimizarAFD.Location = new System.Drawing.Point(3, 283);
            this.btnMinimizarAFD.Name = "btnMinimizarAFD";
            this.btnMinimizarAFD.Size = new System.Drawing.Size(101, 23);
            this.btnMinimizarAFD.TabIndex = 5;
            this.btnMinimizarAFD.Text = "Minimizar AFD";
            this.btnMinimizarAFD.UseVisualStyleBackColor = true;
            this.btnMinimizarAFD.Click += new System.EventHandler(this.btnMinimizarAFD_Click);
            // 
            // FrmConversionAFD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 476);
            this.Controls.Add(this.panelAFD);
            this.Controls.Add(this.panelProcedimiento);
            this.Name = "FrmConversionAFD";
            this.Text = "FrmConversionAFD";
            this.Load += new System.EventHandler(this.FrmConversionAFD_Load);
            this.panelProcedimiento.ResumeLayout(false);
            this.panelAFD.ResumeLayout(false);
            this.panelAFD.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelProcedimiento;
        private System.Windows.Forms.RichTextBox txtProcedimiento;
        private System.Windows.Forms.Panel panelAFD;
        private System.Windows.Forms.Button btnValidarAFD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblResultadoAFD;
        private System.Windows.Forms.Button btnSimularAFD;
        private System.Windows.Forms.TextBox txtCadenaAFD;
        private System.Windows.Forms.Button btnMinimizarAFD;
    }
}