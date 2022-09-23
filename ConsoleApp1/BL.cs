using System.Collections;
using System.Globalization;
using BusinessEntities;
using MySql.Data.MySqlClient;

namespace BusinessLogic
{


    public class RandomDateTime
    {
        DateTime start;
        Random gen;
        int range;

        public RandomDateTime()
        {
            start = new DateTime(1995, 1, 1);
            gen = new Random();
            range = (DateTime.Today - start).Days;
        }

        public DateTime Next()
        {
            return start.AddDays(gen.Next(range)).AddHours(gen.Next(0, 24)).AddMinutes(gen.Next(0, 60)).AddSeconds(gen.Next(0, 60));
        }
    }

    public class Logic
    {

        static string[] Serving = { "regular_cone", "special_cone", "box" };

        static string[] Flavors = { "strawberry", "banana", "palm", "melon", "pistachio", "caramel", "mango", "vanile", "chocolate", "mekopelet" };

        static string[] Topping_for_chocolate_mekupelet = { "Pinets", "Meypel" };

        static string[] Topping_for_vanile = { "Pinets", "Hot chocolate" };

        static string[] Topping = { "Hot chocolate", "Pinets", "Meypel" };

        static string[] Topping_both = { "Pinets" };

        static string[] Confirm_order = { "yes", "no" };

        static string[] Want_topping = { "yes", "no" };

        static string[] How_much_topping_you_want = { "1", "2", "3" };








        static string[] Names = { "Nir", "Alon", "Omer", "Or", "Tal", "Gal", "Moshe", "Shir" };

        public static void createTables()
        {
            MySqlAccess.MySqlAccess.createTables();

        }

        public static void array_ToString(string[] str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                Console.WriteLine(str[i]);
            }
        }



        //id for serve table
        static int c_id = 0;




        public static Serving choose_serving()
        {
            Console.WriteLine("please choose kind of serve:");
            array_ToString(Serving);
            string s = Console.ReadLine();


            Serving serv;

            if (s == "regular_cone")
            {
                serv = new Serving(Logic.c_id, s, 0);
                Logic.c_id += 1;
                MySqlAccess.MySqlAccess.insertObject(serv);
                return serv;


            }
            else if (s == "special_cone")
            {
                serv = new Serving(Logic.c_id, s, 2);
                Logic.c_id += 1;
                MySqlAccess.MySqlAccess.insertObject(serv);
                return serv;

            }
            else if (s == "box")
            {
                serv = new Serving(Logic.c_id, s, 5);
                Logic.c_id += 1;
                MySqlAccess.MySqlAccess.insertObject(serv);
                return serv;
            }

            // else {
            //     Console.Write("Your chooise is wrong, please try again");
            //                 return;

            // }   

            return null;
        }



        public static int choose_topping(int r_id, string[] flavor, int count, string kind_of_serve, int current_price, string name)
        {


            // if(kind_of_serve=="regular_cone" && count<2) {

            // Console.Write("We sorry but getting topping for regular cone is from 2 scoops and more");
            // return current_price;
            //}

            if ((flavor.Contains("chocolate") || flavor.Contains("mekopelet")) && flavor.Contains("vanile"))
            {

                Console.Write("How much topping you want ? (for example type 1 if you want one topping)");
                int s = int.Parse(Console.ReadLine());

                int price = current_price + (s * 2);
                string[] choosing_topping = new string[s];

                Topping topping = new Topping(r_id, choosing_topping);

                for (int i = 0; i < s; i++)
                {
                    Console.Write("please choose tooping: ");
                    array_ToString(Topping_both);

                    string top = Console.ReadLine();
                    choosing_topping[i] = top;
                    Console.WriteLine("\n");
                }

                MySqlAccess.MySqlAccess.insertObject(topping);
                Sale(r_id, price, name);


                return price;

            }




            else if (flavor.Contains("chocolate") || flavor.Contains("mekopelet"))
            {

                Console.Write("How much topping you want ? (for example type 1 if you want one topping)");
                int s = int.Parse(Console.ReadLine());

                int price = current_price + (s * 2);
                string[] choosing_topping = new string[s];

                Topping topping = new Topping(r_id, choosing_topping);
                for (int i = 0; i < s; i++)
                {
                    Console.Write("please choose tooping: ");
                    array_ToString(Topping_for_chocolate_mekupelet);
                    string top = Console.ReadLine();
                    choosing_topping[i] = top;
                    Console.WriteLine("\n");

                }
                MySqlAccess.MySqlAccess.insertObject(topping);
                Sale(r_id, price, name);

                return price;
            }
            else if (flavor.Contains("vanile"))
            {

                Console.Write("How much topping you want ? (for example type 1 if you want one topping)");
                int s = int.Parse(Console.ReadLine());
                string[] choosing_topping = new string[s];
                int price = current_price + (s * 2);
                Topping topping = new Topping(r_id, choosing_topping);
                for (int i = 0; i < s; i++)
                {
                    Console.Write("please choose tooping: ");
                    array_ToString(Topping_for_vanile);
                    string top = Console.ReadLine();
                    choosing_topping[i] = top;
                    Console.WriteLine("\n");
                }
                Sale(r_id, price, name);
                MySqlAccess.MySqlAccess.insertObject(topping);
                return price;
            }

            else
            {
                Console.Write("How much topping you want ? (for example type 1 if you want one topping)");
                int s = int.Parse(Console.ReadLine());
                string[] choosing_topping = new string[s];
                int price = current_price + (s * 2);

                Topping topping = new Topping(r_id, choosing_topping);
                for (int i = 0; i < s; i++)
                {
                    Console.Write("please choose tooping: ");
                    array_ToString(Topping);
                    string top = Console.ReadLine();
                    choosing_topping[i] = top;
                    Console.WriteLine("\n");
                }
                Sale(r_id, price, name);
                MySqlAccess.MySqlAccess.insertObject(topping);
                return price;

            }


            return current_price;
        }

