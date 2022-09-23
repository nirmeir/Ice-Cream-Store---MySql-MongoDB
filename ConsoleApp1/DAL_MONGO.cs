

using MongoDB.Driver;
using MongoDB.Bson;

using BusinessEntities;
using BusinessLogic;
using System.Collections;

namespace MongoAccess
{




    
    class MongoAccess
    {
        public static void test()
        {

            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://nirmeir:Nirmeir1@cluster0.gzkkjse.mongodb.net/test");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("ice_cream_store");
            var dbList = database.ListCollectionNames().ToList();
            



            Console.WriteLine("The list of databases on this server is: ");
            foreach (var db in dbList)
            {
                Console.WriteLine(db);
            }
        }
    


        public static void fillDocuments(List<MongoOrder> orders) //to make it by interface, rename to fillData
        {

             List<BsonDocument> documents = new List<BsonDocument>();
             
            //build list of all documents
            foreach (var mongoOrderd in orders) {

                var document = new BsonDocument { 

                            { "Sales", new BsonDocument { 

                                {"Order Id", mongoOrderd.get_id() },
                                {"Name", mongoOrderd.get_name() },
                                {"dateTime",mongoOrderd.get_dateTime()},
                                {"completed",mongoOrderd.get_completed()},
                                {"total price",mongoOrderd.get_total_price()},
                                {"servine_kind",mongoOrderd.get_serving_kind()},
                                {"flavors",new BsonArray(mongoOrderd.get_flavor_name())},
                                {"toppings",new BsonArray(mongoOrderd.get_topping_name())}
                            } }
                            
                        };
                        
        
                documents.Add(document);
        
            }

            // Console.WriteLine("list is ok");
            //add them all to mongo

            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://nirmeir:Nirmeir1@cluster0.gzkkjse.mongodb.net/test");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("ice_cream_store");
            var collection = database.GetCollection<BsonDocument> ("Sales");
            
            
            


            // Console.WriteLine(documents[0]);
            collection.InsertMany(documents);
        }

        public static void delete_document(){
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://nirmeir:Nirmeir1@cluster0.gzkkjse.mongodb.net/test");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("ice_cream_store");
            var collection = database.GetCollection<BsonDocument> ("Sales");
            collection.DeleteMany(new BsonDocument());
            Console.WriteLine("All Orders deleted");
        }

