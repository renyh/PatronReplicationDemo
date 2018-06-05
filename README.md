# PatronReplicationDemo
实现 卡中心 到 dp2系统 账户信息同步
1. dp2系统中账户信息内容为 XML

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
| readerType | 读者类别 |
| createDate | 创建日期，内容为`RFC 1123`格式 |
| expireDate | 失效日期，内容为`RFC 1123`格式 |
| name | 读者姓名 |
| gender | 读者性别 |
| dateOfBirth | 出生日期，内容为`RFC 1123`格式 |
| idCardNumber | 身份证号 |
| department | 单位部门 |
| post | 职务 |
| address | 地址 |
| tel | 电话 |
| email | 电子邮箱 |
