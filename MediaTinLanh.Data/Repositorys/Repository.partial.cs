using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MediaTinLanh.Data
{
	// Add custom code inside partial class

    public partial class Repository<TEntity> where TEntity : class, new()
    {
        internal DbContext context;
        internal DbSet<TEntity> dbSet;

        public Repository(DbContext context=null)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Lấy đối tượng bằng Id.
        /// </summary>
        /// <param name="id">Id</param>
        public virtual TEntity Single(int? id)
        {
            return dbSet.Find(id);
        }

        ///// <summary>
        ///// Lấy đối tượng theo điều kiện where
        ///// </summary>
        ///// <param name="where">where: "Country=@0" hoặc var where = "Country = @0 OR LastName=@1" hoặc where: "Id IN(1,2,3,4)"  </param>
        ///// <param name="parms">parms: "USA" hoặc var parms = new object[] { "USA", "Smith" } </param>
        //public virtual T Single(string where = null, params object[] parms)
        //{
        //    return t.Single(where, parms);
        //}
        ///// <summary>
        ///// Lấy đối tượng theo theo danh sách Id
        ///// </summary>
        ///// <param name="ids">Số "1,2,3,4" hoặc chuổi "'1','2','3'"</param>
        ///// <returns></returns>
        //public virtual IEnumerable<T> All(string ids)
        //{
        //    return t.All(ids);
        //}
        /// <summary>
        ///Lấy danh sách theo điều kiện, sắp xếp, lấy top
        /// </summary>
        /// <param name="where">where: "Country=@0" hoặc var where = "Country = @0 OR LastName=@1" hoặc where: "Id IN(1,2,3,4)"  </param>
        /// <param name="orderBy">orderBy: "Email Asc"</param>
        /// <param name="top">10</param>
        /// <param name="parms">parms: "USA" hoặc var parms = new object[] { "USA", "Smith" } </param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> All()
        {
            return dbSet.ToList();
        }
        ///// <summary>
        ///// Lấy danh sách có phân trang trả về tổng số dòng
        ///// </summary>
        ///// <param name="totalRows">out totalRows</param>
        ///// <param name="where">where: "Country=@0" hoặc var where = "Country = @0 OR LastName=@1" hoặc where: "Id IN(1,2,3,4)"  </param>
        ///// <param name="orderBy">orderBy: "Email Asc"</param>
        ///// <param name="page">page:4</param>
        ///// <param name="pageSize">pageSize:20</param>
        ///// <param name="parms">parms: "USA" hoặc var parms = new object[] { "USA", "Smith" } </param>
        ///// <returns></returns>
        //public virtual IEnumerable<T> Paged(out int totalRows, string where = null, string orderBy = null, int page = 0, int pageSize = 20, params object[] parms)
        //{
        //    return t.Paged(out totalRows, where, orderBy, page, pageSize, parms);
        //}
        public virtual TEntity Insert(TEntity t)
        {
            return dbSet.Add(t);
        }
        public virtual void Update(TEntity t)
        {
            dbSet.Attach(t);
            context.Entry(t).State = EntityState.Modified;
        }
        public virtual void Delete(TEntity t)
        {
            if (context.Entry(t).State == EntityState.Detached)
            {
                dbSet.Attach(t);
            }
            dbSet.Remove(t);
        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="operation">string sql= select a.FirstName+ ' ' + a.LastName from [user] a inner join artist b on a.Id = b.CreatedBy where a.Id = @0</param>
        ///// <param name="column"></param>
        ///// <param name="where">where: "Country=@0" hoặc var where = "Country = @0 OR LastName=@1" hoặc where: "Id IN(1,2,3,4)"  </param>
        ///// <param name="parms">parms: "USA" hoặc var parms = new object[] { "USA", "Smith" } </param>
        ///// <returns></returns>
        //public virtual object Scalar(string operation, string column, string where = null, params object[] parms)
        //{
        //    return t.Scalar(operation, column, where, parms);
        //}
        /// <summary>
        /// Đếm số lượng theo điều kiện
        /// </summary>
        /// <param name="where">where: "Country=@0" hoặc var where = "Country = @0 OR LastName=@1" hoặc where: "Id IN(1,2,3,4)"  </param>
        /// <param name="parms">parms: "USA" hoặc var parms = new object[] { "USA", "Smith" } </param>
        /// <returns>int</returns>
        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.Count(predicate);
        }
        /// <summary>
        /// Lấy giá trị lớn nhất
        /// </summary>
        /// <param name="column">"Id"</param>
        /// <param name="where">where: "Country=@0" hoặc var where = "Country = @0 OR LastName=@1" hoặc where: "Id IN(1,2,3,4)"  </param>
        /// <param name="parms">parms: "USA" hoặc var parms = new object[] { "USA", "Smith" } </param>
        /// <returns></returns>
        public virtual object Max(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.Max(predicate);
        }
        /// <summary>
        /// Lấy giá trị nhỏ nhất
        /// </summary>
        /// <param name="column">"Id"</param>
        /// <param name="where">where: "Country=@0" hoặc var where = "Country = @0 OR LastName=@1" hoặc where: "Id IN(1,2,3,4)"  </param>
        /// <param name="parms">parms: "USA" hoặc var parms = new object[] { "USA", "Smith" } </param>
        /// <returns></returns>

        public virtual object Min(Expression<Func<TEntity, bool>> predicate)
        {
            return dbSet.Min(predicate);
        }
        /// <summary>
        /// Tính tổng
        /// </summary>
        /// <param name="column">"TotalAmount"</param>
        /// <param name="where">where: "Country=@0" hoặc var where = "Country = @0 OR LastName=@1" hoặc where: "Id IN(1,2,3,4)"  </param>
        /// <param name="parms">parms: "USA" hoặc var parms = new object[] { "USA", "Smith" } </param>
        /// <returns></returns>
        public virtual object Sum(Expression<Func<TEntity, int>> selector)
        {
            return dbSet.Sum(selector);
        }
        /// <summary>
        /// Lấy dữ liệu theo câu truy vấn sql
        /// </summary>
        /// <param name="sql">string sql = @"SELECT Country, COUNT(Id) AS Number FROM[User] GROUP BY Country ORDER BY Number DESC"; </param>
        /// <param name="parms">parms: "USA" hoặc var parms = new object[] { "USA", "Smith" } </param>
        public virtual IEnumerable<TEntity> Query(string sql, params object[] parms)
        {
            return dbSet.SqlQuery(sql, parms);
        }
        ///// <summary>
        ///// Thực thi câu truy vấn sql không cần trả về dữ liệu
        ///// </summary>
        ///// <param name="sql">string sql = "DELETE [User] WHERE TotalOrders=0";</param>
        ///// <param name="parms">parms: "USA" hoặc var parms = new object[] { "USA", "Smith" } </param>
        //public virtual void Execute(string sql, params object[] parms)
        //{
        //    t.Execute(sql, parms);
        //}
    }
}
