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
            this.btnEstadoInicial = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnEstadoFinal = new System.Windows.Forms.Button();
            this.btnTransicion = new System.Windows.Forms.Button();
            this.txtCadena = new System.Windows.Forms.TextBox();
            this.btnSimular = new System.Windows.Forms.Button();
            this.lblResultado = new System.Windows.Forms.Label();
            this.panelEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEditor
            // 
            this.panelEditor.BackColor = System.Drawing.Color.White;
            this.panelEditor.Controls.Add(this.lblResultado);
            this.panelEditor.Controls.Add(this.btnSimular);
            this.panelEditor.Controls.Add(this.txtCadena);
            this.panelEditor.Controls.Add(this.btnTransicion);
            this.panelEditor.Controls.Add(this.btnEstadoFinal);
            this.panelEditor.Controls.Add(this.button1);
            this.panelEditor.Controls.Add(this.btnEstadoInicial);
            this.panelEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEditor.Location = new System.Drawing.Point(0, 0);
            this.panelEditor.Name = "panelEditor";
            this.panelEditor.Size = new System.Drawing.Size(804, 450);
            this.panelEditor.TabIndex = 0;
            this.panelEditor.Paint += new System.Windows.Forms.PaintEventHandler(this.panelEditor_Paint);
            this.panelEditor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelEditor_MouseClick);
            this.panelEditor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelEditor_MouseDown);
            this.panelEditor.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelEditor_MouseMove);
            this.panelEditor.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelEditor_MouseUp);
            // 
            // btnEstadoInicial
            // 
            this.btnEstadoInicial.Location = new System.Drawing.Point(696, 22);
            this.btnEstadoInicial.Name = "btnEstadoInicial";
            this.btnEstadoInicial.Size = new System.Drawing.Size(96, 42);
            this.btnEstadoInicial.TabIndex = 0;
            this.btnEstadoInicial.Text = "Estado Inicial";
            this.btnEstadoInicial.UseVisualStyleBackColor = true;
            this.btnEstadoInicial.Click += new System.EventHandler(this.btnEstadoInicial_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(742, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(8, 8);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnEstadoFinal
            // 
            this.btnEstadoFinal.Location = new System.Drawing.Point(696, 70);
            this.btnEstadoFinal.Name = "btnEstadoFinal";
            this.btnEstadoFinal.Size = new System.Drawing.Size(96, 42);
            this.btnEstadoFinal.TabIndex = 2;
            this.btnEstadoFinal.Text = "Estado Final";
            this.btnEstadoFinal.UseVisualStyleBackColor = true;
            this.btnEstadoFinal.Click += new System.EventHandler(this.btnEstadoFinal_Click);
            // 
            // btnTransicion
            // 
            this.btnTransicion.Location = new System.Drawing.Point(696, 118);
            this.btnTransicion.Name = "btnTransicion";
            this.btnTransicion.Size = new System.Drawing.Size(96, 42);
            this.btnTransicion.TabIndex = 3;
            this.btnTransicion.Text = "Transición";
            this.btnTransicion.UseVisualStyleBackColor = true;
            this.btnTransicion.Click += new System.EventHandler(this.btnTransicion_Click);
            // 
            // txtCadena
            // 
            this.txtCadena.Location = new System.Drawing.Point(696, 189);
            this.txtCadena.Name = "txtCadena";
            this.txtCadena.Size = new System.Drawing.Size(96, 22);
            this.txtCadena.TabIndex = 4;
            // 
            // btnSimular
            // 
            this.btnSimular.Location = new System.Drawing.Point(696, 241);
            this.btnSimular.Name = "btnSimular";
            this.btnSimular.Size = new System.Drawing.Size(96, 32);
            this.btnSimular.TabIndex = 5;
            this.btnSimular.Text = "Simular AFD";
            this.btnSimular.UseVisualStyleBackColor = true;
            this.btnSimular.Click += new System.EventHandler(this.btnSimular_Click);
            // 
            // lblResultado
            // 
            this.lblResultado.AutoSize = true;
            this.lblResultado.Location = new System.Drawing.Point(597, 319);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(72, 16);
            this.lblResultado.TabIndex = 6;
            this.lblResultado.Text = "Resultado:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 450);
            this.Controls.Add(this.panelEditor);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panelEditor.ResumeLayout(false);
            this.panelEditor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelEditor;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnEstadoInicial;
        private System.Windows.Forms.Button btnEstadoFinal;
        private System.Windows.Forms.Button btnTransicion;
        private System.Windows.Forms.TextBox txtCadena;
        private System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.Button btnSimular;
    }
}

