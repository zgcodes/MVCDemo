﻿using MVCDemo.DAL;
using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCDemo.Repositories
{
    public class SysUserRepository:ISysUserRepository
    {
        protected AccountContext db = new AccountContext();

        //添加用户

        public void Add(SysUser sysUser)
        {
            db.SysUsers.Add(sysUser);
            db.SaveChanges();
        }

        //删除用户

        public bool Delete(int id)
        {
            var delSysUser = db.SysUsers.Find(id);
            if (delSysUser != null)
            {
                db.SysUsers.Remove(delSysUser);
                db.SaveChanges();
                return true;
            }

            else
            {
                return false;
            }
        }

        public bool Edit(SysUser sysUser)
        {
            db.Entry(sysUser).State = EntityState.Modified;
            return db.SaveChanges()>0;
        }

        //查询所有用户

        public IQueryable<SysUser> SelectAll()
        {

            return db.SysUsers;

        }

        //通过用户ID询用户

        public SysUser Select(int id)
        {
            SysUser sysUser = db.SysUsers.Find(id);
            //var sysUser1 = from u in db.SysUsers//linq表达式
            //               where u.ID == id
            //               select u;
            //string query = "select * from SysUser where ID = @id";//使用sql
            //SqlParameter[] paras = new SqlParameter[]{
            //new SqlParameter("@id",id)
            //};
            //SysUser sysUser = db.SysUsers.SqlQuery(query, paras).SingleOrDefault();
            return sysUser;

        }

        //通过用户名查询用户

        public SysUser SelectByName(string userName)
        {

            return db.SysUsers.FirstOrDefault(u => u.UserName == userName);

        }
    }
}