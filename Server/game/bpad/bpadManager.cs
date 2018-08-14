using System;
using System.Data;
using System.Collections.Generic;

using Boombang.database;
using Boombang.game.user;

namespace Boombang.game.bpad
{
    public class bpadManager
    {
        public List<friends> amigos(int id_usuario)
        {
            using (DatabaseClient dbClient = Environment.GetDatabase().GetClient())
            {
                dbClient.AddParamWithValue("@usuario", id_usuario);
                int i_query_amigos = dbClient.ReadInt32("SELECT COUNT(*) FROM amigos WHERE (id_usuario = @usuario OR id_amigo = @usuario) AND aceptado = '1' AND @usuario IN (SELECT id FROM usuarios);");
                List<friends> result_amigos = new List<friends>();

                if (i_query_amigos > 0)
                {
                    DataTable dTable = dbClient.ReadDataSet("SELECT * FROM amigos WHERE (id_usuario = @usuario OR id_amigo = @usuario) AND aceptado = '1' AND @usuario IN (SELECT id FROM usuarios);").Tables[0];
                    foreach (DataRow dRow in dTable.Rows)
                    {
                        result_amigos.Add(new friends(Convert.ToInt32(dRow["id_usuario"]), Convert.ToInt32(dRow["id_amigo"]), Convert.ToInt32(dRow["aceptado"])));
                    }
                }
                return result_amigos;
            }
        }

        public bool esAmigo(int id_usuario, int id_amigo)
        {
            using (DatabaseClient dbClient = Environment.GetDatabase().GetClient())
            {
                dbClient.AddParamWithValue("@usuario", id_usuario);
                dbClient.AddParamWithValue("@amigo", id_amigo);
                int i_query_result = dbClient.ReadInt32("SELECT COUNT(*) FROM amigos WHERE (id_usuario = @usuario OR id_amigo = @usuario) AND (id_usuario = @amigo OR id_amigo = @amigo) AND aceptado = '1'");
                return (i_query_result > 0) ? true : false;
            }
        }

        public List<friends> peticiones(int id_usuario)
        {
            using (DatabaseClient dbClient = Environment.GetDatabase().GetClient())
            {
                dbClient.AddParamWithValue("@usuario", id_usuario);
                int i_query_result = dbClient.ReadInt32("SELECT COUNT(*) FROM amigos WHERE id_amigo = @usuario AND aceptado = '0' AND @usuario IN (SELECT id FROM usuarios);");
                List<friends> Result = new List<friends>();

                if (i_query_result > 0)
                {
                    DataTable dTable = dbClient.ReadDataSet("SELECT * FROM amigos WHERE id_amigo = @usuario AND aceptado = '0' AND @usuario IN (SELECT id FROM usuarios);").Tables[0];
                    foreach (DataRow dRow in dTable.Rows)
                    {
                        Result.Add(new friends(Convert.ToInt32(dRow["id_usuario"]), Convert.ToInt32(dRow["id_amigo"]), Convert.ToInt32(dRow["aceptado"])));
                    }
                }
                return Result;
            }
        }

        public void agregar_amigo(int id_usuario, int id_amigo)
        {
            using (DatabaseClient dbClient = Environment.GetDatabase().GetClient())
            {
                dbClient.AddParamWithValue("@usuario", id_usuario);
                dbClient.AddParamWithValue("@amigo", id_amigo);
                dbClient.ExecuteQuery("INSERT INTO amigos (id_usuario,id_amigo,aceptado) VALUES (@usuario,@amigo,'0')");
            }
        }

        public void aceptar_amigo(int id_usuario, int id_amigo)
        {
            using (DatabaseClient dbClient = Environment.GetDatabase().GetClient())
            {
                dbClient.AddParamWithValue("@usuario", id_usuario);
                dbClient.AddParamWithValue("@amigo", id_amigo);
                dbClient.ExecuteQuery("UPDATE amigos SET aceptado = '1' WHERE (id_usuario = @usuario OR id_amigo = @usuario) AND (id_usuario = @amigo OR id_amigo = @amigo);");
            }
        }

        public void borrar_amigo(int id_usuario, int id_amigo)
        {
            using (DatabaseClient dbClient = Environment.GetDatabase().GetClient())
            {
                dbClient.AddParamWithValue("@usuario", id_usuario);
                dbClient.AddParamWithValue("@amigo", id_amigo);
                dbClient.ExecuteQuery("DELETE FROM amigos WHERE (id_amigo = @usuario OR id_usuario = @amigo) AND (id_usuario = @amigo OR id_amigo = @amigo);");
            }
        }

        public friends amigo(int id_usuario, int id_amigo)
        {
            using (DatabaseClient dbClient = Environment.GetDatabase().GetClient())
            {
                dbClient.AddParamWithValue("@usuario", id_usuario);
                dbClient.AddParamWithValue("@amigo", id_amigo);
                int i_result_query = dbClient.ReadInt32("SELECT COUNT(*) FROM amigos WHERE (id_usuario = @usuario OR id_amigo = @amigo) AND (id_usuario = @amigo OR id_amigo = @usuario) AND aceptado =' 1' AND @usuario IN (SELECT id FROM usuarios);");

                if (i_result_query > 0)
                {
                    DataRow dRow = dbClient.ReadDataSet("SELECT * FROM amigos WHERE (id_usuario = @usuario OR id_amigo = @amigo) AND (id_usuario = @amigo OR id_amigo = @usuario) AND aceptado = '1' AND @usuario IN (SELECT id FROM usuarios);").Tables[0].Rows[0];
                    return (new friends(Convert.ToInt32(dRow["id_usuario"]), Convert.ToInt32(dRow["id_amigo"]), Convert.ToInt32(dRow["aceptado"])));
                }
                return null;
            }
        }

        public List<userInfo> buscar_usuario(string query)
        {
            using (DatabaseClient dbClient = Environment.GetDatabase().GetClient())
            {
                List<userInfo> user_result = new List<userInfo>();
                dbClient.AddParamWithValue("@query", query);
                DataTable dTable = dbClient.ReadDataTable("SELECT * FROM usuarios WHERE usuario = @query LIMIT 1");
                foreach (DataRow dRow in dTable.Rows)
                {
                    userInfo User = Environment.Game.User.getUser(int.Parse(dRow["id"].ToString()));
                    if (User != null)
                    {
                        user_result.Add(User);
                    }
                }
                return user_result;
            }
        }
    }
}
