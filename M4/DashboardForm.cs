using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using M4.Context;
using M4.Models;
using Microsoft.EntityFrameworkCore;

namespace M4
{
    public partial class DashboardForm : Form
    {
        private readonly Sessao4 context;
        private LoginForm loginForm;
        private List<Atendimentos> atdList = new List<Atendimentos>();
        private List<TiposAtendimento> tipoAtdList = new List<TiposAtendimento>();
        private List<Cidades> cdList = new List<Cidades>();
        private int finalCount = 0;
        private bool emAtendimento = false;
        private int index = 0;

        public DashboardForm()
        {
            InitializeComponent();
            context = Sessao4.GetInstance();
            SetCalendaries();
            SetComboBox();
            SetData();
        }

        private void SetComboBox()
        {
            dataCb.DataSource = new string[] { "Selecione", "7 Dias", "15 Dias", "30 Dias", "60 Dias" };
            dataCb.SelectedIndex = 0;
            TransCb.DataSource = new string[] { "Selecione", "Ambulância", "UTI Móvel", "Helicóptero" };
            TransCb.SelectedIndex = 0;
            tipoAtdList = context.TiposAtendimento.ToList();
            tipoCb.DataSource = tipoAtdList.Select(a => a.TipoAtendimento).ToList();
            cdList = context.Cidades.Include(c => c.Estado).ToList();
            origemCb.DataSource = cdList.Select(a => a.Cidade).ToList();
            destinoCb.DataSource = cdList.Select(a => a.Cidade).ToList();
            var list = new string[] { "Linha (Line)", "Barras Horizontais (Column)", "Barras Verticais (Bar)" };
            var list2 = new string[] { "Linha (Line)", "Barras Horizontais (Column)", "Barras Verticais (Bar)" };

            chartCbStatus.DataSource = list;
            chartCbStatus.SelectedIndex = 0;
            chartCbAtendimento.DataSource = list2;
            chartCbAtendimento.SelectedIndex = 0;
        }

        private void SetData()
        {
            SetUsersList();
            SetDGV();
            SetPizzaChart();
            SetChartAtendimento();
            SetChartStatus();
        }

        private void SetChartAtendimento()
        {
            var id = Properties.Settings.Default.UsuarioId;
            var atendimentos = context.Atendimentos.Include(a => a.TipoAtendimento).Where(a => a.ResponsavelId == id).ToList();
            chartAtendimento.Series.Clear();
            chartAtendimento.Legends.Clear();
            chartAtendimento.ChartAreas.Clear();
            chartAtendimento.ChartAreas.Add(new ChartArea());

            var series = new Series { BorderWidth = 3, ChartType = chartCbAtendimento.SelectedIndex == 0 ? SeriesChartType.Line : chartCbAtendimento.SelectedIndex == 1 ? SeriesChartType.Column : SeriesChartType.Bar, IsValueShownAsLabel = true};

            series.Points.Add(new DataPoint { AxisLabel = "Consulta", YValues = new double[] { atendimentos.Count(a => a.TipoAtendimento.TipoAtendimento == "Consulta")}, Color = Color.Blue });
            series.Points.Add(new DataPoint { AxisLabel = "Cirurgia", YValues = new double[] { atendimentos.Count(a => a.TipoAtendimento.TipoAtendimento == "Cirurgia") }, Color = Color.Yellow  });
            series.Points.Add(new DataPoint { AxisLabel = "Internação", YValues = new double[] { atendimentos.Count(a => a.TipoAtendimento.TipoAtendimento == "Internação") }, Color = Color.Green });
            series.Points.Add(new DataPoint { AxisLabel = "UTI", YValues = new double[] { atendimentos.Count(a => a.TipoAtendimento.TipoAtendimento == "UTI") }, Color = Color.Purple });
            chartAtendimento.Series.Add(series);
        }

        private void SetChartStatus()
        {
            var id = Properties.Settings.Default.UsuarioId;
            var atendimentos = context.Atendimentos.Include(a => a.StatusClinico).Where(a => a.ResponsavelId == id).ToList();
            var status = context.StatusClinico.ToList();
            chartStatus.Series.Clear();
            chartStatus.Legends.Clear();
            chartStatus.ChartAreas.Clear();
            chartStatus.ChartAreas.Add(new ChartArea());

            var corAguardando = status[0].Cor;
            var corTratamento = status[1].Cor;
            var corAlta = status[2].Cor;
            var corObito = status[3].Cor;
            
            var series = new Series { BorderWidth = 3, ChartType = chartCbStatus.SelectedIndex == 0 ? SeriesChartType.Line : chartCbStatus.SelectedIndex == 1 ? SeriesChartType.Column : SeriesChartType.Bar, IsValueShownAsLabel = true };

            series.Points.Add(new DataPoint { AxisLabel = "Aguardando atendimento", YValues = new double[] { atendimentos.Count(a => a.StatusClinico.Status == "Aguardando atendimento") }, Color = ColorTranslator.FromHtml(corAguardando) });
            series.Points.Add(new DataPoint { AxisLabel = "Em tratamento", YValues = new double[] { atendimentos.Count(a => a.StatusClinico.Status == "Em tratamento") }, Color = ColorTranslator.FromHtml(corTratamento) });
            series.Points.Add(new DataPoint { AxisLabel = "Alta", YValues = new double[] { atendimentos.Count(a => a.StatusClinico.Status == "Alta") }, Color = ColorTranslator.FromHtml(corAlta) });
            series.Points.Add(new DataPoint { AxisLabel = "Óbito", YValues = new double[] { atendimentos.Count(a => a.StatusClinico.Status == "Óbito") }, Color = ColorTranslator.FromHtml(corObito) });
            chartStatus.Series.Add(series);
        }

