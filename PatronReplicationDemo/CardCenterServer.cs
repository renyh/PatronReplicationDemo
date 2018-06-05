using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DigitalPlatform.Interfaces;

namespace PatronReplicationDemo
{
    public class CardCenterServer : MarshalByRefObject, ICardCenter
    {
        // 获得若干读者记录,#### 分批返回 ####
        // parameters:
        //      strPosition 第一次调用前，需要将此参数的值清为空
        //      records 读者XML记录字符串数组。注：读者记录中的某些字段卡中心可能缺乏对应字段，
        //              那么需要在 XML 记录中填入 <元素名 dprms:missing />，这样不至于造成同步时图书馆读者库中的这些字段被清除。
        //              至于读者借阅信息等字段，则不必操心
        // return:
        //      -1  出错
        //      0   正常获得一批记录，但是尚未获得全部
        //      1   正常获得最后一批记录
        public int GetPatronRecords(ref string strPosition, 
            out string[] records, 
            out string strError)
        {
            records = null;
            strError = "";

            return 0;
        }


        // ################### 以下两个接口可不实现

        // 获得一条读者记录
        // parameters:
        //      strID   读者记录标识符号。用什么字段作为标识，Client和Server需要另行约定
        //      strRecord   读者XML记录
        //                  注：读者记录中的某些字段卡中心可能缺乏对应字段，那么需要在XML记录中填入
        //                  <元素名 dprms:missing />，这样不至于造成同步时图书馆读者库中的这些字段被清除。至于读者借阅信息等字段，则不必操心
        // return:
        //      -1  出错(调用出错等特殊情况)
        //      0   读者记录不存在(读者记录正常性不存在)
        //      1   成功返回读者记录
        public int GetPatronRecord(string strID, 
            out string strRecord, 
            out string strError)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 从卡中心扣款
        /// </summary>
        /// <param name="strRecord">账户标识，一般为读者 barcode </param>
        /// <param name="strPriceString">扣款金额字符串,单位：元。一般为类似“CNY12.00”这样的形式</param>
        /// <param name="strPassword">账户密码</param>
        /// <param name="strRest">扣款后的余额，单位：元，格式为 货币符号+金额数字部分，形如“CNY100.00”标识人民币 100元</param>
        /// <param name="strError">错误信息</param>
        /// <returns>
        /// <para>-2  密码不正确</para>
        /// <para>-1  出错(调用出错等特殊原因)</para>
        /// <para>0   扣款不成功(因为余额不足等普通原因)。注意 strError 中应当返回不成功的原因</para>
        /// <para>1   扣款成功</para>
        /// </returns>
        public int Deduct(string strRecord, 
            string strPriceString, 
            string strPassword, 
            out string strRest, 
            out string strError)
        {
            throw new NotImplementedException();
        }
    }
}
