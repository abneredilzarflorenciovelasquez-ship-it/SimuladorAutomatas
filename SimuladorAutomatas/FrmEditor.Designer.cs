namespace SimuladorAutomatas
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelEditor = new System.Windows.Forms.Panel();
            this.btnConvertirAFD = new System.Windows.Forms.Button();
            this.btnValidarAFN = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnValidarAFD = new System.Windows.Forms.Button();
            this.lblResultado = new System.Windows.Forms.Label();
            this.btnSimular = new System.Windows.Forms.Button();
            this.txtCadena = new System.Windows.Forms.TextBox();
            this.btnTransicion = new System.Windows.Forms.Button();
            this.btnEstadoFinal = new System.Windows.Forms.Button();
            this.btnEstadoInicial = new System.Windows.Forms.Button();
            this.btnMinimizarAFD = new System.Windows.Forms.Button();
            this.btnMover = new System.Windows.Forms.Button();
            this.panelEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEditor
            // 
            this.panelEditor.BackColor = System.Drawing.Color.White;
            this.panelEditor.Controls.Add(this.btnMover);
            this.panelEditor.Controls.Add(this.btnMinimizarAFD);
            this.panelEditor.Controls.Add(this.btnConvertirAFD);
            this.panelEditor.Controls.Add(this.btnValidarAFN);
            this.panelEditor.Controls.Add(this.button2);
            this.panelEditor.Controls.Add(this.label1);
            this.panelEditor.Controls.Add(this.btnValidarAFD);
            this.panelEditor.Controls.Add(this.lblResultado);
            this.panelEditor.Controls.Add(this.btnSimular);
            this.panelEditor.Controls.Add(this.txtCadena);
            this.panelEditor.Controls.Add(this.btnTransicion);
            this.panelEditor.Controls.Add(this.btnEstadoFinal);
            this.panelEditor.Controls.Add(this.btnEstadoInicial);
            this.panelEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEditor.Location = new System.Drawing.Point(0, 0);
            this.panelEditor.Name = "panelEditor";
            this.panelEditor.Size = new System.Drawing.Size(1094, 598);
            this.panelEditor.TabIndex = 0;
            this.panelEditor.Paint += new System.Windows.Forms.PaintEventHandler(this.panelEditor_Paint);
            this.panelEditor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelEditor_MouseClick);
            this.panelEditor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelEditor_MouseDown);
            this.panelEditor.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelEditor_MouseMove);
            this.panelEditor.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelEditor_MouseUp);
            // 
            // btnConvertirAFD
            // 
            this.btnConvertirAFD.Location = new System.Drawing.Point(12, 503);
            this.btnConvertirAFD.Name = "btnConvertirAFD";
            this.btnConvertirAFD.Size = new System.Drawing.Size(170, 33);
            this.btnConvertirAFD.TabIndex = 11;
            this.btnConvertirAFD.Text = "Convertir AFN → AFD";
            this.btnConvertirAFD.UseVisualStyleBackColor = true;
            this.btnConvertirAFD.Click += new System.EventHandler(this.btnConvertirAFD_Click);
            // 
            // btnValidarAFN
            // 
            this.btnValidarAFN.Location = new System.Drawing.Point(12, 248);
            this.btnValidarAFN.Name = "btnValidarAFN";
            this.btnValidarAFN.Size = new System.Drawing.Size(97, 23);
            this.btnValidarAFN.TabIndex = 10;
            this.btnValidarAFN.Text = "Validar AFN";
            this.btnValidarAFN.UseVisualStyleBackColor = true;
            this.btnValidarAFN.Click += new System.EventHandler(this.btnValidarAFN_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 395);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 32);
            this.button2.TabIndex = 9;
            this.button2.Text = "Simular AFN";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 310);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Cadena:";
            // 
            // btnValidarAFD
            // 
            this.btnValidarAFD.Location = new System.Drawing.Point(12, 219);
            this.btnValidarAFD.Name = "btnValidarAFD";
            this.btnValidarAFD.Size = new System.Drawing.Size(97, 23);
            this.btnValidarAFD.TabIndex = 7;
            this.btnValidarAFD.Text = "Validar AFD";
            this.btnValidarAFD.UseVisualStyleBackColor = true;
            this.btnValidarAFD.Click += new System.EventHandler(this.btnValidarAFD_Click);
            // 
            // lblResultado
            // 
            this.lblResultado.AutoSize = true;
            this.lblResultado.Location = new System.Drawing.Point(27, 449);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(72, 16);
            this.lblResultado.TabIndex = 6;
            this.lblResultado.Text = "Resultado:";
            // 
            // btnSimular
            // 
            this.btnSimular.Location = new System.Drawing.Point(3, 357);
            this.btnSimular.Name = "btnSimular";
            this.btnSimular.Size = new System.Drawing.Size(113, 32);
            this.btnSimular.TabIndex = 5;
            this.btnSimular.Text = "Simular AFD";
            this.btnSimular.UseVisualStyleBackColor = true;
            this.btnSimular.Click += new System.EventHandler(this.btnSimular_Click);
            // 
            // txtCadena
            // 
            this.txtCadena.Location = new System.Drawing.Point(3, 329);
            this.txtCadena.Name = "txtCadena";
            this.txtCadena.Size = new System.Drawing.Size(96, 22);
            this.txtCadena.TabIndex = 4;
            // 
            // btnTransicion
            // 
            this.btnTransicion.Location = new System.Drawing.Point(3, 96);
            this.btnTransicion.Name = "btnTransicion";
            this.btnTransicion.Size = new System.Drawing.Size(96, 42);
            this.btnTransicion.TabIndex = 3;
            this.btnTransicion.Text = "Transición";
            this.btnTransicion.UseVisualStyleBackColor = true;
            this.btnTransicion.Click += new System.EventHandler(this.btnTransicion_Click);
            // 
            // btnEstadoFinal
            // 
            this.btnEstadoFinal.Location = new System.Drawing.Point(3, 48);
            this.btnEstadoFinal.Name = "btnEstadoFinal";
            this.btnEstadoFinal.Size = new System.Drawing.Size(96, 42);
            this.btnEstadoFinal.TabIndex = 2;
            this.btnEstadoFinal.Text = "Estado Final";
            this.btnEstadoFinal.UseVisualStyleBackColor = true;
            this.btnEstadoFinal.Click += new System.EventHandler(this.btnEstadoFinal_Click);
            // 
            // btnEstadoInicial
            // 
            this.btnEstadoInicial.Location = new System.Drawing.Point(3, 0);
            this.btnEstadoInicial.Name = "btnEstadoInicial";
            this.btnEstadoInicial.Size = new System.Drawing.Size(96, 42);
            this.btnEstadoInicial.TabIndex = 0;
            this.btnEstadoInicial.Text = "Estado Inicial";
            this.btnEstadoInicial.UseVisualStyleBackColor = true;
            this.btnEstadoInicial.Click += new System.EventHandler(this.btnEstadoInicial_Click);
            // 
            // btnMinimizarAFD
            // 
            this.btnMinimizarAFD.Location = new System.Drawing.Point(12, 542);
            this.btnMinimizarAFD.Name = "btnMinimizarAFD";
            this.btnMinimizarAFD.Size = new System.Drawing.Size(113, 32);
            this.btnMinimizarAFD.TabIndex = 12;
            this.btnMinimizarAFD.Text = "Minimizar AFD";
            this.btnMinimizarAFD.UseVisualStyleBackColor = true;
            this.btnMinimizarAFD.Click += new System.EventHandler(this.btnMinimizarAFD_Click);
            // 
            // btnMover
            // 
            this.btnMover.Location = new System.Drawing.Point(3, 144);
            this.btnMover.Name = "btnMover";
            this.btnMover.Size = new System.Drawing.Size(114, 42);
            this.btnMover.TabIndex = 13;
            this.btnMover.Text = "Mover Estado";
            this.btnMover.UseVisualStyleBackColor = true;
            this.btnMover.Click += new System.EventHandler(this.btnMover_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 598);
            this.Controls.Add(this.panelEditor);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelEditor.ResumeLayout(false);
            this.panelEditor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelEditor;
        private System.Windows.Forms.Button btnEstadoInicial;
        private System.Windows.Forms.Button btnEstadoFinal;
        private System.Windows.Forms.Button btnTransicion;
        private System.Windows.Forms.TextBox txtCadena;
        private System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.Button btnSimular;
        private System.Windows.Forms.Button btnValidarAFD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnValidarAFN;
        private System.Windows.Forms.Button btnConvertirAFD;
        private System.Windows.Forms.Button btnMinimizarAFD;
        private System.Windows.Forms.Button btnMover;
    }
}

