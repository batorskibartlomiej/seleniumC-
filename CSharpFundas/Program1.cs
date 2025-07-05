

namespace CSharpFundas
{

     class Program1 : Program4
    {

        String name;
        String lastName;

        public Program1(String name) 
        {
            this.name = name;
        }

        public Program1()
        {
        }

        public Program1(string fisrtName, string lastName) 
        {
            this.name= fisrtName;
            this.lastName= lastName;
        }

        public void getName()
        {
            Console.WriteLine("My name is " + this.name);
        }

        public void getData()
        {
            Console.WriteLine("I am inside method");
            
        }

        private static void Main(string[] args)
        {
            Program1 p = new Program1("Bartek");
            Program1 p1 = new Program1("Bartek", "Gosia");
            p.getData();
            p.SetData();
            p.getName();
            p1.getName();
            Console.WriteLine("Hello, World!");
        }
    }

}

