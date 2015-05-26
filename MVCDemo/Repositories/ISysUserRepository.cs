using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCDemo.Repositories
{
    public interface ISysUserRepository
    {
       
        //添加用户
        void Add(SysUser sysUser);

        //删除用户
        bool Delete(int id);

        //修改用户
        bool Edit(SysUser sysUser);

        //查询所有用户
        IQueryable<SysUser> SelectAll();

        //通过用户ID询用户
        SysUser Select(int id);

        //通过用户名查询用户
        SysUser SelectByName(string userName);
    }
}
