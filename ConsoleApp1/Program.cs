using System;
using System.Data;
using System.Diagnostics;//used for Stopwatch class

using MySql.Data;
using MySql.Data.MySqlClient;

using MySqlAccess;
// using MongoAccess;
using BusinessLogic;
using System.Collections;


// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Console.WriteLine("The current time is " + DateTime.Now);

Stopwatch stopwatch = new Stopwatch();


Console.WriteLine("Please choose between mysql and mongodb");
string dbChoice = Console.ReadLine();

if(dbChoice=="mysql"){

int userInput = 0;
do
{
    Console.WriteLine("_____________________");
    Console.WriteLine("Please chose a task:");
    Console.WriteLine("1 - Create empty tables");
    Console.WriteLine("2 - Fill table with data: ");
    Console.WriteLine("3 - Create Order:");
    Console.WriteLine("4 - Update order:");
    Console.WriteLine("5 - Delete order:");
    Console.WriteLine("6 - Read order:");
    Console.WriteLine("7 - get reciept:");
    Console.WriteLine("8 - get day report:");
    Console.WriteLine("9 - Get uncompleted orders:");
    Console.WriteLine("10 - most common ingredient and flavor: ");
  
    userInput = Int32.Parse(Console.ReadLine());

    switch (userInput)
    {
        case 1:
            BusinessLogic.Logic.createTables();
            break;

        case 2:
            BusinessLogic.Logic.fill_random_Tables(100);
            break;   

        case 3:
            BusinessLogic.Logic.choose_flavor();
            break;
        case 4:
        Console.WriteLine("Enter order id to update:");
        int c_id = Int32.Parse(Console.ReadLine());
            BusinessLogic.Logic.update_order(c_id);
            break;
        case 5:
        Console.WriteLine("Enter order id to delete:");
        int order_id = Int32.Parse(Console.ReadLine());
            BusinessLogic.Logic.deleteOrder(order_id);
            break; 

        case 6:
            BusinessLogic.Logic.get_reciept();
            break;

        case 7:
            BusinessLogic.Logic.get_reciept();
            break;    

        case 8:
            BusinessLogic.Logic.get_day_report();
            break;
       
        case 9:
            BusinessLogic.Logic.get_uncomplete_order();
            break;
         case 10:
            BusinessLogic.Logic.most_common_Ingredients();
            break;
    }

} while (userInput != -1);


Console.WriteLine("Thank you for your time");

}

else if (dbChoice == "mongodb"){
int userInput2 = 0;
do
{
    Console.WriteLine("_____________________");
    Console.WriteLine("Please chose a task:");
    Console.WriteLine("1 - Fill mongo with data:");
    Console.WriteLine("2 - Clear all Orders:");
    Console.WriteLine("3 - Create Order:");
    Console.WriteLine("4 - Delete Order :");
    Console.WriteLine("5 - Update Order :");
    Console.WriteLine("6 - Read Order :");
    Console.WriteLine("7 - Get day report: :");
    Console.WriteLine("8 - Get uncompleted orders: :");
    Console.WriteLine("9 - Get most common ingredient and flavor :");


    userInput2 = Int32.Parse(Console.ReadLine());

    switch (userInput2)
    {
    
        case 1:
            BusinessLogic.Logic.fillCollection(2);
            break;
        case 2:
            BusinessLogic.Logic.delete_all_orders();
            break;
        case 3:
            BusinessLogic.Logic.CreateOrder();
            
            break;
        case 4:
            
            BusinessLogic.Logic.delete_orders();
            break;
        case 5:
            BusinessLogic.Logic.Update_Order();
            break; 
        case 6:
            BusinessLogic.Logic.Read_Order();
            break;       
        case 7:
            BusinessLogic.Logic.get_day_report_mongodb();
            break; 
        case 8:
            BusinessLogic.Logic.get_uncompletedsales_mongodb();
            break;   
         case 9:
            BusinessLogic.Logic.most_common_Ingredients_mongodb();
            break;           
 
    }

} while (userInput2 != -1);


Console.WriteLine("Thank you for your time");
}




    
