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
            this.btnAnalizarLogs = new System.Windows.Forms.Button();
            this.btnExpresionesRegulares = new System.Windows.Forms.Button();
            this.btnEstablecerAlfabeto = new System.Windows.Forms.Button();
            this.txtAlfabeto = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnAbrir = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnMover = new System.Windows.Forms.Button();
            this.btnMinimizarAFD = new System.Windows.Forms.Button();
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
            this.panelEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEditor
            // 
            this.panelEditor.BackColor = System.Drawing.Color.White;
            this.panelEditor.Controls.Add(this.btnAnalizarLogs);
            this.panelEditor.Controls.Add(this.btnExpresionesRegulares);
            this.panelEditor.Controls.Add(this.btnEstablecerAlfabeto);
            this.panelEditor.Controls.Add(this.txtAlfabeto);
            this.panelEditor.Controls.Add(this.label2);
            this.panelEditor.Controls.Add(this.btnNuevo);
            this.panelEditor.Controls.Add(this.btnAbrir);
            this.panelEditor.Controls.Add(this.btnGuardar);
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
            this.panelEditor.Size = new System.Drawing.Size(1220, 918);
            this.panelEditor.TabIndex = 0;
            this.panelEditor.Paint += new System.Windows.Forms.PaintEventHandler(this.panelEditor_Paint);
            this.panelEditor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelEditor_MouseClick);

            this.panelEditor.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panelEditor_MouseDoubleClick);

            this.panelEditor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelEditor_MouseDown);
            this.panelEditor.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelEditor_MouseMove);
            this.panelEditor.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelEditor_MouseUp);
            // 
            // btnAnalizarLogs
            // 
            this.btnAnalizarLogs.Location = new System.Drawing.Point(18, 678);
            this.btnAnalizarLogs.Name = "btnAnalizarLogs";
            this.btnAnalizarLogs.Size = new System.Drawing.Size(113, 43);
            this.btnAnalizarLogs.TabIndex = 21;
            this.btnAnalizarLogs.Text = "Analizar Logs";
            this.btnAnalizarLogs.UseVisualStyleBackColor = true;
            this.btnAnalizarLogs.Click += new System.EventHandler(this.btnAnalizarLogs_Click);
            // 
            // btnExpresionesRegulares
            // 
            this.btnExpresionesRegulares.Location = new System.Drawing.Point(18, 629);
            this.btnExpresionesRegulares.Name = "btnExpresionesRegulares";
            this.btnExpresionesRegulares.Size = new System.Drawing.Size(150, 43);
            this.btnExpresionesRegulares.TabIndex = 20;
            this.btnExpresionesRegulares.Text = "Expresiones Regulares y Logs";
            this.btnExpresionesRegulares.UseVisualStyleBackColor = true;
            this.btnExpresionesRegulares.Click += new System.EventHandler(this.btnExpresionesRegulares_Click);
            // 
            // btnEstablecerAlfabeto
            // 
            this.btnEstablecerAlfabeto.Location = new System.Drawing.Point(183, 6);
            this.btnEstablecerAlfabeto.Name = "btnEstablecerAlfabeto";
            this.btnEstablecerAlfabeto.Size = new System.Drawing.Size(87, 34);
            this.btnEstablecerAlfabeto.TabIndex = 19;
            this.btnEstablecerAlfabeto.Text = "Establecer";
            this.btnEstablecerAlfabeto.UseVisualStyleBackColor = true;
            this.btnEstablecerAlfabeto.Click += new System.EventHandler(this.btnEstablecerAlfabeto_Click);
            // 
            // txtAlfabeto
            // 
            this.txtAlfabeto.Location = new System.Drawing.Point(96, 12);
            this.txtAlfabeto.Name = "txtAlfabeto";
            this.txtAlfabeto.Size = new System.Drawing.Size(72, 22);
            this.txtAlfabeto.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 16);
            this.label2.TabIndex = 17;
            this.label2.Text = "Alfabeto:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Location = new System.Drawing.Point(18, 817);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(113, 32);
            this.btnNuevo.TabIndex = 16;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnAbrir
            // 
            this.btnAbrir.Location = new System.Drawing.Point(18, 779);
            this.btnAbrir.Name = "btnAbrir";
            this.btnAbrir.Size = new System.Drawing.Size(113, 32);
            this.btnAbrir.TabIndex = 15;
            this.btnAbrir.Text = "Abrir";
            this.btnAbrir.UseVisualStyleBackColor = true;
            this.btnAbrir.Click += new System.EventHandler(this.btnAbrir_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(18, 738);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(113, 32);
            this.btnGuardar.TabIndex = 14;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnMover
            // 
            this.btnMover.Location = new System.Drawing.Point(18, 40);
            this.btnMover.Name = "btnMover";
            this.btnMover.Size = new System.Drawing.Size(113, 43);
            this.btnMover.TabIndex = 13;
            this.btnMover.Text = "Crear/Mover";
            this.btnMover.UseVisualStyleBackColor = true;
            this.btnMover.Click += new System.EventHandler(this.btnMover_Click);
            // 
            // btnMinimizarAFD
            // 
            this.btnMinimizarAFD.Location = new System.Drawing.Point(18, 580);
            this.btnMinimizarAFD.Name = "btnMinimizarAFD";
            this.btnMinimizarAFD.Size = new System.Drawing.Size(113, 43);
            this.btnMinimizarAFD.TabIndex = 12;
            this.btnMinimizarAFD.Text = "Minimizar AFD";
            this.btnMinimizarAFD.UseVisualStyleBackColor = true;
            this.btnMinimizarAFD.Click += new System.EventHandler(this.btnMinimizarAFD_Click);
            // 
            // btnConvertirAFD
            // 
            this.btnConvertirAFD.Location = new System.Drawing.Point(18, 531);
            this.btnConvertirAFD.Name = "btnConvertirAFD";
            this.btnConvertirAFD.Size = new System.Drawing.Size(113, 43);
            this.btnConvertirAFD.TabIndex = 11;
            this.btnConvertirAFD.Text = "     Convertir       AFN → AFD";
            this.btnConvertirAFD.UseVisualStyleBackColor = true;
            this.btnConvertirAFD.Click += new System.EventHandler(this.btnConvertirAFD_Click);
            // 
            // btnValidarAFN
            // 
            this.btnValidarAFN.Location = new System.Drawing.Point(18, 285);
            this.btnValidarAFN.Name = "btnValidarAFN";
            this.btnValidarAFN.Size = new System.Drawing.Size(113, 43);
            this.btnValidarAFN.TabIndex = 10;
            this.btnValidarAFN.Text = "Validar AFN";
            this.btnValidarAFN.UseVisualStyleBackColor = true;
            this.btnValidarAFN.Click += new System.EventHandler(this.btnValidarAFN_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(18, 482);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 43);
            this.button2.TabIndex = 9;
            this.button2.Text = "Simular AFN";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 340);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Cadena";
            // 
            // btnValidarAFD
            // 
            this.btnValidarAFD.Location = new System.Drawing.Point(18, 236);
            this.btnValidarAFD.Name = "btnValidarAFD";
            this.btnValidarAFD.Size = new System.Drawing.Size(113, 43);
            this.btnValidarAFD.TabIndex = 7;
            this.btnValidarAFD.Text = "Validar AFD";
            this.btnValidarAFD.UseVisualStyleBackColor = true;
            this.btnValidarAFD.Click += new System.EventHandler(this.btnValidarAFD_Click);
            // 
            // lblResultado
            // 
            this.lblResultado.AutoSize = true;
            this.lblResultado.Location = new System.Drawing.Point(15, 399);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(72, 16);
            this.lblResultado.TabIndex = 6;
            this.lblResultado.Text = "Resultado:";
            // 
            // btnSimular
            // 
            this.btnSimular.Location = new System.Drawing.Point(18, 433);
            this.btnSimular.Name = "btnSimular";
            this.btnSimular.Size = new System.Drawing.Size(113, 43);
            this.btnSimular.TabIndex = 5;
            this.btnSimular.Text = "Simular AFD";
            this.btnSimular.UseVisualStyleBackColor = true;
            this.btnSimular.Click += new System.EventHandler(this.btnSimular_Click);
            // 
            // txtCadena
            // 
            this.txtCadena.Location = new System.Drawing.Point(18, 359);
            this.txtCadena.Name = "txtCadena";
            this.txtCadena.Size = new System.Drawing.Size(113, 22);
            this.txtCadena.TabIndex = 4;
            // 
            // btnTransicion
            // 
            this.btnTransicion.Location = new System.Drawing.Point(18, 187);
            this.btnTransicion.Name = "btnTransicion";
            this.btnTransicion.Size = new System.Drawing.Size(113, 43);
            this.btnTransicion.TabIndex = 3;
            this.btnTransicion.Text = "Transición";
            this.btnTransicion.UseVisualStyleBackColor = true;
            this.btnTransicion.Click += new System.EventHandler(this.btnTransicion_Click);
            // 
            // btnEstadoFinal
            // 
            this.btnEstadoFinal.Location = new System.Drawing.Point(18, 138);
            this.btnEstadoFinal.Name = "btnEstadoFinal";
            this.btnEstadoFinal.Size = new System.Drawing.Size(113, 43);
            this.btnEstadoFinal.TabIndex = 2;
            this.btnEstadoFinal.Text = "Estado Final";
            this.btnEstadoFinal.UseVisualStyleBackColor = true;
            this.btnEstadoFinal.Click += new System.EventHandler(this.btnEstadoFinal_Click);
            // 
            // btnEstadoInicial
            // 
            this.btnEstadoInicial.Location = new System.Drawing.Point(18, 89);
            this.btnEstadoInicial.Name = "btnEstadoInicial";
            this.btnEstadoInicial.Size = new System.Drawing.Size(113, 43);
            this.btnEstadoInicial.TabIndex = 0;
            this.btnEstadoInicial.Text = "Estado Inicial";
            this.btnEstadoInicial.UseVisualStyleBackColor = true;
            this.btnEstadoInicial.Click += new System.EventHandler(this.btnEstadoInicial_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1220, 918);
            this.Controls.Add(this.panelEditor);
            this.Name = "Form1";
            this.Text = "Simulador de Autómatas";
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
        private System.Windows.Forms.Button btnAbrir;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAlfabeto;
        private System.Windows.Forms.Button btnEstablecerAlfabeto;
        private System.Windows.Forms.Button btnExpresionesRegulares;
        private System.Windows.Forms.Button btnAnalizarLogs;
    }
}