        public static void choose_flavor()
        {
            MongoOrder tmpMongo;

            // List<MongoOrder> data = new List<MongoOrder>(num);
            Console.WriteLine("Hello, what your name please? ");
            string name = Console.ReadLine();

            Serving serv = choose_serving();
            int id = serv.get_id_serving();
            string kind_serving = serv.get_kind_serving();

            if (serv != null)
            {


                if (kind_serving == "regular_cone" || kind_serving == "special_cone")
                {
                    Console.WriteLine("Please choose 1 , 2 , 3 scoops");
                    int s = int.Parse(Console.ReadLine());

                    string[] flav_chooice = new string[s];

                    if (s == 1 && kind_serving == "regular_cone")
                    {
                        serv.set_price(7);
                        for (int i = 0; i < s; i++)
                        {
                            Console.WriteLine("Please choose the flavor you want: ");
                            array_ToString(Flavors);
                            string flav1 = Console.ReadLine();
                            flav_chooice[i] = flav1;
                            Console.WriteLine("\n");

                        }

                        Flavors flav = new Flavors(id, flav_chooice);
                        MySqlAccess.MySqlAccess.insertObject(flav);
                        Sale(c_id, serv.get_price(), name);
                        return;
                    }


                    if (s == 1)
                    {
                        serv.set_price(7);

                        for (int i = 0; i < s; i++)
                        {
                            Console.WriteLine("Please choose the flavor you want: ");
                            array_ToString(Flavors);
                            string flav1 = Console.ReadLine();
                            flav_chooice[i] = flav1;
                            Console.WriteLine("\n");

                        }

                        Flavors flav = new Flavors(id, flav_chooice);
                        MySqlAccess.MySqlAccess.insertObject(flav);

                        Console.Write("You want topping ? (answer with yes or no) ");
                        string ans = Console.ReadLine();
                        if (ans == "yes")
                        {
                            choose_topping(id, flav.get_flavor_name(), s, kind_serving, serv.get_price(), name);
                        }
                        else
                        {

                            Sale(id, serv.get_price(), name);
                            return;
                        }
                    }
                    else if (s == 2)
                    {
                        serv.set_price(12);
                        for (int i = 0; i < s; i++)
                        {
                            Console.WriteLine("Please choose the flavor you want: ");
                            array_ToString(Flavors);
                            string flav1 = Console.ReadLine();
                            flav_chooice[i] = flav1;
                            Console.WriteLine("\n");
                        }

                        Flavors flav = new Flavors(id, flav_chooice);
                        MySqlAccess.MySqlAccess.insertObject(flav);
                        Console.Write("You want topping ? (answer with yes or no) ");
                        string ans = Console.ReadLine();
                        if (ans == "yes")
                        {
                            choose_topping(id, flav.get_flavor_name(), s, kind_serving, serv.get_price(), name);
                        }
                        else
                        {
                            Sale(id, serv.get_price(), name);
                            return;
                        }
                    }


                    else if (s == 3)
                    {
                        serv.set_price(18);
                        for (int i = 0; i < s; i++)
                        {
                            Console.WriteLine("Please choose the flavor you want: ");
                            array_ToString(Flavors);
                            string flav1 = Console.ReadLine();
                            flav_chooice[i] = flav1;
                            Console.WriteLine("\n");
                        }

                        Flavors flav = new Flavors(id, flav_chooice);
                        MySqlAccess.MySqlAccess.insertObject(flav);
                        Console.Write("You want topping ? (answer with yes or no) ");
                        string ans = Console.ReadLine();
                        if (ans == "yes")
                        {
                            choose_topping(id, flav.get_flavor_name(), s, kind_serving, serv.get_price(), name);
                        }
                        else
                        {
                            Sale(id, serv.get_price(), name);
                            return;
                        }
                    }
                }
                else if (kind_serving == "box")
                {
                    Console.WriteLine("Please choose how much scoops you want ? ");
                    int s = int.Parse(Console.ReadLine());
                    string[] flav_chooice = new string[s];

                    if (s == 1)
                    {
                        serv.set_price(7);
                        for (int i = 0; i < s; i++)
                        {
                            Console.WriteLine("Please choose the flavor you want: ");
                            array_ToString(Flavors);
                            string flav1 = Console.ReadLine();
                            flav_chooice[i] += flav1;
                            Console.WriteLine("\n");

                        }
                        Flavors flav = new Flavors(id, flav_chooice);
                        MySqlAccess.MySqlAccess.insertObject(flav);
                        Console.Write("You want topping ? (answer with yes or no) ");
                        string ans = Console.ReadLine();
                        if (ans == "yes")
                        {
                            choose_topping(id, flav.get_flavor_name(), s, kind_serving, serv.get_price(), name);
                        }
                        else
                        {
                            Sale(id, serv.get_price(), name);
                            return;
                        }
                    }
                    else if (s == 2)
                    {
                        serv.set_price(12);
                        for (int i = 0; i < s; i++)
                        {
                            Console.WriteLine("Please choose the flavor you want: ");
                            array_ToString(Flavors);
                            string flav1 = Console.ReadLine();
                            flav_chooice[i] = flav1;
                            Console.WriteLine("\n");
                        }

                        Flavors flav = new Flavors(id, flav_chooice);
                        MySqlAccess.MySqlAccess.insertObject(flav);
                        Console.Write("You want topping ? (answer with yes or no) ");
                        string ans = Console.ReadLine();
                        if (ans == "yes")
                        {
                            choose_topping(id, flav.get_flavor_name(), s, kind_serving, serv.get_price(), name);
                        }
                        else
                        {
                            Sale(id, serv.get_price(), name);
                            return;
                        }
                    }
                    else if (s == 3)
                    {
                        serv.set_price(18);
                        for (int i = 0; i < s; i++)
                        {
                            Console.WriteLine("Please choose the flavor you want: ");
                            array_ToString(Flavors);
                            string flav1 = Console.ReadLine();
                            flav_chooice[i] = flav1;
                            Console.WriteLine("\n");
                        }

                        Flavors flav = new Flavors(id, flav_chooice);
                        MySqlAccess.MySqlAccess.insertObject(flav);
                        Console.Write("You want topping ? (answer with yes or no) ");
                        string ans = Console.ReadLine();
                        if (ans == "yes")
                        {
                            choose_topping(id, flav.get_flavor_name(), s, kind_serving, serv.get_price(), name);
                        }
                        else
                        {
                            Sale(id, serv.get_price(), name);
                            return;
                        }
                    }
                    else if (s > 3)
                    {
                        int price = 18 + (s - 3) * 6;
                        serv.set_price(price);
                        for (int i = 0; i < s; i++)
                        {
                            Console.WriteLine("Please choose the flavor you want: ");
                            array_ToString(Flavors);
                            string flav1 = Console.ReadLine();
                            flav_chooice[i] += flav1;
                            Console.WriteLine("\n");
                        }
                        Flavors flav = new Flavors(id, flav_chooice);
                        MySqlAccess.MySqlAccess.insertObject(flav);
                        Console.Write("You want topping ? (answer with yes or no) ");
                        string ans = Console.ReadLine();
                        if (ans == "yes")
                        {
                            choose_topping(id, flav.get_flavor_name(), s, kind_serving, serv.get_price(), name);
                        }
                        else
                        {
                            Sale(id, serv.get_price(), name);
                            return;
                        }

                    }

                }

            }
        }



