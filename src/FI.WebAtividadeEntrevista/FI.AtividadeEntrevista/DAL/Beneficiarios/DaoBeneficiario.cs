using FI.AtividadeEntrevista.DML;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FI.AtividadeEntrevista.DAL
{
    /// <summary>
    /// Classe de acesso a dados de Beneficiario
    /// </summary>
    internal class DaoBeneficiario : AcessoDados
    {
        /// <summary>
        /// Inclui um novo beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        internal long Incluir(DML.Beneficiario beneficiario)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", beneficiario.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", beneficiario.CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("IdCliente", beneficiario.IdCliente));

            DataSet ds = base.Consultar("FI_SP_IncBeneficiario", parametros);
            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            return ret;
        }


        /// <summary>
        /// Lista todos os beneficiarios do cliente
        /// </summary>
        /// <param name="idCliente">Id do Cliente</param>
        internal List<DML.Beneficiario> Listar(long idCliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("IdCliente", idCliente));

            DataSet ds = base.Consultar("FI_SP_ConsBeneficiario", parametros);
            List<DML.Beneficiario> cli = Converter(ds);

            return cli;
        }

        /// <summary>
        /// Exclui o beneficiario pelo id
        /// </summary>
        /// <param name="id">Id do Cliente</param>
        internal void Excluir(long id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", id));

            base.Executar("FI_SP_DelBeneficiario", parametros);
        }

        private List<DML.Beneficiario> Converter(DataSet ds)
        {
            List<DML.Beneficiario> lista = new List<DML.Beneficiario>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DML.Beneficiario cli = new DML.Beneficiario();
                    cli.Id = row.Field<long>("Id");
                    cli.Nome = row.Field<string>("Nome");
                    cli.CPF = row.Field<string>("CPF");
                    cli.IdCliente = row.Field<long>("IdCliente");
                    lista.Add(cli);
                }
            }

            return lista;
        }
    }
}
