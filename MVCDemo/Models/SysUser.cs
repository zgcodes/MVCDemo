using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCDemo.Models
{
    public class SysUser
    {
        public int ID { get; set; }

        [StringLength(10, ErrorMessage = "长度不能超过10个字")]
        public string UserName { get; set; }

        [StringLength(10, ErrorMessage = "长度不能超过10个字")]
        public string Email { get; set; }

        [StringLength(15,ErrorMessage="长度不能超过15个字")]
        public string Password { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }
        public virtual ICollection<SysUserRole> SysUserRoles { get; set; }
    }
}
