namespace MTDS.Common
{
    public class PageResult
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        /// <param name="message"></param>
        /// <param name="isSuccess"></param>
        public PageResult(string message, bool isSuccess)
        {
            Message = message;
            IsSuccess = isSuccess;
        }

        /// <summary>
        /// 返回错误结果
        /// </summary>
        /// <param name="message"></param>
        public PageResult(string message)
        {
            Message = message;
            IsSuccess = false;
        }

        /// <summary>
        /// 返回正确结果
        /// </summary>
        public PageResult()
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
    }
}
