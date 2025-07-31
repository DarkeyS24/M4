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

namespace M4
{
    public partial class LoginForm : Form
    {
        private readonly Sessao4 context;

        public LoginForm()
        {
            InitializeComponent();
            context = Sessao4.GetInstance();
        }

        private void entrarBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(emailTxt.Text) || string.IsNullOrEmpty(senhaTxt.Text))
            {
                MessageBox.Show("Insira o seu E-mail e senha","Campos vazíos",MessageBoxButtons.OK);
                return;
            }
            else
            {
                double senha = 0;
                try
                {
                    senha = double.Parse(senhaTxt.Text);

                }catch(Exception)
                {
                    MessageBox.Show("A senha só comtem números", "Senha invalida", MessageBoxButtons.OK);
                    return;
                }
                var user = context.Usuarios.FirstOrDefault(u => u.Email == emailTxt.Text && u.Senha == senha);
                if (user == null)
                {
                    MessageBox.Show("Usuario não encontrado", "Usuario invalido", MessageBoxButtons.OK);
                }
                else if(user != null && user.TipoUsuario == "Comum")
                {
                    MessageBox.Show("Usuario não posee acesso de administrador", "Acesso negado", MessageBoxButtons.OK);
                }
                else
                {
                    Properties.Settings.Default.UsuarioId = user.Id;
                    DashboardForm dashboardForm = new DashboardForm();
                    dashboardForm.SetPrevForm(this);
                    dashboardForm.Show();
                    this.Hide();

                    senhaTxt.Text = string.Empty;
                    emailTxt.Text = string.Empty;
                }
            }
        }
    }
}
