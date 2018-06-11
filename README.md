# PatronReplicationDemo
实现 卡中心 到 dp2系统 账户信息同步
# 实现数据同步接口
需要补充完善 [文件](https://github.com/paopaofeng/PatronReplicationDemo/blob/master/PatronReplicationDemo/CardCenterServer.cs) 下`GetPatronRecords()`函数。该函数定义如下：
```
        public int GetPatronRecords(ref string strPosition, 
            out string[] records, 
            out string strError)
```
其中返回值有：

- `-1`：出错
- `0`：正常获得一批记录，但是尚未获得全部
- `1`：正常获得最后一批记录


-strPosition

`ref`字符串类型参数，表示获取记录的起始位置，第一次调用时为空`""`，表示从第`一`条开始取数据，函数结束时，需为此参数赋值，表示返回下一次获取数据时的位置。

- records

读者`XML`记录字符串数组。读者记录中的某些字段卡中心可能缺乏对应字段，那么需要在`XML`记录中填入`<元素名 dprms:missing />`，这样不至于引起同步时图书馆读者库中的这些字段被清除。
## dp2系统中账户信息内容 XML 记录体

```
<root>
  <barcode>P0001</barcode> 
  <cardNumber>C0001</cardNumber> 
  <state>注销</state> 
  <readerType>本科生</readerType> 
  <createDate>Tue, 05 Jun 2018 00:00:00 +0800</createDate> 
  <expireDate>Wed, 06 Jun 2018 00:00:00 +0800</expireDate> 
  <name>姓名</name> 
  <gender>男</gender> 
  <dateOfBirth>Tue, 05 Jun 2018 00:00:00 +0800</dateOfBirth> 
  <idCardNumber>130</idCardNumber> 
  <department>单位</department> 
  <post>职务</post> 
  <address>地址</address> 
  <tel>电话</tel> 
  <email>email:</email> 
</root>
```
### 字段解释
|  字段名   |  注释  |
|:----------|---------:|
| barcode | 读者证条码号，也是读者标识，具备唯一性|
| state | 读者状态 |
| readerType | 读者类别，如果卡中心不具备可由图书馆分配 |
| createDate | 创建日期，内容为`RFC 1123`格式 |
| expireDate | 失效日期，内容为`RFC 1123`格式 |
| name | 读者姓名 |
| gender | 读者性别，内容为“男”或“女”或空 |
| dateOfBirth | 出生日期，内容为`RFC 1123`格式 |
| idCardNumber | 身份证号 |
| department | 单位部门 |
| post | 职务 |
| address | 地址 |
| tel | 电话 |
| email | 电子邮箱 |
