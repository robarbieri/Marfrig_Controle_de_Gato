using System;
using LSC.Desktop.App.Report;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace LSC.Desktop.App.Forms
{
    public partial class frmReportView : Form
    {

        private IEnumerable<CompraItemModelRpt> _compraItemModelRpt;
        private int _compraId;
        private DateTime _dataEntrega;
        private string _pecuarista;
        private decimal _totalPedido;

        bool _sucess;

        public frmReportView(IEnumerable<CompraItemModelRpt> compraItemModelRpt, int compraId, DateTime dataEntrega, string pecuarista)
        {
            InitializeComponent();
            _compraItemModelRpt = compraItemModelRpt;
            _compraId = compraId;
            _dataEntrega = dataEntrega;
            _pecuarista = pecuarista;
            _sucess = false;

            if (compraItemModelRpt != null && compraItemModelRpt.Count() > 0)
                _totalPedido = _compraItemModelRpt.Sum(x => (x.Preco * x.Quantidade));
        }

        private void frmReportView_Activated(object sender, EventArgs e)
        {

            if (!_sucess)
            {

                // Set the processing mode for the ReportViewer to Local  
                rptViewer.ProcessingMode = ProcessingMode.Local;

                LocalReport localReport = rptViewer.LocalReport;

                //localReport.ReportPath = @"D:\Sistemas\Estudos\TesteReportView\TesteReportView\RelatorioTeste.rdlc";
                localReport.ReportPath = @".\Report\rptCompraDetalhe.rdlc";


                ReportDataSource rptDataSource = new ReportDataSource();
                rptDataSource.Name = "CompraItem";
                rptDataSource.Value = _compraItemModelRpt;

                // CompraId
                ReportParameter paramCompraGadoId = new ReportParameter();
                paramCompraGadoId.Name = "CompraGadoId";
                paramCompraGadoId.Values.Add(_compraId.ToString());

                // DataEntrega
                ReportParameter paramDataEntrega = new ReportParameter();
                paramDataEntrega.Name = "DataEntrega";
                paramDataEntrega.Values.Add(_dataEntrega.ToShortDateString());

                // DataEntrega
                ReportParameter paramPecuarista = new ReportParameter();
                paramPecuarista.Name = "Pecuarista";
                paramPecuarista.Values.Add(_pecuarista);

                // Total Pedidos
                ReportParameter paramTotalPedido = new ReportParameter();
                paramTotalPedido.Name = "TotalPedido";
                paramTotalPedido.Values.Add(_totalPedido.ToString("N2"));

                localReport.SetParameters(new ReportParameter[] { paramCompraGadoId, paramDataEntrega, paramPecuarista, paramTotalPedido });

                localReport.DataSources.Add(rptDataSource);

                rptViewer.RefreshReport();

            }

            rptViewer.RefreshReport();
        }
    }
}
