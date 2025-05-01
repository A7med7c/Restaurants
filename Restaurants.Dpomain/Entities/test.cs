//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Restaurants.Domain.Entities
//{
//    internal class test
//    {
//        ✅ 1. User(اللي بيجي مع Identity) :

//    أغلب الشغل ده بيبقى جاهز مع Identity + Authorization

//    POST /api/account/register – تسجيل مستخدم جديد

//    POST /api/account/login – تسجيل دخول

//    GET /api/account/me – معلومات المستخدم الحالي(بعد تسجيل الدخول)

//✅ 2. Restaurant Endpoints

//    GET /api/restaurants – جلب كل المطاعم

//    GET /api/restaurants/{id
//    } – مطعم معين

//    POST /api/restaurants – إضافة مطعم جديد(من قِبل الـ Owner)

//    PUT /api/restaurants/{id
//} – تعديل بيانات مطعم

//    DELETE /api/restaurants/{id} – حذف مطعم

//    (Optional) GET / api / users /{ userId}/ restaurants – المطاعم المملوكة لمستخدم معين

//✅ 3. MenuCategory Endpoints

//    GET /api/restaurants/{restaurantId}/ categories – كل الفئات في مطعم معين

//    GET /api/restaurants/{restaurantId}/ categories /{ categoryId} – فئة معينة

//    POST /api/restaurants/{restaurantId}/ categories – إضافة فئة جديدة

//    PUT /api/restaurants/{restaurantId}/ categories /{ categoryId} – تعديل فئة

//    DELETE /api/restaurants/{restaurantId}/ categories /{ categoryId} – حذف فئة

//✅ 4. Dish Endpoints

//بما إن الـ Dish تابع لـ Category:

//    GET / api / restaurants /{ restaurantId}/ categories /{ categoryId}/ dishes – كل الأطباق في فئة

//    GET /api/restaurants/{restaurantId}/ categories /{ categoryId}/ dishes /{ dishId} – طبق معين

//    POST /api/restaurants/{restaurantId}/ categories /{ categoryId}/ dishes – إضافة طبق

//    PUT /api/restaurants/{restaurantId}/ categories /{ categoryId}/ dishes /{ dishId} – تعديل طبق

//    DELETE /api/restaurants/{restaurantId}/ categories /{ categoryId}/ dishes /{ dishId}
//    }
//}
