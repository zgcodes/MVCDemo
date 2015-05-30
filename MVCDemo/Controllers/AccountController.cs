using MVCDemo.DAL;
using MVCDemo.Models;
using MVCDemo.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace MVCDemo.Controllers
{
    public class AccountController : Controller
    {
        AccountContext db = new AccountContext();
        private ISysUserRepository _sysUserRepository;

        public AccountController(ISysUserRepository sysUserRepository)
        {
            _sysUserRepository=sysUserRepository;
        }


        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            var users = from u in db.SysUsers select u;//这里获取的是 IQueryable，var users =  db.SysUsers;获取的是集合
            //查询
            if (searchString != null)//查询参数不为null，表示点击了查询，页数是1
            {
                page = 1;
            }
            else {
                searchString = currentFilter;//没输查询条件，查询条件用上次查询的条件（可能是点击查询或排序或翻页，都是一样用上次条件）。
            }
            ViewBag.CurrentFilter = searchString;//记录本次查询条件

            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.UserName.Contains(searchString));
            }

            //排序
            ViewBag.CurrentSort = sortOrder;//记录当前升降序，初始值为null，导致降序，第二次为name_desc，导致升序 TODO
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";//继续下次CurrentSort的值，初始值为name_desc，第二次为""
            switch (sortOrder)//TODO
            { 
                case "name_desc":
                    users = users.OrderByDescending(m => m.UserName);
                break;
                default:
                users = users.OrderBy(m => m.UserName);
                break;
            }

            int pageSize = 3;
            int pageNumber = page??1;//当前页数

            return View(users.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int id)
        {
            SysUser sysUser = _sysUserRepository.Select(id);
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
            sysUser.CreateDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                _sysUserRepository.Add(sysUser);
                return RedirectToAction("Index");
            }
            return View();
        }

        #endregion

        #region 修改

        public ActionResult Edit(int id)
        {
            SysUser sysUser = _sysUserRepository.Select(id);
            return View(sysUser);
        }

        [HttpPost]
        public ActionResult Edit(SysUser sysUser)
        {
            _sysUserRepository.Edit(sysUser);
            return RedirectToAction("Index");
        }

       #endregion

        #region 删除
        public ActionResult Delete(int id)
        {
            SysUser sysUser = _sysUserRepository.Select(id);
            return View(sysUser);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _sysUserRepository.Delete(id);
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
