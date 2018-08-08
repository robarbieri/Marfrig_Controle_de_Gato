using LSC.Cross.Tools;
using LSC.Cross.Transport;
using LSC.WebApi.Client;
using LSC.WebApi.Client.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LSC.Desktop.App.Forms
{
    public partial class frmAnimal : Form
    {

        private bool isUpdate;
        private int rowEdit;
        private int idEdit;

        public frmAnimal()
        {
            InitializeComponent();
            isUpdate = false;
        }

        #region Métodos Locais

        private void MontaGrid(bool firstTime = false)
        {

            if (firstTime)
                dgvLista.Columns.Clear();

            Tools.SetColumnsGrid
            (
                dgvLista,
                firstTime,
                new List<GridColumns>()
                {
                    new GridColumns("Id","Id"),
                    new GridColumns("Descrição", "Descricao"),
                    new GridColumns("Preço", "Preco")
                }
            );

            Tools.FormatColumnsGrid
            (
                dgvLista,
                DataGridViewContentAlignment.MiddleLeft,
                DataGridViewContentAlignment.MiddleCenter
            );

            dgvLista.Columns["Preco"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["Preco"].DefaultCellStyle.Format = "N2";

        }

        #endregion

        private void frmAnimal_Activated(object sender, EventArgs e)
        {

            MontaGrid(true);

            using (Consumer cli = new Consumer(ReadConfig.GetStringParameter("UrlApi"), ReadConfig.GetStringParameter("ContentType")))
            {
                try
                {
                    IEnumerable<AnimalTransport> result = cli.Get<IEnumerable<AnimalTransport>>(ReadConfig.GetStringParameter("ListarAnimais"), string.Empty);

                    foreach (AnimalTransport item in result)
                        dgvLista.Rows.Add(item.AnimalId, item.Descricao, item.Preco);

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Falha ao carregar dados \r\n {ex.Message}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

        }

        private void txtPreco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Tools.NumberKeyPress(e.KeyChar, true, true, false, true))
                e.KeyChar = '\0';
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {

            // Validando Descricao
            if (string.IsNullOrWhiteSpace(txtDescricao.Text))
            {
                MessageBox.Show("Informe uma descrição", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescricao.Focus();
                return;
            }

            decimal.TryParse(txtPreco.Text, out decimal preco);
            if (preco <= 0)
            {
                MessageBox.Show("Preço Inválido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPreco.Focus();
                return;
            }

            // Persistindo na base
            using (Consumer cli = new Consumer(ReadConfig.GetStringParameter("UrlApi"), ReadConfig.GetStringParameter("ContentType")))
            {
                try
                {

                    AnimalTransport a = new AnimalTransport()
                    {
                        AnimalId = idEdit,
                        Descricao = txtDescricao.Text,
                        Preco = preco
                    };

                    int row = rowEdit;

                    if (isUpdate)
                    {
                        cli.Put<AnimalTransport, AnimalTransport>(a, ReadConfig.GetStringParameter("Animal"));

                        dgvLista.Rows[rowEdit].Cells["Descricao"].Value = a.Descricao;
                        dgvLista.Rows[rowEdit].Cells["Preco"].Value = preco.ToString("N2");

                    }
                    else
                    {
                        a = cli.Post<AnimalTransport, AnimalTransport>(a, ReadConfig.GetStringParameter("Animal"));
                        row = dgvLista.Rows.Add(a.AnimalId, a.Descricao, a.Preco);
                    }

                    isUpdate = false;
                    txtDescricao.Text = string.Empty;
                    txtPreco.Text = string.Empty;

                    dgvLista.Rows[row].Selected = true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Falha ao gravar dados \r\n {ex.Message}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }


        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            isUpdate = false;
            txtDescricao.Text = string.Empty;
            txtPreco.Text = string.Empty;
            txtDescricao.Focus();
        }

        private void dgvLista_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            isUpdate = true;
            rowEdit = e.RowIndex;

            idEdit = int.Parse(dgvLista.Rows[rowEdit].Cells["Id"].Value.ToString());
            txtDescricao.Text = dgvLista.Rows[rowEdit].Cells["Descricao"].Value.ToString();
            txtPreco.Text = dgvLista.Rows[rowEdit].Cells["Preco"].Value.ToString();

            txtDescricao.Focus();

        }

        private void dgvLista_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {

                if (dgvLista.SelectedRows.Count > 0)
                {

                    int animalId = int.Parse(dgvLista.SelectedRows[0].Cells["Id"].Value.ToString());
                    string descricao = dgvLista.SelectedRows[0].Cells["Descricao"].Value.ToString();

                    if (MessageBox.Show($"Você está prestes a excluir o registro \r\n\r\n[ {animalId}-{descricao} ]\r\n\r\nVocê não poderá desfazer essa ação\r\n\r\nConfirma a operação?", "Excluir", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {


                        // Persistindo na base
                        using (Consumer cli = new Consumer(ReadConfig.GetStringParameter("UrlApi"), ReadConfig.GetStringParameter("ContentType")))
                        {
                            try
                            {
                                
                                cli.Delete<DefaultResponse>(ReadConfig.GetStringParameter("Animal"), $"/{animalId}");

                                dgvLista.Rows.Remove(dgvLista.SelectedRows[0]);

                                isUpdate = false;
                                txtDescricao.Text = string.Empty;
                                txtPreco.Text = string.Empty;

                                dgvLista.Focus();

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Falha ao excluir dados? \r\n {ex.Message}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }

                    }

                }

            }
        }
    }
}
