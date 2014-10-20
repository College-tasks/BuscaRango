using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BuscaRangoCode
{
    public class TagService
    {
        /// <summary>
        /// Insere um objeto na base de dados
        /// </summary>
        /// <param name="obj">Objeto a ser inserido</param>
        /// <returns>Objeto "Retorno" (Sucesso ou falha da operação)</returns>
        public static Retorno Insert(BR_Tag obj)
        {
            // Cria objeto de retorno
            Retorno ret = new Retorno();

            // Usando o contexto ER_Entities, execute o bloco de código
            using (var ctx = new ER_Entities())
            {
                try
                {
                    // Adiciona e salva
                    ctx.BR_Tag.Add(obj);
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
        public static Retorno Update(BR_Tag updateObj, int id)
        {
            // Cria objeto de retorno
            Retorno ret = new Retorno();

            // Usando o contexto ER_Entities, execute o bloco de código
            using (var ctx = new ER_Entities())
            {
                try
                {
                    // Recebe o primeiro objeto da lista de Entidades
                    BR_Tag obj = ctx.BR_Tag.FirstOrDefault(x => x.Id == id);
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
                    var obj = ctx.BR_Tag;
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
        /// Seleciona todos objetos
        /// </summary>
        /// <returns>Objeto de "Retorno" com todas entradas do banco</returns>
        public static Retorno SelectAllComPrato()
        {
            // Cria objeto de retorno
            Retorno ret = new Retorno();

            // Usando o contexto ER_Entities, execute o bloco de código
            using (var ctx = new ER_Entities())
            {
                try
                {
                    // Recupera todos objetos do grupo
                    var obj = ctx.BR_Tag.Include(x => x.BR_Prato);

                    List<BR_Tag> lst = obj.ToList();
                    List<BR_Tag> lstFiltrada = lst.Where(x => x.BR_Prato.Count > 0).ToList();

                    ret.RetObj = lstFiltrada;
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
        public static Retorno SelectAllComEstabelecimento()
        {
            // Cria objeto de retorno
            Retorno ret = new Retorno();

            // Usando o contexto ER_Entities, execute o bloco de código
            using (var ctx = new ER_Entities())
            {
                try
                {
                    // Recupera todos objetos do grupo
                    var obj = ctx.BR_Tag.Include(x => x.BR_Estabelecimento);

                    List<BR_Tag> lst = obj.ToList();
                    List<BR_Tag> lstFiltrada = lst.Where(x => x.BR_Estabelecimento.Count > 0).ToList();

                    ret.RetObj = lstFiltrada;
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
                    // Recebe o primeiro objeto da lista de Entidades que possui a expressão especificada
                    var obj = ctx.BR_Tag.FirstOrDefault(x => x.Id == id);
                    ret.RetObj = obj;
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
                    var obj = ctx.BR_Tag.FirstOrDefault(x => x.Id == id);
                    ctx.BR_Tag.Remove(obj);
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
