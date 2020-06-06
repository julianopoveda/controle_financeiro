using System;
using System.Collections.Generic;

namespace back
{
    public abstract class Contas
    {
        protected Dictionary<string, string> Validacao { get; set; }
        public Contas(int quantidadeCampos)
        {
            Validacao = new Dictionary<string, string>(quantidadeCampos);
        }

        protected abstract bool Validar();

        //usei func pq sim :)
        public void AddIfInvalid(Func<bool> validacao, string campo, string mensagemErro){
            if(validacao())
                Validacao.Add(campo, mensagemErro);
        }
    }        
}