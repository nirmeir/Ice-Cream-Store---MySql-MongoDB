using MySql.Data;
using MySql.Data.MySqlClient;

using BusinessEntities;
using BusinessLogic;
using System.Collections;

namespace MySqlAccess
{
    class MySqlAccess
    {

        static string connStr = "server=localhost;user=root;port=3306;password=";

        /*
        this call will represent CRUD operation
        CRUD stands for Create,Read,Update,Delete
        */
        public static void createTables()
        {

            try
            {
                MySqlConnection conn = new MySqlConnection(connStr);
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();


                string sql = "DROP DATABASE IF EXISTS ice_cream_store;";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                

                //sql = "CREATE SCHEMA `ice_cream_store`";
                sql = "CREATE DATABASE ice_cream_store;";
                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                // create Serving
                sql = "CREATE TABLE `ice_cream_store`.`Serving` (" +
                    "`c_id` INTEGER NOT NULL , " +
                    "`kind_serving` VARCHAR(20) NOT NULL," +
                    "`price` INTEGER NOT NULL);";

                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                // create Flavors
                sql = "CREATE TABLE `ice_cream_store`.`Flavors` (" +
                    "`c_id` INT NOT NULL , " +
                    "`flavor_name` VARCHAR(20) NOT NULL);";


                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                // create topping
                sql = "CREATE TABLE `ice_cream_store`.`Topping` (" +
                    "`c_id` INT NOT NULL , " +
                    "`topping_name` VARCHAR(45) NOT NULL);";

                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                sql = "CREATE TABLE `ice_cream_store`.`Sale` (" +
                    "`c_id` INT NOT NULL primary key, " +
                    "`name` VARCHAR(45) NOT NULL,"+
                    "`OrderDate` dateTime NOT NULL," +
                    "`completed` INT NOT NULL," +
                    "`total_price` INT NOT NULL );";

                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

               
                conn.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void insertObject(Object obj)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connStr);
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = null;

                if (obj is Serving)
                {
                    Serving serving = (Serving)obj;
                    sql = "INSERT INTO `ice_cream_store`.`Serving` (`c_id`, `kind_serving`, `price`) " +
                    "VALUES ('" + serving.get_id_serving() + "', '" + serving.get_kind_serving() + "', '" + serving.get_price() + "');";
                }

                if (obj is Flavors)
                {
                    Flavors flav = (Flavors)obj;
                    for (int i = 0; i < flav.get_flavor_name().Length; i++)
                    {
                    sql = "INSERT INTO `ice_cream_store`.`Flavors` (`c_id`, `flavor_name`) " +
                    "VALUES ('" + flav.get_flavor_id() + "', '" + flav.get_flavor_name()[i] + "' );";
                     MySqlCommand cmd1 = new MySqlCommand(sql, conn);
                     cmd1.ExecuteNonQuery();
                    }
                    conn.Close();
                    return;
                }

                if (obj is Topping)
                {
                    Topping topping = (Topping)obj;
                    for (int i = 0; i < topping.get_topping_name().Length; i++)
                    {
                    sql = "INSERT INTO `ice_cream_store`.`Topping` (`c_id`, `topping_name`) " +
                    "VALUES ('" + topping.get_topping_id() + "', '" + topping.get_topping_name()[i] + "');";
                    MySqlCommand cmd1 = new MySqlCommand(sql, conn);
                    cmd1.ExecuteNonQuery();
                    }
                    conn.Close();
                    return;
                }

               

                if (obj is Sale)
                {
                    Sale sale = (Sale)obj;
                    sql = "INSERT INTO `ice_cream_store`.`Sale` (`c_id`, `Name`, `OrderDate`, `Completed`, `total_price`) " +
                    "VALUES ('" + sale.getrid() + "', '" + sale.get_name()+ "', '" +
                     sale.getDateTime() + "', '" + sale.getCompleted() + "', '" + sale.getTotalPrice()+ "');";
                }

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static ArrayList readAll(string tableName)
        {
            ArrayList all = new ArrayList();

            try
            {
                MySqlConnection conn = new MySqlConnection(connStr);
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = "SELECT * FROM `Garage`."+tableName;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    //Console.WriteLine(rdr[0] + " -- " + rdr[1]);
                    Object[] numb = new Object[rdr.FieldCount];
                    rdr.GetValues(numb);
                    //Array.ForEach(numb, Console.WriteLine);
                    all.Add(numb);
                }
                rdr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return all;
        }
    }

}