using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Modelo;
using Persistencia;
using static System.Console;

namespace SoN_Financeiro
{
    class Program
    {
        private List<Conta> contas;
        private List<Categoria> categorias;

        private ContaDAL conta;
        private CategoriaDAL categoria;

        public Program()
        {
            string strConn = Db.Conexao.GetStringConnection();
            this.conta = new ContaDAL(new SqlConnection(strConn));
            this.categoria = new CategoriaDAL(new SqlConnection(strConn));
        }

        static void Main(string[] args)
        {
            int opc;

            Program p = new Program();

            do
            {
                Title = "CONTROLE FINANCEIRO SON";
                Uteis.MontaMenu();
                opc = Convert.ToInt32(ReadLine());

                if(!(opc >= 1 && opc <= 6))
                {
                    Clear();
                    // muda cores de background e texto no console
                    BackgroundColor = ConsoleColor.DarkRed;
                    ForegroundColor = ConsoleColor.White;
                    Uteis.MontaHeader("INFORME UMA OPÇÃO VÁLIDA !", 'X');
                    ResetColor();

                } else
                {
                    Clear();
                    switch (opc)
                    {
                        case 1:

                            Title = "LISTAGEM DE CONTAS - CONTROLE FINANCEIRO SON";
                            Uteis.MontaHeader("Listagem de Contas");

                            ListarContas(p);
                            
                            ReadLine();
                            Clear();

                            break;
                        case 2:

                            Title = "NOVA CONTA - CONTROLE FINANCEIRO SON";
                            Uteis.MontaHeader("CADASTRANDO UMA NOVA CONTA");

                            CadastrarConta(p);

                            ReadLine();
                            Clear();

                            break;
                        case 3:

                            Title = "EDITAR CONTAS - CONTROLE FINANCEIRO SON";
                            Uteis.MontaHeader("Editar Conta");
                            ReadLine();
                            Clear();

                            break;
                        case 4:

                            Title = "EXCLUIR CONTA - CONTROLE FINANCEIRO SON";
                            Uteis.MontaHeader("Excluir uma Conta");
                            ReadLine();
                            Clear();

                            break;
                        case 5:

                            Title = "RELATÓRIO DE CONTAS - CONTROLE FINANCEIRO SON";
                            Uteis.MontaHeader("Relatório por Data de Vencimento");

                            Write("Informe a data inicial (dd/mm/aaaa): ");
                            string data_inicial = ReadLine();

                            Write("Informe a data final (dd/mm/aaaa): ");
                            string data_final = ReadLine();

                            // irá listar apenas as contas com data de vencimento dentro do intervalo especificado
                            ListarContas(p, data_inicial, data_final);

                            ReadLine();
                            Clear();

                            break;
                    }
                }
            } while (opc != 6);
        }

        // métodos estáticos das views que serão chamados dentro do switch
        private static void ListarContas(Program p, string data_inicial = "", string data_final = "")
        {
            p.contas = p.conta.ListarTodos(data_inicial, data_final);

            // usando pacote do nuget ConsoleTable para exibição em tabela dos dados (passa-se as colunas como parametro)
            ConsoleTable table = new ConsoleTable("ID", "Descrição", "Tipo", "Valor", "Data Vencimento");

            foreach (var c in p.contas)
            {
                // adiciona linha para cada registro
                table.AddRow(c.Id, c.Descricao, c.Tipo.Equals('R') ? "Receber" : "Pagar", String.Format("{0:c}", c.Valor), String.Format("{0:dd/MM/yyyy}", c.DataVencimento));
            }
            table.Write();
        }

        private static void CadastrarConta(Program p)
        {
            string descricao = "";

            // validação para descrição
            do
            {
                Write("Informe a descriçao da conta: ");
                descricao = ReadLine();

                if (descricao.Equals(""))
                {
                    BackgroundColor = ConsoleColor.DarkRed;
                    ForegroundColor = ConsoleColor.White;
                    Uteis.MontaHeader("INFORME UMA DESCRIÇÃO PARA A CONTA", '*');
                    ResetColor();
                }
            } while (descricao.Equals(""));

            Write("Informe o valor: ");
            double valor = Convert.ToDouble(ReadLine());

            Write("Informe o tipo (R para Receber ou P para Pagar): ");
            char tipo = Convert.ToChar(ReadLine());

            Write("Informe o data de vencimento (dd/mm/aaaa): ");
            DateTime dataVencimento = DateTime.Parse(ReadLine());

            // exibirá a lista de categorias para escolha
            WriteLine("Selecione uma categoria pela ID: \n");
            p.categorias = p.categoria.ListarTodos();
            ConsoleTable table = new ConsoleTable("ID", "Nome");

            foreach (var c in p.categorias)
            {
                // adiciona linha para cada registro
                table.AddRow(c.Id, c.Nome);
            }
            table.Write();

            Write("Categoria: ");
            int cat_id = Convert.ToInt32(ReadLine());
            Categoria cat_cadastro = p.categoria.GetCategoria(cat_id);

            // criando obj conta
            Conta conta = new Conta()
            {
                Descricao = descricao,
                Valor = valor,
                Tipo = tipo,
                DataVencimento = dataVencimento,
                Categoria = cat_cadastro
            };

            // salvando no banco de dados
            p.conta.Salvar(conta);

            BackgroundColor = ConsoleColor.DarkGreen;
            ForegroundColor = ConsoleColor.White;
            Uteis.MontaHeader("CONTA SALVA COM SUCESSO !", '+');
            ResetColor();
        }

    }
}
