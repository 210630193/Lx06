using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DAL
{
    public class Comment
    {
        private Comment() { }
        private static Comment _instance = new Comment();
        public static Comment Instance
        {
            get
            {
                return _instance;
            }
        }

        string cns = AppConfigurtaionServices.Configuration.GetConnectionString("cns");
        public Model.Comment GetModel(int id)
        {
            using (IDbConnection cn = new MySqlConnection(cns))
            {
                string sql = "select * from Comment where commentid=@id";
                return cn.QueryFirstOrDefault<Model.Comment>(sql, new { id = id });

            }
        }
        public IEnumerable<Model.Comment> GetAll()
        {
            using (IDbConnection cn = new MySqlConnection(cns))
            {
                string sql = "select * from Commment";
                return cn.Query<Model.Comment>(sql);
            }
        }
    }

}
//4
public interface GetCount()
    {
    using (IDbConnection cn = new MySqlConnection(cns))
    {
        string sql = "select count(1) from Comment";
        return cn.ExecuteScalar<int>(sql);
    }
}
public IEnumerable<Model.CommentNo> GetPage(Model.Page page)
{
    using (IDbConnection cn = new MySqlConnection(cns))
    {
        string sql = "with a as(select row_number() over(order by commentTime desc) as num, Comment.*,workName from Comment join workinfo on Comment.workId=workinfo.workId)";
        sql += "select* from a where num between (@pageIndex-1)*pageSize+1 and @pageIndex@pageSize";
        return cn.Query<Model.CommentNo>(sql,page);
    }
}
public interface GetWorkCount( int id)
    {
    using (IDbConnection cn = new MySqlConnection(cns))
    {
        string sql = "select count(1) from Comment where workId=@workId";

        return cn.ExecuteScalar<int>(sql, new { workId = id });
    }
}
public IEnumerable<Model.CommentNo> GetWorkPage(Model.Page page)
{
    using (IDbConnection cn = new MySqlConnection(cns))
    {
        string sql = "with a as(select row_number() over(order by commentTime desc) as num, Comment from Comment  where Comment.workId=@workinfo.workId)";

        sql += "select* from a where num between (@pageIndex-1)*pageSize+1 and @pageIndex@pageSize";
        return cn.Query<Model.CommentNo>(sql, page);
    }
}
    //5
    public int Add(Model.Comment comment)
    {
        using (IDbConnection cn = new MySqlConnection(cns))
    {
        string sql = "insert into Comment(commentID,workId,userName,CommentContent,CommentTime)"+
            "values(@commentID,@wordId,@userName,@CommentContent,@CommentTime");"
        sql += "SELECT @@IDENTITY";
        return cn.ExecuteScalar<int>(Sql, comment);
    }
    }
public int Update(Model.Comment comment)
{
    using (IDbConnection cn = new MySqlConnection(cns))
    {
        string sql = "update Comment set CommentContent=@CommentContent,CommentTime =@CommentContext where commentId=@commentID)";
       
        return cn.Execute<int>(sql, comment);
    }
    }
public int Delete(int id)
{
    using (IDbConnection cn = new MySqlConnection(cns))
    {
        string sql = "deletefrom Comment  where commentId=@commentID)";

        return cn.Execute(sql,new { id = id });
    }
}

