namespace M4
{
    partial class FilteredTableForm
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.dashboardLink = new System.Windows.Forms.LinkLabel();
            this.atendimetoLink = new System.Windows.Forms.LinkLabel();
            this.parametersBtn = new System.Windows.Forms.Button();
            this.exportBtn = new System.Windows.Forms.Button();
            this.filterPanel = new System.Windows.Forms.Panel();
            this.defineBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.transListCb = new System.Windows.Forms.CheckedListBox();
            this.userListCb = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.xLbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvPanel = new System.Windows.Forms.Panel();
            this.dgvFiltered = new System.Windows.Forms.DataGridView();
            this.destinoList = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.origemList = new System.Windows.Forms.CheckedListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.sexoList = new System.Windows.Forms.CheckedListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.transList = new System.Windows.Forms.CheckedListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numberTxt = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.pacientesTxt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.inicioPicker = new System.Windows.Forms.DateTimePicker();
            this.terminoPicker = new System.Windows.Forms.DateTimePicker();
            this.panel2.SuspendLayout();
            this.filterPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.dgvPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiltered)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.dashboardLink);
            this.panel2.Controls.Add(this.atendimetoLink);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1273, 60);
            this.panel2.TabIndex = 2;
            // 
            // dashboardLink
            // 
            this.dashboardLink.AutoSize = true;
            this.dashboardLink.Location = new System.Drawing.Point(708, 17);
            this.dashboardLink.Name = "dashboardLink";
            this.dashboardLink.Size = new System.Drawing.Size(155, 33);
            this.dashboardLink.TabIndex = 1;
            this.dashboardLink.TabStop = true;
            this.dashboardLink.Text = "Dashboard";
            // 
            // atendimetoLink
            // 
            this.atendimetoLink.AutoSize = true;
            this.atendimetoLink.Location = new System.Drawing.Point(350, 17);
            this.atendimetoLink.Name = "atendimetoLink";
            this.atendimetoLink.Size = new System.Drawing.Size(335, 33);
            this.atendimetoLink.TabIndex = 0;
            this.atendimetoLink.TabStop = true;
            this.atendimetoLink.Text = "Central de Atendimentos";
            this.atendimetoLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.atendimetoLink_LinkClicked);
            // 
            // parametersBtn
            // 
            this.parametersBtn.Location = new System.Drawing.Point(953, 82);
            this.parametersBtn.Name = "parametersBtn";
            this.parametersBtn.Size = new System.Drawing.Size(307, 50);
            this.parametersBtn.TabIndex = 3;
            this.parametersBtn.Text = "Definir Parâmetros";
            this.parametersBtn.UseVisualStyleBackColor = true;
            this.parametersBtn.Click += new System.EventHandler(this.parametersBtn_Click);
            // 
            // exportBtn
            // 
            this.exportBtn.Location = new System.Drawing.Point(738, 82);
            this.exportBtn.Name = "exportBtn";
            this.exportBtn.Size = new System.Drawing.Size(193, 50);
            this.exportBtn.TabIndex = 4;
            this.exportBtn.Text = "Exportar";
            this.exportBtn.UseVisualStyleBackColor = true;
            this.exportBtn.Click += new System.EventHandler(this.exportBtn_Click);
            // 
            // filterPanel
            // 
            this.filterPanel.BackColor = System.Drawing.Color.White;
            this.filterPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.filterPanel.Controls.Add(this.defineBtn);
            this.filterPanel.Controls.Add(this.cancelBtn);
            this.filterPanel.Controls.Add(this.transListCb);
            this.filterPanel.Controls.Add(this.userListCb);
            this.filterPanel.Controls.Add(this.label1);
            this.filterPanel.Controls.Add(this.panel4);
            this.filterPanel.Controls.Add(this.panel3);
            this.filterPanel.Controls.Add(this.label2);
            this.filterPanel.Controls.Add(this.label3);
            this.filterPanel.Location = new System.Drawing.Point(226, 256);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Size = new System.Drawing.Size(845, 573);
            this.filterPanel.TabIndex = 5;
            this.filterPanel.Visible = false;
            // 
            // defineBtn
            // 
            this.defineBtn.Location = new System.Drawing.Point(438, 412);
            this.defineBtn.Name = "defineBtn";
            this.defineBtn.Size = new System.Drawing.Size(161, 50);
            this.defineBtn.TabIndex = 8;
            this.defineBtn.Text = "Definir";
            this.defineBtn.UseVisualStyleBackColor = true;
            this.defineBtn.Click += new System.EventHandler(this.defineBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(244, 412);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(161, 50);
            this.cancelBtn.TabIndex = 7;
            this.cancelBtn.Text = "Cancelar";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // transListCb
            // 
            this.transListCb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.transListCb.CheckOnClick = true;
            this.transListCb.Font = new System.Drawing.Font("Uber Move", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.transListCb.FormattingEnabled = true;
            this.transListCb.Items.AddRange(new object[] {
            "Id do Paciente",
            "Unidade de Origem",
            "Unidade de Destino",
            "Data da Transferência",
            "Tipo de Transporte",
            "Nivel de Transporte"});
            this.transListCb.Location = new System.Drawing.Point(438, 183);
            this.transListCb.Name = "transListCb";
            this.transListCb.Size = new System.Drawing.Size(390, 182);
            this.transListCb.TabIndex = 4;
            // 
            // userListCb
            // 
            this.userListCb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userListCb.CheckOnClick = true;
            this.userListCb.Font = new System.Drawing.Font("Uber Move", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userListCb.FormattingEnabled = true;
            this.userListCb.Items.AddRange(new object[] {
            "Id do Usuario",
            "Email",
            "Senha",
            "Telefone",
            "Nome",
            "Data de Nascimento"});
            this.userListCb.Location = new System.Drawing.Point(15, 183);
            this.userListCb.Name = "userListCb";
            this.userListCb.Size = new System.Drawing.Size(390, 182);
            this.userListCb.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 33);
            this.label1.TabIndex = 2;
            this.label1.Text = "Selecionar Colunas";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Location = new System.Drawing.Point(-1, 521);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(845, 51);
            this.panel4.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.xLbl);
            this.panel3.Location = new System.Drawing.Point(-1, -1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(845, 51);
            this.panel3.TabIndex = 0;
            // 
            // xLbl
            // 
            this.xLbl.AutoSize = true;
            this.xLbl.Location = new System.Drawing.Point(802, 8);
            this.xLbl.Name = "xLbl";
            this.xLbl.Size = new System.Drawing.Size(34, 33);
            this.xLbl.TabIndex = 2;
            this.xLbl.Text = "X";
            this.xLbl.Click += new System.EventHandler(this.xLbl_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 33);
            this.label2.TabIndex = 5;
            this.label2.Text = "Usuário";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(432, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(198, 33);
            this.label3.TabIndex = 6;
            this.label3.Text = "Transferências";
            // 
            // dgvPanel
            // 
            this.dgvPanel.BackColor = System.Drawing.Color.White;
            this.dgvPanel.Controls.Add(this.terminoPicker);
            this.dgvPanel.Controls.Add(this.inicioPicker);
            this.dgvPanel.Controls.Add(this.label11);
            this.dgvPanel.Controls.Add(this.label10);
            this.dgvPanel.Controls.Add(this.pacientesTxt);
            this.dgvPanel.Controls.Add(this.label9);
            this.dgvPanel.Controls.Add(this.numberTxt);
            this.dgvPanel.Controls.Add(this.transList);
            this.dgvPanel.Controls.Add(this.label7);
            this.dgvPanel.Controls.Add(this.sexoList);
            this.dgvPanel.Controls.Add(this.label6);
            this.dgvPanel.Controls.Add(this.origemList);
            this.dgvPanel.Controls.Add(this.label5);
            this.dgvPanel.Controls.Add(this.destinoList);
            this.dgvPanel.Controls.Add(this.dgvFiltered);
            this.dgvPanel.Controls.Add(this.label4);
            this.dgvPanel.Controls.Add(this.label8);
            this.dgvPanel.Location = new System.Drawing.Point(12, 155);
            this.dgvPanel.Name = "dgvPanel";
            this.dgvPanel.Size = new System.Drawing.Size(1248, 931);
            this.dgvPanel.TabIndex = 6;
            this.dgvPanel.Visible = false;
            // 
            // dgvFiltered
            // 
            this.dgvFiltered.BackgroundColor = System.Drawing.Color.White;
            this.dgvFiltered.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFiltered.Location = new System.Drawing.Point(412, 19);
            this.dgvFiltered.Name = "dgvFiltered";
            this.dgvFiltered.RowHeadersWidth = 51;
            this.dgvFiltered.RowTemplate.Height = 24;
            this.dgvFiltered.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFiltered.Size = new System.Drawing.Size(818, 899);
            this.dgvFiltered.TabIndex = 0;
            // 
            // destinoList
            // 
            this.destinoList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.destinoList.CheckOnClick = true;
            this.destinoList.Font = new System.Drawing.Font("Uber Move", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.destinoList.FormattingEnabled = true;
            this.destinoList.Location = new System.Drawing.Point(15, 826);
            this.destinoList.Name = "destinoList";
            this.destinoList.Size = new System.Drawing.Size(371, 92);
            this.destinoList.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 791);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(251, 33);
            this.label4.TabIndex = 3;
            this.label4.Text = "Cidade de Destino";
            // 
            // origemList
            // 
            this.origemList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.origemList.CheckOnClick = true;
            this.origemList.Font = new System.Drawing.Font("Uber Move", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.origemList.FormattingEnabled = true;
            this.origemList.Location = new System.Drawing.Point(15, 684);
            this.origemList.Name = "origemList";
            this.origemList.Size = new System.Drawing.Size(371, 92);
            this.origemList.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 649);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(252, 33);
            this.label5.TabIndex = 5;
            this.label5.Text = "Cidade de Origem";
            // 
            // sexoList
            // 
            this.sexoList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sexoList.CheckOnClick = true;
            this.sexoList.Font = new System.Drawing.Font("Uber Move", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sexoList.FormattingEnabled = true;
            this.sexoList.Items.AddRange(new object[] {
            "Masculino",
            "Feminino",
            "Prefiro não dizer"});
            this.sexoList.Location = new System.Drawing.Point(15, 540);
            this.sexoList.Name = "sexoList";
            this.sexoList.Size = new System.Drawing.Size(371, 92);
            this.sexoList.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 505);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 33);
            this.label6.TabIndex = 7;
            this.label6.Text = "Sexo";
            // 
            // transList
            // 
            this.transList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.transList.CheckOnClick = true;
            this.transList.Font = new System.Drawing.Font("Uber Move", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.transList.FormattingEnabled = true;
            this.transList.Items.AddRange(new object[] {
            "Ambulância",
            "UTI móvel",
            "Helicóptero"});
            this.transList.Location = new System.Drawing.Point(15, 402);
            this.transList.Name = "transList";
            this.transList.Size = new System.Drawing.Size(371, 92);
            this.transList.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 367);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(259, 33);
            this.label7.TabIndex = 9;
            this.label7.Text = "Tipo de Transporte";
            // 
            // numberTxt
            // 
            this.numberTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numberTxt.Location = new System.Drawing.Point(15, 311);
            this.numberTxt.Name = "numberTxt";
            this.numberTxt.Size = new System.Drawing.Size(371, 40);
            this.numberTxt.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 276);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(304, 33);
            this.label8.TabIndex = 11;
            this.label8.Text = "Número de Resultados";
            // 
            // pacientesTxt
            // 
            this.pacientesTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pacientesTxt.Location = new System.Drawing.Point(15, 225);
            this.pacientesTxt.Name = "pacientesTxt";
            this.pacientesTxt.Size = new System.Drawing.Size(371, 40);
            this.pacientesTxt.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 190);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(137, 33);
            this.label9.TabIndex = 13;
            this.label9.Text = "Pacientes";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(277, 33);
            this.label10.TabIndex = 15;
            this.label10.Text = "Inicio do Tratamento";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 105);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(314, 33);
            this.label11.TabIndex = 17;
            this.label11.Text = "Término do Tratamento";
            // 
            // inicioPicker
            // 
            this.inicioPicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.inicioPicker.Location = new System.Drawing.Point(15, 54);
            this.inicioPicker.Name = "inicioPicker";
            this.inicioPicker.Size = new System.Drawing.Size(208, 40);
            this.inicioPicker.TabIndex = 18;
            // 
            // terminoPicker
            // 
            this.terminoPicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.terminoPicker.Location = new System.Drawing.Point(15, 140);
            this.terminoPicker.Name = "terminoPicker";
            this.terminoPicker.Size = new System.Drawing.Size(208, 40);
            this.terminoPicker.TabIndex = 19;
            // 
            // FilteredTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(1272, 1100);
            this.Controls.Add(this.dgvPanel);
            this.Controls.Add(this.filterPanel);
            this.Controls.Add(this.exportBtn);
            this.Controls.Add(this.parametersBtn);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Uber Move", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "FilteredTableForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ponte Dourada - Dashboard";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.filterPanel.ResumeLayout(false);
            this.filterPanel.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.dgvPanel.ResumeLayout(false);
            this.dgvPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiltered)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.LinkLabel dashboardLink;
        private System.Windows.Forms.LinkLabel atendimetoLink;
        private System.Windows.Forms.Button parametersBtn;
        private System.Windows.Forms.Button exportBtn;
        private System.Windows.Forms.Panel filterPanel;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label xLbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox userListCb;
        private System.Windows.Forms.Button defineBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.CheckedListBox transListCb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel dgvPanel;
        private System.Windows.Forms.DataGridView dgvFiltered;
        private System.Windows.Forms.TextBox numberTxt;
        private System.Windows.Forms.CheckedListBox transList;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckedListBox sexoList;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckedListBox origemList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckedListBox destinoList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox pacientesTxt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker terminoPicker;
        private System.Windows.Forms.DateTimePicker inicioPicker;
    }
}