        public static void Sale(int r_id, int price, string name)
        {

            int complete = 1;
            Console.WriteLine("Hello" + name);
            Console.WriteLine("your total price is:" + price);
            Console.WriteLine("your r_id price is:" + r_id);
            Console.WriteLine("Are you confirm the order ? (answer with yes or no) ");
            string ans = Console.ReadLine();

            if (ans == "yes")
            {
                complete = 1;
                Console.WriteLine("Your order is completed");
                Console.WriteLine("Thank you for choosing us");
                Console.WriteLine("Have a nice day");
                Sale sale = new Sale(name, r_id, DateTime.Now, complete, price);
                sale.set_completed(complete);
                Console.WriteLine(sale.getCompleted());
                MySqlAccess.MySqlAccess.insertObject(sale);



            }
            else if (ans == "no")
            {
                complete = 0;
                Console.WriteLine("Your order is canceled");
                Console.WriteLine("Thank you for choosing us");
                Console.WriteLine("Have a nice day");
                Sale sale = new Sale(name, r_id, DateTime.Now, complete, price);
                sale.set_completed(complete);
                MySqlAccess.MySqlAccess.insertObject(sale);
            }

        }



        public static void get_day_report()
        {
            Console.WriteLine("Please enter the date you want to get the report:(yyyy/mm/dd)");
            string date = Console.ReadLine();

            string connStr = "server=localhost;user=root;port=3306;password=";
            MySqlConnection conn = new MySqlConnection(connStr);
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();

            string sql = "SELECT SUM(total_price) FROM ice_cream_store.sale  WHERE DATE(OrderDate)= '" + date + "'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine("The total price of the sales on " + date + " is: " + rdr[0]);
            }
            rdr.Close();
            sql = "SELECT COUNT(total_price) FROM ice_cream_store.sale  WHERE DATE(OrderDate)= '" + date + "'";

