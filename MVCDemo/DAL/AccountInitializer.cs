using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCDemo.DAL
{
    public class AccountInitializer : DropCreateDatabaseIfModelChanges<AccountContext>
    {
        //迁移模式：1、每次执行update-databse指令时都会执行Seed方法（好像没用）
        //非迁移模式：2、运行时，如果数据模型变化，会执行Seed方法
        protected override void Seed(AccountContext context)
        {
            var sysUsers = new List<SysUser>
            {
                new SysUser{UserName="Tom1",Email="Tom@qq.com",Password="1"},
                new SysUser{UserName="Jerry1",Email="Jerry@qq.com",Password="2"}
            };
            sysUsers.ForEach(s => context.SysUsers.Add(s));
            context.SaveChanges();

            var sysRoles = new List<SysRole>
            {
                new SysRole{RoleName="Administrators",RoleDesc="Administrtors have full authorization to perform system administration."},
                new SysRole{RoleName="General Users",RoleDesc="General Users can access the shared data they are authorized for."}
            };
            sysRoles.ForEach(s => context.SysRoles.Add(s));
            context.SaveChanges();
        }
    }
}