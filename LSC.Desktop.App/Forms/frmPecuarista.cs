using LSC.Cross.Tools;
using LSC.Cross.Transport;
using LSC.WebApi.Client;
using LSC.WebApi.Client.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LSC.Desktop.App.Forms
{
    public partial class frmPecuarista : Form
    {

        private bool isUpdate;
        private int rowEdit;
        private int idEdit;

        public frmPecuarista()
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
                    new GridColumns("Nome", "Nome")
                }
            );

            Tools.FormatColumnsGrid
            (
                dgvLista,
                DataGridViewContentAlignment.MiddleLeft,
                DataGridViewContentAlignment.MiddleCenter
            );

        }

        #endregion

        private void frmPecuarista_Activated(object sender, EventArgs e)
        {

            MontaGrid(true);

            using (Consumer cli = new Consumer(ReadConfig.GetStringParameter("UrlApi"), ReadConfig.GetStringParameter("ContentType")))
            {
                try
                {
                    IEnumerable<PecuaristaTransport> result = cli.Get<IEnumerable<PecuaristaTransport>>(ReadConfig.GetStringParameter("ListarPecuaristas"), string.Empty);

                    foreach (PecuaristaTransport item in result)
                        dgvLista.Rows.Add(item.PecuaristaId, item.Nome);

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Falha ao carregar dados \r\n {ex.Message}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

        }

        private void btnGravar_Click(object sender, EventArgs e)
        {

            // Validando Nome
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Informe uma descrição", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return;
            }

            // Persistindo na base
            using (Consumer cli = new Consumer(ReadConfig.GetStringParameter("UrlApi"), ReadConfig.GetStringParameter("ContentType")))
            {
                try
                {

                    PecuaristaTransport a = new PecuaristaTransport()
                    {
                        PecuaristaId = idEdit,
                        Nome = txtNome.Text
                    };

                    int row = rowEdit;

                    if (isUpdate)
                    {
                        cli.Put<PecuaristaTransport, PecuaristaTransport>(a, ReadConfig.GetStringParameter("Pecuarista"));

                        dgvLista.Rows[rowEdit].Cells["Nome"].Value = a.Nome;

                    }
                    else
                    {
                        a = cli.Post<PecuaristaTransport, PecuaristaTransport>(a, ReadConfig.GetStringParameter("Pecuarista"));
                        row = dgvLista.Rows.Add(a.PecuaristaId, a.Nome);
                    }

                    isUpdate = false;
                    txtNome.Text = string.Empty;

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
            txtNome.Text = string.Empty;
            txtNome.Focus();
        }

        private void dgvLista_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            isUpdate = true;
            rowEdit = e.RowIndex;

            idEdit = int.Parse(dgvLista.Rows[rowEdit].Cells["Id"].Value.ToString());
            txtNome.Text = dgvLista.Rows[rowEdit].Cells["Nome"].Value.ToString();

            txtNome.Focus();

        }

        private void dgvLista_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {

                if (dgvLista.SelectedRows.Count > 0)
                {

                    int pecuaristaId = int.Parse(dgvLista.SelectedRows[0].Cells["Id"].Value.ToString());
                    string nome = dgvLista.SelectedRows[0].Cells["Nome"].Value.ToString();

                    if (MessageBox.Show($"Você está prestes a excluir o registro \r\n\r\n[ {pecuaristaId}-{nome} ]\r\n\r\nVocê não poderá desfazer essa ação\r\n\r\nConfirma a operação?", "Excluir", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {


                        // Persistindo na base
                        using (Consumer cli = new Consumer(ReadConfig.GetStringParameter("UrlApi"), ReadConfig.GetStringParameter("ContentType")))
                        {
                            try
                            {
                                
                                cli.Delete<DefaultResponse>(ReadConfig.GetStringParameter("Pecuarista"), $"/{pecuaristaId}");

                                dgvLista.Rows.Remove(dgvLista.SelectedRows[0]);

                                isUpdate = false;
                                txtNome.Text = string.Empty;

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
