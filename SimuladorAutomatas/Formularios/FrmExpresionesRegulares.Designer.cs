namespace SimuladorAutomatas.Formularios
{
    partial class FrmExpresionesRegulares
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtExpresion = new System.Windows.Forms.TextBox();
            this.btnValidarExpresion = new System.Windows.Forms.Button();
            this.btnGenerarAutomata = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCadenaRegex = new System.Windows.Forms.TextBox();
            this.btnValidarCadena = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvRegistro = new System.Windows.Forms.DataGridView();
            this.lblResultadoRegex = new System.Windows.Forms.Label();
            this.btnConvertirAFDRegex = new System.Windows.Forms.Button();
            this.btnMinimizarAFDRegex = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lblLogs = new System.Windows.Forms.Label();
            this.txtLogs = new System.Windows.Forms.TextBox();
            this.btnValidarLogs = new System.Windows.Forms.Button();
            this.dgvResultadosLogs = new System.Windows.Forms.DataGridView();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistro)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultadosLogs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(290, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "VALIDADOR DE EXPRESIONES REGULARES";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Expresión Regular:";
            // 
            // txtExpresion
            // 
            this.txtExpresion.Location = new System.Drawing.Point(190, 80);
            this.txtExpresion.Name = "txtExpresion";
            this.txtExpresion.Size = new System.Drawing.Size(144, 22);
            this.txtExpresion.TabIndex = 2;
            this.txtExpresion.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btnValidarExpresion
            // 
            this.btnValidarExpresion.Location = new System.Drawing.Point(72, 117);
            this.btnValidarExpresion.Name = "btnValidarExpresion";
            this.btnValidarExpresion.Size = new System.Drawing.Size(108, 61);
            this.btnValidarExpresion.TabIndex = 3;
            this.btnValidarExpresion.Text = "Validar Expresión";
            this.btnValidarExpresion.UseVisualStyleBackColor = true;
            this.btnValidarExpresion.Click += new System.EventHandler(this.btnValidarExpresion_Click);
            // 
            // btnGenerarAutomata
            // 
            this.btnGenerarAutomata.Location = new System.Drawing.Point(204, 117);
            this.btnGenerarAutomata.Name = "btnGenerarAutomata";
            this.btnGenerarAutomata.Size = new System.Drawing.Size(108, 61);
            this.btnGenerarAutomata.TabIndex = 4;
            this.btnGenerarAutomata.Text = "Generar Autómata";
            this.btnGenerarAutomata.UseVisualStyleBackColor = true;
            this.btnGenerarAutomata.Click += new System.EventHandler(this.btnGenerarAutomata_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 269);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(169, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Cadena que desea probar:";
            // 
            // txtCadenaRegex
            // 
            this.txtCadenaRegex.Location = new System.Drawing.Point(53, 298);
            this.txtCadenaRegex.Name = "txtCadenaRegex";
            this.txtCadenaRegex.Size = new System.Drawing.Size(249, 22);
            this.txtCadenaRegex.TabIndex = 6;
            // 
            // btnValidarCadena
            // 
            this.btnValidarCadena.Location = new System.Drawing.Point(131, 336);
            this.btnValidarCadena.Name = "btnValidarCadena";
            this.btnValidarCadena.Size = new System.Drawing.Size(99, 61);
            this.btnValidarCadena.TabIndex = 7;
            this.btnValidarCadena.Text = "Validar Cadena";
            this.btnValidarCadena.UseVisualStyleBackColor = true;
            this.btnValidarCadena.Click += new System.EventHandler(this.btnValidarCadena_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 416);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(159, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Registro de Validaciones";
            // 
            // dgvRegistro
            // 
            this.dgvRegistro.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRegistro.Location = new System.Drawing.Point(53, 444);
            this.dgvRegistro.Name = "dgvRegistro";
            this.dgvRegistro.RowHeadersWidth = 51;
            this.dgvRegistro.RowTemplate.Height = 24;
            this.dgvRegistro.Size = new System.Drawing.Size(460, 168);
            this.dgvRegistro.TabIndex = 9;
            // 
            // lblResultadoRegex
            // 
            this.lblResultadoRegex.AutoSize = true;
            this.lblResultadoRegex.Location = new System.Drawing.Point(381, 642);
            this.lblResultadoRegex.Name = "lblResultadoRegex";
            this.lblResultadoRegex.Size = new System.Drawing.Size(72, 16);
            this.lblResultadoRegex.TabIndex = 10;
            this.lblResultadoRegex.Text = "Resultado:";
            // 
            // btnConvertirAFDRegex
            // 
            this.btnConvertirAFDRegex.Location = new System.Drawing.Point(72, 184);
            this.btnConvertirAFDRegex.Name = "btnConvertirAFDRegex";
            this.btnConvertirAFDRegex.Size = new System.Drawing.Size(108, 61);
            this.btnConvertirAFDRegex.TabIndex = 11;
            this.btnConvertirAFDRegex.Text = "Convertir a AFD";
            this.btnConvertirAFDRegex.UseVisualStyleBackColor = true;
            this.btnConvertirAFDRegex.Click += new System.EventHandler(this.btnConvertirAFDRegex_Click);
            // 
            // btnMinimizarAFDRegex
            // 
            this.btnMinimizarAFDRegex.Location = new System.Drawing.Point(204, 184);
            this.btnMinimizarAFDRegex.Name = "btnMinimizarAFDRegex";
            this.btnMinimizarAFDRegex.Size = new System.Drawing.Size(108, 61);
            this.btnMinimizarAFDRegex.TabIndex = 12;
            this.btnMinimizarAFDRegex.Text = "Minimizar AFD";
            this.btnMinimizarAFDRegex.UseVisualStyleBackColor = true;
            this.btnMinimizarAFDRegex.Click += new System.EventHandler(this.btnMinimizarAFDRegex_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(710, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "VALIDACION DE LOGS";
            // 
            // lblLogs
            // 
            this.lblLogs.AutoSize = true;
            this.lblLogs.Location = new System.Drawing.Point(651, 85);
            this.lblLogs.Name = "lblLogs";
            this.lblLogs.Size = new System.Drawing.Size(115, 16);
            this.lblLogs.TabIndex = 14;
            this.lblLogs.Text = "Logs para validar:";
            // 
            // txtLogs
            // 
            this.txtLogs.AcceptsReturn = true;
            this.txtLogs.Location = new System.Drawing.Point(663, 128);
            this.txtLogs.Multiline = true;
            this.txtLogs.Name = "txtLogs";
            this.txtLogs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLogs.Size = new System.Drawing.Size(460, 157);
            this.txtLogs.TabIndex = 15;
            // 
            // btnValidarLogs
            // 
            this.btnValidarLogs.Location = new System.Drawing.Point(843, 322);
            this.btnValidarLogs.Name = "btnValidarLogs";
            this.btnValidarLogs.Size = new System.Drawing.Size(99, 61);
            this.btnValidarLogs.TabIndex = 16;
            this.btnValidarLogs.Text = "Validar logs";
            this.btnValidarLogs.UseVisualStyleBackColor = true;
            this.btnValidarLogs.Click += new System.EventHandler(this.btnValidarLogs_Click);
            // 
            // dgvResultadosLogs
            // 
            this.dgvResultadosLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultadosLogs.Location = new System.Drawing.Point(663, 444);
            this.dgvResultadosLogs.Name = "dgvResultadosLogs";
            this.dgvResultadosLogs.RowHeadersWidth = 51;
            this.dgvResultadosLogs.RowTemplate.Height = 24;
            this.dgvResultadosLogs.Size = new System.Drawing.Size(460, 168);
            this.dgvResultadosLogs.TabIndex = 17;
            // 
            // FrmExpresionesRegulares
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1162, 676);
            this.Controls.Add(this.dgvResultadosLogs);
            this.Controls.Add(this.btnValidarLogs);
            this.Controls.Add(this.txtLogs);
            this.Controls.Add(this.lblLogs);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnMinimizarAFDRegex);
            this.Controls.Add(this.btnConvertirAFDRegex);
            this.Controls.Add(this.lblResultadoRegex);
            this.Controls.Add(this.dgvRegistro);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnValidarCadena);
            this.Controls.Add(this.txtCadenaRegex);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnGenerarAutomata);
            this.Controls.Add(this.btnValidarExpresion);
            this.Controls.Add(this.txtExpresion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "FrmExpresionesRegulares";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmExpresionesRegulares";
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistro)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultadosLogs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtExpresion;
        private System.Windows.Forms.Button btnValidarExpresion;
        private System.Windows.Forms.Button btnGenerarAutomata;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCadenaRegex;
        private System.Windows.Forms.Button btnValidarCadena;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvRegistro;
        private System.Windows.Forms.Label lblResultadoRegex;
        private System.Windows.Forms.Button btnConvertirAFDRegex;
        private System.Windows.Forms.Button btnMinimizarAFDRegex;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblLogs;
        private System.Windows.Forms.TextBox txtLogs;
        private System.Windows.Forms.Button btnValidarLogs;
        private System.Windows.Forms.DataGridView dgvResultadosLogs;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}