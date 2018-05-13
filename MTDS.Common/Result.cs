using System;

namespace MTDS.Common
{
    public class Result
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        /// <param name="message"></param>
        /// <param name="isSuccess"></param>
        public Result(string message, bool isSuccess)
        {
            Message = message;
            IsSuccess = isSuccess;
        }

        /// <summary>
        /// 返回错误结果
        /// </summary>
        /// <param name="message"></param>
        public Result(string message)
        {
            Message = message;
            IsSuccess = false;
        }

        /// <summary>
        /// 返回错误结果
        /// 从Exception中取最内层的错误消息
        /// </summary>
        /// <param name="ex"></param>
        public Result(Exception ex)
        {
            var thisException = ex;
            while (thisException.InnerException != null 
                && !thisException.InnerException.Message.IsEmpty())
            {
                thisException = thisException.InnerException;
            }
            Message = thisException.Message;
            IsSuccess = false;
        }

        /// <summary>
        /// 返回正确结果
        /// </summary>
        public Result()
        {
            Message = string.Empty;
            IsSuccess = true;
        }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 附加对象
        /// </summary>
        public object Tag { get; set; }
    }
}