    public static void DeleteOrder(){
        Console.WriteLine("Please enter the Order id you want to delete");
            int id=Int16.Parse(Console.ReadLine());
           var settings = MongoClientSettings.FromConnectionString("mongodb+srv://nirmeir:Nirmeir1@cluster0.gzkkjse.mongodb.net/test");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("ice_cream_store");
            var collection = database.GetCollection<BsonDocument> ("Sales");
            var filter = Builders<BsonDocument>.Filter.Eq("Sales.Order Id", id);
            collection.DeleteOne(filter);
            Console.WriteLine("Order " + id + "deleted");
    }
      public static void Update_Order(){
        Console.WriteLine("Please enter the Order id you want to update");
            int id=Int16.Parse(Console.ReadLine());
        Console.WriteLine("What do you want to update? (flavors/topping)");
        string update=Console.ReadLine();
        if(update=="flavors"){
            Console.WriteLine("You want to change or add new flavors? (change/add)");
            string change=Console.ReadLine();

            if(change=="change"){
                Console.WriteLine("Please enter the flavors you want to change");
                string flavors=Console.ReadLine();
                Console.WriteLine("Please enter the new flavors");
                string new_flavors=Console.ReadLine();
                var settings = MongoClientSettings.FromConnectionString("mongodb+srv://nirmeir:Nirmeir1@cluster0.gzkkjse.mongodb.net/test");
                settings.ServerApi = new ServerApi(ServerApiVersion.V1);
                var client = new MongoClient(settings);
                var database = client.GetDatabase("ice_cream_store");
                var collection = database.GetCollection<BsonDocument> ("Sales");
                var filter = Builders<BsonDocument>.Filter.Eq("Sales.Order Id", id );

                var index=collection.Find(filter).ToList()[0]["Sales"]["flavors"].AsBsonArray.IndexOf(flavors);
                var update2 = Builders<BsonDocument>.Update.Set("Sales.flavors."+index,new_flavors);
                collection.UpdateOne(filter, update2);

                Console.WriteLine("Order " + id + "updated");
            }
            else if(change=="add"){
                Console.WriteLine("Please enter the flavors you want to add");
                string flavors=Console.ReadLine();
                var settings = MongoClientSettings.FromConnectionString("mongodb+srv://nirmeir:Nirmeir1@cluster0.gzkkjse.mongodb.net/test");
                settings.ServerApi = new ServerApi(ServerApiVersion.V1);
                var client = new MongoClient(settings);
                var database = client.GetDatabase("ice_cream_store");
                var collection = database.GetCollection<BsonDocument> ("Sales");
                var filter = Builders<BsonDocument>.Filter.Eq("Sales.Order Id", id );
                var update2 = Builders<BsonDocument>.Update.Push("Sales.flavors",flavors);
                collection.UpdateOne(filter, update2);
                Console.WriteLine("Your Order is updated");
            }
        }
        else if(update=="topping") {
            Console.WriteLine("You want to change or add new tooping? (change/add)");
            string change2=Console.ReadLine();

            if(change2=="change"){
                Console.WriteLine("Please enter the topping you want to change");
                string topping=Console.ReadLine();
                Console.WriteLine("Please enter the new tooping");
                string new_tooping=Console.ReadLine();
                var settings = MongoClientSettings.FromConnectionString("mongodb+srv://nirmeir:Nirmeir1@cluster0.gzkkjse.mongodb.net/test");
                settings.ServerApi = new ServerApi(ServerApiVersion.V1);
                var client = new MongoClient(settings);
                var database = client.GetDatabase("ice_cream_store");
                var collection = database.GetCollection<BsonDocument> ("Sales");
                var filter = Builders<BsonDocument>.Filter.Eq("Sales.Order Id", id );
                var filter2=Builders<BsonDocument>.Filter.Eq("Sales.topping",topping);
                var index=collection.Find(filter).ToList()[0]["Sales"]["toppings"].AsBsonArray.IndexOf(topping);
                var update2 = Builders<BsonDocument>.Update.Set("Sales.toppings."+index,new_tooping);
                collection.UpdateOne(filter, update2);
                Console.WriteLine("Your Order is updated");
            }
            else if(change2=="add"){
                Console.WriteLine("Please enter the tooping you want to add");
                string tooping=Console.ReadLine();
                var settings = MongoClientSettings.FromConnectionString("mongodb+srv://nirmeir:Nirmeir1@cluster0.gzkkjse.mongodb.net/test");
                settings.ServerApi = new ServerApi(ServerApiVersion.V1);
                var client = new MongoClient(settings);
                var database = client.GetDatabase("ice_cream_store");
                var collection = database.GetCollection<BsonDocument> ("Sales");
                var filter = Builders<BsonDocument>.Filter.Eq("Sales.Order Id", id );
                var update2 = Builders<BsonDocument>.Update.Push("Sales.topping",tooping);
                collection.UpdateOne(filter, update2);
                Console.WriteLine("Your Order is updated");
            }
        }
        else{
            Console.WriteLine("You type wrong input, Please try again");
        }
        
}

    public static void Read_Order(){
        Console.WriteLine("Please enter the Order id you want to read");
            int id=Int16.Parse(Console.ReadLine());
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://nirmeir:Nirmeir1@cluster0.gzkkjse.mongodb.net/test");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("ice_cream_store");
            var collection = database.GetCollection<BsonDocument> ("Sales");
            var filter = Builders<BsonDocument>.Filter.Eq("Sales.Order Id", id );
            var result = collection.Find(filter).ToList();
            Console.WriteLine(result[0]["Sales"]);

    }

    // public static void get_day_report_mongodb(){
    //     Console.WriteLine("Please enter the date you want to get the report(yyyy/mm/dd) ");
    //     string day=Console.ReadLine();
    //     var settings = MongoClientSettings.FromConnectionString("mongodb+srv://nirmeir:Nirmeir1@cluster0.gzkkjse.mongodb.net/test");
    //     settings.ServerApi = new ServerApi(ServerApiVersion.V1);
    //     var client = new MongoClient(settings);
    //     var database = client.GetDatabase("ice_cream_store");
    //     var collection = database.GetCollection<BsonDocument> ("Sales");
    //     var filter = Builders<BsonDocument>.Filter.AnyEq("Sales.dateTime", day );
    //     var result = collection.Find(filter).ToList();
    //         // Console.WriteLine(result[0]["Sales"]);
    //         Console.WriteLine(result["Sles"].ToString());

    //     }
        public static void get_day_report_mongodb()
        {
            //total price sales by day
            Console.WriteLine("Please enter the date you want to get the report(yyyy/mm/dd) ");
            string day = Console.ReadLine();
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://nirmeir:Nirmeir1@cluster0.gzkkjse.mongodb.net/test");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("ice_cream_store");
            var collection = database.GetCollection<BsonDocument>("Sales");
            var filter = Builders<BsonDocument>.Filter.AnyEq("Sales.dateTime", day);
            var result = collection.Find(filter).ToList();
            var total = 0;
            var count_sales = 0;
            foreach (var item in result)
            {
                total += item["Sales"]["total price"].AsInt32;
            }
            Console.WriteLine("Total sales for the day is: " + total);
              foreach (var item in result)
            {
                count_sales +=1;
            }
            Console.WriteLine("Total sales: " + count_sales);

            var AVG = total / count_sales;
            Console.WriteLine("AVG sales: " + AVG);
        }