        private void SetPizzaChart()
        {
            var tranferencias = context.TransferenciasPacientes.ToList();
            pizzaChart.Series.Clear();
            pizzaChart.ChartAreas.Clear();
            pizzaChart.ChartAreas.Add(new ChartArea());
            var serie = new Series { ChartType = SeriesChartType.Pie };
            if (dataCb.SelectedIndex == 0 && TransCb.SelectedIndex == 0)
            {
               var price = tranferencias.Sum(t => t.ValorTotalPago);
               totalTxt.Text = "R$" + price;
               transTxt.Text = tranferencias.Count().ToString();
               serie.Points.AddXY("Ambulância", tranferencias.Where(e => e.TipoTransporte == "Ambulância").Count());
               serie.Points.AddXY("UTI Móvel", tranferencias.Where(e => e.TipoTransporte == "UTI Móvel").Count());
               serie.Points.AddXY("Helicóptero", tranferencias.Where(e => e.TipoTransporte == "Helicóptero").Count());
            }
            else if (dataCb.SelectedIndex > 0 && TransCb.SelectedIndex == 0)
            {
                double? price = 0;
                switch(dataCb.SelectedIndex)
                {
                    case 1:
                        serie.Points.AddXY("Ambulância", tranferencias.Where(e => e.DataTransferencia > DateTime.Now.AddDays(-7) && e.TipoTransporte == "Ambulância").Count());
                        serie.Points.AddXY("UTI Móvel", tranferencias.Where(e => e.DataTransferencia > DateTime.Now.AddDays(-7) && e.TipoTransporte == "UTI Móvel").Count());
                        serie.Points.AddXY("Helicóptero", tranferencias.Where(e => e.DataTransferencia > DateTime.Now.AddDays(-7) && e.TipoTransporte == "Helicóptero").Count());

                        price = tranferencias.Where(e => e.DataTransferencia > DateTime.Now.AddDays(-7)).Sum(t => t.ValorTotalPago);
                        totalTxt.Text = "R$" + price;
                        transTxt.Text = tranferencias.Where(e => e.DataTransferencia > DateTime.Now.AddDays(-7)).Count().ToString();
                        break;
                    case 2:
                        serie.Points.AddXY("Ambulância", tranferencias.Where(e => e.DataTransferencia > DateTime.Now.AddDays(-15) && e.TipoTransporte == "Ambulância").Count());
                        serie.Points.AddXY("UTI Móvel", tranferencias.Where(e => e.DataTransferencia > DateTime.Now.AddDays(-15) && e.TipoTransporte == "UTI Móvel").Count());
                        serie.Points.AddXY("Helicóptero", tranferencias.Where(e => e.DataTransferencia > DateTime.Now.AddDays(-15) && e.TipoTransporte == "Helicóptero").Count());

                        price = tranferencias.Where(e => e.DataTransferencia > DateTime.Now.AddDays(-15)).Sum(t => t.ValorTotalPago);
                        totalTxt.Text = "R$" + price;
                        transTxt.Text = tranferencias.Where(e => e.DataTransferencia > DateTime.Now.AddDays(-15)).Count().ToString();
                        break;
                    case 3:
                        serie.Points.AddXY("Ambulância", tranferencias.Where(e => e.DataTransferencia > DateTime.Now.AddDays(-30) && e.TipoTransporte == "Ambulância").Count());
                        serie.Points.AddXY("UTI Móvel", tranferencias.Where(e => e.DataTransferencia > DateTime.Now.AddDays(-30) && e.TipoTransporte == "UTI Móvel").Count());
                        serie.Points.AddXY("Helicóptero", tranferencias.Where(e => e.DataTransferencia > DateTime.Now.AddDays(-30) && e.TipoTransporte == "Helicóptero").Count());

                        price = tranferencias.Where(e => e.DataTransferencia > DateTime.Now.AddDays(-30)).Sum(t => t.ValorTotalPago);
                        totalTxt.Text = "R$" + price;
                        transTxt.Text = tranferencias.Where(e => e.DataTransferencia > DateTime.Now.AddDays(-30)).Count().ToString();
                        break;
                    case 4:
                        serie.Points.AddXY("Ambulância", tranferencias.Where(e => e.DataTransferencia > DateTime.Now.AddDays(-60) && e.TipoTransporte == "Ambulância").Count());
                        serie.Points.AddXY("UTI Móvel", tranferencias.Where(e => e.DataTransferencia > DateTime.Now.AddDays(-60) && e.TipoTransporte == "UTI Móvel").Count());
                        serie.Points.AddXY("Helicóptero", tranferencias.Where(e => e.DataTransferencia > DateTime.Now.AddDays(-60) && e.TipoTransporte == "Helicóptero").Count());

                        price = tranferencias.Where(e => e.DataTransferencia > DateTime.Now.AddDays(-60)).Sum(t => t.ValorTotalPago);
                        totalTxt.Text = "R$" + price;
                        transTxt.Text = tranferencias.Where(e => e.DataTransferencia > DateTime.Now.AddDays(-60)).Count().ToString();
                        break;
                }
            }
            else if (dataCb.SelectedIndex == 0 && TransCb.SelectedIndex > 0)
            {
                serie.Points.AddXY(TransCb.SelectedItem.ToString(), tranferencias.Where(e => e.TipoTransporte == TransCb.SelectedItem.ToString()).Count());

                var price = tranferencias.Where(e => e.TipoTransporte == TransCb.SelectedItem.ToString()).Sum(t => t.ValorTotalPago);
                totalTxt.Text = "R$" + price;
                transTxt.Text = tranferencias.Where(e => e.TipoTransporte == TransCb.SelectedItem.ToString()).Count().ToString();
            }
            else
            {
                double? price = 0;
                switch (dataCb.SelectedIndex)
                {
                    case 1:
                        serie.Points.AddXY(TransCb.SelectedItem.ToString(), tranferencias.Where(e => e.DataTransferencia > DateTime.Now.AddDays(-7) && e.TipoTransporte == TransCb.SelectedItem.ToString()).Count());

                        price = tranferencias.Where(e => e.TipoTransporte == TransCb.SelectedItem.ToString() && e.DataTransferencia > DateTime.Now.AddDays(-7)).Sum(t => t.ValorTotalPago);
                        totalTxt.Text = "R$" + price;
                        transTxt.Text = tranferencias.Where(e => e.TipoTransporte == TransCb.SelectedItem.ToString() && e.DataTransferencia > DateTime.Now.AddDays(-7)).Count().ToString();
                        break;
                    case 2:
                        serie.Points.AddXY(TransCb.SelectedItem.ToString(), tranferencias.Where(e => e.DataTransferencia > DateTime.Now.AddDays(-15) && e.TipoTransporte == TransCb.SelectedItem.ToString()).Count());

                        price = tranferencias.Where(e => e.TipoTransporte == TransCb.SelectedItem.ToString() && e.DataTransferencia > DateTime.Now.AddDays(-15)).Sum(t => t.ValorTotalPago);
                        totalTxt.Text = "R$" + price;
                        transTxt.Text = tranferencias.Where(e => e.TipoTransporte == TransCb.SelectedItem.ToString() && e.DataTransferencia > DateTime.Now.AddDays(-15)).Count().ToString();
                        break;
                    case 3:
                        serie.Points.AddXY(TransCb.SelectedItem.ToString(), tranferencias.Where(e => e.DataTransferencia > DateTime.Now.AddDays(-30) && e.TipoTransporte == TransCb.SelectedItem.ToString()).Count());

                        price = tranferencias.Where(e => e.TipoTransporte == TransCb.SelectedItem.ToString() && e.DataTransferencia > DateTime.Now.AddDays(-30)).Sum(t => t.ValorTotalPago);
                        totalTxt.Text = "R$" + price;
                        transTxt.Text = tranferencias.Where(e => e.TipoTransporte == TransCb.SelectedItem.ToString() && e.DataTransferencia > DateTime.Now.AddDays(-30)).Count().ToString();
                        break;
                    case 4:
                        serie.Points.AddXY(TransCb.SelectedItem.ToString(), tranferencias.Where(e => e.DataTransferencia > DateTime.Now.AddDays(-60) && e.TipoTransporte == TransCb.SelectedItem.ToString()).Count());

                        price = tranferencias.Where(e => e.TipoTransporte == TransCb.SelectedItem.ToString() && e.DataTransferencia > DateTime.Now.AddDays(-60)).Sum(t => t.ValorTotalPago);
                        totalTxt.Text = "R$" + price;
                        transTxt.Text = tranferencias.Where(e => e.TipoTransporte == TransCb.SelectedItem.ToString() && e.DataTransferencia > DateTime.Now.AddDays(-60)).Count().ToString();
                        break;
                }
            }
            pizzaChart.Series.Add(serie);
        }

