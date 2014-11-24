using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BuscaRangoCode
{
    public class ComentariosEstabelecimentosService
    {
        /// <summary>
        /// Insere um objeto na base de dados
        /// </summary>
        /// <param name="obj">Objeto a ser inserido</param>
        /// <returns>Objeto "Retorno" (Sucesso ou falha da operação)</returns>
        public static Retorno Insert(BR_Comentario_Estabelecimento obj)
        {
            // Cria objeto de retorno
            Retorno ret = new Retorno();

            // Usando o contexto ER_Entities, execute o bloco de código
            using (var ctx = new ER_Entities())
            {
                try
                {
                    // Adiciona e salva
                    ctx.BR_Comentario_Estabelecimento.Add(obj);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    ret.Sucesso = false;
                    ret.MsgErro = ex.Message;
                }
            }

            // Retorna o objeto de retorno
            return ret;
        }

        /// <summary>
        /// Faz o Update de um objeto
        /// </summary>
        /// <param name="updateObj">Objeto com as novas propriedades</param>
        /// <param name="id">Id do objeto a ser editado</param>
        /// <returns>Objeto "Retorno" (Sucesso ou falha da operação)</returns>
        public static Retorno Update(BR_Comentario_Prato updateObj, int id)
        {
            // Cria objeto de retorno
            Retorno ret = new Retorno();

            // Usando o contexto ER_Entities, execute o bloco de código
            using (var ctx = new ER_Entities())
            {
                try
                {
                    // Recebe o primeiro objeto da lista de Entidades
                    BR_Comentario_Estabelecimento obj = ctx.BR_Comentario_Estabelecimento.FirstOrDefault(x => x.Id == id);
                    // Edita os campos atuais

                    // Salva as mudanças feitas no contexto
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    ret.Sucesso = false;
                    ret.MsgErro = ex.Message;
                }
            }

            // Retorna o objeto de retorno
            return ret;
        }

        /// <summary>
        /// Seleciona todos objetos
        /// </summary>
        /// <returns>Objeto de "Retorno" com todas entradas do banco</returns>
        public static Retorno SelectAll()
        {
            // Cria objeto de retorno
            Retorno ret = new Retorno();

            // Usando o contexto ER_Entities, execute o bloco de código
            using (var ctx = new ER_Entities())
            {
                try
                {
                    // Recupera todos objetos do grupo
                    var obj = ctx.BR_Comentario_Estabelecimento.Include("BR_Usuario");
                    ret.RetObj = obj.ToList();
                }
                catch (Exception ex)
                {
                    ret.Sucesso = false;
                    ret.MsgErro = ex.Message;
                }
            }

            // Retorna o objeto de retorno
            return ret;
        }

        /// <summary>
        /// Seleciona um objeto pelo seu ID
        /// </summary>
        /// <param name="id">ID do objeto</param>
        /// <returns>Objeto "Retorno"</returns>
        public static Retorno SelectById(int id)
        {
            // Cria objeto de retorno
            Retorno ret = new Retorno();

            // Usando o contexto ER_Entities, execute o bloco de código
            using (var ctx = new ER_Entities())
            {
                try
                {
                    var obj = ctx.BR_Comentario_Estabelecimento.Include("BR_Usuario").Where(x => x.Id_Estabelecimento == id);
                    ret.RetObj = obj.ToList();
                }
                catch (Exception ex)
                {
                    ret.Sucesso = false;
                    ret.MsgErro = ex.Message;
                }
            }

            // Retorna o objeto de retorno
            return ret;
        }

        /// <summary>
        /// Deleta um objeto
        /// </summary>
        /// <param name="id">ID do objeto a ser deletado</param>
        /// <returns>Objeto "Retorno" (Sucesso ou falha da operação)</returns>
        public static Retorno Delete(int id)
        {
            // Cria objeto de retorno
            Retorno ret = new Retorno();

            // Usando o contexto ER_Entities, execute o bloco de código
            using (var ctx = new ER_Entities())
            {
                try
                {
                    // Recebe o primeiro objeto da lista de Entidades que possui a expressão especificada
                    var obj = ctx.BR_Comentario_Estabelecimento.FirstOrDefault(x => x.Id == id);
                    ctx.BR_Comentario_Estabelecimento.Remove(obj);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    ret.Sucesso = false;
                    ret.MsgErro = ex.Message;
                }
            }

            // Retorna o objeto de retorno
            return ret;
        }
    }
}


