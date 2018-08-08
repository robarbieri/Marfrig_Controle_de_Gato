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
    public partial class frmCompra : Form
    {

        protected IDictionary<int, AnimalTransport> _animalCache;

        public frmCompra()
        {
            InitializeComponent();
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
                    new GridColumns("ItemId","ItemId"),
                    new GridColumns("AnimalId","AnimalId"),
                    new GridColumns("Animal", "AnimalDescricao"),
                    new GridColumns("Quantidade", "Quantidade"),
                    new GridColumns("Preço", "Preco"),
                    new GridColumns("Subtotal", "Subtotal")
                }
            );

            Tools.FormatColumnsGrid
            (
                dgvLista,
                DataGridViewContentAlignment.MiddleLeft,
                DataGridViewContentAlignment.MiddleCenter
            );

            dgvLista.Columns["ItemId"].Visible = false;
            dgvLista.Columns["AnimalId"].Visible = false;

            dgvLista.Columns["Quantidade"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["Quantidade"].DefaultCellStyle.Format = "N2";

            dgvLista.Columns["Preco"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["Preco"].DefaultCellStyle.Format = "N2";

            dgvLista.Columns["Subtotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["Subtotal"].DefaultCellStyle.Format = "N2";

        }

        private void TotalizaPedido()
        {

            decimal total = 0;

            foreach (DataGridViewRow linha in dgvLista.Rows)
            {
                decimal subtotal = decimal.Parse(linha.Cells["Subtotal"].Value.ToString());
                total += subtotal;
            }

            lblTotal.Text = total.ToString("N2");

        }

        private bool ItemLancado(int animalId)
        {
            foreach (DataGridViewRow linha in dgvLista.Rows)
            {
                if (linha.Cells["AnimalId"].Value.ToString() == animalId.ToString())
                    return true;
            }
            return false;
        }

        #endregion

        private void frmCompra_Activated(object sender, EventArgs e)
        {

            MontaGrid(true);

            using (Consumer cli = new Consumer(ReadConfig.GetStringParameter("UrlApi"), ReadConfig.GetStringParameter("ContentType")))
            {
                try
                {

                    // Carregando combo de pecuarista
                    IEnumerable<PecuaristaTransport> pecuaristas = cli.Get<IEnumerable<PecuaristaTransport>>(ReadConfig.GetStringParameter("ListarPecuaristas"), string.Empty);

                    cmbPecuarista.DataSource = null;
                    cmbPecuarista.DisplayMember = null;
                    cmbPecuarista.ValueMember = null;

                    cmbPecuarista.DataSource =
                    (
                        from p in pecuaristas
                        select new
                        {
                            Id = p.PecuaristaId,
                            p.Nome
                        }
                    ).ToList();

                    cmbPecuarista.DisplayMember = "Nome";
                    cmbPecuarista.ValueMember = "Id";
                    cmbPecuarista.SelectedIndex = -1;

                    // Carregando combo de Animais
                    IEnumerable<AnimalTransport> animais = cli.Get<IEnumerable<AnimalTransport>>(ReadConfig.GetStringParameter("ListarAnimais"), string.Empty);

                    cmbAnimal.DataSource = null;
                    cmbAnimal.DisplayMember = null;
                    cmbAnimal.ValueMember = null;

                    cmbAnimal.DataSource =
                    (
                        from a in animais
                        select new
                        {
                            Id = a.AnimalId,
                            a.Descricao
                        }
                    ).ToList();

                    cmbAnimal.DisplayMember = "Descricao";
                    cmbAnimal.ValueMember = "Id";
                    cmbAnimal.SelectedIndex = -1;

                    // Populando cache de animais
                    _animalCache = new Dictionary<int, AnimalTransport>();
                    foreach (AnimalTransport a in animais)
                        _animalCache.Add(a.AnimalId, a);

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Falha ao carregar dados \r\n {ex.Message}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            TotalizaPedido();

        }

        private void txtPreco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Tools.NumberKeyPress(e.KeyChar, true, true, false, true))
                e.KeyChar = '\0';
        }

        private void txtQtde_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Tools.NumberKeyPress(e.KeyChar, true, true, false, true))
                e.KeyChar = '\0';
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {

            int animalId;
            string animalNome;

            // Validar dados
            if (cmbAnimal.SelectedIndex < 0)
            {
                MessageBox.Show("Selecine um animal da lista", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbAnimal.Focus();
                return;
            }
            animalId = (int)cmbAnimal.SelectedValue;
            animalNome = _animalCache[animalId].Descricao;

            decimal.TryParse(txtPreco.Text, out decimal preco);
            if (preco <= 0)
            {
                MessageBox.Show("Preço Inválido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPreco.Focus();
                return;
            }

            decimal.TryParse(txtQtde.Text, out decimal qtde);
            if (qtde <= 0)
            {
                MessageBox.Show("Quantidade inválida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQtde.Focus();
                return;
            }

            if (ItemLancado(animalId))
            {
                MessageBox.Show("Esse animal já está no pedido.\r\nPara alterar remova-o e adicione-o novamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQtde.Focus();
                return;
            }

            decimal subtotal = (preco * qtde);

            dgvLista.Rows.Add(1, animalId, animalNome, preco, qtde, subtotal);

            TotalizaPedido();

            cmbAnimal.SelectedIndex = -1;
            txtPreco.Text = string.Empty;
            txtQtde.Text = string.Empty;

            cmbAnimal.Focus();

        }

        private void cmbAnimal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAnimal.SelectedIndex >= 0 && _animalCache != null && _animalCache.Count() > 0)
            {

                int animalId = (int)cmbAnimal.SelectedValue;
                txtPreco.Text = _animalCache[animalId].Preco.ToString("N2");
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {

            int pecuaristaId = 0;

            // Validando dados
            if (!DateTime.TryParse(dtpDataEntrega.Text, out DateTime dataPedido))
            {
                MessageBox.Show("Data de entrega inválida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpDataEntrega.Focus();
                return;
            }
            else
            {
                if (dataPedido < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))
                {
                    MessageBox.Show("Data de entrega não pode ser inferior a data atual", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpDataEntrega.Focus();
                    return;
                }
            }

            if (cmbPecuarista.SelectedIndex < 0)
            {
                MessageBox.Show("Selecione um pecualista", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbPecuarista.Focus();
                return;
            }
            pecuaristaId = int.Parse(cmbPecuarista.SelectedValue.ToString());

            if (dgvLista.Rows.Count.Equals(0))
            {
                MessageBox.Show("Selecione ao menos um item para a compra", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbAnimal.Focus();
                return;
            }

            if (MessageBox.Show("Confirma a gravação da compra?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            // Preparando objeto de transporte
            CompraGadoTransport compra = new CompraGadoTransport()
            {
                DataEntrega = dataPedido,
                PecuaristaId = pecuaristaId
            };

            foreach (DataGridViewRow item in dgvLista.Rows)
            {
                compra.CompraGadoItens.Add(new CompraGadoItemTransport()
                {
                    AnimalId = int.Parse(item.Cells["AnimalId"].Value.ToString()),
                    Preco = decimal.Parse(item.Cells["Preco"].Value.ToString()),
                    Quantidade = decimal.Parse(item.Cells["Quantidade"].Value.ToString())
                });
            }

            // Persistindo na base
            using (Consumer cli = new Consumer(ReadConfig.GetStringParameter("UrlApi"), ReadConfig.GetStringParameter("ContentType")))
            {
                try
                {

                    compra = cli.Post<CompraGadoTransport, CompraGadoTransport>(compra, ReadConfig.GetStringParameter("CompraGado"));

                    MessageBox.Show($"Pedido Gravado com sucesso!\r\n\r\nId do pedido: {compra.CompraGadoId}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    dtpDataEntrega.ResetText();
                    cmbPecuarista.SelectedIndex = -1;
                    cmbAnimal.SelectedIndex = -1;
                    txtPreco.Text = string.Empty;
                    txtQtde.Text = string.Empty;
                    dgvLista.Rows.Clear();

                    dtpDataEntrega.Focus();

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Falha ao gravar dados \r\n {ex.Message}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

        }

        private void dgvLista_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (dgvLista.SelectedRows.Count > 0)
                {
                    if (MessageBox.Show("Confirma a remoção deste item?", "Remover item", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        dgvLista.Rows.Remove(dgvLista.SelectedRows[0]);
                        TotalizaPedido();
                    }
                }
            }
        }

    }

}