        private void SetUsersList()
        {
            SetAtendimentosList();

            pacienteTxt.Text = atdList[0].Paciente.Nome;
            respTxt.Text = atdList[0].Responsavel.Nome;
            SetComboBoxesEnabled();

            var fullPathOrigem = AppDomain.CurrentDomain.BaseDirectory + $@"Images\{atdList[0].HospitalOrigem.Estado.Sigla.ToLower()}.png";
            origemCb.SelectedItem = atdList[0].HospitalOrigem.Cidade;
            origemPb.Image = Image.FromFile(fullPathOrigem);


            var fullPathDestino = AppDomain.CurrentDomain.BaseDirectory + $@"Images\{atdList[0].UnidadeDestino.Estado.Sigla.ToLower()}.png";
            destinoCb.SelectedItem = atdList[0].UnidadeDestino.Cidade;
            destinoPb.Image = Image.FromFile(fullPathDestino);

            tipoCb.SelectedItem = atdList[0].TipoAtendimento.TipoAtendimento;

            if (atdList[0].DataSolicitacao != null)
            {
                solicitacaoPicker.Value = atdList[0].DataSolicitacao.Value;
                solicitacaoPicker.CustomFormat = "dd/MM/yyyy";
            }

            if (atdList[0].DataIncioTratamento != null)
            {
                inicioPicker.Value = atdList[0].DataIncioTratamento.Value;
                inicioPicker.CustomFormat = "dd/MM/yyyy";
            }

            if (atdList[0].DataTerminoTratamento != null)
            {
                terminoPicker.Value = atdList[0].DataTerminoTratamento.Value;
                inicioPicker.CustomFormat = "dd/MM/yyyy";
            }

            leftBtn.Visible = false;
            firstPacienteBtn.Visible = false;

            if (atdList[index].StatusClinicoId == 1)
            {
                emAtendimento = false;
                finalCount = 0;
            }
            else if (atdList[index].StatusClinicoId >= 2)
            {
                finalCount = 0;
                emAtendimento = true;
            }
            else if (atdList[index].StatusClinicoId >= 3)
            {
                finalCount = 1;
                emAtendimento = true;
            }
            else if (atdList[index].StatusClinicoId >= 4)
            {
                finalCount = 2;
                emAtendimento = true;
            }
        }

