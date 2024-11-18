using LIBRARYEFCORE.Models;
using LIBRARYEFCORE.Repositories;
using System;
using System.Linq;





namespace LIBRARYEFCORE


{
    public class Program
    {
        static void Main(string[] args)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            UserRepository userRepository = new UserRepository(context);
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("1. Admin Mode");
                Console.WriteLine("2. User Mode");
                Console.WriteLine("3. Registration");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AdminMode(context);
                        break;
                    case "2":
                        UserMode(context);
                        break;
                    case "3":
                        Registration(userRepository);
                        break;
                    case "4":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }

        // Admin Mode
        public static void AdminMode(ApplicationDbContext context)
        {
            Console.WriteLine("Enter Admin Email:");
            string email = Console.ReadLine();
            Console.WriteLine("Enter Password:");
            string password = Console.ReadLine();

            var adminRepo = new AdminRepository(context);
            var admin = adminRepo.GetByEmail(email);

            if (admin != null && admin.Password == password)
            {
                Console.WriteLine("Welcome Admin!");

                bool adminExit = false;
                while (!adminExit)
                {
                    Console.WriteLine("1. View Books");
                    Console.WriteLine("2. Add Book");
                    Console.WriteLine("3. Update Book");
                    Console.WriteLine("4. Delete Book");
                    Console.WriteLine("5. Logout");
                    var adminChoice = Console.ReadLine();

                    switch (adminChoice)
                    {
                        case "1":
                            ViewBooks(context);
                            break;
                        case "2":
                            AddBook(context);
                            break;
                        case "3":
                            UpdateBook(context);
                            break;
                        case "4":
                            DeleteBook(context);
                            break;
                        case "5":
                            adminExit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice, try again.");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid login credentials.");
            }
        }

        // Admin Actions
        public static void ViewBooks(ApplicationDbContext context)
        {
            var bookRepo = new BookRepository(context);
            var books = bookRepo.GetAll();
            foreach (var book in books)
            {
                Console.WriteLine($"ID: {book.BID}, Name: {book.BName}, Author: {book.Author}, Available Copies: {book.TotalCopies - book.BorrowedCopies}");
            }
        }

        public static void AddBook(ApplicationDbContext context)
        {
            var bookRepo = new BookRepository(context);
            var categoryRepo = new CategoryRepository(context);

            Console.WriteLine("Enter Book Name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter Author:");
            string author = Console.ReadLine();

            Console.WriteLine("Enter Total Copies:");
            int totalCopies = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Copy Price:");
            decimal copyPrice = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Enter Borrowing Period (in days):");
            int borrowingPeriod = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Category Name:");
            string categoryName = Console.ReadLine();

            var category = categoryRepo.GetByName(categoryName);

            if (category != null)
            {
                var book = new Book
                {
                    BName = name,
                    Author = author,
                    TotalCopies = totalCopies,
                    BorrowedCopies = 0,
                    CopyPrice = copyPrice,
                    AllowedBorrowingPeriod = borrowingPeriod,
                    CID = category.CID
                };

                bookRepo.Insert(book);
                category.NumberOfBooks++;
                context.SaveChanges();

                Console.WriteLine("Book added successfully!");
            }
            else
            {
                Console.WriteLine("Category not found.");
            }
        }

        public static void UpdateBook(ApplicationDbContext context)
        {
            var bookRepo = new BookRepository(context);

            Console.WriteLine("Enter Book ID to update:");
            int bookId = int.Parse(Console.ReadLine());

            var book = bookRepo.GetById(bookId);

            if (book != null)
            {
                Console.WriteLine("Enter new Book Name:");
                book.BName = Console.ReadLine();

                Console.WriteLine("Enter new Author:");
                book.Author = Console.ReadLine();

                Console.WriteLine("Enter new Total Copies:");
                book.TotalCopies = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter new Copy Price:");
                book.CopyPrice = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Enter new Borrowing Period:");
                book.AllowedBorrowingPeriod = int.Parse(Console.ReadLine());

                bookRepo.UpdateById(bookId, book);

                Console.WriteLine("Book updated successfully!");
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }

        public static void DeleteBook(ApplicationDbContext context)
        {
            var bookRepo = new BookRepository(context);

            Console.WriteLine("Enter Book ID to delete:");
            int bookId = int.Parse(Console.ReadLine());

            var book = bookRepo.GetById(bookId);

            if (book != null)
            {
                bookRepo.DeleteById(bookId);
                Console.WriteLine("Book deleted successfully!");
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }

        // User Mode
        public static void UserMode(ApplicationDbContext context)
        {
            Console.WriteLine("Enter User Name:");
            string Uname = Console.ReadLine();
            Console.WriteLine("Enter Passcode:");
            string passcode = Console.ReadLine();

            var userRepo = new UserRepository(context);
            var user = userRepo.GetByName(Uname); 

            if (user != null && user.Passcode == passcode) 
            {
                Console.WriteLine("Welcome User!");

                bool userExit = false;
                while (!userExit)
                {
                    Console.WriteLine("1. View Books");
                    Console.WriteLine("2. Borrow Book");
                    Console.WriteLine("3. Return Book");
                    Console.WriteLine("4. Logout");
                    var userChoice = Console.ReadLine();

                    switch (userChoice)
                    {
                        case "1":
                            ViewBooks(context);
                            break;
                        case "2":
                            BorrowBook(context, user);
                            break;
                        case "3":
                            ReturnBook(context, user);
                            break;
                        case "4":
                            userExit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice, try again.");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid username or passcode.");
            }
        }
        static void Registration(UserRepository userRepository)
        {
            Console.WriteLine("\n=== User Registration ===");

            Console.Write("Enter Username: ");
            string userName = Console.ReadLine();

            Console.Write("Enter Gender: ");
            string gender = Console.ReadLine();

            Console.Write("Enter Passcode: ");
            string passcode = Console.ReadLine();

            // Validate inputs
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(gender) || string.IsNullOrWhiteSpace(passcode))
            {
                Console.WriteLine("Error: All fields are required.\n");
                return;
            }

            
            User newUser = new User
            {
                UName = userName,
                Gender = gender,
                Passcode = passcode
            };

            
            string result = userRepository.RegisterUser(newUser);
            Console.WriteLine($"{result}\n");
        }

        // User Actions
        public static void BorrowBook(ApplicationDbContext context, User user)
        {
            var bookRepo = new BookRepository(context);
            var borrowingRepo = new BorrowingRepository(context);

            Console.WriteLine("Enter Book Name to borrow:");
            string bookName = Console.ReadLine();

            var book = bookRepo.GetByName(bookName);

            if (book != null && book.TotalCopies - book.BorrowedCopies > 0)
            {
                var borrowing = new Borrowing
                {
                    UserId = user.UID,
                    BookId = book.BID,
                    BorrowingDate = DateTime.Now,
                    PredictedReturnDate = DateTime.Now.AddDays(book.AllowedBorrowingPeriod),
                    IsReturned = false
                };

                borrowingRepo.Insert(borrowing);

                book.BorrowedCopies++;
                context.SaveChanges();

                Console.WriteLine("Book borrowed successfully!");
            }
            else
            {
                Console.WriteLine("Sorry, the book is not available.");
            }
        }

        public static void ReturnBook(ApplicationDbContext context, User user)
        {
            var borrowingRepo = new BorrowingRepository(context);

            Console.WriteLine("Enter Borrowing ID to return:");
            int borrowingId = int.Parse(Console.ReadLine());

            var borrowing = borrowingRepo.GetById(borrowingId);

            if (borrowing != null && borrowing.UserId == user.UID && !borrowing.IsReturned)
            {
                Console.WriteLine("Enter Rating (1 to 5):");
                int rating = int.Parse(Console.ReadLine());

                borrowingRepo.ReturnBook(borrowingId, DateTime.Now, rating);
                Console.WriteLine("Book returned successfully!");
            }
            else
            {
                Console.WriteLine("Invalid borrowing ID or the book is already returned.");
            }
        }
    }
}

