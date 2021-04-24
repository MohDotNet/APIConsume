using System;
using System.Net.Http;
using System.Net.Http.Headers;


namespace APIConsume
{

    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello Calling the api!");
            getPeople();

            Console.WriteLine("Showing user with id : 2");
            Console.WriteLine();
            GetAPerson(2);
            Console.ReadLine();
        }

        static void getPeople()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://vmprod:5000/");

                var response = client.GetAsync("People");

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Person[]>();
                    readTask.Wait();

                    var people = readTask.Result;

                    foreach (var item in people)
                    {
                        Console.WriteLine("ID : " + item.Id + " Firstname: " + item.Firstname + " Lastname : " + item.Surname + " Age: " + item.Age);
                    }
                }

            }
        }

        static void GetAPerson(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("HTTP://vmprod:5000/");

                var response = client.GetAsync("People/GetPerson/" + id);
                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    
                    var readTask = result.Content.ReadAsAsync<Person>();
                    readTask.Wait();

                    var retrievedPerson = readTask.Result;

                    Console.WriteLine("Firstname : " + retrievedPerson.Firstname + " Lastname: " + retrievedPerson.Surname + " Age : " + retrievedPerson.Age);
                }

            }
        }

    }

    class Person
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

    }
}
