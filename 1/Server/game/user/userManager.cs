using System;
using System.Data;

using Boombang.database;

namespace Boombang.game.user
{
    public partial class userManager
    {
        public bool checkUsername(string usuario)
        {
            using (DatabaseClient dbClient = Environment.GetDatabase().GetClient())
            {
                Console.WriteLine("[DEBUG] -- Nombre de usuario: " + usuario);
                dbClient.AddParamWithValue("@usuario", usuario);
                int result = dbClient.ReadInt32("SELECT COUNT(*) FROM usuarios WHERE usuario = @usuario");
                return (result > 0) ? true : false;
            }
        }

        public bool login(string usuario, string password)
        {
            using (DatabaseClient dbClient = Environment.GetDatabase().GetClient())
            {
                dbClient.AddParamWithValue("@usuario", usuario);
                dbClient.AddParamWithValue("@password", password);
                int result = dbClient.ReadInt32("SELECT COUNT(*) FROM usuarios WHERE usuario = @usuario AND password = @password");
                return (result > 0) ? true : false;
            }
        }

        public void register(string usuario, string password, int tipo_avatar, string colores_avatar, int edad, string email, string ip)
        {
            using (DatabaseClient dbClient = Environment.GetDatabase().GetClient())
            {
                dbClient.AddParamWithValue("@usuario", usuario);
                dbClient.AddParamWithValue("@password", password);
                dbClient.AddParamWithValue("@tipo_avatar", tipo_avatar);
                dbClient.AddParamWithValue("@colores_avatar", colores_avatar);
                dbClient.AddParamWithValue("@edad", edad);
                dbClient.AddParamWithValue("@email", email);
                dbClient.AddParamWithValue("@ip", ip);
                dbClient.ExecuteQuery("INSERT INTO usuarios (`usuario`, `password`, `email`, `edad`, `ip_registro`, `ip_actual`, `tipo_avatar`, `colores_avatar`) VALUES (@usuario, @password, @email, @edad, @ip, @ip, @tipo_avatar, @colores_avatar)");
            }
        }

        public int getUserID(string usuario)
        {
            using (DatabaseClient dbClient = Environment.GetDatabase().GetClient())
            {
                dbClient.AddParamWithValue("@usuario", usuario);
                int userID = dbClient.ReadInt32("SELECT id FROM usuarios WHERE usuario = @usuario;");
                return userID;
            }
        }

        public userInfo getUserByName(string usuario)
        {
            return getUser(getUserID(usuario));
        }

        public userInfo getUser(int userID)
        {
            using (DatabaseClient dbClient = Environment.GetDatabase().GetClient())
            {
                dbClient.AddParamWithValue("@userid", userID);
                DataRow userRow = dbClient.ReadDataRow("SELECT * FROM usuarios WHERE id = @userid;");

                userInfo userData = new userInfo();
                userData.id = userID;
                userData.usuario = userRow["usuario"].ToString();
                userData.password = userRow["usuario"].ToString();
                userData.email = userRow["email"].ToString();
                userData.pais = userRow["pais"].ToString();
                userData.ciudad = userRow["ciudad"].ToString();
                userData.ip_actual = userRow["ip_actual"].ToString();
                userData.ultimo_login = userRow["ultimo_login"].ToString();
                userData.colores_avatar = userRow["colores_avatar"].ToString();
                userData.bocadillo = userRow["bocadillo"].ToString();
                userData.hobby_1 = userRow["hobby_1"].ToString();
                userData.hobby_2 = userRow["hobby_2"].ToString();
                userData.hobby_3 = userRow["hobby_3"].ToString();
                userData.deseo_1 = userRow["deseo_1"].ToString();
                userData.deseo_2 = userRow["deseo_2"].ToString();
                userData.deseo_3 = userRow["deseo_3"].ToString();

                userData.es_moderador = (Convert.ToInt32(userRow["moderador"].ToString()) == 1) ? true : false;

                userData.edad = Convert.ToInt32(userRow["edad"].ToString());
                userData.tiempo_registrado = Convert.ToInt32(userRow["tiempo_registrado"].ToString());
                userData.tipo_avatar = Convert.ToInt32(userRow["tipo_avatar"].ToString());
                userData.creditos_plata = Convert.ToInt32(userRow["creditos_plata"].ToString());
                userData.creditos_oro = Convert.ToInt32(userRow["creditos_oro"].ToString());
                userData.besos_enviados = Convert.ToInt32(userRow["besos_enviados"].ToString());
                userData.cocteles_enviados = Convert.ToInt32(userRow["cocteles_enviados"].ToString());
                userData.flores_enviadas = Convert.ToInt32(userRow["flores_enviadas"].ToString());
                userData.uppercuts_enviados = Convert.ToInt32(userRow["uppercuts_enviados"].ToString());
                userData.cocos_enviados = Convert.ToInt32(userRow["cocos_enviados"].ToString());
                userData.besos_recibidos = Convert.ToInt32(userRow["besos_recibidos"].ToString());
                userData.cocteles_recibidos = Convert.ToInt32(userRow["cocteles_recibidos"].ToString());
                userData.flores_recibidas = Convert.ToInt32(userRow["flores_recibidas"].ToString());
                userData.uppercuts_recibidos = Convert.ToInt32(userRow["uppercuts_recibidos"].ToString());
                userData.cocos_recibidos = Convert.ToInt32(userRow["cocos_recibidos"].ToString());
                userData.votos_legal = Convert.ToInt32(userRow["votos_legal"].ToString());
                userData.votos_sexy = Convert.ToInt32(userRow["votos_sexy"].ToString());
                userData.votos_simpatico = Convert.ToInt32(userRow["votos_simpatico"].ToString());
				userData.ring = Convert.ToInt32(userRow["ring"].ToString());

                return userData;
            }
        }

