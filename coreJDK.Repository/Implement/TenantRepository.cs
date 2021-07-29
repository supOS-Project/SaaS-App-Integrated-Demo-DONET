using coreJDK.Model;
using Dapper;
using System.Data;
using System.Text;

namespace coreJDK.Repository.Implement
{
    public class TenantRepository : BaseRepository<TenantModel>, ITenantRepository
    {

        #region 获取租户详情
        /// <summary>
        /// 获取租户详情
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public TenantModel GetTenantModel(string tenantId)
        {
            using (IDbConnection conn = DbDapper.GetMySqlConnection())
            {
                StringBuilder query = new StringBuilder();

                query.AppendLine("select tenant_id as tenantId,instance_id as instanceId,instance_name as instanceName," +
                    "start_time as startTime,end_time as endTime,app_id as appId,region,status from sys_tenant " +
                    (!string.IsNullOrEmpty(tenantId) ? "where tenant_id = @tenantId" : ""));
                return conn.QueryFirstOrDefault<TenantModel>(query.ToString(), new { tenantId });
            }
        }
        #endregion

        #region 添加租户
        /// <summary>
        /// 添加租户
        /// </summary>
        /// <param name="tenantModel"></param>
        /// <returns></returns>
        public bool AddTenant(TenantModel tenantModel)
        {
            using (IDbConnection conn = DbDapper.GetMySqlConnection())
            {
                StringBuilder insertSql = new StringBuilder();
                insertSql.AppendLine("insert into sys_tenant");
                insertSql.AppendLine("(tenant_id,instance_id,instance_name,start_time,end_time,app_id,region,status)");
                insertSql.AppendLine("value");
                insertSql.AppendLine("(@tenantId,@instanceId,@instanceName,@startTime,@endTime,@appId,@region,@status)");
                return conn.Execute(insertSql.ToString(), tenantModel) > 0;
            }
        }
        #endregion

        #region 修改租户时间 
        /// <summary>
        /// 修改租户时间
        /// </summary>
        /// <param name="tenantModel"></param>
        /// <returns></returns>
        public bool UpdateTenantTimeByTenantId(TenantModel tenantModel)
        {
            using (IDbConnection conn = DbDapper.GetMySqlConnection())
            {
                const string updateSql = @"UPDATE sys_tenant SET end_time = @endTime WHERE tenant_id=@tenantId";
                return conn.Execute(updateSql, tenantModel) > 0;
            }
        }
        #endregion

        #region 修改租户的状态
        /// <summary>
        /// 修改租户的状态
        /// </summary>
        /// <param name="tenantModel"></param>
        /// <returns></returns>
        public bool UpdateTenantStatusByTenantId(TenantModel tenantModel) {
            using (IDbConnection conn = DbDapper.GetMySqlConnection())
            {
                StringBuilder updateSql = new StringBuilder();
                updateSql.AppendLine("update sys_tenant set status = @status where tenant_id=@tenantId");
                return conn.Execute(updateSql.ToString(), tenantModel) > 0;
            }
        }
        #endregion

    }
}
