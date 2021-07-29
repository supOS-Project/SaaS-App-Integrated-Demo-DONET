using System;
using System.Collections.Generic;
using System.Text;

namespace coreJDK.Model
{
    public class LzResultModel<T>
    {
        /// <summary>
        /// 返回码
        /// </summary>
        public int code { get; set; }

        /// <summary>
        ///返回信息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 返回具体数据
        /// </summary>
        public T data { get; set; }
    }
}
