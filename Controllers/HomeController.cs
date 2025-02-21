using BookShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{
    public class HomeController : Controller
    {
        BookShopDB bookShopDB = new BookShopDB();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserLogin(User userInfo)
        {

            var username = userInfo.UserName;
            var password = userInfo.Password;

            var user = bookShopDB.Users
                .FirstOrDefault(u => u.UserName == username && u.Password == password);

            if (user != null)
            {


                var cookieOptions = new CookieOptions
                {
                    IsEssential = true // Ensure cookie works even without consent
                };

                Response.Cookies.Append("Username", user.UserName, cookieOptions);
                Response.Cookies.Append("UserId", Convert.ToString(user.Id), cookieOptions);
                Response.Cookies.Append("UserRole", user.Role, cookieOptions);


                ViewBag.Message = "Login successful!";
                if(user.Role == "Admin")
                {
                    return RedirectToAction("Admin");
                }
                else
                {

                    return RedirectToAction("Seller");
                }


            }
            else
            {
                // User not found - Login failed
                ViewBag.Message = "Invalid username or password.";
                return View();
            }
        }

        [HttpGet]
        public IActionResult LogoutUser()
        {
            Response.Cookies.Delete("Username");
            Response.Cookies.Delete("UserRole");
            Response.Cookies.Delete("UserId");

            return Redirect("/");
        }

        [HttpGet]
        public IActionResult Admin()
        {
            var username = Request.Cookies["Username"];
            var userrole = Request.Cookies["UserRole"];


            if (!string.IsNullOrEmpty(username) && userrole == "Admin")
            {
                var userSeller = bookShopDB.Users.Where( u => u.Role == "Seller").ToList();

                return View(userSeller);
            }
            else
            {
                return RedirectToAction("UserLogin");
            }

        }

        [HttpGet]
        public IActionResult newSeller()
        {
            var username = Request.Cookies["Username"];
            var userrole = Request.Cookies["UserRole"];


            if (!string.IsNullOrEmpty(username) && userrole == "Admin")
            {

                return View();
            }
            else
            {
                return RedirectToAction("UserLogin");
            }

        }

        [HttpPost]
        public IActionResult newSeller(User userInfo)
        {
            var username = Request.Cookies["Username"];
            var userrole = Request.Cookies["UserRole"];


            if (!string.IsNullOrEmpty(username) && userrole == "Admin")
            {
               bookShopDB.Users.Add(userInfo);
                bookShopDB.SaveChanges();
                return Redirect("/Home/admin");
            }
            else
            {
                return RedirectToAction("UserLogin");
            }

        }

        [HttpGet]
        public IActionResult editSeller(int id)
        {
            var username = Request.Cookies["Username"];
            var userrole = Request.Cookies["UserRole"];


            if (!string.IsNullOrEmpty(username) && userrole == "Admin")
            {

                var userSeller = bookShopDB.Users.Find(id);

                return View(userSeller);
            }
            else
            {
                return RedirectToAction("UserLogin");
            }

        }

        [HttpPost]
        public IActionResult editSeller(User userInfo)
        {
            var username = Request.Cookies["Username"];
            var userrole = Request.Cookies["UserRole"];


            if (!string.IsNullOrEmpty(username) && userrole == "Admin")
            {
                var userSeller = bookShopDB.Users.Find(userInfo.Id);
                userSeller.UserName = userInfo.UserName;
                userSeller.Email = userInfo.Email;
                userSeller.MobileNo = userInfo.MobileNo;
                userSeller.Password = userInfo.Password;

                bookShopDB.SaveChanges();

                 return Redirect("/Home/admin");
            }
            else
            {
                return RedirectToAction("UserLogin");
            }

        }

        [HttpGet]
        public IActionResult deleteSeller(int id)
        {
            var username = Request.Cookies["Username"];
            var userrole = Request.Cookies["UserRole"];


            if (!string.IsNullOrEmpty(username) && userrole == "Admin")
            {

                var userSeller = bookShopDB.Users.Find(id);
                bookShopDB.Users.Remove(userSeller);
                bookShopDB.SaveChanges();
                return Redirect("/home/admin");
            }
            else
            {
                return RedirectToAction("UserLogin");
            }

        }

        [HttpGet]
        public IActionResult Seller()
        {
            var username = Request.Cookies["Username"];
            var userrole = Request.Cookies["UserRole"];
                
            if (!string.IsNullOrEmpty(username) && userrole == "Seller")
            {
                var books = bookShopDB.Books.ToList();
                return View(books);
            }
            else
            {
                return RedirectToAction("UserLogin");
            }
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            var username = Request.Cookies["Username"];
            var userrole = Request.Cookies["UserRole"];

            if (!string.IsNullOrEmpty(username) && userrole == "Seller")
            {
                return View();
            }
            else
            {
                return RedirectToAction("UserLogin");
            }
        }

        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            var username = Request.Cookies["Username"];
            var userrole = Request.Cookies["UserRole"];

            if (!string.IsNullOrEmpty(username) && userrole == "Seller")
            {
                book.UserId = int.Parse(Request.Cookies["UserId"]); // Associate the book with the logged-in seller
                bookShopDB.Books.Add(book);
                bookShopDB.SaveChanges();
                return RedirectToAction("Seller");
            }
            else
            {
                return RedirectToAction("UserLogin");
            }
        }

        [HttpGet]
        public IActionResult EditBook(int id)
        {
            var username = Request.Cookies["Username"];
            var userrole = Request.Cookies["UserRole"];

            if (!string.IsNullOrEmpty(username) && userrole == "Seller")
            {
                var book = bookShopDB.Books.Find(id);
                if (book == null || book.UserId != int.Parse(Request.Cookies["UserId"]))
                {
                    return Unauthorized(); // Prevent editing books not owned by the seller
                }

                return View(book);
            }
            else
            {
                return RedirectToAction("UserLogin");
            }
        }

        [HttpPost]
        public IActionResult EditBook(Book book)
        {
            var username = Request.Cookies["Username"];
            var userrole = Request.Cookies["UserRole"];

            if (!string.IsNullOrEmpty(username) && userrole == "Seller")
            {
                var existingBook = bookShopDB.Books.Find(book.Id);

                if (existingBook != null && existingBook.UserId == int.Parse(Request.Cookies["UserId"]))
                {
                    existingBook.BookName = book.BookName;
                    existingBook.Author = book.Author;
                    existingBook.Category = book.Category;
                    existingBook.Price = book.Price;

                    bookShopDB.SaveChanges();
                }

                return RedirectToAction("Seller");
            }
            else
            {
                return RedirectToAction("UserLogin");
            }
        }

        [HttpGet]
        public IActionResult DeleteBook(int id)
        {
            var username = Request.Cookies["Username"];
            var userrole = Request.Cookies["UserRole"];

            if (!string.IsNullOrEmpty(username) && userrole == "Seller")
            {
                var book = bookShopDB.Books.Find(id);

                if (book != null && book.UserId == int.Parse(Request.Cookies["UserId"]))
                {
                    bookShopDB.Books.Remove(book);
                    bookShopDB.SaveChanges();
                }

                return RedirectToAction("Seller");
            }
            else
            {
                return RedirectToAction("UserLogin");
            }
        }


    }
}
