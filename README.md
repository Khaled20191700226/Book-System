# نظام إدارة الكتب والطلبات (Book System)

واجهة برمجية (REST API) لإدارة الكتب والعملاء والطلبات، مبنية بـ ASP.NET Core 8 وEntity Framework Core وSQL Server.

## ماذا يفعل المشروع؟
- عمليات إضافة وعرض وتعديل وحذف (CRUD) لأربعة كيانات: الكتب، العملاء، الطلبات، عناصر الطلب.
- علاقات بين الجداول: العميل لديه عدة طلبات، والطلب فيه عدة عناصر، وكل عنصر مرتبط بكتاب.
- توثيق تفاعلي عبر Swagger عند التشغيل في وضع التطوير.

## التقنيات
C# · ASP.NET Core 8 Web API · Entity Framework Core · SQL Server · Swagger

## التشغيل
1. عدّل سلسلة الاتصال `DefaultConnection` في `BookSystem/appsettings.json`.
2. نفّذ `dotnet ef database update` لإنشاء قاعدة البيانات.
3. نفّذ `dotnet run --project BookSystem` ثم افتح `/swagger`.

## ملاحظة
صمّمتُ المشروع وطوّرته بمساعدة أدوات ذكاء اصطناعي، وأقدر أشرح كل جزء فيه.
