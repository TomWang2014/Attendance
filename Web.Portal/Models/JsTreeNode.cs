//------------------------------------------------------------
// <copyright file="JsTreeNode.cs" company="zjzx">
//   ©2015 中教在线 版权所有
// </copyright>
// <author>李天赐</author>
// <date>2016/11/9 13:25:14</date>
// <summary>
//  主要功能有：
//  
// </summary>
//------------------------------------------------------------

namespace Attendance.Web.Portal.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// 用来传递给jstree的JSON节点
    /// 注：为了省事。。这里的属性名和jstree规定的属性名一致）
    /// http://www.jstree.com/docs/json/
    /// </summary>
    public class JsTreeNode
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public JsTreeNode()
        {
            children = new List<JsTreeNode>();
        }

        /// <summary>
        /// 显示的文本
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// id， jstree拿这个来构建树，所有在一个jstree里面不能有相同的id的节点
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 子节点列表
        /// </summary>
        public dynamic children { get; set; }

        /// <summary>
        /// 扩展属性
        /// </summary>
        public List<string> exts { get; set; }

        /// <summary>
        /// link属性
        /// </summary>
        public object a_attr { get; set; }

        /// <summary>
        /// 节点的状态
        /// </summary>
        public state state { get; set; }

        /// <summary>
        /// 节点的类型
        /// </summary>
        public string type { get; set; }
    }

    /// <summary>
    /// 节点类型 
    /// </summary>
    public class state
    {
        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool selected { get; set; }

        /// <summary>
        /// 是否打开
        /// </summary>
        public bool opened { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool disabled { get; set; }
    }
}