        private void SetComboBoxesEnabled()
        {
            if (atdList[0].StatusClinicoId != 1)
            {
                origemCb.Enabled = false;
                destinoCb.Enabled = false;
                tipoCb.Enabled = false;
            }
            else
            {
                origemCb.Enabled = true;
                destinoCb.Enabled = true;
                tipoCb.Enabled = true;
            }
        }

        private void SetDGV()
        {
            var tranferencias = context.TransferenciasPacientes.ToList();
            anoLbl.Text = DateTime.Now.Year.ToString();
            SetDGVData(tranferencias);
        }

        private void SetDGVData(List<TransferenciasPacientes> tranferencias)
        {
            dgvTransferencias.Rows.Clear();

            var ano = anoLbl.Text;

            janeiroColumn.HeaderText = $"01/{ano}";
            fevereiroColumn.HeaderText = $"02/{ano}";
            maioColumn.HeaderText = $"03/{ano}";
            abrilColumn.HeaderText = $"04/{ano}";
            marcoColumn.HeaderText = $"05/{ano}";
            junhoColumn.HeaderText = $"06/{ano}";
            julhoColumn.HeaderText = $"07/{ano}";
            agostoColumn.HeaderText = $"08/{ano}";
            outubroColumn.HeaderText = $"10/{ano}";
            novembroColumn.HeaderText = $"11/{ano}";
            dezembroColumn.HeaderText = $"12/{ano}";

            #region SetMês
            var mes1 = tranferencias.Where(t => t.DataTransferencia.Value.Month == 1 && t.DataTransferencia.Value.Year == int.Parse(anoLbl.Text)).ToList();
            var mes2 = tranferencias.Where(t => t.DataTransferencia.Value.Month == 2 && t.DataTransferencia.Value.Year == int.Parse(anoLbl.Text)).ToList();
            var mes3 = tranferencias.Where(t => t.DataTransferencia.Value.Month == 3 && t.DataTransferencia.Value.Year == int.Parse(anoLbl.Text)).ToList();
            var mes4 = tranferencias.Where(t => t.DataTransferencia.Value.Month == 4 && t.DataTransferencia.Value.Year == int.Parse(anoLbl.Text)).ToList();
            var mes5 = tranferencias.Where(t => t.DataTransferencia.Value.Month == 5 && t.DataTransferencia.Value.Year == int.Parse(anoLbl.Text)).ToList();
            var mes6 = tranferencias.Where(t => t.DataTransferencia.Value.Month == 6 && t.DataTransferencia.Value.Year == int.Parse(anoLbl.Text)).ToList();
            var mes7 = tranferencias.Where(t => t.DataTransferencia.Value.Month == 7 && t.DataTransferencia.Value.Year == int.Parse(anoLbl.Text)).ToList();
            var mes8 = tranferencias.Where(t => t.DataTransferencia.Value.Month == 8 && t.DataTransferencia.Value.Year == int.Parse(anoLbl.Text)).ToList();
            var mes9 = tranferencias.Where(t => t.DataTransferencia.Value.Month == 9 && t.DataTransferencia.Value.Year == int.Parse(anoLbl.Text)).ToList();
            var mes10 = tranferencias.Where(t => t.DataTransferencia.Value.Month == 10 && t.DataTransferencia.Value.Year == int.Parse(anoLbl.Text)).ToList();
            var mes11 = tranferencias.Where(t => t.DataTransferencia.Value.Month == 11 && t.DataTransferencia.Value.Year == int.Parse(anoLbl.Text)).ToList();
            var mes12 = tranferencias.Where(t => t.DataTransferencia.Value.Month == 12 && t.DataTransferencia.Value.Year == int.Parse(anoLbl.Text)).ToList();
            #endregion

            dgvTransferencias.Rows.Add("Ambulância",
                mes1.Where(e => e.TipoTransporte == "Ambulância").Sum(e => e.ValorTotalPago),
                mes2.Where(e => e.TipoTransporte == "Ambulância").Sum(e => e.ValorTotalPago),
                mes3.Where(e => e.TipoTransporte == "Ambulância").Sum(e => e.ValorTotalPago),
                mes4.Where(e => e.TipoTransporte == "Ambulância").Sum(e => e.ValorTotalPago),
                mes5.Where(e => e.TipoTransporte == "Ambulância").Sum(e => e.ValorTotalPago),
                mes6.Where(e => e.TipoTransporte == "Ambulância").Sum(e => e.ValorTotalPago),
                mes7.Where(e => e.TipoTransporte == "Ambulância").Sum(e => e.ValorTotalPago),
                mes8.Where(e => e.TipoTransporte == "Ambulância").Sum(e => e.ValorTotalPago),
                mes9.Where(e => e.TipoTransporte == "Ambulância").Sum(e => e.ValorTotalPago),
                mes10.Where(e => e.TipoTransporte == "Ambulância").Sum(e => e.ValorTotalPago),
                mes11.Where(e => e.TipoTransporte == "Ambulância").Sum(e => e.ValorTotalPago),
                mes12.Where(e => e.TipoTransporte == "Ambulância").Sum(e => e.ValorTotalPago));

            dgvTransferencias.Rows.Add("UTI Móvel",
                mes1.Where(e => e.TipoTransporte == "UTI Móvel").Sum(e => e.ValorTotalPago),
                mes2.Where(e => e.TipoTransporte == "UTI Móvel").Sum(e => e.ValorTotalPago),
                mes3.Where(e => e.TipoTransporte == "UTI Móvel").Sum(e => e.ValorTotalPago),
                mes4.Where(e => e.TipoTransporte == "UTI Móvel").Sum(e => e.ValorTotalPago),
                mes5.Where(e => e.TipoTransporte == "UTI Móvel").Sum(e => e.ValorTotalPago),
                mes6.Where(e => e.TipoTransporte == "UTI Móvel").Sum(e => e.ValorTotalPago),
                mes7.Where(e => e.TipoTransporte == "UTI Móvel").Sum(e => e.ValorTotalPago),
                mes8.Where(e => e.TipoTransporte == "UTI Móvel").Sum(e => e.ValorTotalPago),
                mes9.Where(e => e.TipoTransporte == "UTI Móvel").Sum(e => e.ValorTotalPago),
                mes10.Where(e => e.TipoTransporte == "UTI Móvel").Sum(e => e.ValorTotalPago),
                mes11.Where(e => e.TipoTransporte == "UTI Móvel").Sum(e => e.ValorTotalPago),
                mes12.Where(e => e.TipoTransporte == "UTI Móvel").Sum(e => e.ValorTotalPago));

            dgvTransferencias.Rows.Add("Helicóptero",
                mes1.Where(e => e.TipoTransporte == "Helicóptero").Sum(e => e.ValorTotalPago),
                mes2.Where(e => e.TipoTransporte == "Helicóptero").Sum(e => e.ValorTotalPago),
                mes3.Where(e => e.TipoTransporte == "Helicóptero").Sum(e => e.ValorTotalPago),
                mes4.Where(e => e.TipoTransporte == "Helicóptero").Sum(e => e.ValorTotalPago),
                mes5.Where(e => e.TipoTransporte == "Helicóptero").Sum(e => e.ValorTotalPago),
                mes6.Where(e => e.TipoTransporte == "Helicóptero").Sum(e => e.ValorTotalPago),
                mes7.Where(e => e.TipoTransporte == "Helicóptero").Sum(e => e.ValorTotalPago),
                mes8.Where(e => e.TipoTransporte == "Helicóptero").Sum(e => e.ValorTotalPago),
                mes9.Where(e => e.TipoTransporte == "Helicóptero").Sum(e => e.ValorTotalPago),
                mes10.Where(e => e.TipoTransporte == "Helicóptero").Sum(e => e.ValorTotalPago),
                mes11.Where(e => e.TipoTransporte == "Helicóptero").Sum(e => e.ValorTotalPago),
                mes12.Where(e => e.TipoTransporte == "Helicóptero").Sum(e => e.ValorTotalPago));
        }

