supOS saas3.0 版本的.net core2.1 demo

本程序是对supOS saas3.0平台接入的示例代码，系统采用.net core2.1版本

目录结构如下：

1. coreJDK .net core主工程项目

   >LZAuthVerifyMiddleware.cs实现蓝卓OAuth的中间件
   >TenantController.cs实现租户的相关接口
   >UserController.cs示例根据租户ID获取用户列表

2. coreJDK.Common 公共类库

   >LzConfigHelper.cs 蓝卓相关配置实体类
   >RASHelper.cs RAS加解密帮助类
   >RequestHelper.cs http请求帮助类
   >Result.cs 公共的返回实体类
   >SysConfigHelper.cs 系统配置实体类

3. coreJDK.Repository 数据仓库

   >TenantRepository.cs租户的数据仓库实现
   >UserRepository.cs用户的数据仓库实现

4. Model 实体库

   >LzAuthInfoModel.cs蓝卓认证的实体类
   >LzResultModel.cs蓝卓返回实体类
   >TenantModel.cs租户实体
   >TenantParamsModel.cs租户相关参数实体
   >UserModel.cs用户实体

5. Services业务逻辑处理层

   >TenantService.cs租户的业务实现类
   >UserService.cs用户的业务实现类
