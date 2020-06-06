using System;
using System.Collections.Generic;
using System.Linq;

namespace back
{
    public class Receita : Contas
    {
        public string Natureza { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataPagamento { get; set; }
        public bool Recorrente { get; set; }
        public RecorrenciaEnum Recorrencia { get; set; }

        public Receita() : base(4) { }

        public Receita(string natureza, decimal valor, DateTime dataPagamento, bool recorrente, RecorrenciaEnum recorrencia) : base(4)
        {
            Natureza = natureza;
            Valor = valor;
            DataPagamento = dataPagamento;
            Recorrente = recorrente;
            Recorrencia = recorrencia;
        }

        public static Receita NovaReceita(string natureza, decimal valor, DateTime dataPagamento)
        {
            Receita receita = new Receita(natureza, valor, dataPagamento, false, RecorrenciaEnum.unica);
            if (receita.Validar())
                return receita;
            else
                throw new Exception("Nova receita inválida");
        }

        public static ICollection<Receita> NovaReceitaRecorrente(string natureza, decimal valor, DateTime dataPrimeiroPagamento, DateTime dataUltimoPagamento, RecorrenciaEnum recorrencia)
        {
            ICollection<Receita> receitas = new List<Receita>();
            DateTime dataReceita = dataPrimeiroPagamento;

            while (dataReceita <= dataUltimoPagamento)
            {
                Receita receita = new Receita(natureza, valor, dataReceita, true, recorrencia);
                if (receita.Validar())
                    receitas.Add(receita);
                else
                    throw new Exception("Os valores informados para gerar a receita recorrente é inválido");

                dataReceita = CalcularProximaData(dataReceita, recorrencia);
            }

            return receitas;
        }

        private static DateTime CalcularProximaData(DateTime dataReceita, RecorrenciaEnum recorrencia)
        => (recorrencia) switch
        {
            RecorrenciaEnum.diaria => dataReceita.AddDays(1),
            RecorrenciaEnum.semanal => dataReceita.AddHours(7),
            RecorrenciaEnum.mensal => dataReceita.AddMonths(1),            
            _ => throw new Exception("Recorrencia deve ser diferente de única")
        };

        protected override bool Validar()
        {
            AddIfInvalid(() => !string.IsNullOrEmpty(Natureza), "Natureza", "Campo não pode ser vazio");
            AddIfInvalid(() => Valor > 0, "Valor", "Valor deve ser positivo");
            AddIfInvalid(() => DataPagamento > DateTime.MinValue, "DataPagamento", "Data de pagamento não pode ser vázia");

            return !Validacao.Any();
        }

    }
}
