﻿namespace Consignado.HttpApi.Dominio.Proposta
{
    public class DDD
    {
        public string Numero { get; private set; }

        public DDD(string numero)
        {
            Numero = numero;
        }
    }
}
