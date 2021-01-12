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

# 最新更新-年前最后一次版本
介绍地址：https://blog.csdn.net/qq_33484542/article/details/112523810
更新内容：
1. 用于注册请求流大小限制的上限。（示例为解决上传文件的大小）
public void ConfigureServices(IServiceCollection services)
{ 
    app.SetFormOptions(optins => 
    {
      optins.MultipartBodyLengthLimit = 60000000;用于处理上传文件大小被限制的问题。
    }) 
}

2. 优化 ApiOut.View 方法，默认视图存储位置（示例：\wwwroot\Views\类名\方法名.html）

3. 新增 ApiOut.File, 下载文件流的函数。

4. 新增 IFormFile.Save 保存上传的文件

5. 新增 OnResult 函数接口，同时实现了 （ApiAshx/MinApi）两种路由模式

6. 优化 SQL 所有可以传入 where 条件的接口均允许传入（ (NOLOCK)WHERE）该参数。

7. 优化 ApiVal 增加第二个条件，允许指定Key值，处理部分无法通过代码实现的写法。（示例如下：）
 public async Task<IApiOut> Upload(
    [ApiVal(Val.File)] IFormFile file_data, 
    [ApiVal(Val.Header, ".123")] string abc, 
    [ApiVal(Val.Header, "User-Agent")] string agent)
{
    await file_data.Save(AppContext.BaseDirectory + "Upload\\" + file_data.FileName);//顺带实现了上传保存文件的示例
    return await ApiOut.JsonAsyn(new { mag = "保存成功！" });
}

8. 多个已知Bug优化。

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
