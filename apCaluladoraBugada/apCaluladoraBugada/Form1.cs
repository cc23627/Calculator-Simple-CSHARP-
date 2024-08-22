

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace apCaluladoraBugada
{


    public partial class Form1 : Form
    {
        float[] valores = new float[26];
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
            btnIgual.Click += BtnOperacoes_Click;
        }


        private void Form1_Load(object sender, EventArgs e) => Verificacao_txtVisor();

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
                case "=":
                    int z = 0;
                    string texto_visor = txtVisor.Text;
                    string transformada = "";
                    // Faz "transformada" ser a operação atual do visor, só que com as letras inves dos valores
                    for (int i = 0; i < texto_visor.Length;)
                    {
                        if (!"0123456789".Contains(texto_visor[i]))
                        {
                            transformada += texto_visor[i];
                            i++;
                            continue;
                        }
                        float aux = 0;
                        while (i < texto_visor.Length && "0123456789".Contains(texto_visor[i]))
                        {
                            aux = 10 * aux + (texto_visor[i] - '0');
                            i++;
                        }
                        if (i < texto_visor.Length && texto_visor[i] == '.')
                        {
                            int k = -1;
                            while (i < texto_visor.Length && "0123456789".Contains(texto_visor[i]))
                            {
                                aux = (float)(Math.Pow((double)10, (double)k) * (texto_visor[i] - '0') + aux);
                                k++;
                                i++;
                            }
                        }
                        valores[z] = aux;
                        transformada += (char)('A' + z);
                        z++;
                    }
                    string n = Posfixa(transformada);
                    string l = "";
                    foreach(char p in n)
                    {
                        if(EhOperador(p))
                        {
                            l += p + " ";
                        }
                        else
                        {
                            l += valores[p - 'A'] + " "; 
                        }
                    }
                    txtResultado.Text = ValorDaExpressaoPosfixa(n).ToString();
                    lbPosfixa.Text = "infixa: " + texto_visor + "\nposfixa: " + l;
                    break;
                default:
                    txtVisor.Text += numeroClicado;
                    break;
            }
            Verificacao_txtVisor();
        }

        float ValorDaExpressaoPosfixa(string cadeiaPosfixa)
        {
            var umaPilha = new PilhaVetor<float>();
            for (int atual = 0; atual < cadeiaPosfixa.Length; atual++)
            {
                char simbolo = cadeiaPosfixa[atual];
                if (!EhOperador(simbolo)) // É Operando 
                    umaPilha.Empilhar(valores[simbolo -'A']);
                else
                {
                    float operando2 = umaPilha.Desempilhar();
                    float operando1 = umaPilha.Desempilhar();
                    float valorParcial = ValorDaSubExpressao((char)operando1, simbolo, (char)operando2);
                    umaPilha.Empilhar(valorParcial);
                }
            }
            return umaPilha.Desempilhar(); // resultado final
        }

        float ValorDaSubExpressao(char operando1, char simbolo, char operando2)
        {
            switch (simbolo)
            {
                case '+':
                    return (float)operando1 + (float)operando2;
                    
                case '-':
                    return (float)operando1 - (float)operando2;
                    
                case '*':
                    return (float)operando2 * (float)operando1;
                    
                case '/':
                    return (float)operando1 / (float)operando2;

                case '^':
                    for (int i = 0; i < operando2; i++) operando1 *= operando1;
                    return (float)operando1;
            }
            throw new Exception("Pode não fera");

        }
        bool EhOperador(char operador)
        {
            return "+*/-^()".Contains(operador);
        }

        bool TemPrecedencia(char a, char b)
        {
            switch (a)
            {
                case '/':
                case '*':
                    return '^' != b;
                case '-':
                    return b == '+';
                case '+':
                    return b == '-';
                default: 
                    return true;

            }

        }
        string Posfixa(string lido)
        {
            string resultado = "";
            char operadorComMaiorPrecedencia;
            PilhaVetor<char> operadores_lidos = new PilhaVetor<char>();
            for (int i = 0; i < lido.Length; i++)
            {
                if (!EhOperador(lido[i]))
                {
                    resultado += lido[i];
                }
                else
                {
                    bool parar = false;
                    while (!parar && !operadores_lidos.EstaVazia &&
                    TemPrecedencia(operadores_lidos.OTopo(), lido[i]))
                    {
                        operadorComMaiorPrecedencia = operadores_lidos.Desempilhar();
                        if (operadorComMaiorPrecedencia != '(')
                            resultado += operadorComMaiorPrecedencia;
                        else
                            parar = true;
                    }
                    if (lido[i] != ')')
                        operadores_lidos.Empilhar(lido[i]);

                }
            }
            while (!operadores_lidos.EstaVazia)
            {
                operadorComMaiorPrecedencia = operadores_lidos.Desempilhar();
                if (operadorComMaiorPrecedencia != '(')
                    resultado += operadorComMaiorPrecedencia;
            }
            return resultado;
        }
    }
}
