using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data.Common;

namespace EXAMEN
{
    public class DataBasePacientes
    {
        //Datos de configuración para acceder a la db
        static string servidor = "localhost";
        static string dbName = "registro";
        static string usuario = "postgres";
        static string password = "Root";
        static string puerto = "5432";

        NpgsqlConnection conexion = new NpgsqlConnection();

        public NpgsqlConnection crearDb()
        {

            // 1. Cadena de conexión para crear la db
            string cadenaDeConexionDb = "server= localhost; port= 5432; user id= postgres; password= Root;";

            //2. Query para crear la db
            const string QUERY_CREATE_DB = "CREATE DATABASE registro " + "WITH OWNER = postgres " + "ENCODING = 'UTF8' " + "CONNECTION LIMIT = -1;";

            //3. Creamos una instancia de la clase  NpgsqlConnection  para establecer la conexión
            NpgsqlConnection dbConexion = new NpgsqlConnection(cadenaDeConexionDb); // Recibe cadena de conexión al servidor

            //4.Creamos una instancia de NpgsqlCommand para crear la db
            NpgsqlCommand npgsqlCommandQuery = new NpgsqlCommand(QUERY_CREATE_DB, dbConexion);

            //5. Abrimos la conexión para crear la DB
            try
            {
                //Verificamos el estado de la conexión
                if (dbConexion.State == System.Data.ConnectionState.Closed)
                {
                    dbConexion.Open();
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            //6. Creamos la DB
            try
            {
                //Verificamos si la conexión se ha realizado
                if (dbConexion.State == System.Data.ConnectionState.Open)
                {
                    npgsqlCommandQuery.ExecuteNonQuery();
                    Console.WriteLine("Se ha creado la base de datos.......");
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            // 7. Cerramos la conexión
            desconectar(dbConexion);
            return dbConexion;
        }

        public void CrearTablaDb()
        {
            // 1. Cadena de conexión para crear la tabla
            string cadenaDeConexionTabla = "server=" + servidor + ";" + "port=" + puerto + ";" + "user id=" + usuario + ";" + "password=" + password + ";" + "database=" + dbName + ";";

            //2. Creamos una instancia de la clase  NpgsqlConnection  para establecer la conexión
            NpgsqlConnection dbConectionTabla = new NpgsqlConnection(cadenaDeConexionTabla); // Recibe cadena de conexión al la db para la tabla

            //3. Query para crear la tabla
            const string QUERY_CREATE_TABLA_DB = "CREATE TABLE public.registro_paciente (paciente_id SERIAL," +
                "nombre_paciente CHARACTER VARYING(30) NOT NULL," +
                "apellido_paciente CHARACTER VARYING(30) NOT NULL," +
                "edad_paciente BIGINT NOT NULL," +
                "motivo_consulta CHARACTER VARYING(500) NOT NULL," +
                "CONSTRAINT pk_game_id PRIMARY KEY(paciente_id)" +
                "); ";
            //4. Creamos una instancia de NpgsqlCommand para establecer la conexión
            NpgsqlCommand commandQueryTabla = new NpgsqlCommand(QUERY_CREATE_TABLA_DB, dbConectionTabla);

            // 5. Abrimos la conexión para crear la tabla 
            try
            {
                //Verificamos el estado de la conexión
                if (dbConectionTabla.State == System.Data.ConnectionState.Closed)
                {
                    dbConectionTabla.Open();
                    Console.WriteLine("La conexion esta abierta....");
                }
                else
                {
                    Console.WriteLine("La conexion esta abierta....");
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            //6. Creamos la tabla en la DB
            try
            {
                //Verificamos el estado de la conexión
                if (dbConectionTabla.State == System.Data.ConnectionState.Open)
                {
                    commandQueryTabla.ExecuteNonQuery();
                    Console.WriteLine("Tabla creada........");
                }

            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            //7. Cerramos conexión
            desconectar(dbConectionTabla);
        }


        //Insertar elementos a la Db
        public void insertar(ModeloPacientes mPacientes)
        {

            string cadenaDeConexionTabla = "server=" + servidor + ";" + "port=" + puerto + ";" + "user id=" + usuario + ";" + "password=" + password + ";" + "database=" + dbName + ";";

            NpgsqlConnection connectionInsertarElement = new NpgsqlConnection(cadenaDeConexionTabla);


            //3. Creamos una instancia de la clase ModeloGames
            //
            //4. Creamos query para insertar elementos en la db
            string QUERY_INSERT_ELEMENTOS = String.Format("INSERT INTO registro_paciente (nombre_paciente, apellido_paciente, edad_paciente, motivo_consulta) VALUES ('{0}', '{1}', {2}, '{3}');", mPacientes.PacienteNombre, mPacientes.PacienteApellido, mPacientes.PacienteEdad, mPacientes.PacienteMotivoDeConsulta);

            //5.Creamos uns instancia de NpgsqlWrite
            NpgsqlCommand commandInsert = new NpgsqlCommand(QUERY_INSERT_ELEMENTOS, connectionInsertarElement);


            //6. Abrir la conexión a la db
            try
            {
                if (connectionInsertarElement.State == System.Data.ConnectionState.Closed)
                {
                    connectionInsertarElement.Open();
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            //7. Insertamos elementos
            try
            {
                if (connectionInsertarElement.State == System.Data.ConnectionState.Open)
                {
                    commandInsert.ExecuteNonQuery();
                    Console.WriteLine("Se ha guardo tu información");
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            desconectar(connectionInsertarElement);

        }

        public List<ModeloPacientes>ConsultaDos(int dato)
        {

            List<ModeloPacientes> moldeloPaciente = new List<ModeloPacientes>();
            ModeloPacientes modelo = new ModeloPacientes();
            //List<ModeloGames> list = new List<ModeloGames>();

            //Configuración para conectarse a la db
            // 1. Cadena de conexión para crear la tabla
            string cadenaDeConexion = "server=" + servidor + ";" + "port=" + puerto + ";" + "user id=" + usuario + ";" + "password=" + password + ";" + "database=" + dbName + ";";

            //3. Instancia de NpgsqlConnection
            NpgsqlConnection connectionDos = new NpgsqlConnection(cadenaDeConexion);
            //4. consulta
            string QUERY_CONSULTA = string.Format("SELECT paciente_id,nombre_paciente,apellido_paciente,edad_paciente FROM registro_paciente WHERE paciente_id = {0};", dato);
            //5. Instancia de NpgsqlCommand
            NpgsqlCommand commandConsultaDos = new NpgsqlCommand(QUERY_CONSULTA, connectionDos);

            //6 Abrimos la conexion

            try
            {
                if (connectionDos.State == System.Data.ConnectionState.Closed)
                {
                    connectionDos.Open();
                    Console.WriteLine("Se ha realizado la conexion exitosa.........");
                }
                else
                {
                    Console.WriteLine("Ya hay una conexion establecida.........");
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            //7. ejecutamos query
            try
            {
                if (connectionDos.State == System.Data.ConnectionState.Open)
                {
                    NpgsqlDataReader dataReader2 = commandConsultaDos.ExecuteReader();
                    while (dataReader2.Read())
                    {
                        Console.WriteLine("Almacenando datos......");
                        modelo.PacienteId = dataReader2.GetInt32(0);
                        modelo.PacienteNombre = dataReader2.GetString(1);
                        modelo.PacienteApellido = dataReader2.GetString(2);
                        modelo.PacienteEdad = dataReader2.GetInt32(3);
                        modelo.PacienteMotivoDeConsulta = dataReader2.GetString(4);

                        moldeloPaciente.Add(modelo);
                    }
                }

            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            desconectar(connectionDos);
            return moldeloPaciente;
        }

        public void eliminar(int game_id)
        {

            ModeloPacientes moldeloPaciente = new ModeloPacientes();
            //List<ModeloGames> list = new List<ModeloGames>();

            //Configuración para conectarse a la db
            // 1. Cadena de conexión para crear la tabla
            string cadenaDeConexion = "server=" + servidor + ";" + "port=" + puerto + ";" + "user id=" + usuario + ";" + "password=" + password + ";" + "database=" + dbName + ";";

            //3. Instancia de NpgsqlConnection
            NpgsqlConnection connectionDos = new NpgsqlConnection(cadenaDeConexion);
            //4. consulta
            string QUERY_CONSULTA = string.Format("DELETE FROM videogame WHERE game_id = {0};", game_id);
            //5. Instancia de NpgsqlCommand
            NpgsqlCommand commandDelete = new NpgsqlCommand(QUERY_CONSULTA, connectionDos);

            //6 Abrimos la conexion

            try
            {
                if (connectionDos.State == System.Data.ConnectionState.Closed)
                {
                    connectionDos.Open();
                    Console.WriteLine("Se ha realizado la conexion exitosa.........");
                }
                else
                {
                    Console.WriteLine("Ya hay una conexion establecida.........");
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            //7. ejecutamos query
            try
            {
                if (connectionDos.State == System.Data.ConnectionState.Open)
                {
                    commandDelete.ExecuteNonQuery();
                    Console.WriteLine("Registro eliminado.....");
                }

            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            desconectar(connectionDos);

        }




        //Método para desconectar db
        public void desconectar(NpgsqlConnection connection)
        {

            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    Console.WriteLine("Se ha cerrado la conexion a la DB");
                }
                else
                {
                    Console.WriteLine("La conexion ya ha sido cerrada");
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine("Error al cerrar la conexion {0}", ex);
            }

        }
    }
}
