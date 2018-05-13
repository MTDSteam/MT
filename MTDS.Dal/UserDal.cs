using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTDS.Model;
namespace MTDS.Dal
{
    public class UserDal
    {
        MTDSEntities entity = new MTDSEntities();

        public List<User> GetList()
        {
            return entity.User.ToList();
        }

        public int Insert(User model)
        {
            entity.User.Add(model);
            return entity.SaveChanges();
        }

        public int Update(User model)
        {
            return 0;
        }

        public int Delete(User model)
        {
            entity.User.Remove(model);
            return entity.SaveChanges();
        }

    }
}
