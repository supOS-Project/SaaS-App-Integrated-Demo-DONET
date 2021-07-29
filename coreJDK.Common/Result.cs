namespace coreJDK.Common
{

    public struct Result
    {
        private int _code;
        public int code
        {
            get
            {
                return _code;
            }
        }

        private string _msg;
        public string msg
        {
            get
            {
                return _msg;
            }
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        public static Result Success(string msg)
        {
            return new Result { _code = 200, _msg = msg };
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        public static Result Warning(string msg)
        {
            return new Result { _code = 1, _msg = msg };
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        public static Result Error(string msg)
        {
            return new Result { _code = 500, _msg = msg };
        }
    }

    public struct Result<T>
    {
        private int _code;
        public int code
        {
            get
            {
                return _code;
            }
        }

        private string _msg;
        public string msg
        {
            get
            {
                return _msg;
            }
        }

        private T _data;
        public T data
        {
            get
            {
                return _data;
            }
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="data">返回数据</param>
        /// <returns></returns>
        public static Result<T> Success(string msg, T data)
        {
            return new Result<T> { _code = 200, _msg = msg, _data = data };
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="data">返回数据</param>
        /// <returns></returns>
        public static Result<T> Warning(string msg, T data)
        {
            return new Result<T> { _code = 1, _msg = msg, _data = data };
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="data">返回数据</param>
        /// <returns></returns>
        public static Result<T> Error(string msg, T data)
        {
            return new Result<T> { _code = 500, _msg = msg, _data = data };
        }
    }
}
