using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        [HttpGet] //获取用户数量
        public ActionResult Get()
        {
            return Json(DAL.Comment.Instance.GetCount());
        }
        [HttpGet("id")] //获取用户数量
        public ActionResult Get(int id)
        {
            return Json(DAL.Comment.Instance.GetWorkCount(id));
        }
        //8
        [HttpPost("page")] //获取用户数量
        public ActionResult getpage ([FromBody] Model.Page page )
        {
            var result = DAL.Comment.Instance.GetPage(page);
            if(result.Count() == 0)
                return Json(Result.Err("返回记录数为0 "));
            else
                return Json(Result.Ok(result));
        }
        [HttpPost("workPage")] //获取用户数量
        public ActionResult getWorkPage([FromBody] Model.Page page)
        {
            var result = DAL.Comment.Instance.GetWorkPage(page);
            if (result.Count() == 0)
                return Json(Result.Err("返回记录数为0 "));
            else
                return Json(Result.Ok(result));
        }
        //11
        public ActionResult Delete(int id)
        {
            try
            {
                var n = DAL.Comment.Instance.Delete(id);
                if (n != 0)
                    return Json(Result.Ok("删除成功"));
                else
                    return Json(Result.Err("commentId不存在"));
            }
            catch (Exception ex)
            {
                return Json(Result.Err(ex.Message));
            }
        }
        [HttpPost]
        public ActionResult Post([FromBody]Model.Comment comment)
        {
            comment.commentTime = DateTime.Now;
            try
            {
                int n = DAL.Comment.Instance.Add(comment);
                return Json(Result.Ok("发表评论成功", n));
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("foreign key"))
                    if (ex.Message.ToLower().Contains("username"))
                        return Json(Result.Err("合法用户才能添加记录"));
                    else
                        return Json(Result.Err("评论所属作品不存在"));
                else if (ex.Message.ToLower().Contains("null")
                     return Json(Result.Err("评论内容、作品Id、用户名不能为空"));
                else
                    return Json(Result.Err(ex.Message));
            }
        }
    }
    
}
        
