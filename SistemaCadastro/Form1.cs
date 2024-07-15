using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaCadastro
{
    public partial class Form1 : Form
    {
        // Lista genérica: coleção dinâmica (Lista do tipo Pessoa)
        // é instanciada na raiz para que seja possível acessá-la em todo o código
        List<Pessoa> pessoas; // nome da coleção é pessoas

        // Construtor
        public Form1()
        {
            InitializeComponent();
            pessoas = new List<Pessoa>(); // inicializa a lista 

            // Add opções
            comboEC.Items.Add("Casado");
            comboEC.Items.Add("Solteiro");
            comboEC.Items.Add("Viúvo");
            comboEC.Items.Add("Separado");

            comboEC.SelectedIndex = 0; // Deixa o elemento 1 do combo selecionado por default

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            int index = -1; // Utilizado para verificar se a pessoa já está cadastrada (-1 = não cadastrada)

            foreach (Pessoa pessoa in pessoas)
            {
                // verifica se a pessoa já está cadastrada (Compara os nomes da lista com o valor passado no textBox)
                if(pessoa.Nome == txtNome.Text)
                {
                    index = pessoas.IndexOf(pessoa);
                    // MessageBox.Show("Usuário já cadastrado!");
                }
            }

            // Verifica se os campos foram preenchidos
            if(txtNome.Text == "")
            {
                MessageBox.Show("Preencha o campo nome."); // Exibe caixa com mensagem
                txtNome.Focus(); // Foca o cursor no campo nome
                return; // Sai do método e finaliza execução do código (não permite avançar sem o preenchimento)
            }
            
            if(txtTelefone.Text == "(  )      -")
            {
                MessageBox.Show("Preencha o campo telefone."); // Exibe caixa com mensagem
                txtTelefone.Focus(); // Foca o cursor no campo telefone
                return; // Sai do método e finaliza execução do código (não permite avançar sem o preenchimento)
            }


            char sexo; // Variável para salvar o sexo (um caractere)

            // Verifica qual sexo foi selecionado (char deve ser escrito entre aspas simples)
            if (radioM.Checked)  
            { 
                sexo = 'M';
            } 
            else if (radioF.Checked) 
            { 
                sexo = 'F';
            }
            else { 
                sexo = 'O'; 
            }

            // Dados preenchidos -> cria objeto do tipo pessoa com os dados e insere na lista
            Pessoa p = new Pessoa();
            p.Nome = txtNome.Text;
            p.DataNascimento = txtData.Text; // data retorna string
            p.EstadoCivil = comboEC.SelectedItem.ToString(); // Pega o item que está em um objeto e converte para string
            p.Telefone = txtTelefone.Text;
            p.CasaPropria = checkCasa.Checked; // salva true ou false (bool)
            p.Veiculo = checkVeiculo.Checked;
            p.Sexo = sexo; // Seta o valor que foi definido na verificação acima.


            // Decide se vai cadastrar uma nova pessoa ou atualizar registro
            if(index < 0)
            {
                // -1 = Nâo existe, então cadastra.
                pessoas.Add(p);
            }
            else
            {
                // a partir do 0, cadastro existe, então atualiza.
                pessoas[index] = p;
            }


            // Chama o método para limpar todos os campos
            btnLimpar_Click(btnLimpar, EventArgs.Empty); // objeto passado é o próprio btnLimpar -> sem info para evento

            // Lista todos os registros
            Listar();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            // Utilizado para pegar o índice do registro a ser excluído
            int index = lista.SelectedIndex; // selectedIndex => selecionado ao clicar no registro
            pessoas.RemoveAt(index); // exclui

            Listar(); // Exibe lista atualizada
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNome.Text = ""; 
            txtData.Text = ""; 
            comboEC.SelectedItem = 0; // elemento 1 selecionado por padrão
            txtTelefone.Text = "";
            checkCasa.Checked = false; 
            checkVeiculo.Checked = false;
            radioM.Checked = true; // Selecionado por padrão
            radioF.Checked = false; 
            radioO.Checked = false; 
            txtNome.Focus(); // Foca no campo Nome
        }

        private void Listar()
        {
            lista.Items.Clear(); // Acessa o controle e remote todos os registros

            // Percorre a lista e exibe todos os registros
            foreach (Pessoa p in pessoas)
            {
                lista.Items.Add($"{p.Nome} | {p.Telefone}"); // Add pelo nome
            }

        }

        private void lista_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = lista.SelectedIndex; // Pega o indice do elemento selecionado na lista (controle)
            Pessoa p = pessoas[index]; // Pega o objeto salvo

            // Passa os valores do objeto para os campos
            txtNome.Text = p.Nome;
            txtData.Text = p.DataNascimento;
            comboEC.SelectedItem = p.EstadoCivil; 
            txtTelefone.Text = p.Telefone;
            checkCasa.Checked = p.CasaPropria;
            checkVeiculo.Checked = p.Veiculo;

            // Verificação para saber o sexo selecionado
            switch (p.Sexo)
            {
                case 'M':
                    radioM.Checked = true; 
                    break;
                case 'F':
                    radioF.Checked = true;
                    break;
                default:
                   radioO.Checked = true;
                    break;
            }
            txtNome.Focus(); 
        }
    }
}
