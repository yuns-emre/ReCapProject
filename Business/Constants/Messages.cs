using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string CarAdded = "Ürününüz eklendi";
        public static string CarNameInvalid = "Ürün ismi geçersiz";
        public static string CarListed = "Arabalar listelendi";
        public static string MaintenanceTime = "Sistem bakımda";
        public static string Error="İşleminiz başarısız olmuştur.";
        public static string Successed="İşleminiz başarı ile gerçekleştirilmiştir.";
        public static string CarImageLimitExceeded = "En fazla 5 adet resim koyabilirsiniz.";
        public static string UserNotFound = "Kullanıcı bulunamadı.";
        public static string PasswordError = "Şifre yanlış.";
        public static string UserAlreadyExists = "Kullanıcı var";
        public static string AccessTokenCreated = "Token oluşturuldu.";
        public static string AuthorizationDenied = "Yetkiniz yok.";
        public static string SuccessfulLogin = "Giriş başarılı.";
        internal static string UserRegistered = "Kullanıcı kayıt edildi.";
    }
}