        public void SetPrevForm(LoginForm form)
        {
            this.loginForm = form;
        }

        private void SetCalendaries()
        {
            solicitacaoPicker.Format = DateTimePickerFormat.Custom;
            solicitacaoPicker.CustomFormat = " ";
            solicitacaoPicker.Enabled = false;

            inicioPicker.Format = DateTimePickerFormat.Custom;
            inicioPicker.CustomFormat = " ";
            inicioPicker.Enabled = false;

            terminoPicker.Format = DateTimePickerFormat.Custom;
            terminoPicker.CustomFormat = " ";
            terminoPicker.Enabled = false;
        }

        private void DashboardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            loginForm.Show();
        }

        private void statusPanel_Paint(object sender, PaintEventArgs e)
        {
            SetAtendimentosList();
            PaintEllipse();
        }

        private void PaintEllipse()
        {
            SetAtendimentosList();
            Graphics g = statusPanel.CreateGraphics();

            if (atdList.Count > 0)
            {
                if (atdList[index].StatusClinicoId == 1)
                {
                    Brush brush = new SolidBrush(ColorTranslator.FromHtml(atdList[index].StatusClinico.Cor));
                    g.FillEllipse(brush, new Rectangle(50, 0, 50, 50));

                    Pen pen = new Pen(Color.Black, 2);
                    g.DrawLine(pen, 100, 25, 500, 25);
                }
                else if (atdList[index].StatusClinicoId == 2)
                {
                    Brush brush = new SolidBrush(ColorTranslator.FromHtml(atdList[index].StatusClinico.Cor));
                    g.FillEllipse(brush, new Rectangle(50, 0, 50, 50));
                    g.FillEllipse(brush, new Rectangle(275, 0, 50, 50));

                    Pen pen = new Pen(Color.Black, 2);
                    g.DrawLine(pen, 100, 25, 275, 25);
                    g.DrawLine(pen, 325, 25, 500, 25);
                }
                else
                {
                    Brush brush = new SolidBrush(ColorTranslator.FromHtml(atdList[index].StatusClinico.Cor));
                    g.FillEllipse(brush, new Rectangle(50, 0, 50, 50));
                    g.FillEllipse(brush, new Rectangle(275, 0, 50, 50));
                    g.FillEllipse(brush, new Rectangle(500, 0, 50, 50));

                    Pen pen = new Pen(Color.Black, 2);
                    g.DrawLine(pen, 100, 25, 275, 25);
                    g.DrawLine(pen, 325, 25, 500, 25);
                }
                SetLabels();
            }
        }

