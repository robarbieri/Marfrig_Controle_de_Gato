namespace LSC.Desktop.App.Forms
{
    partial class frmConsultaCompra
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
            this.lblId = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblDataEntrega = new System.Windows.Forms.Label();
            this.lblPecuarista = new System.Windows.Forms.Label();
            this.cmbPecuarista = new System.Windows.Forms.ComboBox();
            this.pnlAnimais = new System.Windows.Forms.Panel();
            this.hsbPagina = new System.Windows.Forms.HScrollBar();
            this.dgvLista = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.mskDataInicial = new System.Windows.Forms.MaskedTextBox();
            this.mskDataFinal = new System.Windows.Forms.MaskedTextBox();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.pnlAnimais.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).BeginInit();
            this.SuspendLayout();
            // 
            // lblId
            // 
            this.lblId.Location = new System.Drawing.Point(20, 10);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(89, 18);
            this.lblId.TabIndex = 0;
            this.lblId.Text = "Id:";
            this.lblId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(121, 10);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(111, 20);
            this.txtId.TabIndex = 1;
            this.txtId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtId_KeyPress);
            // 
            // lblDataEntrega
            // 
            this.lblDataEntrega.Location = new System.Drawing.Point(11, 39);
            this.lblDataEntrega.Name = "lblDataEntrega";
            this.lblDataEntrega.Size = new System.Drawing.Size(104, 18);
            this.lblDataEntrega.TabIndex = 2;
            this.lblDataEntrega.Text = "Data de Entrega de:";
            this.lblDataEntrega.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPecuarista
            // 
            this.lblPecuarista.Location = new System.Drawing.Point(238, 12);
            this.lblPecuarista.Name = "lblPecuarista";
            this.lblPecuarista.Size = new System.Drawing.Size(77, 18);
            this.lblPecuarista.TabIndex = 4;
            this.lblPecuarista.Text = "Pecuarista:";
            this.lblPecuarista.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbPecuarista
            // 
            this.cmbPecuarista.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPecuarista.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPecuarista.FormattingEnabled = true;
            this.cmbPecuarista.Location = new System.Drawing.Point(321, 10);
            this.cmbPecuarista.Name = "cmbPecuarista";
            this.cmbPecuarista.Size = new System.Drawing.Size(326, 21);
            this.cmbPecuarista.TabIndex = 2;
            // 
            // pnlAnimais
            // 
            this.pnlAnimais.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAnimais.Controls.Add(this.hsbPagina);
            this.pnlAnimais.Controls.Add(this.dgvLista);
            this.pnlAnimais.Location = new System.Drawing.Point(17, 70);
            this.pnlAnimais.Name = "pnlAnimais";
            this.pnlAnimais.Size = new System.Drawing.Size(642, 281);
            this.pnlAnimais.TabIndex = 6;
            // 
            // hsbPagina
            // 
            this.hsbPagina.LargeChange = 1;
            this.hsbPagina.Location = new System.Drawing.Point(163, 258);
            this.hsbPagina.Name = "hsbPagina";
            this.hsbPagina.Size = new System.Drawing.Size(318, 17);
            this.hsbPagina.TabIndex = 13;
            this.hsbPagina.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hsbPagina_Scroll);
            // 
            // dgvLista
            // 
            this.dgvLista.AllowUserToAddRows = false;
            this.dgvLista.AllowUserToDeleteRows = false;
            this.dgvLista.AllowUserToResizeRows = false;
            this.dgvLista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLista.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvLista.Location = new System.Drawing.Point(5, 8);
            this.dgvLista.MultiSelect = false;
            this.dgvLista.Name = "dgvLista";
            this.dgvLista.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLista.Size = new System.Drawing.Size(628, 247);
            this.dgvLista.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(250, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 18);
            this.label6.TabIndex = 7;
            this.label6.Text = "Até:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mskDataInicial
            // 
            this.mskDataInicial.Location = new System.Drawing.Point(121, 39);
            this.mskDataInicial.Mask = "00/00/0000";
            this.mskDataInicial.Name = "mskDataInicial";
            this.mskDataInicial.Size = new System.Drawing.Size(111, 20);
            this.mskDataInicial.TabIndex = 3;
            this.mskDataInicial.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.mskDataInicial.ValidatingType = typeof(System.DateTime);
            // 
            // mskDataFinal
            // 
            this.mskDataFinal.Location = new System.Drawing.Point(321, 39);
            this.mskDataFinal.Mask = "00/00/0000";
            this.mskDataFinal.Name = "mskDataFinal";
            this.mskDataFinal.Size = new System.Drawing.Size(111, 20);
            this.mskDataFinal.TabIndex = 4;
            this.mskDataFinal.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.mskDataFinal.ValidatingType = typeof(System.DateTime);
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.Location = new System.Drawing.Point(557, 39);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(90, 23);
            this.btnPesquisar.TabIndex = 8;
            this.btnPesquisar.Text = "Pesquisar";
            this.btnPesquisar.UseVisualStyleBackColor = true;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(525, 359);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(134, 23);
            this.btnPrint.TabIndex = 9;
            this.btnPrint.Text = "Imprimir Selecionado";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // frmConsultaCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 391);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnPesquisar);
            this.Controls.Add(this.mskDataFinal);
            this.Controls.Add(this.mskDataInicial);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pnlAnimais);
            this.Controls.Add(this.cmbPecuarista);
            this.Controls.Add(this.lblPecuarista);
            this.Controls.Add(this.lblDataEntrega);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.lblId);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConsultaCompra";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Consulta de Compras";
            this.Activated += new System.EventHandler(this.frmConsultaCompra_Activated);
            this.pnlAnimais.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblDataEntrega;
        private System.Windows.Forms.Label lblPecuarista;
        private System.Windows.Forms.ComboBox cmbPecuarista;
        private System.Windows.Forms.Panel pnlAnimais;
        private System.Windows.Forms.DataGridView dgvLista;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MaskedTextBox mskDataInicial;
        private System.Windows.Forms.MaskedTextBox mskDataFinal;
        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.HScrollBar hsbPagina;
        private System.Windows.Forms.Button btnPrint;
    }
}