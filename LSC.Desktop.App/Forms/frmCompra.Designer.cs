namespace LSC.Desktop.App.Forms
{
    partial class frmCompra
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
            this.dtpDataEntrega = new System.Windows.Forms.DateTimePicker();
            this.lblPecuarista = new System.Windows.Forms.Label();
            this.cmbPecuarista = new System.Windows.Forms.ComboBox();
            this.pnlAnimais = new System.Windows.Forms.Panel();
            this.btnGravar = new System.Windows.Forms.Button();
            this.dgvLista = new System.Windows.Forms.DataGridView();
            this.txtQtde = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnIncluir = new System.Windows.Forms.Button();
            this.txtPreco = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbAnimal = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlAnimais.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).BeginInit();
            this.SuspendLayout();
            // 
            // lblId
            // 
            this.lblId.Location = new System.Drawing.Point(14, 12);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(89, 18);
            this.lblId.TabIndex = 0;
            this.lblId.Text = "Id:";
            this.lblId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtId
            // 
            this.txtId.Enabled = false;
            this.txtId.Location = new System.Drawing.Point(109, 12);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(111, 20);
            this.txtId.TabIndex = 1;
            // 
            // lblDataEntrega
            // 
            this.lblDataEntrega.Location = new System.Drawing.Point(11, 41);
            this.lblDataEntrega.Name = "lblDataEntrega";
            this.lblDataEntrega.Size = new System.Drawing.Size(92, 18);
            this.lblDataEntrega.TabIndex = 2;
            this.lblDataEntrega.Text = "Data de Entrega:";
            this.lblDataEntrega.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpDataEntrega
            // 
            this.dtpDataEntrega.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataEntrega.Location = new System.Drawing.Point(109, 41);
            this.dtpDataEntrega.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dtpDataEntrega.Name = "dtpDataEntrega";
            this.dtpDataEntrega.Size = new System.Drawing.Size(111, 20);
            this.dtpDataEntrega.TabIndex = 2;
            // 
            // lblPecuarista
            // 
            this.lblPecuarista.Location = new System.Drawing.Point(26, 69);
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
            this.cmbPecuarista.Location = new System.Drawing.Point(109, 67);
            this.cmbPecuarista.Name = "cmbPecuarista";
            this.cmbPecuarista.Size = new System.Drawing.Size(538, 21);
            this.cmbPecuarista.TabIndex = 3;
            // 
            // pnlAnimais
            // 
            this.pnlAnimais.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAnimais.Controls.Add(this.label5);
            this.pnlAnimais.Controls.Add(this.lblTotal);
            this.pnlAnimais.Controls.Add(this.label4);
            this.pnlAnimais.Controls.Add(this.btnGravar);
            this.pnlAnimais.Controls.Add(this.dgvLista);
            this.pnlAnimais.Controls.Add(this.txtQtde);
            this.pnlAnimais.Controls.Add(this.label3);
            this.pnlAnimais.Controls.Add(this.btnIncluir);
            this.pnlAnimais.Controls.Add(this.txtPreco);
            this.pnlAnimais.Controls.Add(this.label2);
            this.pnlAnimais.Controls.Add(this.cmbAnimal);
            this.pnlAnimais.Controls.Add(this.label1);
            this.pnlAnimais.Location = new System.Drawing.Point(17, 99);
            this.pnlAnimais.Name = "pnlAnimais";
            this.pnlAnimais.Size = new System.Drawing.Size(642, 312);
            this.pnlAnimais.TabIndex = 6;
            // 
            // btnGravar
            // 
            this.btnGravar.Location = new System.Drawing.Point(539, 268);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(90, 23);
            this.btnGravar.TabIndex = 9;
            this.btnGravar.Text = "Gravar Compra";
            this.btnGravar.UseVisualStyleBackColor = true;
            this.btnGravar.Click += new System.EventHandler(this.btnGravar_Click);
            // 
            // dgvLista
            // 
            this.dgvLista.AllowUserToAddRows = false;
            this.dgvLista.AllowUserToDeleteRows = false;
            this.dgvLista.AllowUserToResizeRows = false;
            this.dgvLista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLista.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvLista.Location = new System.Drawing.Point(16, 68);
            this.dgvLista.MultiSelect = false;
            this.dgvLista.Name = "dgvLista";
            this.dgvLista.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLista.Size = new System.Drawing.Size(613, 194);
            this.dgvLista.TabIndex = 12;
            this.dgvLista.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvLista_KeyUp);
            // 
            // txtQtde
            // 
            this.txtQtde.Location = new System.Drawing.Point(235, 39);
            this.txtQtde.MaxLength = 12;
            this.txtQtde.Name = "txtQtde";
            this.txtQtde.Size = new System.Drawing.Size(103, 20);
            this.txtQtde.TabIndex = 6;
            this.txtQtde.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQtde.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQtde_KeyPress);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(181, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Qtde:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnIncluir
            // 
            this.btnIncluir.Location = new System.Drawing.Point(539, 39);
            this.btnIncluir.Name = "btnIncluir";
            this.btnIncluir.Size = new System.Drawing.Size(90, 23);
            this.btnIncluir.TabIndex = 7;
            this.btnIncluir.Text = "Incluir";
            this.btnIncluir.UseVisualStyleBackColor = true;
            this.btnIncluir.Click += new System.EventHandler(this.btnIncluir_Click);
            // 
            // txtPreco
            // 
            this.txtPreco.Location = new System.Drawing.Point(57, 39);
            this.txtPreco.MaxLength = 12;
            this.txtPreco.Name = "txtPreco";
            this.txtPreco.Size = new System.Drawing.Size(103, 20);
            this.txtPreco.TabIndex = 5;
            this.txtPreco.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPreco.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPreco_KeyPress);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Preço:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmbAnimal
            // 
            this.cmbAnimal.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbAnimal.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbAnimal.FormattingEnabled = true;
            this.cmbAnimal.Location = new System.Drawing.Point(57, 8);
            this.cmbAnimal.Name = "cmbAnimal";
            this.cmbAnimal.Size = new System.Drawing.Size(572, 21);
            this.cmbAnimal.TabIndex = 4;
            this.cmbAnimal.SelectedIndexChanged += new System.EventHandler(this.cmbAnimal_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Animal:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(13, 273);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "Total do Pedido:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(101, 273);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(133, 17);
            this.lblTotal.TabIndex = 14;
            this.lblTotal.Text = "###.###,##";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.Color.Maroon;
            this.label5.Location = new System.Drawing.Point(264, 270);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(184, 31);
            this.label5.TabIndex = 15;
            this.label5.Text = "Selecione uma linha e pressione <DELETE> para excluir o item";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 423);
            this.Controls.Add(this.pnlAnimais);
            this.Controls.Add(this.cmbPecuarista);
            this.Controls.Add(this.lblPecuarista);
            this.Controls.Add(this.dtpDataEntrega);
            this.Controls.Add(this.lblDataEntrega);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.lblId);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCompra";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cadastro de Compra de Gado";
            this.Activated += new System.EventHandler(this.frmCompra_Activated);
            this.pnlAnimais.ResumeLayout(false);
            this.pnlAnimais.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblDataEntrega;
        private System.Windows.Forms.DateTimePicker dtpDataEntrega;
        private System.Windows.Forms.Label lblPecuarista;
        private System.Windows.Forms.ComboBox cmbPecuarista;
        private System.Windows.Forms.Panel pnlAnimais;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbAnimal;
        private System.Windows.Forms.TextBox txtPreco;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnIncluir;
        private System.Windows.Forms.TextBox txtQtde;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvLista;
        private System.Windows.Forms.Button btnGravar;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}