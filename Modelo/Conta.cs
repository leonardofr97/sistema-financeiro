using System;

namespace Modelo
{
    public class Conta
    {
        // com a interrogação é dito que _id pode ser inteiro ou nulo
        private int? _id;
        private string _descricao;
        private double _valor;
        private char _tipo;
        private DateTime _data_vencimento;
        private Categoria _categoria;

        public int? Id { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }

        // validação para tipo
        public char Tipo 
        { 
            get => _tipo;
            set => _tipo = !value.Equals('P') && !value.Equals('R') ? throw new Exception("Use P para Pagar e R para Receber") : value;
        }
        public DateTime DataVencimento { get; set; }
        public Categoria Categoria { get; set; }
    }
}
