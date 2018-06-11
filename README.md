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

- records
返回读者记录内容，是一个数组，每次返回记录数量，即数组大小，由开发者决定。下一次`dp2Library`调用的时候，会用`strPosition`参数表示希望从何处开始获取下一批记录。每次函数返回的时候，都给出了`strPosition`返回值，注意这是个`ref`类型的参数，`in` `out` 都会起作用，上一次调用`dp2Library`会得到返回的一个`strPosition`，正好用到下一次调用，每次都有这个参数用来指定本次需要返回的开始位置。具体字符串里面放什么东西什么格式，由第三方自由发挥，达到循环获取直到全部记录都被获取的目的即可。
# 1. dp2系统中账户信息内容为 XML

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