            cmd = new MySqlCommand(sql, conn);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine("The number of sales on " + date + " is: " + rdr[0]);

            }
            rdr.Close();
            sql = "SELECT AVG(total_price) FROM ice_cream_store.sale  WHERE DATE(OrderDate)= '" + date + "'"; ;
            cmd = new MySqlCommand(sql, conn);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine("The average price of the sales on " + date + " is: " + rdr[0]);
            }
            rdr.Close();

            conn.Close();

        }

        public static void get_reciept()
        {
            Console.WriteLine("Please enter your customer id: ");
            int c_id = int.Parse(Console.ReadLine());
            string connStr = "server=localhost;user=root;port=3306;password=";
            MySqlConnection conn = new MySqlConnection(connStr);
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();

            string sql = "SELECT * FROM ice_cream_store.sale WHERE sale.c_id= '" + c_id + "'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine("Coustomer id: " + rdr[0]);
                Console.WriteLine("Name: " + rdr[1]);
                Console.WriteLine("Date: " + rdr[2]);
                Console.WriteLine("Total Price: " + rdr[4]);
                Console.WriteLine("\n");
            }
            rdr.Close();

            sql = "SELECT serving.kind_serving FROM ice_cream_store.serving WHERE serving.c_id= '" + c_id + "'";
            cmd = new MySqlCommand(sql, conn);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine("You take: " + rdr[0]);
                Console.WriteLine("\n");
            }
            rdr.Close();

            sql = "SELECT flavors.flavor_name FROM ice_cream_store.flavors WHERE flavors.c_id= '" + c_id + "'";
            cmd = new MySqlCommand(sql, conn);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine("The Flavor you chooise is: " + rdr[0]);
                Console.WriteLine("\n");
            }
            rdr.Close();

            sql = "SELECT topping.topping_name FROM ice_cream_store.topping WHERE topping.c_id = '" + c_id + "'";
            cmd = new MySqlCommand(sql, conn);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine("The Topping you chooise is: " + rdr[0]);
                Console.WriteLine("\n");
            }
            rdr.Close();
            conn.Close();


        }

        public static void get_uncomplete_order()
        {
            string connStr = "server=localhost;user=root;port=3306;password=";
            MySqlConnection conn = new MySqlConnection(connStr);
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();

            string sql = "SELECT * FROM ice_cream_store.sale WHERE sale.completed = 0 ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine("this is the uncompleted order: ");
                Console.WriteLine("Coustomer id: " + rdr[0]);
                Console.WriteLine("Name: " + rdr[1]);
                Console.WriteLine("Date: " + rdr[2]);
                Console.WriteLine("Total Price: " + rdr[4]);
                Console.WriteLine("\n");

            }
            rdr.Close();
            conn.Close();


        }

        public static void most_common_Ingredients()
        {

            string connStr = "server=localhost;user=root;port=3306;password=";
            MySqlConnection conn = new MySqlConnection(connStr);
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();

            int most_flavor = 0;
            string flavor_name = "";

            string sql = "SELECT COUNT(flavors.flavor_name) AS value_occurrence ,flavors.flavor_name FROM ice_cream_store.flavors left join ice_cream_store.sale on flavors.c_id=sale.c_id GROUP BY flavor_name ORDER BY value_occurrence DESC LIMIT 1; ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                most_flavor = int.Parse(rdr[0].ToString());
                flavor_name = rdr[1].ToString();
            }
            rdr.Close();

            int most_serving = 0;
            string serving_name = "";

            sql = "SELECT COUNT(serving.kind_serving) AS value_occurrence ,serving.kind_serving FROM ice_cream_store.serving left join ice_cream_store.sale on serving.c_id=sale.c_id GROUP BY kind_serving ORDER BY value_occurrence DESC LIMIT 1; ";
            cmd = new MySqlCommand(sql, conn);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                most_serving = int.Parse(rdr[0].ToString());
                serving_name = rdr[1].ToString();
            }
            rdr.Close();
            int most_topping = 0;
            string topping_name = "";
            sql = "SELECT COUNT(topping.topping_name) AS value_occurrence ,topping.topping_name FROM ice_cream_store.topping left join ice_cream_store.sale on topping.c_id=sale.c_id GROUP BY topping_name ORDER BY value_occurrence DESC LIMIT 1; ";
            cmd = new MySqlCommand(sql, conn);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                most_topping = int.Parse(rdr[0].ToString());
                topping_name = rdr[1].ToString();
            }
            rdr.Close();



            int temp = Math.Max(most_flavor, most_serving);
            int maximum = Math.Max(temp, most_topping);

            if (maximum == most_flavor)
            {
                Console.WriteLine("The most common Ingredients is: " + flavor_name);
            }
            else if (maximum == most_serving)
            {
                Console.WriteLine("The most common Ingredients is: " + serving_name);
            }
            else
            {
                Console.WriteLine("The most common Ingredients is: " + topping_name);
            }

            Console.WriteLine("The most common flavor is: " + flavor_name + " with " + most_flavor + " orders");
            conn.Close();

        }

        public static void deleteOrder(int order_num)
        {
            string connStr = "server=localhost;user=root;port=3306;password=";
            MySqlConnection conn = new MySqlConnection(connStr);
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();

            string sql = "DELETE FROM `ice_cream_store`.`sale` WHERE c_id = " + order_num + ";";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

            sql = "DELETE FROM `ice_cream_store`.`flavors` WHERE c_id = " + order_num + ";";
            cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

            sql = "DELETE FROM `ice_cream_store`.`topping` WHERE c_id = " + order_num + ";";
            cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

            sql = "DELETE FROM `ice_cream_store`.`serving` WHERE c_id = " + order_num + ";";
            cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Order " + order_num + " has been deleted");

            conn.Close();
        }


        public static void update_order(int c_id)
        {

            Console.WriteLine("Your order is: ");
            get_reciept();

            Console.WriteLine("What you want to update in your order (serving,flavors,topping) ? ");
            string update = Console.ReadLine();

            if (update == "serving")
            {
                Console.WriteLine("What is the new serving you want to add ? ");
                string new_serving = Console.ReadLine();
                string connStr = "server=localhost;user=root;port=3306;password=";
                MySqlConnection conn = new MySqlConnection(connStr);
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = "UPDATE `ice_cream_store`.`serving` SET `kind_serving` = '" + new_serving + "' WHERE (`c_id` = '" + c_id + "');";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine("The serving has been updated");
                conn.Close();
            }
            else if (update == "flavors")
            {

                Console.WriteLine("You want to change flavor or add new one ? (change/add) ");
                string change_or_add = Console.ReadLine();
                if (change_or_add == "add")
                {
                    Console.WriteLine("What is the flavor you want to add ? ");
                    string new_flavor2 = Console.ReadLine();
                    string connStr2 = "server=localhost;user=root;port=3306;password=";
                    MySqlConnection conn2 = new MySqlConnection(connStr2);
                    Console.WriteLine("Connecting to MySQL...");
                    conn2.Open();

                    string sql2 = "INSERT INTO `ice_cream_store`.`flavors` (`c_id`, `flavor_name`) VALUES ('" + c_id + "', '" + new_flavor2 + "');";
                    MySqlCommand cmd2 = new MySqlCommand(sql2, conn2);
                    cmd2.ExecuteNonQuery();
                    Console.WriteLine("The flavor has been updated");
                    conn2.Close();
                }
                else if (change_or_add == "change")
                {
                    Console.WriteLine("What is the flavor you want to change ? ");
                    string new_flavor3 = Console.ReadLine();

                    Console.WriteLine("What is the new flavor you want ? ");
                    string new_flavor4 = Console.ReadLine();


                    string connStr3 = "server=localhost;user=root;port=3306;password=";
                    MySqlConnection conn3 = new MySqlConnection(connStr3);
                    Console.WriteLine("Connecting to MySQL...");
                    conn3.Open();

                    // string sql3 = "UPDATE`ice_cream_store`.`flavors` (`c_id`, `flavor_name`) VALUES ('"+c_id+"', '"+new_flavor3+"');";
                    string sql4 = "UPDATE`ice_cream_store`.`flavors` SET `flavor_name` = '" + new_flavor4 + "' WHERE (`flavor_name` = '" + new_flavor3 + "');";
                    MySqlCommand cmd3 = new MySqlCommand(sql4, conn3);
                    cmd3.ExecuteNonQuery();
                    Console.WriteLine("The flavor has been change");
                    conn3.Close();
                }

            }
            else if (update == "topping")
            {
                Console.WriteLine("What is the new topping you want to add ? ");
                string new_topping = Console.ReadLine();
                string connStr = "server=localhost;user=root;port=3306;password=";
                MySqlConnection conn = new MySqlConnection(connStr);
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = "UPDATE `ice_cream_store`.`topping` SET `topping_name` = '" + new_topping + "' WHERE (`c_id` = '" + c_id + "');";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine("The topping has been updated");
                conn.Close();
            }
            else
            {
                Console.WriteLine("You have entered a wrong input");
            }


            Console.WriteLine("you want to update anything else ? (yes/no)");
            string ans = Console.ReadLine();
            if (ans == "yes")
            {
                update_order(c_id);
            }
            else
            {
                Console.WriteLine("Thank you for updating your order");
            }


        }

        public static void fill_random_Tables(int num)
        {

            Random rnd = new Random();
            Random r = new Random();




            for (int i = 0; i < num; i++)
            {
                RandomDateTime date = new RandomDateTime();
                DateTime randomDate = date.Next();

                int index = rnd.Next(0, Serving.Length);
                string serving = Serving[index];
                Serving serv = new Serving(Logic.c_id, serving, 0);

                if (serving == "regular_cone")
                {
                    serv = new Serving(Logic.c_id, serving, 0);
                    Logic.c_id += 1;
                    MySqlAccess.MySqlAccess.insertObject(serv);
                }
                else if (serving == "special_cone")
                {
                    serv = new Serving(Logic.c_id, serving, 2);
                    Logic.c_id += 1;
                    MySqlAccess.MySqlAccess.insertObject(serv);
                }
                else if (serving == "box")
                {
                    serv = new Serving(Logic.c_id, serving, 5);
                    Logic.c_id += 1;
                    MySqlAccess.MySqlAccess.insertObject(serv);
                }

                int id = serv.get_id_serving();
                string kind_serving = serv.get_kind_serving();

                if (serv != null)
                {


                    if (kind_serving == "regular_cone" || kind_serving == "special_cone")
                    {

                        int s = r.Next(1, 3);

                        string[] flav_chooice = new string[s];


                        if (s == 1 && kind_serving == "regular_cone")
                        {
                            serv.set_price(7);
                            for (int j = 0; j < s; j++)
                            {

                                int index2 = rnd.Next(0, Flavors.Length);
                                string flavor = Flavors[index2];
                                flav_chooice[j] = flavor;

                            }
                            int index3 = rnd.Next(0, Names.Length);
                            string name = Names[index3];
                            Flavors flav = new Flavors(id, flav_chooice);
                            MySqlAccess.MySqlAccess.insertObject(flav);

                            int confirm_order = rnd.Next(0, Confirm_order.Length);
                            string confirm = Confirm_order[confirm_order];

                            if (confirm == "yes")
                            {
                                int complete = 1;
                                Sale sale = new Sale(name, id, randomDate, complete, serv.get_price());
                                MySqlAccess.MySqlAccess.insertObject(sale);
                            }
                            else
                            {
                                int complete = 0;
                                Sale sale = new Sale(name, id, randomDate, complete, serv.get_price());
                                MySqlAccess.MySqlAccess.insertObject(sale);
                            }
                        }

                        if (s == 1)
                        {
                            choosing(serv, s, id, 7);
                        }


                        else if (s == 2)
                        {
                            choosing(serv, s, id, 12);
                        }


                        else if (s == 3)
                        {
                            choosing(serv, s, id, 18);
                        }
                    }

                    else if (kind_serving == "box")
                    {
                        int s = r.Next(1, 7);
                        string[] flav_chooice = new string[s];

                        if (s == 1)
                        {
                            choosing(serv, s, id, 7);
                        }

                        else if (s == 2)
                        {
                            choosing(serv, s, id, 12);
                        }
                        else if (s == 3)
                        {
                            choosing(serv, s, id, 18);
                        }
                        else if (s > 3)
                        {
                            int price = 18 + (s - 3) * 6;
                            choosing(serv, s, id, price);
                        }

                    }

                }

            }


        }

        public static void choosing(Serving serv, int s, int id, int price)
        {

            RandomDateTime date = new RandomDateTime();
            DateTime randomDate = date.Next();
            Random rnd = new Random();
            String[] flav_chooice = new String[s];

            serv.set_price(price);
            Console.WriteLine("The price is " + serv.get_price());

            for (int j = 0; j < s; j++)
            {

                int index2 = rnd.Next(0, Flavors.Length);
                string flavor = Flavors[index2];
                flav_chooice[j] = flavor;

            }
            int index3 = rnd.Next(0, Names.Length);
            string name = Names[index3];
            Flavors flav = new Flavors(id, flav_chooice);
            MySqlAccess.MySqlAccess.insertObject(flav);

            int index4 = rnd.Next(0, Want_topping.Length);
            string topping = Want_topping[index4];

            if (topping == "yes")
            {
                int count_topping = rnd.Next(0, How_much_topping_you_want.Length);
                int topping_count = Int16.Parse(How_much_topping_you_want[count_topping]);
                string[] topping_chooice = new string[topping_count];
                int new_price = topping_count * 2;
                serv.set_price(new_price);

                Console.WriteLine("The new price is " + serv.get_price());



                for (int k = 0; k < topping_count; k++)
                {
                    int index_topping = rnd.Next(0, Topping.Length);
                    string topping_choose = Topping[index_topping];
                    topping_chooice[k] = topping_choose;
                }
                Topping top = new Topping(id, topping_chooice);
                MySqlAccess.MySqlAccess.insertObject(top);
                int confirm_order = rnd.Next(0, Confirm_order.Length);
                string confirm = Confirm_order[confirm_order];

                if (confirm == "yes")
                {
                    int complete = 1;
                    Sale sale = new Sale(name, id, randomDate, complete, serv.get_price());
                    MySqlAccess.MySqlAccess.insertObject(sale);
                }
                else
                {
                    int complete = 0;
                    Sale sale = new Sale(name, id, randomDate, complete, serv.get_price());
                    MySqlAccess.MySqlAccess.insertObject(sale);
                }
            }

            else
            {
                int confirm_order = rnd.Next(0, Confirm_order.Length);
                string confirm = Confirm_order[confirm_order];
                if (confirm == "yes")
                {
                    int complete = 1;
                    Sale sale = new Sale(name, id, randomDate, complete, serv.get_price());
                    MySqlAccess.MySqlAccess.insertObject(sale);
                }
                else
                {
                    int complete = 0;
                    Sale sale = new Sale(name, id, randomDate, complete, serv.get_price());
                    MySqlAccess.MySqlAccess.insertObject(sale);
                }

            }



        }



        public static void fillCollection(int num)
        {
            
            int id=0;
            MongoOrder tmpMongo;
            List<MongoOrder> data = new List<MongoOrder>(num);
            Random r = new Random();
            string [] flavor_choosing = new string[0];
            string [] tooping_choosing=new string[0];

            for (int i = 0; i < num; i++)
            {
                RandomDateTime date = new RandomDateTime();
                DateTime randomDate = date.Next();
                string date2= randomDate.ToString("yyyy-MM-dd HH:mm:ss");
                tmpMongo = new MongoOrder();

                tmpMongo.set_id(id);
                id++;
                tmpMongo.set_name(Names[r.Next(0, Names.Length)]);
                tmpMongo.set_dateTime(date2);
                tmpMongo.set_servine_kind(Serving[r.Next(0, Serving.Length)]);
                string kind_serving = tmpMongo.get_serving_kind();
    

                if (kind_serving == "regular_cone" || kind_serving == "special_cone")
                {
                

                    int cout_scoops = r.Next(1, 3);

                    

                    if (kind_serving == "special_cone")
                    {
                        tmpMongo.set_price(2);
                    }

                    if (kind_serving == "regular_cone" && cout_scoops == 1)
                    {
                        flavor_choosing = new string[cout_scoops];
                        tmpMongo.set_price(7);
                        for (int j = 0; j < cout_scoops; j++)
                        {
                            flavor_choosing[j] = Flavors[r.Next(0, Flavors.Length)];
    
                        }
                        tmpMongo.set_topping_name(tooping_choosing);
                        tmpMongo.set_flavor_name(flavor_choosing);
                    }

                    else if (cout_scoops == 1 ){

                        Console.WriteLine("inside 1 scoops ID"+id);
                        tmpMongo.set_price(7);
                        choose(cout_scoops,tmpMongo,data);

                    }
                    
                    
                    else if(cout_scoops==2){
                        Console.WriteLine("inside 2 scoops ID"+id);
                        tmpMongo.set_price(12);
                        choose(cout_scoops,tmpMongo,data);
                    }

                    else if(cout_scoops==3){
                        tmpMongo.set_price(18);
                        choose(cout_scoops,tmpMongo,data);
                    }
                
                }
                
                else if(kind_serving=="box") {
                    tmpMongo.set_price(5);
                    int cout_scoops = r.Next(1, 7);
                    if(cout_scoops==1){
                        tmpMongo.set_price(7);
                    }
                    if(cout_scoops==2){
                        tmpMongo.set_price(12);
                    }
                    if(cout_scoops==3){
                        tmpMongo.set_price(18);
                    }
                    if(cout_scoops>3){
                        int price=18+(cout_scoops-3)*6;
                        tmpMongo.set_price(price);
                    }

                    choose(cout_scoops,tmpMongo,data);
                }

            int index=r.Next(0,Confirm_order.Length);
            string confirm=Confirm_order[index];
            if(confirm=="yes"){
                tmpMongo.set_completed(1);
            }
            else{
                tmpMongo.set_completed(0);
            }

            data.Add(tmpMongo);

        }
            MongoAccess.MongoAccess.fillDocuments(data);    
        }

        public static void choose(int scoops, MongoOrder tmpMongo, List<MongoOrder> data)
        {
            string[] flavor_choosing=new string[scoops];
            string[] tooping_choosing=new string[0];
             Random r = new Random();

            for (int j = 0; j < scoops; j++)
            {
                flavor_choosing[j] = Flavors[r.Next(0, Flavors.Length)];
            }
            tmpMongo.set_flavor_name(flavor_choosing);
            int index_want_topping = r.Next(0, Want_topping.Length);
            string want_topping = Want_topping[index_want_topping];

            if (want_topping == "yes")
            {
                int count_topping = r.Next(0, How_much_topping_you_want.Length);
                int topping_count = Int16.Parse(How_much_topping_you_want[count_topping]);
                tooping_choosing=new string[topping_count];
                int new_price = topping_count * 2;
                tmpMongo.set_price(new_price);
                
                choose_topping(tmpMongo,topping_count);


            }
            else
            {
                tmpMongo.set_topping_name(tooping_choosing);
            }

        }

    public static void choose_topping(MongoOrder tmpMongo,int topping_count){
        string[] tooping_choosing=new string[topping_count];
        
        

    if ((tmpMongo.get_flavor_name().Contains("chocolate") || tmpMongo.get_flavor_name().Contains("mekopelet")) && tmpMongo.get_flavor_name().Contains("vanile")){

        Console.WriteLine("Please choose topping from the following list: ");
        Console.WriteLine("\n");
        
        for (int k = 0; k < topping_count; k++)
            {
                array_ToString(Topping_both);
                
                string topping=Console.ReadLine();
                Console.WriteLine("\n");
                tooping_choosing[k] = topping;
            }
            tmpMongo.set_topping_name(tooping_choosing);

    }

    else if (tmpMongo.get_flavor_name().Contains("chocolate") || tmpMongo.get_flavor_name().Contains("mekopelet")) {

         Console.WriteLine("Please choose topping from the following list: ");
         Console.WriteLine("\n");
        
        for (int k = 0; k < topping_count; k++)
            {
                array_ToString(Topping_for_chocolate_mekupelet);
                
                string topping=Console.ReadLine();
                Console.WriteLine("\n");
                tooping_choosing[k] = topping;
            }
            tmpMongo.set_topping_name(tooping_choosing);;
    }

    else if (tmpMongo.get_flavor_name().Contains("vanile") ) {
         Console.WriteLine("Please choose topping from the following list: ");
         Console.WriteLine("\n");
  
        for (int k = 0; k < topping_count; k++)
            {
                array_ToString(Topping_for_vanile);
                
                string topping=Console.ReadLine();
                Console.WriteLine("\n");
                tooping_choosing[k] = topping;
            }
            tmpMongo.set_topping_name(tooping_choosing);
    }
    else{
         Console.WriteLine("Please choose topping from the following list: ");
         Console.WriteLine("\n");
        
        for (int k = 0; k < topping_count; k++)
            {
                array_ToString(Topping);
                
                string topping=Console.ReadLine();
                Console.WriteLine("\n");
                tooping_choosing[k] = topping;
            }
            tmpMongo.set_topping_name(tooping_choosing);
    }

    }


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////


