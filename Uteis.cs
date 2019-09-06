using System;
using static System.Console;

namespace SoN_Financeiro
{
    public static class Uteis
    {
        public static void MontaMenu()
        {
            MontaHeader("CONTROLE FINANCEIRO - SON");
            WriteLine("Selecione uma opção abaixo: ");
            WriteLine("-----------------------------------------");
            WriteLine("1 - Listar");
            WriteLine("2 - Cadastrar");
            WriteLine("3 - Editar");
            WriteLine("4 - Excluir");
            WriteLine("5 - Relatório");
            WriteLine("6 - Sair");
            WriteLine("Opção: ");
        }

        public static void MontaHeader(string titulo, char cod = '=', int len = 30)
        {
            // cria strings especificando os caracteres e o tamanho
            WriteLine(new string(cod, len) + " " + titulo + " " + new string(cod, len) + "\n");
        }
    }
}
