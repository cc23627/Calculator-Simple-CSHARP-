using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace apCaluladoraBugada
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            btn_nove.Click += BtnNumeros_Click;
            btn_oito.Click += BtnNumeros_Click;
            btn_sete.Click += BtnNumeros_Click;
            btn_seis.Click += BtnNumeros_Click;
            btn_cinco.Click += BtnNumeros_Click;
            btn_quatro.Click += BtnNumeros_Click;
            btn_tres.Click += BtnNumeros_Click;
            btn_dois.Click += BtnNumeros_Click;
            btn_um.Click += BtnNumeros_Click;
            btn_zero.Click += BtnNumeros_Click;

            txtVisor.KeyPress += TxtVisor_KeyPress;

            btn_divisao.Click += BtnOperacoes_Click;
            btn_multiplicacao.Click += BtnOperacoes_Click;
            btn_potenciacao.Click += BtnOperacoes_Click;
            btn_soma.Click += BtnOperacoes_Click;
            btn_subtracao.Click += BtnOperacoes_Click;
            btn_abreParenteses.Click += BtnOperacoes_Click;
            btn_fechaParenteses.Click += BtnOperacoes_Click;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Verificacao_txtVisor();
        }

        private void Verificacao_txtVisor()
        {

            // Verificação se está vazio
            bool textoVazio = string.IsNullOrEmpty(txtVisor.Text);
            // Vai desativar ou ativar a depender da condição acima
            btnIgual.Enabled = !textoVazio;
            btn_divisao.Enabled = !textoVazio;
            btn_multiplicacao.Enabled = !textoVazio;
            btn_potenciacao.Enabled = !textoVazio;
            btn_soma.Enabled = !textoVazio;
            btn_subtracao.Enabled = !textoVazio;
            btn_fechaParenteses.Enabled = txtVisor.Text.Contains("(");


        }

        private void BtnNumeros_Click(object sender, EventArgs e)
        {
            Button botaoClicado = (Button)sender;
            string numeroClicado = botaoClicado.Text;
            
            switch (numeroClicado)
            {
                default:
                    txtVisor.Text += numeroClicado;
                    break;
            }
            Verificacao_txtVisor();
        }

        private void TxtVisor_KeyPress(object sender, KeyPressEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnOperacoes_Click(object sender, EventArgs e)
        {
            Button botaoClicado = (Button)sender;
            string numeroClicado = botaoClicado.Text;

            switch (numeroClicado)
            {
                default:
                    txtVisor.Text += numeroClicado;
                    break;
            }
            Verificacao_txtVisor();
        }
    }
}