        public void update_look(userInfo User)
        {
            using (DatabaseClient dbClient = Environment.GetDatabase().GetClient())
            {
                dbClient.AddParamWithValue("@id", User.id);
                dbClient.AddParamWithValue("@tipo_avatar", User.tipo_avatar);
                dbClient.AddParamWithValue("@colores_avatar", User.colores_avatar);

                dbClient.ExecuteQuery("UPDATE usuarios SET tipo_avatar = @tipo_avatar, colores_avatar = @colores_avatar WHERE id = @id;");
            }
        }

        public void update_criticaldata(userInfo User)
        {
            using (DatabaseClient dbClient = Environment.GetDatabase().GetClient())
            {
                dbClient.AddParamWithValue("@id", User.id);
                dbClient.AddParamWithValue("@ip_actual", User.ip_actual);
                dbClient.AddParamWithValue("@ultimo_login", User.ultimo_login);

                dbClient.ExecuteQuery("UPDATE usuarios SET ip_actual = @ip_actual, ultimo_login = @ultimo_login WHERE id = @id;");
            }
        }

        public void update_hobbies(userInfo User)
        {
            using (DatabaseClient dbClient = Environment.GetDatabase().GetClient())
            {
                dbClient.AddParamWithValue("@id", User.id);
                dbClient.AddParamWithValue("@hobby_1", User.hobby_1);
                dbClient.AddParamWithValue("@hobby_2", User.hobby_2);
                dbClient.AddParamWithValue("@hobby_3", User.hobby_3);

                dbClient.ExecuteQuery("UPDATE usuarios SET hobby_1 = @hobby_1, hobby_2 = @hobby_2, hobby_3 = hobby_3 WHERE id = @id;");
            }
        }

        public void update_wishes(userInfo User)
        {
            using (DatabaseClient dbClient = Environment.GetDatabase().GetClient())
            {
                dbClient.AddParamWithValue("@id", User.id);
                dbClient.AddParamWithValue("@deseo_1", User.deseo_1);
                dbClient.AddParamWithValue("@deseo_2", User.deseo_2);
                dbClient.AddParamWithValue("@deseo_3", User.deseo_3);

                dbClient.ExecuteQuery("UPDATE usuarios SET deseo_1 = @deseo_1, deseo_2 = @deseo_2, deseo_3 = deseo_3 WHERE id = @id;");
            }
        }

        public void update_votes(userInfo User)
        {
            using (DatabaseClient dbClient = Environment.GetDatabase().GetClient())
            {
                dbClient.AddParamWithValue("@id", User.id);
                dbClient.AddParamWithValue("@legal", User.votos_legal);
                dbClient.AddParamWithValue("@sexy", User.votos_sexy);
                dbClient.AddParamWithValue("@simpatico", User.votos_simpatico);

                dbClient.ExecuteQuery("UPDATE usuarios SET votos_simpatico = @simpatico, votos_sexy = @sexy, votos_legal = @legal WHERE id = @id;");
            }
        }

        public void update_bocadillo(userInfo User)
        {
            using (DatabaseClient dbClient = Environment.GetDatabase().GetClient())
            {
                dbClient.AddParamWithValue("@id", User.id);
                dbClient.AddParamWithValue("@bocadillo", User.bocadillo);

                dbClient.ExecuteQuery("UPDATE usuarios SET bocadillo = @bocadillo WHERE id = @id;");
            }
        }

        public void update_statistics(userInfo User)
        {
            using (DatabaseClient dbClient = Environment.GetDatabase().GetClient())
            {
                dbClient.AddParamWithValue("@id", User.id);
                dbClient.AddParamWithValue("@besos_enviados", User.besos_enviados);
                dbClient.AddParamWithValue("@cocteles_enviados", User.cocteles_enviados);
                dbClient.AddParamWithValue("@flores_enviadas", User.flores_enviadas);
                dbClient.AddParamWithValue("@uppercuts_enviados", User.uppercuts_enviados);
                dbClient.AddParamWithValue("@cocos_enviados", User.cocos_enviados);
                dbClient.AddParamWithValue("@besos_recibidos", User.besos_recibidos);
                dbClient.AddParamWithValue("@cocteles_recibidos", User.cocteles_recibidos);
                dbClient.AddParamWithValue("@flores_recibidas", User.flores_recibidas);
                dbClient.AddParamWithValue("@uppercuts_recibidos", User.uppercuts_recibidos);
                dbClient.AddParamWithValue("@cocos_recibidos", User.cocos_recibidos);
				dbClient.AddParamWithValue("@ring", User.ring);

                dbClient.ExecuteQuery("UPDATE usuarios SET besos_enviados = @besos_enviados, cocteles_enviados = @cocteles_enviados, flores_enviadas = @flores_enviadas, uppercuts_enviados = @uppercuts_enviados, cocos_enviados = @cocos_enviados, besos_recibidos = @besos_recibidos, cocteles_recibidos = @cocteles_recibidos, flores_recibidas = @flores_recibidas, uppercuts_recibidos = @uppercuts_recibidos, cocos_recibidos = @cocos_recibidos , ring = @ring WHERE id = @id;");
            }
        }

        public void update_credits(userInfo User)
        {
            using (DatabaseClient dbClient = Environment.GetDatabase().GetClient())
            {
                dbClient.AddParamWithValue("@id", User.id);
                dbClient.AddParamWithValue("@creditos", User.creditos_oro);

                dbClient.ExecuteQuery("UPDATE usuario SET creditos_oro = @creditos WHERE id = @id;");

            }
        }
    }
}
