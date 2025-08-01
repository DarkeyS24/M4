﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using M4.Context;
using M4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Office.Interop.Excel;
using excel = Microsoft.Office.Interop.Excel;

namespace M4
{
    public partial class FilteredTableForm : Form
    {
        private LoginForm form;
        private readonly Sessao4 context;
        private List<Cidades> cidadesOrigemList;
        private List<Cidades> cidadesDestinoList;
        private List<Atendimentos> atdList = new List<Atendimentos>();
        private bool inicio = false;
        private bool termino = false;
        public FilteredTableForm()
        {
            InitializeComponent();
            context = Sessao4.GetInstance();
            SetCheckBoxList();
        }

        private void SetCheckBoxList()
        {
            var list = context.Cidades.Include(c => c.Estado).ToList();
            var list2 = context.Cidades.Include(c => c.Estado).ToList();
            cidadesOrigemList = list;
            cidadesDestinoList = list2;
            foreach (var item in list.Select(l => l.Cidade).ToList())
            {
                origemList.Items.Add(item);
            }
            foreach (var item in list2.Select(l => l.Cidade).ToList())
            {
                destinoList.Items.Add(item);
            }
        }

        private void xLbl_Click(object sender, EventArgs e)
        {
            filterPanel.Visible = false;
        }

        private void parametersBtn_Click(object sender, EventArgs e)
        {
            filterPanel.Visible = true;
        }

        public void SetloginForm(LoginForm form)
        {
            this.form = form;
        }

        private void atendimetoLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DashboardForm dashboard = new DashboardForm();
            dashboard.Show();
            dashboard.SetPrevForm(form);
            dashboard.Show();
            this.Dispose();
        }

        private void defineBtn_Click(object sender, EventArgs e)
        {
            atdList = context.Atendimentos.Where(a => a.ResponsavelId == Properties.Settings.Default.UsuarioId).ToList();
            filterPanel.Visible = false;
            dgvPanel.Visible = true;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            filterPanel.Visible = false;
            DashboardForm dashboard = new DashboardForm();
            dashboard.Show();
            dashboard.SetPrevForm(form);
            dashboard.Show();
            this.Dispose();
        }

        private void exportBtn_Click(object sender, EventArgs e)
        {
            excel.Application application = new excel.Application();
            excel.Workbook workbook = application.Workbooks.Add();
            excel.Worksheet worksheet = workbook.Worksheets.Add();

            worksheet.Name = "Filtered Table";
            var columsCount = 0;
            for (int i = 0; i < dgvFiltered.Columns.Count; i++)
            {
                columsCount++;
                worksheet.Cells[1, i + 1] = dgvFiltered.Columns[i].HeaderText;
            }

            var rowCount = 1;
            foreach (DataGridViewRow row in dgvFiltered.Rows)
            {
                if (!row.IsNewRow)
                {
                    for (int i = 1; i < columsCount; i++)
                    {
                        worksheet.Cells[(rowCount + 1), i].Value = row.Cells[i-1].Value.ToString();
                    }
                }
                rowCount++;
            }

            worksheet.Columns.AutoFit();
            string filePath = AppDomain.CurrentDomain.BaseDirectory + $@"Excel\{DateTime.Now:yyyyMMdd_HHmmss}_Filtered_Table";
            workbook.SaveAs(filePath);
            workbook.Close();
            application.Quit();

            MessageBox.Show("Dados Exportados");
        }

        private void limparInicioBtn_Click(object sender, EventArgs e)
        {
            inicio = false;
            inicioPicker.Format = DateTimePickerFormat.Custom;
            inicioPicker.CustomFormat = " ";
        }

        private void limparTerminoBtn_Click(object sender, EventArgs e)
        {
            termino = false;
            terminoPicker.Format = DateTimePickerFormat.Custom;
            terminoPicker.CustomFormat = " ";
        }

        private void inicioPicker_ValueChanged(object sender, EventArgs e)
        {
            inicio = true;
            inicioPicker.CustomFormat = "dd/MM/yyyy";
        }

        private void terminoPicker_ValueChanged(object sender, EventArgs e)
        {
            termino = true;
            terminoPicker.CustomFormat = "dd/MM/yyyy";
        }

        private void pacientesTxt_TextChanged(object sender, EventArgs e)
        {
            FilterTable();
        }

        private void numberPicker_ValueChanged(object sender, EventArgs e)
        {
            FilterTable();
        }

        private void transList_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterTable();
        }

        private void sexoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterTable();
        }

        private void origemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterTable();
        }

        private void destinoList_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterTable();
        }

        private void FilterTable()
        {
            var list = atdList;
            if (inicio)
            {
                list = list.Where(l => l.DataIncioTratamento == inicioPicker.Value).ToList();
            }

            if (termino)
            {
                list = list.Where(l => l.DataTerminoTratamento == terminoPicker.Value).ToList();
            }

            if (!string.IsNullOrEmpty(pacientesTxt.Text))
            {
                list = list.Where(l => l.Paciente.Nome.ToLower().Contains(pacientesTxt.Text.ToLower())).ToList();
            }

            if (transList.CheckedItems.Count > 0)
            {
                var newList = list;
                foreach (var item in transList.CheckedItems)
                {
                    newList.AddRange(list.Where(l => l.TipoAtendimento.TipoAtendimento == item.ToString()).ToList());
                }
                list = newList;
            }

            if (sexoList.CheckedItems.Count > 0)
            {
                var newList = list;
                foreach (var item in sexoList.CheckedItems)
                {
                    newList.AddRange(list.Where(l => l.Paciente.Sexo == item.ToString()).ToList());
                }
                list = newList;
            }

            if (origemList.CheckedItems.Count > 0)
            {
                var newList = list;
                foreach (var item in origemList.CheckedItems)
                {
                    newList.AddRange(list.Where(l => l.HospitalOrigem.Cidade == item.ToString()).ToList());
                }
                list = newList;
            }

            if (destinoList.CheckedItems.Count > 0)
            {
                var newList = list;
                foreach (var item in destinoList.CheckedItems)
                {
                    newList.AddRange(list.Where(l => l.UnidadeDestino.Cidade == item.ToString()).ToList());
                }
                list = newList;
            }

            if (numberPicker.Value > 0)
            {
                if (numberPicker.Value < list.Count)
                {
                    list = list.GetRange(0, (int)numberPicker.Value);
                }
                else
                {
                    MessageBox.Show($"A lista não tem essa quantidade de registros, Items na lista: {list.Count}");
                }
            }

            if (list.Count > 0)
            {
                
            }
            else
            {
                MessageBox.Show("Não tem items na lista com esses parametros");
            }
        }
    }
}