public static void CreateOrder()
        {
            
            
            MongoOrder tmpMongo;
            List<MongoOrder> data = new List<MongoOrder>();
            Random r = new Random();
            string [] flavor_choosing = new string[0];
            string [] tooping_choosing=new string[0];

                RandomDateTime date = new RandomDateTime();
                DateTime randomDate = date.Next();
                string date_string = randomDate.ToString("dd/MM/yyyy");
                tmpMongo = new MongoOrder();
                tmpMongo.set_id(Logic.c_id);
                Logic.c_id++;
                Console.WriteLine("Hello, please enter your name");
                string name = Console.ReadLine();
                Console.WriteLine("\n");
                tmpMongo.set_name(name);
                tmpMongo.set_dateTime(date_string);
                Console.WriteLine("Hello " + name + " please choose your kind of serving");
                array_ToString(Serving);
                Console.WriteLine("\n");

                string kind_serving = Console.ReadLine();
                tmpMongo.set_servine_kind(kind_serving);

                
                if (kind_serving == "regular_cone" || kind_serving == "special_cone")
                {

                    Console.WriteLine("Hello " + name + "How much scoopd you want (1/2/3");
                    int cout_scoops = Int16.Parse(Console.ReadLine());
                    Console.WriteLine("\n");
                    

                    if (kind_serving == "special_cone")
                    {
                        tmpMongo.set_price(2);
                    }

                    if (kind_serving == "regular_cone" && cout_scoops == 1)
                    {
                        flavor_choosing = new string[cout_scoops];
                        tmpMongo.set_price(7);

                        Console.WriteLine("Hello " + name + " please choose your flavor");
                        for (int j = 0; j < cout_scoops; j++)
                        {
                            
                            array_ToString(Flavors);
                            string flavor = Console.ReadLine();
                            Console.WriteLine("\n");
                            flavor_choosing[j] = flavor;
    
                        }
                        tmpMongo.set_topping_name(tooping_choosing);
                        tmpMongo.set_flavor_name(flavor_choosing);
                    }

                    else if (cout_scoops == 1 ){

                        tmpMongo.set_price(7);
                        choose(cout_scoops,tmpMongo);

                    }
                    
                    
                    else if(cout_scoops==2){
                        tmpMongo.set_price(12);
                        choose(cout_scoops,tmpMongo);
                    }

                    else if(cout_scoops==3){
                        tmpMongo.set_price(18);
                        choose(cout_scoops,tmpMongo);
                    }
                
                }
                
                else if(kind_serving=="box") {
                    tmpMongo.set_price(5);
                    Console.WriteLine("how much scoops you want ? ");
                    int cout_scoops = Int16.Parse(Console.ReadLine());
                    Console.WriteLine("\n");

                    ;
                    if(cout_scoops==1){
                        tmpMongo.set_price(7);
                    }
                    if(cout_scoops==2){
                        tmpMongo.set_price(12);
                    }
                    if(cout_scoops==3){
                        tmpMongo.set_price(18);
                    }
                    if(cout_scoops>3){
                        int price=18+(cout_scoops-3)*6;
                        tmpMongo.set_price(price);
                    }

                    choose(cout_scoops,tmpMongo);
                }

            int index=r.Next(0,Confirm_order.Length);
            string confirm=Confirm_order[index];
            if(confirm=="yes"){
                tmpMongo.set_completed(1);
            }
            else{
                tmpMongo.set_completed(0);
            }

            data.Add(tmpMongo);

            Console.WriteLine("Your order is: " + data[0].ToString());
            
            Console.WriteLine("\n");
             Console.WriteLine(data[0]);
             Console.WriteLine("\n");
            Console.WriteLine("You confirm your order? (yes/no)");
            string confirm2 = Console.ReadLine();

            if(confirm2=="yes"){
                Console.WriteLine("\n");
                Console.WriteLine("Your order is confirmed");
                tmpMongo.set_completed(1);
            }
            else{
                Console.WriteLine("\n");
                Console.WriteLine("Your order is canceled");
                tmpMongo.set_completed(0);
            }

            
            MongoAccess.MongoAccess.fillDocuments(data); 

        }

