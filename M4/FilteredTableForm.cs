using System;
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

        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            filterPanel.Visible = false;
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
            string filePath = AppDomain.CurrentDomain.BaseDirectory + $@"Excel\{DateTime.Now.ToString("yyyyMMdd_HHmmss")}_Filtered_Table";
            workbook.SaveAs(filePath);
            workbook.Close();
            application.Quit();

            MessageBox.Show("Dados Exportados");
        }
    }
}
