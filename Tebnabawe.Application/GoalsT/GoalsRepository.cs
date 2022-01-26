using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tebnabawe.Application.Bases;
using Tebnabawe.Data.Models;

namespace Tebnabawe.Application.GoalsT
{
        public class GoalsRepository : BaseRepository<Goals>
        {
            private DbContext EC_DbContext;

            public GoalsRepository(DbContext EC_DbContext) : base(EC_DbContext)
            {
                this.EC_DbContext = EC_DbContext;
            }
            #region CRUB

            public IEnumerable<Goals> GetAllGoals()
            {
                return GetAll()
                    .ToList();
            }

            public bool InsertGoal(Goals goal)
            {
                return Insert(goal);
            }
            public void UpdateGoal(Goals goal)
            {
                Update(goal);
            }
            public Goals GetGoalById(int id)
            {
                var goal = DbSet
                    .FirstOrDefault(g => g.Id == id);
                return goal;
            }
        public IEnumerable<Goals> GetGoalsByAbout(int aboutId)
        {
            var query = DbSet
                .Where(g => g.AboutId == aboutId);
            return query;
        }
        
        #endregion
    }
}