public static void choose(int scoops, MongoOrder tmpMongo)
        {
            string[] flavor_choosing=new string[scoops];
            string[] tooping_choosing=new string[0];
            
            Console.WriteLine( "please choose your flavor: " );
            Console.WriteLine("\n");
            for (int j = 0; j < scoops; j++)
            {
                array_ToString(Flavors);
                string flavor = Console.ReadLine();
                Console.WriteLine("\n");
                flavor_choosing[j] = flavor;
                
            }
            tmpMongo.set_flavor_name(flavor_choosing);
            Console.WriteLine("You want topping? (yes/no) ");
            string topping = Console.ReadLine();
            Console.WriteLine("\n");
      
            if (topping == "yes")
            {
                Console.WriteLine("How much topping you want? type 1 if you want one topping ");
                
                int count_topping = Int16.Parse(Console.ReadLine());
                Console.WriteLine("\n");

            
                tooping_choosing=new string[count_topping];
                int new_price = count_topping * 2;
                tmpMongo.set_price(new_price);
                
                choose_topping(tmpMongo,count_topping);

            }
            else
            {
                tmpMongo.set_topping_name(tooping_choosing);
            }
            

        }

        public static void delete_all_orders(){
            // Console.WriteLine("Please enter the id of the document you want to delete");
            // int id=Int16.Parse(Console.ReadLine());
            // MongoAccess.MongoAccess.deleteDocument(id);
            MongoAccess.MongoAccess.delete_document();
        }

        public static void delete_orders(){
            MongoAccess.MongoAccess.DeleteOrder();;
            
        }

         public static void Update_Order(){
            MongoAccess.MongoAccess.Update_Order();
            
        }
         public static void Read_Order(){
            MongoAccess.MongoAccess.Read_Order();
            
        }
        public static void get_day_report_mongodb(){

            MongoAccess.MongoAccess.get_day_report_mongodb();
        }

          public static void get_uncompletedsales_mongodb(){

            MongoAccess.MongoAccess.get_uncomplete_sales_mongodb();
        }
         public static void most_common_Ingredients_mongodb(){

            MongoAccess.MongoAccess.most_common_flavors_mongodb();
        }


                
        }

}