        private void rigthAnoBtn_Click(object sender, EventArgs e)
        {
            var ano = int.Parse(anoLbl.Text);
            ano++;
            anoLbl.Text = ano.ToString();
            var tranferencias = context.TransferenciasPacientes.ToList();
            SetDGVData(tranferencias);
        }

        private void leftAnoBtn_Click(object sender, EventArgs e)
        {
            var ano = int.Parse(anoLbl.Text);
            ano--;
            anoLbl.Text = ano.ToString();
            var tranferencias = context.TransferenciasPacientes.ToList();
            SetDGVData(tranferencias);
        }

        private void TransCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPizzaChart();
        }

        private void dataCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPizzaChart();
        }

        private void chartCbAtendimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetChartAtendimento();
        }

        private void chartCbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetChartStatus();
        }

        private void limparBtn_Click(object sender, EventArgs e)
        {
            dataCb.SelectedIndex = 0;
            TransCb.SelectedIndex = 0;
        }

        private void lastPacienteBtn_Click(object sender, EventArgs e)
        {
            lastPacienteBtn.Visible = false;
            rigthBtn.Visible = false;

            SetAtendimentosList();

            pacienteTxt.Text = atdList[atdList.Count - 1].Paciente.Nome;
            respTxt.Text = atdList[atdList.Count - 1].Responsavel.Nome;

            if (atdList[atdList.Count - 1].StatusClinicoId != 1)
            {
                origemCb.Enabled = false;
                destinoCb.Enabled = false;
                tipoCb.Enabled = false;
            }
            else
            {
                origemCb.Enabled = true;
                destinoCb.Enabled = true;
                tipoCb.Enabled = true;
            }

            var fullPathOrigem = AppDomain.CurrentDomain.BaseDirectory + $@"Images\{atdList[atdList.Count - 1].HospitalOrigem.Estado.Sigla.ToLower()}.png";
            origemCb.SelectedItem = atdList[atdList.Count - 1].HospitalOrigem.Cidade;
            origemPb.Image = Image.FromFile(fullPathOrigem);


            var fullPathDestino = AppDomain.CurrentDomain.BaseDirectory + $@"Images\{atdList[atdList.Count - 1].UnidadeDestino.Estado.Sigla.ToLower()}.png";
            destinoCb.SelectedItem = atdList[atdList.Count - 1].UnidadeDestino.Cidade;
            destinoPb.Image = Image.FromFile(fullPathDestino);

            tipoCb.SelectedItem = atdList[atdList.Count - 1].TipoAtendimento.TipoAtendimento;

            if (atdList[atdList.Count - 1].DataSolicitacao != null)
            {
                solicitacaoPicker.Value = atdList[atdList.Count - 1].DataSolicitacao.Value;
                solicitacaoPicker.CustomFormat = "dd/MM/yyyy";
            }
            else
            {
                inicioPicker.CustomFormat = " ";
            }

            if (atdList[atdList.Count - 1].DataIncioTratamento != null)
            {
                inicioPicker.Value = atdList[atdList.Count - 1].DataIncioTratamento.Value;
                inicioPicker.CustomFormat = "dd/MM/yyyy";
            }
            else
            {
                inicioPicker.CustomFormat = " ";
            }

            if (atdList[atdList.Count - 1].DataTerminoTratamento != null)
            {
                terminoPicker.Value = atdList[atdList.Count - 1].DataTerminoTratamento.Value;
                inicioPicker.CustomFormat = "dd/MM/yyyy";
            }
            else
            {
                inicioPicker.CustomFormat = " ";
            }
            index = atdList.Count - 1;

            SetBooleanParameters();
        }

        private void SetAtendimentosList()
        {
            var id = Properties.Settings.Default.UsuarioId;
            var list = context.Atendimentos
                .Include(a => a.Paciente)
                .Include(a => a.Responsavel)
                .Include(a => a.HospitalOrigem)
                .Include(a => a.UnidadeDestino)
                .Include(a => a.StatusClinico)
                .Include(a => a.TipoAtendimento)
                .Where(a => a.ResponsavelId == id)
                .OrderBy(a => a.Paciente.Nome)
                .ToList();

            if (list.Count > 0)
            {
                atdList = list.OrderBy(l => l.Paciente.Nome).ToList();
            }

            SetLabels();
        }

        private void SetLabels()
        {
            if (atdList[index].StatusClinicoId == 1)
            {
                aguardandoLbl.Visible = true;
                tratamentoLbl.Visible = false;
                altaLbl.Visible = false;
                obitoLbl.Visible = false;
            }
            else if (atdList[index].StatusClinicoId == 2)
            {
                aguardandoLbl.Visible = false;
                tratamentoLbl.Visible = true;
                altaLbl.Visible = false;
                obitoLbl.Visible = false;
            }
            else if (atdList[index].StatusClinicoId == 3)
            {
                aguardandoLbl.Visible = false;
                tratamentoLbl.Visible = false;
                altaLbl.Visible = true;
                obitoLbl.Visible = false;
            }
            else if (atdList[index].StatusClinicoId == 4)
            {
                aguardandoLbl.Visible = false;
                tratamentoLbl.Visible = false;
                altaLbl.Visible = false;
                obitoLbl.Visible = true;
            }
        }

        private void rigthBtn_Click(object sender, EventArgs e)
        {
            if (index < (atdList.Count - 1))
            {
                firstPacienteBtn.Visible = true;
                leftBtn.Visible = true;
                index++;

                if (index == (atdList.Count - 1))
                {
                    lastPacienteBtn.Visible = false;
                    rigthBtn.Visible = false;
                }
            }
            else
            {
                lastPacienteBtn.Visible = false;
                rigthBtn.Visible = false;
                return;
            }
                pacienteTxt.Text = atdList[index].Paciente.Nome;
            respTxt.Text = atdList[index].Responsavel.Nome;

            if (atdList[index].StatusClinicoId != 1)
            {
                origemCb.Enabled = false;
                destinoCb.Enabled = false;
                tipoCb.Enabled = false;
            }
            else
            {
                origemCb.Enabled = true;
                destinoCb.Enabled = true;
                tipoCb.Enabled = true;
            }

            var fullPathOrigem = AppDomain.CurrentDomain.BaseDirectory + $@"Images\{atdList[index].HospitalOrigem.Estado.Sigla.ToLower()}.png";
            origemCb.SelectedItem = atdList[index].HospitalOrigem.Cidade;
            origemPb.Image = Image.FromFile(fullPathOrigem);


            var fullPathDestino = AppDomain.CurrentDomain.BaseDirectory + $@"Images\{atdList[index].UnidadeDestino.Estado.Sigla.ToLower()}.png";
            destinoCb.SelectedItem = atdList[index].UnidadeDestino.Cidade;
            destinoPb.Image = Image.FromFile(fullPathDestino);

            tipoCb.SelectedItem = atdList[index].TipoAtendimento.TipoAtendimento;

            if (atdList[index].DataSolicitacao != null)
            {
                solicitacaoPicker.Value = atdList[index].DataSolicitacao.Value;
                solicitacaoPicker.CustomFormat = "dd/MM/yyyy";
            }
            else
            {
                inicioPicker.CustomFormat = " ";
            }

            if (atdList[index].DataIncioTratamento != null)
            {
                inicioPicker.Value = atdList[index].DataIncioTratamento.Value;
                inicioPicker.CustomFormat = "dd/MM/yyyy";
            }
            else
            {
                inicioPicker.CustomFormat = " ";
            }

            if (atdList[index].DataTerminoTratamento != null)
            {
                terminoPicker.Value = atdList[index].DataTerminoTratamento.Value;
                inicioPicker.CustomFormat = "dd/MM/yyyy";
            }
            else
            {
                inicioPicker.CustomFormat = " ";
            }

            SetBooleanParameters();
        }

        private void firstPacienteBtn_Click(object sender, EventArgs e)
        {
            leftBtn.Visible = false;
            firstPacienteBtn.Visible = false;
            SetAtendimentosList();

            pacienteTxt.Text = atdList[0].Paciente.Nome;
            respTxt.Text = atdList[0].Responsavel.Nome;

            if (atdList[0].StatusClinicoId != 1)
            {
                origemCb.Enabled = false;
                destinoCb.Enabled = false;
                tipoCb.Enabled = false;
            }
            else
            {
                origemCb.Enabled = true;
                destinoCb.Enabled = true;
                tipoCb.Enabled = true;
            }

            var fullPathOrigem = AppDomain.CurrentDomain.BaseDirectory + $@"Images\{atdList[0].HospitalOrigem.Estado.Sigla.ToLower()}.png";
            origemCb.SelectedItem = atdList[0].HospitalOrigem.Cidade;
            origemPb.Image = Image.FromFile(fullPathOrigem);


            var fullPathDestino = AppDomain.CurrentDomain.BaseDirectory + $@"Images\{atdList[0].UnidadeDestino.Estado.Sigla.ToLower()}.png";
            destinoCb.SelectedItem = atdList[0].UnidadeDestino.Cidade;
            destinoPb.Image = Image.FromFile(fullPathDestino);

            tipoCb.SelectedItem = atdList[0].TipoAtendimento.TipoAtendimento;

            if (atdList[0].DataSolicitacao != null)
            {
                solicitacaoPicker.Value = atdList[0].DataSolicitacao.Value;
                solicitacaoPicker.CustomFormat = "dd/MM/yyyy";
            }
            else
            {
                inicioPicker.CustomFormat = " ";
            }

            if (atdList[0].DataIncioTratamento != null)
            {
                inicioPicker.Value = atdList[0].DataIncioTratamento.Value;
                inicioPicker.CustomFormat = "dd/MM/yyyy";
            }
            else
            {
                inicioPicker.CustomFormat = " ";
            }

            if (atdList[0].DataTerminoTratamento != null)
            {
                terminoPicker.Value = atdList[0].DataTerminoTratamento.Value;
                inicioPicker.CustomFormat = "dd/MM/yyyy";
            }
            else
            {
                inicioPicker.CustomFormat = " ";
            }
            index = 0;
            SetBooleanParameters();
        }

        private void leftBtn_Click(object sender, EventArgs e)
        {
            if (index > 0)
            {
                lastPacienteBtn.Visible = true;
                rigthBtn.Visible = true;
                index--;
                if (index == 0)
                {
                    firstPacienteBtn.Visible = false;
                    leftBtn.Visible = false;
                }
            }
            else
            {
                firstPacienteBtn.Visible = false;
                leftBtn.Visible = false;
                return;
            }

            pacienteTxt.Text = atdList[index].Paciente.Nome;
            respTxt.Text = atdList[index].Responsavel.Nome;

            if (atdList[index].StatusClinicoId != 1)
            {
                origemCb.Enabled = false;
                destinoCb.Enabled = false;
                tipoCb.Enabled = false;
            }
            else
            {
                origemCb.Enabled = true;
                destinoCb.Enabled = true;
                tipoCb.Enabled = true;
            }

                var fullPathOrigem = AppDomain.CurrentDomain.BaseDirectory + $@"Images\{atdList[index].HospitalOrigem.Estado.Sigla.ToLower()}.png";
            origemCb.SelectedItem = atdList[index].HospitalOrigem.Cidade;
            origemPb.Image = Image.FromFile(fullPathOrigem);


            var fullPathDestino = AppDomain.CurrentDomain.BaseDirectory + $@"Images\{atdList[index].UnidadeDestino.Estado.Sigla.ToLower()}.png";
            destinoCb.SelectedItem = atdList[index].UnidadeDestino.Cidade;
            destinoPb.Image = Image.FromFile(fullPathDestino);

            tipoCb.SelectedItem = atdList[index].TipoAtendimento.TipoAtendimento;

            if (atdList[index].DataSolicitacao != null)
            {
                solicitacaoPicker.Value = atdList[index].DataSolicitacao.Value;
                solicitacaoPicker.CustomFormat = "dd/MM/yyyy";
            }
            else
            {
                inicioPicker.CustomFormat = " ";
            }

            if (atdList[index].DataIncioTratamento != null)
            {
                inicioPicker.Value = atdList[index].DataIncioTratamento.Value;
                inicioPicker.CustomFormat = "dd/MM/yyyy";
            }
            else
            {
                inicioPicker.CustomFormat = " ";
            }

            if (atdList[index].DataTerminoTratamento != null)
            {
                terminoPicker.Value = atdList[index].DataTerminoTratamento.Value;
                inicioPicker.CustomFormat = "dd/MM/yyyy";
            }
            else
            {
                inicioPicker.CustomFormat = " ";
            }

            SetBooleanParameters();
        }

        private void SetBooleanParameters()
        {
            if (atdList[index].StatusClinicoId == 1)
            {
                emAtendimento = false;
                finalCount = 0;
            }else if (atdList[index].StatusClinicoId >= 2)
            {
                finalCount = 0;
                emAtendimento = true;
            }else if (atdList[index].StatusClinicoId >= 3)
            {
                finalCount = 1;
                emAtendimento = true;
            }else if (atdList[index].StatusClinicoId >= 4)
            {
                finalCount = 2;
                emAtendimento = true;
            }
            statusPanel.Invalidate();
            PaintEllipse();
        }

        private void statusPanel_MouseClick(object sender, MouseEventArgs e)
        {
            Atendimentos atendimento = new Atendimentos();
            atendimento = atdList[index];
            if (new Rectangle(50, 0, 50, 50).Contains(e.Location))
            {
                if (!emAtendimento)
                {
                    atendimento.StatusClinicoId = 2;
                    atendimento.HospitalOrigemId = origemCb.SelectedIndex + 1;
                    atendimento.UnidadeDestinoId = destinoCb.SelectedIndex + 1;
                    atendimento.TipoAtendimentoId = tipoCb.SelectedIndex + 1;
                    atendimento.DataIncioTratamento = DateTime.Now;
                    inicioPicker.CustomFormat = "dd/MM/yyyy";
                    inicioPicker.Value = DateTime.Now;
                    context.Atendimentos.Update(atendimento);
                    context.SaveChanges();
                    statusPanel.Invalidate();
                    emAtendimento = true;
                    PaintEllipse();
                }
            }else if (new Rectangle(275, 0, 50, 50).Contains(e.Location))
            {
                finalCount++;
                if (finalCount == 1)
                {
                    atendimento.StatusClinicoId = 3;
                    atendimento.DataTerminoTratamento = DateTime.Now;
                    terminoPicker.CustomFormat = "dd/MM/yyyy";
                    terminoPicker.Value = DateTime.Now;
                    context.Atendimentos.Update(atendimento);
                    context.SaveChanges();
                    statusPanel.Invalidate();
                    altaLbl.Visible = true;
                    obitoLbl.Visible = false;
                    PaintEllipse();
                }
                else if (finalCount == 2)
                {
                    atendimento.StatusClinicoId = 4;
                    atendimento.DataTerminoTratamento = DateTime.Now;
                    terminoPicker.CustomFormat = "dd/MM/yyyy";
                    terminoPicker.Value = DateTime.Now;
                    context.Atendimentos.Update(atendimento);
                    context.SaveChanges();
                    statusPanel.Invalidate();
                    altaLbl.Visible = false;
                    obitoLbl.Visible = true;
                    PaintEllipse();
                }
                else
                {
                    return;
                }
            }
            SetComboBoxesEnabled();
        }

        private void destinoCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            var c = context.Cidades.Include(ci => ci.Estado).ToList();
            var fullPathDestino = AppDomain.CurrentDomain.BaseDirectory + $@"Images\{c[destinoCb.SelectedIndex].Estado.Sigla.ToLower()}.png";
            destinoPb.Image = Image.FromFile(fullPathDestino);
        }

        private void origemCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            var c = context.Cidades.Include(ci => ci.Estado).ToList();
            var fullPathOrigem = AppDomain.CurrentDomain.BaseDirectory + $@"Images\{c[origemCb.SelectedIndex].Estado.Sigla.ToLower()}.png";
            origemPb.Image = Image.FromFile(fullPathOrigem);
        }
    }
}