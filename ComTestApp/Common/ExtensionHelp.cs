using mshtml;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using ComTestApp.Entitys;
using System.Threading.Tasks;
using AllIn.Core.Util;

namespace ComTestApp.Common
{
    public static class ExtensionHelp
    {
        /// <summary>
        /// string转实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T ToDeserializeObject<T>(this string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }

        /// <summary>
        /// 实体转json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string ToSerializeObject<T>(this T o)
        {
            return JsonConvert.SerializeObject(o);
        }

        /// <summary>
        /// List排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <param name="field">字段</param>
        /// <param name="rule">DESC/ASC</param>
        /// <returns></returns>
        public static List<T> ListSort<T>(this List<T> o, string field, string rule = "")
        {
            if (rule.ToLower().Equals("asc") && rule.ToLower().Equals("desc") && !rule.IsEmpty())
            {
                throw new Exception("rule格式录入错误！");
            }
            try
            {
                o.Sort(delegate(T t1,T t2) 
                    {
                        Type t = typeof(T);
                        PropertyInfo pro = t.GetProperty(field);
                        return rule.ToLower().Equals("desc") ?
                            pro.GetValue(t1, null).ToString().CompareTo(pro.GetValue(t2, null).ToString()) :
                            pro.GetValue(t2, null).ToString().CompareTo(pro.GetValue(t1, null).ToString()) ;                            
                    });
                return o;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 计算两个日期的时间间隔
        /// </summary>
        /// <param name="DateTime1">第一个日期和时间</param>
        /// <param name="DateTime2">第二个日期和时间</param>
        /// <returns></returns>
        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            dateDiff = (ts.Days > 0 ? ts.Days.ToString() + "天" : "")
                + (ts.Hours > 0 ? ts.Hours.ToString() + "小时" : "")
                + (ts.Minutes > 0 ? ts.Minutes.ToString() + "分钟" : "")
                + (ts.Seconds > 0 ? ts.Seconds.ToString() + "秒" : "");
            return dateDiff;
        }

        /// <summary>
        /// 扩展调用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <returns></returns>
        public static BaseEntityHelper.BaseHelperModel BaseEntityHelp<T>(this T o)
        {
            return new BaseEntityHelper.BaseHelperModel();
        }

        #region 查询分页相关

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="orderFildes">order by No.BatchNumber</param>
        /// <param name="IsLike"></param>
        /// <returns></returns>
        public static List<T> SelectEntity<T>(T t, string orderFildes = "", bool IsLike = false)
        {
            string sqlStr;
            Dictionary<string, Object> parameters = GetProperties(t, IsLike);
            sqlStr = "select * from " + GetTableName<T>();
            if (parameters != null && parameters.Count > 0)
            {
                string whereStr = " where ";
                foreach (var item in parameters)
                {
                    if (whereStr.Contains("=") || whereStr.Contains("like"))
                    {
                        if (IsLike)
                        {
                            whereStr = whereStr + " and " + item.Key + " like @" + item.Key;
                        }
                        else
                        {
                            whereStr = whereStr + " and " + item.Key + " = @" + item.Key;
                        }
                    }
                    else
                    {
                        if (IsLike)
                        {
                            whereStr = whereStr + item.Key + " like @" + item.Key;
                        }
                        else
                        {
                            whereStr = whereStr + item.Key + " = @" + item.Key;
                        }
                    }
                }
                sqlStr = sqlStr + whereStr;
            }
            if (!orderFildes.IsEmpty()) sqlStr += " " + orderFildes;
            //LogHelper.ToLog("sql语句:" + sqlStr);
            return SelectDataBySqlString<T>(sqlStr, parameters);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="orderFildes"></param>
        /// <param name="IsLike"></param>
        /// <param name="PageNum"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public static List<T> SelectPageEntity<T>(T t, string orderFildes, bool IsLike, int PageNum, int PageSize)
        {
            string sqlStr;
            Dictionary<string, Object> parameters = GetProperties(t, IsLike);
            sqlStr = "select * from " + GetTableName<T>();
            if (parameters != null && parameters.Count > 0)
            {
                string whereStr = " where ";
                foreach (var item in parameters)
                {
                    string colapp = "";
                    if (item.Value.GetType().FullName.Equals("System.DateTime"))
                    {
                        colapp = item.Key.Contains("Start") ? " >= " : " <= ";
                    }
                    if (whereStr.Contains("=") || whereStr.Contains("like"))
                    {
                        if (IsLike)
                        {
                            whereStr = whereStr + " and " + item.Key + (colapp.IsEmpty() ? " like @" : colapp + " @") + item.Key;
                        }
                        else
                        {
                            whereStr = whereStr + " and " + item.Key + (colapp.IsEmpty() ? " = @" : colapp + " @") + item.Key;
                        }
                    }
                    else
                    {
                        if (IsLike)
                        {
                            whereStr = whereStr + item.Key + (colapp.IsEmpty() ? " like @" : colapp + " @") + item.Key;
                        }
                        else
                        {
                            whereStr = whereStr + item.Key + (colapp.IsEmpty() ? " = @" : colapp + " @") + item.Key;
                        }
                    }
                }
                sqlStr = sqlStr + whereStr;
            }
            if (!orderFildes.IsEmpty()) sqlStr += " " + orderFildes;
            if (PageSize > 0 && PageNum > 0) sqlStr = sqlStr + string.Format(" limit {0},{1}", (PageNum - 1) * PageSize, PageSize).ToString();
            return SelectDataBySqlString<T>(sqlStr, parameters);
        }

        private static List<T> SelectDataBySqlString<T>(string sqlStr, Dictionary<string, Object> parameters)
        {
            Console.WriteLine("sql语句:" + sqlStr);
            return (new BaseEntityHelper.SqLiteHelper()).ExecuteDataTable(sqlStr, parameters).ToSerializeObject().ToDeserializeObject<List<T>>();
        }

        #endregion

        private static Dictionary<string, Object> GetProperties<T>(T t, Boolean IsLike = false)
        {
            var ret = new Dictionary<string, object>();
            if (t == null) { return null; }
            PropertyInfo[] properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (properties.Length <= 0) { return null; }
            foreach (PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(t, null);
                if (!(value is System.DBNull) && value != null && !value.ToString().IsEmpty())//item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String")
                {
                    if (!value.ToString().Equals("0"))
                    {
                        if (IsLike)
                        {
                            if (value.GetType().Name.Equals("String"))
                            {
                                ret.Add(name, "%" + value + "%");
                            }
                        }
                        else
                        {
                            if (value.GetType().FullName.Equals("System.DateTime"))
                            {
                                LogHelper.ToLog("参数日期默认值：" + value.ToString());
                                LogHelper.ToLog("系统默认日期值：" + default(DateTime).ToString());
                                if (!value.ToString().Equals(default(DateTime).ToString()))
                                {
                                    ret.Add(name, value);
                                }
                            }
                            else
                            {
                                ret.Add(name, value);
                            }
                        }
                    }
                }
            }
            return ret;
        }

        private static string GetTableName<T>()
        {
            string tableName = string.Empty;
            Type objType = typeof(T);
            object[] objs = objType.GetCustomAttributes(typeof(CommonEnitityMappingAttribute), true);
            foreach (object obj in objs)
            {
                CommonEnitityMappingAttribute attr = obj as CommonEnitityMappingAttribute;
                if (attr != null)
                {
                    tableName = attr.TableName;//表名只有获取一次
                    break;
                }
            }
            return tableName;
        }

        /// <summary>
        /// 是否为Null或空或空白字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string str)
        {
            return String.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// 是否为数值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string str)
        {
            return Regex.IsMatch(str, @"^[+-]?/d*[.]?/d*$");
        }

        /// <summary>
        /// 是否为日期格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDate(this string str)
        {
            try
            {
                DateTime.Parse(str);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// 获取枚举项目名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetEnumName<T>(this string str)
        {
            return Enum.Parse(typeof(T), str).ToString();
        }

        /// <summary>
        /// 弹框提示内容格式化
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string AlertMsgTrim(this string str)
        {
            return str.Replace("Window", "").Replace("Content", "").Replace("Title", "").Replace("-", "").Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace("来自网页的消息", "").Replace("确定", "").Replace("[", "").Replace("]", "").Replace("不允许此页创建更多消息", "");
        }

        /// <summary>
        /// 获取枚举项目值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetEnumVal<T>(this string str)
        {
            return Convert.ToInt32(Enum.Parse(typeof(T), str)).ToString();
        }

        /// <summary>
        /// 判断某个项目是否存在于枚举中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsExistsEnum<T>(this string str)
        {
            return Enum.IsDefined(typeof(T), str);
        }


        /// <summary>
        /// 获取实体类字段及注解信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <returns></returns>
        public static List<FieldOfInfo> GetFieldInfo<T>(this T o)
        {
            List<FieldOfInfo> fieldInfos = new List<FieldOfInfo>();
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in properties)
            {
                object[] proDescrition = property.GetCustomAttributes(typeof(CommonEnitityMappingAttribute), true);
                if (proDescrition.Length > 0)
                {
                    CommonEnitityMappingAttribute cem = proDescrition[0] as CommonEnitityMappingAttribute;
                    if (cem != null)
                    {
                        fieldInfos.Add(new FieldOfInfo() { EntityName = property.Name, ColumnName = cem.ColumnName, Notes = cem.Notes, IsRequired = cem.IsRequired, IsDate = cem.IsDate, IsNum = cem.IsNum, SelectType = cem.SelectType, IsCheckBox = cem.IsCheckBox, VisibleForDv = cem.VisibleForDv });
                    }
                }
            }
            return fieldInfos;
        }

        /// <summary>
        /// 绑定数据到实体并返回实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T GetEntityForTbRow<T>(this DataRow dr, T t)
        {
            foreach (DataColumn Col in dr.Table.Columns)
            {
                foreach (var item in t.GetFieldInfo())
                {
                    if (item.Notes == Col.ColumnName)
                    {
                        object val;
                        switch (item.Notes)
                        {
                            case "是否有试用期":
                                val = dr[item.Notes].ToString().Equals("是");
                                break;
                            default:
                                val = dr[item.Notes];
                                break;
                        }
                        val = val is System.DBNull ? "" : val;
                        typeof(T).GetProperty(item.EntityName).SetValue(t, val.ToString().IsNumeric() ? int.Parse(val.ToString()) : val, null);
                        break;
                    }
                }
            }
            return t;
        }

    }
}
