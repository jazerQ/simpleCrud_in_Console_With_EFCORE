using EF;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EF
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
    }
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public ApplicationContext() => Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=EF.db");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
        using (ApplicationContext db = new ApplicationContext())
        {

                Crud();
                /*var entity = db.Users.Find(id);
                if(entity != null)
                {
                    db.Users.Remove(entity);
                    db.SaveChanges();
                }
                User Tom = new User { Name = "Tom", Age = 18 };
                User Alice = new User { Name = "Alice" , Age = 20};
                db.Users.Add(Tom);
                db.Users.Add(Alice);
                db.SaveChanges();
                Console.WriteLine("объекты успешно сохранены");

                var users = db.Users.ToList();
                Console.WriteLine("список объектов:");
                foreach(var elem in users)
                {
                    Console.WriteLine($"{elem.Id}  -   {elem.Name}   -   {elem.Age}");
                }*/
                
        }
    }

        private static void Crud()
        {
            
            Console.WriteLine("ну, выбирай чувак\n1. сделать запись в таблицу\n 2. удалить запись из таблицы\n 3. обновить данные \n4. вывести всю базу данных");
            try
            {

                int? select = int.Parse(Console.ReadLine());
                switch (select)
                {
                    case 1:
                        Create();
                        Crud();
                        break;
                    case 2:
                        Delete();
                        Crud();
                        break;
                    case 3:
                        Update();
                        Crud();
                        break;
                    case 4:
                        using (ApplicationContext db = new ApplicationContext())
                        {
                            PrintFullDB(db);
                        }
                        Crud();
                        break;
                    default:
                        Console.Clear();
                        Crud();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " ты негодяй!");
                Crud();
            }
        }
        private static void Create()
        {
            Console.Clear();
            using (ApplicationContext db = new ApplicationContext()) { 
                PrintFullDB(db);

                Console.WriteLine("введи свое имя");
                string name = Console.ReadLine();
                Console.WriteLine("введи возраст");
                int age = int.Parse(Console.ReadLine());
                User user = new User() { Name = name, Age = age };
                db.Users.Add(user);
                db.SaveChanges();
                Console.WriteLine("Объекты успешно сохранились");
            }
        }
        private static void Delete()
        {
            Console.Clear();
            using (ApplicationContext db = new ApplicationContext())
            {
                PrintFullDB(db);

                Console.WriteLine("введи Id которое хочешь удалить");
                int? id = int.Parse(Console.ReadLine());
                var entity = db.Users.Find(id);
                if(entity != null)
                {
                    db.Users.Remove(entity);
                    db.SaveChanges();
                    Console.WriteLine("успешно удалили");
                    return;
                }
                Console.WriteLine("ничего не нашли");
                
                
            }
        }
        private static void Update()
        {
            Console.Clear();
            using (ApplicationContext db = new ApplicationContext())
            {
                PrintFullDB(db);

                Console.WriteLine("введи Id которое хочешь обновить");
                int? id = int.Parse(Console.ReadLine());
                var entity = db.Users.Find(id);
                if (entity != null)
                {
                    Console.WriteLine("введи свое имя");
                    string name = Console.ReadLine();
                    Console.WriteLine("введи возраст");
                    int age = int.Parse(Console.ReadLine());
                    entity.Name = name;
                    entity.Age = age;
                    db.SaveChanges();
                    Console.WriteLine("успешно обновили");
                    return;
                }
                Console.WriteLine("ничего не нашли");


            }
        }
        private static void PrintFullDB(ApplicationContext db)
        {
            foreach (var elem in db.Users.ToList())
            {
                Console.WriteLine($"{elem.Id}  -   {elem.Name}   -   {elem.Age}");
            }

            Console.WriteLine("\n\n\n");

        }
    }
}