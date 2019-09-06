using System;
using System.Collections.Generic;
using System.Data.SqlClient;
// cria um apelido para a classe importada
using CategoriaModelo = Modelo.Categoria;

namespace Persistencia
{
    public class CategoriaDAL
    {
        private SqlConnection conn;

        public CategoriaDAL(SqlConnection conn)
        {
            this.conn = conn;
        }

        public CategoriaModelo GetCategoria(int id)
        {
            CategoriaModelo categoria = new CategoriaModelo();

            // retorna o registro que tem o msm id passado como parametro
            var command = new SqlCommand("select id, nome from categorias where id = @id", this.conn);
            command.Parameters.AddWithValue("@id", id);
            this.conn.Open();

            using(SqlDataReader rd = command.ExecuteReader())
            {
                rd.Read();

                // atribui os valores recebidos dos campos aos atributos do objeto categoria
                categoria.Id = Convert.ToInt32(rd["id"].ToString());
                categoria.Nome = rd["nome"].ToString();
            }

            this.conn.Close();
            return categoria;
        }

        public List<CategoriaModelo> ListarTodos()
        {
            List<CategoriaModelo> categorias = new List<CategoriaModelo>();

            var command = new SqlCommand("select id, nome from categorias", this.conn);

            this.conn.Open();

            using(SqlDataReader rd = command.ExecuteReader())
            {
                // a cada registro retornado, irá atribuir seus valores aos objetos categoria criados
                while(rd.Read())
                {
                    CategoriaModelo categoria = new CategoriaModelo()
                    {
                        Id = Convert.ToInt32(rd["id"].ToString()),
                        Nome = rd["nome"].ToString(),
                    };

                    // adiciona o objeto á lista de objetos
                    categorias.Add(categoria);
                }
            }
            return categorias;
        }

    }
}
