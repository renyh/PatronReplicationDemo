# PatronReplicationDemo
该项目是 dp2系统（dp2Library） 从 卡中心 获得账户信息，并实现账户同步的中间件程序示例程序。相对于 dp2系统（dp2Library），该程序是服务器端，由 .Net 的 Remoting 技术实现，dp2系统（dp2Library）可使用命名管道协议访问，也可以通过 TCP/IP 协议访问。该程序启动后，等待 dp2系统（dp2Library）发起请求。在接收到获得账户信息的请求后，该程序通过实现 `GetPatronRecords()` 函数从卡中心获得账户信息，组织成 XML 格式（格式要求参考 **dp2系统中账户信息内容 XML 记录格式**） 字符串数组返回。

该项目需引用`DigitalPlatform.Interfaces`类库，参考[源码](https://github.com/DigitalPlatform/dp2/tree/master/DigitalPlatform.Interfaces)。

# 获得账户信息函数
需要补充完善 [文件](https://github.com/paopaofeng/PatronReplicationDemo/blob/master/PatronReplicationDemo/CardCenterServer.cs) 下`GetPatronRecords()`函数。该函数定义如下：
```
public int GetPatronRecords(ref string strPosition, 
    out string[] records, 
    out string strError)
```
其中返回值有：

- `-1`：出错，错误内容是 strError 的值。
- `0`：正常获得一批记录，但是尚未获得全部。
- `1`：正常获得最后一批记录。


- strPosition

`ref`字符串类型参数，表示获取记录的起始位置，第一次调用时为空`""`，表示从第`1`条开始取数据，函数结束时，需为此参数赋值，表示返回下一次获取数据时的位置。

- records

读者`XML`记录字符串数组。读者记录中的某些字段卡中心可能缺乏对应字段，那么需要在`XML`记录中填入`<元素名 dprms:missing />`，这样不至于引起同步时图书馆读者库中的这些字段被清除。

## dp2系统中账户信息内容 XML 记录格式

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

## 函数传入传出参数值与返回值关系对照

假设源数据有 1000 条，分批获取全部数据，每批次 100 条数据，获取数据过程中没有发生错误，那么参数与返回值之间对照关系如下表：

| strPosition 传入 | strPosition 返回 | 函数返回值 | strError |
|:----------|---------|---------|---------:|
| "" | 101 | 0 | "" |
| 101 | 201 | 0 | "" |
| 201 | 301 | 0 | "" |
| 301 | 401 | 0 | "" |
| 401 | 501 | 0 | "" |
| 501 | 601 | 0 | "" |
| 601 | 701 | 0 | "" |
| 701 | 801 | 0 | "" |
| 801 | 901 | 0 | "" |
| 901 | - | 1 | "" |

>最后的一批数据返回时，`strPosition` 的返回值可以忽略，不赋值。


如果获取数据过程中发生错误，那么对照关系如下：

| strPosition 传入 | strPosition 返回 | 函数返回值 | strError |
|:----------|---------|---------|---------:|
| "" | 101 | 0 | "" |
| 101 | 201 | 0 | "" |
| 201 | 301 | 0 | "" |
| 301 | 401 | 0 | "" |
| 401 | 501 | 0 | "" |
| 501 | 601 | -1 | [详细的错误原因] |

>获取过程中间出现错误，那么获取数据操作将中断，后面的批次不再继续。