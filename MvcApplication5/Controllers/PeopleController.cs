using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication5.Models;

namespace MvcApplication5.Controllers
{
    public class PeopleController : Controller
    {
        private int _index = 0;

        public void Foobar()
        {
            _index++;
            Response.Write("<h1>" + _index + "</h1>");
        }

        public ActionResult Index()
        {
            var db = new PeopleDb(@"Data Source=.\sqlexpress;Initial Catalog=Food;Integrated Security=True");
            var ppl = db.GetAll();
            return View(ppl);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(string name, string address, string nopoint)
        {
            var db = new PeopleDb(@"Data Source=.\sqlexpress;Initial Catalog=Food;Integrated Security=True");
            Customer c = new Customer();
            c.Name = name;
            c.Address = address;
            db.Add(c);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int personId)
        {
            var db = new PeopleDb(@"Data Source=.\sqlexpress;Initial Catalog=Food;Integrated Security=True");
            Customer c = db.FindById(personId);
            return View(c);
        }

        [HttpPost]
        public ActionResult Edit(string name, string address, int customerId)
        {
            var db = new PeopleDb(@"Data Source=.\sqlexpress;Initial Catalog=Food;Integrated Security=True");
            Customer c = new Customer();
            c.Name = name;
            c.Address = address;
            c.Id = customerId;
            db.Update(c);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var db = new PeopleDb(@"Data Source=.\sqlexpress;Initial Catalog=Food;Integrated Security=True");
            db.Delete(id);
            return RedirectToAction("Index");
        }

        private string GenerateRandomString()
        {
            Random rnd = new Random();
            List<char> chars = new List<char>();
            for (int i = 0; i <= 20; i++)
            {
                chars.Add((char)rnd.Next(65, 85));
            }
            return new string(chars.ToArray());
        }

    }
}
