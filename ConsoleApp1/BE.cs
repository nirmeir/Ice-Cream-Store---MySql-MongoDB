using System.Collections;

//added for mongo semi-generated class


namespace BusinessEntities
{

    // data holder classes (theoreticaly may be a struct ?)
    // public class customer
    // {
    //     string name;
        
    //     public customer(string name)
    //     {
    //         this.name = name;
    //     }

    //     public string getName() { return name; }
    
    //     public override string ToString()
    //     {
    //         return base.ToString() + ": " + name ;
    //     }
    // }

    public class Serving
    {
        int id_serving;
        int price;
        string kind_serving;

    
        public Serving(int id_serving,string kind_serving, int price)
        {
            this.id_serving=id_serving;
            this.kind_serving=kind_serving;
            this.price=price;
            
        
        }

        public int get_id_serving() {
              return this.id_serving; }
        public string get_kind_serving() 

        { return kind_serving; }

        public void set_price(int price){
            this.price+=price;
        }
        public int get_price(){
            return this.price;
        }
       

        public override string ToString()
        {

            return base.ToString() + ": " +  id_serving + " , "+kind_serving+" , "+price;
        }
    }


    public class Flavors
    {
        int f_id;
        string [] flavor_name;

        
        
        public Flavors(int f_id, string [] flavor_name)
        {
            this.f_id=f_id;
            this.flavor_name = flavor_name;
            
        }

     
        public int get_flavor_id(){
           return this.f_id; }

        public void get_to_string_flavor_name(){
            for(int i = 0 ; i< this.flavor_name.Length; i++){
                Console.WriteLine(this.flavor_name[i]);
             }
        }
        public String[] get_flavor_name(){
                return this.flavor_name;
             }
        }

    

    public class Topping
    {
        int topping_id;
        string []topping_name;
      

        public Topping(int topping_id,  string[] topping_name )
        {
            this.topping_id = topping_id;
            this.topping_name = topping_name;
            
        }

        public int get_topping_id() { 
            return topping_id; }
        public String [] get_topping_name() {
             return this.topping_name; }

        
        }



    public class Sale
    {
        int rid;
        string name;
        DateTime dateTime;
        int completed;
        int total_price;

       public Sale(string name,int rid, DateTime dateTime, int completed, int total_price)
        {
            this.rid = rid;
            this.dateTime = dateTime;
            this.completed = completed;
            this.total_price = total_price;
            this.name=name;
        }

    public string get_name(){
        return this.name;
    }
        public int getrid() { return rid; }
       
        public int getTotalPrice() { return total_price; }
        public string getDateTime()
        {
            string formatForMySql = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            return formatForMySql;
        }
        public int getCompleted() { 
        return this.completed; }

        public void set_completed(int comp)
    {
        this.completed = comp;
    }


    }


    // public class MongoOrder
    // {
    //     //a work around: we will have 3 classes in one, to get its' data ?
    //     Sale sale;
    //     Flavors flavor;
    //     Topping topping;

    //     string orderDate;
    //     int completed;

    //     public void setStatus(string orderDate, string completeDate, int completed, int payed)
    //     {
    //         this.orderDate = orderDate;
    //         this.completed = completed;
    //     }

    //     public void setSale(Sale o){
    //         this.sale = o;
    //     }

    //     public void setFlavors(Flavors v){
    //         this.flavor = v;
    //     }

    //     public void setTopping(Topping t){
    //         topping = t;
    //     }

    //     public Sale GetSale(){
    //         return sale;
    //     }

    //     public Flavors GetFlavors(){
    //         return flavor;
    //     }

    //     public Topping GetTopping(){
    //         return topping;
    //     }

    //     public string getOrderDate() { return orderDate; }
    //     public int getCompleted() { return completed; }

    // }

      public class MongoOrder
    {
        //a work around: we will have 3 classes in one, to get its' data ?

        int id;
        string name;
        string dateTime;

        int completed;
        int total_price;

        string [] topping_name;
        string [] flavor_name= new string[3];

        string servine_kind;


        public void setStatus( int id,string name, string dateTime,int completed,int total_price,string servine_kind, string [] topping_name, string [] flavor_name)
        {
            this.id = id;
            this.name = name;
            this.dateTime = dateTime;
            this.completed = completed;
            this.total_price = total_price;
            this.servine_kind=servine_kind;
            this.topping_name = topping_name;
            this.flavor_name = flavor_name;
        }
        
        public int get_id(){
            return this.id;
        }
        public void set_id(int id){
            this.id=id;
        }

        public string get_name(){
            return this.name;
        }
        public string get_dateTime(){
            return this.dateTime;
        }
        public int get_completed(){
            return this.completed;
        }
        public int get_total_price(){
            return this.total_price;
        }
        public string [] get_topping_name(){
            return this.topping_name;
        }
        public string [] get_flavor_name(){
            return this.flavor_name;
        }

        public void set_price(int price){
            this.total_price+=price;
        }
        public void set_name(string name){
            this.name=name;
        }
        public void set_dateTime(string dateTime){
            this.dateTime=dateTime;
        }
        public void set_completed(int completed){
            this.completed=completed;
        }
        public void set_topping_name(string [] topping_name){
            this.topping_name=topping_name;
        }
        public void set_flavor_name(string [] flavor_name){
            this.flavor_name=flavor_name;
        }
        public void set_servine_kind(string servine_kind){
            this.servine_kind=servine_kind;
        }
        public string get_serving_kind(){
            return this.servine_kind;
        }
    }

}