         public static void get_uncomplete_sales_mongodb()
        {
            //get all umcomplete sales
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://nirmeir:Nirmeir1@cluster0.gzkkjse.mongodb.net/test");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("ice_cream_store");
            var collection = database.GetCollection<BsonDocument>("Sales");
            var filter = Builders<BsonDocument>.Filter.Eq("Sales.completed", 0);
            var result = collection.Find(filter).ToList();
            int total=0;
            foreach (var item in result)
            {
                Console.WriteLine(item["Sales"]);
                total+=1;
            }
            Console.WriteLine("Total uncomplete sales: "+total);

           
        }

            
 public static void most_common_flavors_mongodb()
        {
           //get the most common flavors choise
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://nirmeir:Nirmeir1@cluster0.gzkkjse.mongodb.net/test");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("ice_cream_store");
            var collection = database.GetCollection<BsonDocument>("Sales");
            var filter = Builders<BsonDocument>.Filter.Eq("Sales.completed", 1);
            int count_of_sales = 0;
            var result = collection.Find(filter).ToList();
           
    
            Dictionary<string, int> tasteCount = new Dictionary<string, int>();
            foreach (var item in result)
            {
                var flavors = item["Sales"]["flavors"].AsBsonArray;
                foreach (var flavor in flavors)
                {
                    if (tasteCount.ContainsKey(flavor.AsString))
                    {
                        tasteCount[flavor.AsString] += 1;
                    }
                    else
                    {
                        tasteCount.Add(flavor.AsString, 1);
                    }
                }
                
            }
            
            // find the most used taste
            var mostUsedTaste = "";
            var mostUsedTasteCount = 0;
            foreach (var taste in tasteCount)
            {
                if (taste.Value > mostUsedTasteCount)
                {
                    mostUsedTaste = taste.Key;
                    mostUsedTasteCount = taste.Value;
                }
            }
            Console.WriteLine("The most used taste is: " + mostUsedTaste + " with " + mostUsedTasteCount + " sales");

            Dictionary<string, int> toppingCount = new Dictionary<string, int>();
            foreach (var item in result)
            {
                var toppings = item["Sales"]["toppings"].AsBsonArray;
                foreach (var topping in toppings)
                {
                    if (toppingCount.ContainsKey(topping.AsString))
                    {
                        toppingCount[topping.AsString] += 1;
                    }
                    else
                    {
                        toppingCount.Add(topping.AsString, 1);
                    }
                }
                
            }
            
            // find the most used taste
            var mostUsedTopping = "";
            var mostUsedToppingCount = 0;
            foreach (var top in toppingCount)
            {
                if (top.Value > mostUsedToppingCount)
                {
                    mostUsedTopping = top.Key;
                    mostUsedToppingCount = top.Value;

                }

                //check most common kind_serving 
                //string value
                Dictionary<string, int> kind_servingCount = new Dictionary<string, int>();
                foreach (var item in result)
                {
                    var kind_serving = item["Sales"]["servine_kind"].AsString;
                    if (kind_servingCount.ContainsKey(kind_serving))
                    {
                        kind_servingCount[kind_serving] += 1;
                    }
                    else
                    {
                        kind_servingCount.Add(kind_serving, 1);
                    }
                }
                // find the most used kind serving
                var mostUsedkind_serving = "";
                var mostUsedkind_servingCount = 0;
                foreach (var kind_serving in kind_servingCount)
                {
                    if (kind_serving.Value > mostUsedkind_servingCount)
                    {
                        mostUsedkind_serving = kind_serving.Key;
                        mostUsedkind_servingCount = kind_serving.Value;
                    }
                }

            int max=Math.Max(mostUsedTasteCount, mostUsedToppingCount);
            max=Math.Max(max, mostUsedkind_servingCount);

            if (max == mostUsedTasteCount)
            {
                Console.WriteLine("The most common choise is: " + mostUsedTaste + " with " + mostUsedTasteCount + " sales");
            }
            else if (max == mostUsedToppingCount)
            {
                Console.WriteLine("The most common choise is: " + mostUsedTopping + " with " + mostUsedToppingCount + " sales");
            }
            else
            {
                Console.WriteLine("The most common choise is: " + mostUsedkind_serving + " with " + mostUsedkind_servingCount + " sales");
            }

 
        }

    
    }
    }
}
    


