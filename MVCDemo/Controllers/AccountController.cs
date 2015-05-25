using MVCDemo.DAL;
using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDemo.Controllers
{
    public class AccountController : Controller
    {
        private AccountContext db = new AccountContext();

        public ActionResult Index()
        {
            return View(db.SysUsers);
        }

        public ActionResult Details(int id)
        {
            SysUser sysUser = db.SysUsers.Find(id);
            return View(sysUser);
        }

        #region 添加

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SysUser sysUser)
        {
            db.SysUsers.Add(sysUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

        #region 修改

        public ActionResult Edit(int id)
        {
            SysUser sysUser = db.SysUsers.Find(id);
            return View(sysUser);
        }

        [HttpPost]
        public ActionResult Edit(SysUser sysUser)
        {
            db.Entry(sysUser).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       #endregion

        #region 删除
        public ActionResult Delete(int id)
        {
            SysUser sysUser = db.SysUsers.Find(id);
            return View(sysUser);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SysUser sysUser = db.SysUsers.Find(id);
            db.SysUsers.Remove(sysUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

        public ActionResult Login()
        {
            ViewBag.LoginState = "登录前...";
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            string email = fc["inputEmail3"];
            string passWord = fc["inputPassword3"];
            var users = db.SysUsers.Where(m=>m.Email == email && m.Password == passWord);
            if (users.Count() > 0)
            {
                ViewBag.LoginState = "登录后...";
            }
            else {
                ViewBag.LoginState = "用户不存在";
            }
            
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}
