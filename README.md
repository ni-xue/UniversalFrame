# UniversalFrame
现在已经整个迁移至core，如果有需求请使用core开发，原本版本将不在继续维护，将core版本作为该项目的生命延续。
上面提供了一个项目，用该框架实现了，基本满足正常开发。
有任何问题建议提问，我将会在第一时间给出答复。

1. 当前这个库仅用于显示，暂时为开源，仅用于问题提交反馈。
2. 欢迎有兴趣的小伙伴参与，一起开发维护的路上，目前涉及范围有限，需要更多志同道合的朋友一起创造。
3. 在此，非常感谢，长久以来对作者支持的小伙伴们，感觉一路有你。
4. 相关开发文档：https://blog.csdn.net/qq_33484542
5. 使用API文档：http://core.turl.xin/

有很长一段时间没有更新该项目了，作者会在最近几天继续更新，保证用户可以拉倒最新的项目实例。

最新 实例 已经更新啦，欢迎拉取。

后续，打算更新部分界面效果，附带数据

# 3.0.0版本
介绍地址：https://blog.csdn.net/qq_33484542/article/details/114820469
更新内容：
1. 新增路由自定义模式 MapApiRoute

2. 新增特性 路由 [AshxRoute(“url/{id?}”)] 支持接口/控制，注册

3. 新增DbProviderType.SqlServer1 用于包括新的 SqlServer（SKD:Microsoft.Data.SqlClient）

4. 优化Api命名空间引用复杂问题，简化引用。

5. 优化 TcpFrame 命名空间下的，全部有关部分，重新定义新的协议，支持字节流传输和字符串传输，基础协议更小。

6. ClientFrame 模块 新增 心跳功能，需要手动开启 AddKeepAlive(5);（心跳模式开启后，将会自动检查是否断开连接）

7. ApiPacket 允许发起方，发送超过配置包大小的包，系统会自动分包处理

8. 重新优化多宝协议，现在更安全，更可靠。

9. ClientFrame 转发模式，优化，现在性能可靠。

10. TcpFrame 模块下存在大量与内存使用有关的资源，目前已经全部通过GC管理起来，内存泄漏无需关心，后期会着重处理有关GC部分。

11. 多个已知Bug优化。

# 执照
``` a

Copyright 2018-2020 Gianluca Cacace

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

```
