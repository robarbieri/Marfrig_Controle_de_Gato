using LSC.Cross.Tools;
using LSC.Cross.Transport;
using LSC.Desktop.App.Report;
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
    public partial class frmConsultaCompra : Form
    {

        private IEnumerable<CompraGadoTransport> _cacheCompra;
        const int registrosPorPagina = 10;
        int pagina;


        public frmConsultaCompra()
        {
            InitializeComponent();
        }

        protected enum MetodoPesquisaEnum
        {
            Tudo,
            Id,
            Pecuarista,
            Periodo,
            PecuaristaPeriodo
        }

        private void ExibeCompraGrid(int pagina)
        {
            int inicio = ((pagina * registrosPorPagina) - registrosPorPagina);
            int final = (inicio + registrosPorPagina - 1);

            int numItem = 0;
            dgvLista.Rows.Clear();
            foreach (CompraGadoTransport compra in _cacheCompra)
            {

                if (numItem >= inicio && numItem <= final)
                {
                    dgvLista.Rows.Add(compra.CompraGadoId, compra.Pecuarista.Nome, compra.DataEntrega, compra.CompraGadoItens.Sum(x => x.Quantidade * x.Preco));
                }
                numItem++;

                if (numItem > final)
                    break;

            }
        }

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
                    new GridColumns("Id","CompraId"),
                    new GridColumns("Pecuarista", "PecuaristaNome"),
                    new GridColumns("Dt.Entrega", "DataEntrega"),
                    new GridColumns("Total", "Total"),
                }
            );

            Tools.FormatColumnsGrid
            (
                dgvLista,
                DataGridViewContentAlignment.MiddleLeft,
                DataGridViewContentAlignment.MiddleCenter
            );

            dgvLista.Columns["DataEntrega"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["DataEntrega"].DefaultCellStyle.Format = "dd/MM/yyyy";

            dgvLista.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLista.Columns["Total"].DefaultCellStyle.Format = "N2";

        }

        private void frmConsultaCompra_Activated(object sender, EventArgs e)
        {

            _cacheCompra = new List<CompraGadoTransport>();

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

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Falha ao carregar dados \r\n {ex.Message}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {

            MetodoPesquisaEnum metodo = MetodoPesquisaEnum.Tudo;

            int compraId = 0;
            int pecuaristaId = 0;
            DateTime dataInicial = DateTime.Now;
            DateTime dataFinal = DateTime.Now;

            // Validando e tomando decisão de método de busca de dados
            // ------------------------------------------------------------------------

            // Id
            if (!string.IsNullOrEmpty(txtId.Text))
            {
                if (!int.TryParse(txtId.Text, out compraId))
                {
                    MessageBox.Show("Id do pedido inválido!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtId.Focus();
                    return;
                }
                metodo = MetodoPesquisaEnum.Id;
            }
            else
            {

                // Pecuarista
                if (cmbPecuarista.SelectedIndex >= 0)
                {
                    pecuaristaId = int.Parse(cmbPecuarista.SelectedValue.ToString());
                    metodo = MetodoPesquisaEnum.Pecuarista;
                }

                if (!string.IsNullOrEmpty(mskDataInicial.Text) || !string.IsNullOrEmpty(mskDataFinal.Text))
                {

                    if
                    (
                        mskDataInicial.Text.Length != 8 ||
                        !DateTime.TryParse($"{mskDataInicial.Text.Substring(0, 2)}/{mskDataInicial.Text.Substring(2, 2)}/{mskDataInicial.Text.Substring(4, 4)}", out dataInicial)
                    )
                    {
                        MessageBox.Show("Data inicial inválida!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        mskDataInicial.Focus();
                        return;
                    }

                    if
                    (
                        mskDataFinal.Text.Length != 8 ||
                        !DateTime.TryParse($"{mskDataFinal.Text.Substring(0, 2)}/{mskDataFinal.Text.Substring(2, 2)}/{mskDataFinal.Text.Substring(4, 4)}", out dataFinal)
                    )
                    {
                        MessageBox.Show("Data final inválida!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        mskDataInicial.Focus();
                        return;
                    }

                    if (dataFinal < dataInicial)
                    {
                        MessageBox.Show("Data final não pode ser superior a data inicial!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        mskDataInicial.Focus();
                        return;
                    }

                    if (metodo == MetodoPesquisaEnum.Pecuarista)
                        metodo = MetodoPesquisaEnum.PecuaristaPeriodo;
                    else
                        metodo = MetodoPesquisaEnum.Periodo;

                }

            }

            string apiMethod = apiMethod = ReadConfig.GetStringParameter("ListarCompras");
            string queryString = string.Empty;

            // Preparando consumo/consulta
            switch (metodo)
            {
                case MetodoPesquisaEnum.Tudo:
                    break;
                case MetodoPesquisaEnum.Id:
                    apiMethod = ReadConfig.GetStringParameter("CompraGado");
                    queryString = $"/{compraId}";
                    break;
                case MetodoPesquisaEnum.Periodo:
                    queryString = $"/Periodo/{dataInicial.ToString("yyyy-MM-dd")}/{dataFinal.ToString("yyyy-MM-dd")}";
                    break;
                case MetodoPesquisaEnum.Pecuarista:
                    queryString = $"/Pecuarista/{pecuaristaId.ToString()}";
                    break;
                case MetodoPesquisaEnum.PecuaristaPeriodo:
                    queryString = $"/PeriodoPecuarista/{pecuaristaId}/{dataInicial.ToString("yyyy-MM-dd")}/{dataFinal.ToString("yyyy-MM-dd")}";
                    break;
            }

            pagina = 1;

            using (Consumer cli = new Consumer(ReadConfig.GetStringParameter("UrlApi"), ReadConfig.GetStringParameter("ContentType")))
            {
                try
                {

                    // Carregando combo de pecuarista
                    if (metodo == MetodoPesquisaEnum.Id)
                    {
                        CompraGadoTransport result = cli.Get<CompraGadoTransport>(apiMethod, queryString);
                        ((List<CompraGadoTransport>)_cacheCompra).Add(result);
                    }
                    else
                        _cacheCompra = cli.Get<IEnumerable<CompraGadoTransport>>(apiMethod, queryString);

                    hsbPagina.Visible = false;
                    if (_cacheCompra.Count() > registrosPorPagina)
                    {
                        hsbPagina.Visible = true;
                        hsbPagina.Minimum = 1;
                        hsbPagina.Value = 1;
                        hsbPagina.Maximum = (_cacheCompra.Count() / registrosPorPagina);

                        if ((_cacheCompra.Count() % registrosPorPagina) > 0)
                            hsbPagina.Maximum++;

                    }

                    ExibeCompraGrid(pagina);

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Falha ao carregar dados \r\n {ex.Message}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Tools.NumberKeyPress(e.KeyChar, false, false, false, true))
                e.KeyChar = '\0';
        }

        private void hsbPagina_Scroll(object sender, ScrollEventArgs e)
        {
            pagina = e.NewValue;
            ExibeCompraGrid(pagina);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {


            if (dgvLista.SelectedRows.Count > 0)
            {
                
                int compraId = int.Parse(dgvLista.SelectedRows[0].Cells["CompraId"].Value.ToString());

                CompraGadoTransport compra = _cacheCompra.Where(x => x.CompraGadoId.Equals(compraId)).FirstOrDefault();

                IList<CompraItemModelRpt> items =
                (
                    from i in compra.CompraGadoItens
                    select new CompraItemModelRpt()
                    {
                        Animal = i.Animal.Descricao,
                        Preco = i.Preco,
                        Quantidade = i.Quantidade,
                        Subtotal = (i.Preco * i.Quantidade)
                    }
                ).ToList();

                var formRpt = new frmReportView(items, compraId, compra.DataEntrega, $"{compra.Pecuarista.PecuaristaId} - {compra.Pecuarista.Nome}");
                formRpt.ShowDialog(this);

            }

        }
    }

}
