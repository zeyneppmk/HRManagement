# HRManagement

![Image](https://github.com/user-attachments/assets/24fa6d36-ed1e-49a8-b187-a28143970d8c)


## 📑 Proje Özeti

HRManagement, kurumsal insan kaynakları süreçlerini dijital ortama taşımayı hedefleyen modern bir web tabanlı yönetim sistemidir. 
Geleneksel e-posta ve dosya temelli işleyişin yerine; kullanıcı, departman, izin, görev, bildirim, etkinlik ve raporlama gibi tüm modüller entegre edilmiştir.
Sistem, ASP.NET Core MVC + API mimarisi ile katmanlı bir yapıda geliştirilmiş, rol bazlı yetkilendirme ve anlık bildirim (SignalR) özellikleriyle kurumsal düzeyde kullanılabilir hale getirilmiştir.
 
 ---

 ## 🚀 Öne Çıkan Özellikler

- Rol bazlı yönetim (Admin, Department Manager, Employee)

- Kullanıcı & Personel detayları (User & UserDetail)

- İzin talepleri ve onay akışı (LeaveRequest Workflow)

- Görev atama ve durum takibi (TaskItem + bildirim)

- Anlık bildirimler (SignalR ile Inbox & Notification Center)

- Etkinlik & Takvim yönetimi (FullCalendar entegrasyonu)

- Modern Dashboard (dinamik grafikler & istatistikler)

- Raporlama modülü (departman, maaş, izin, görev bazlı)

- Responsive & modern UI (Bootstrap + Custom CSS)


---

## 🏗️ Sistemin Mimarisi

- **Backend (API Katmanı)**
  - ⚡.NET Core Web API
  - 🗄️Entity Framework Core (Code-First)
  - 🔄 Repository + UnitOfWork
  - 📦 DTO + AutoMapper

- **Frontend (MVC Katmanı)**
  - 🧭 ASP.NET Core MVC (Areas: Admin, Manager, Employee)
  - 🎨 Bootstrap + Custom CSS
  - 🔔 SignalR entegrasyonu

- **Veritabanı**
  - 🛢️ SQL Server (EF Core Migrations ile yönetim)

- **Modüller**
  - 👥 Users, Departments, Roles, UserDetails
  - 📅 LeaveRequests, TaskItems, AppNotifications
  - 📊 Events (General vs Holidays), Reports, Payments

---

## ⚙️ Kurulum Adımları

**1.** Repository’yi klonlayın:
```bash
git clone https://github.com/kullanici/hrmanagement.git
cd hrmanagement
```


**2.** `appsettings.json` içinde veritabanı bağlantısını düzenleyin.

**3.** Migration ve veritabanı güncellemelerini çalıştırın:
```bash
dotnet ef database update
```

**4.** Projeyi çalıştırın:
- Visual Studio kullanıyorsanız F5 ile başlatabilirsiniz.
- Terminal üzerinden çalıştırmak için:
  
```bash
dotnet run
```

Bu işlem sonunda uygulama, kendi web sunucusu üzerinde (Kestrel) çalışmaya başlayacaktır.

---

## 📑 Log ve Hata Kontrolleri

- **Logging**: ASP.NET Core built-in logging kullanıldı.

- **Exception Handling**: Global exception middleware ile hata yakalama.

- **Validations**: FluentValidation & DataAnnotations.

- **Debugging**: Development ortamında ayrıntılı hata mesajları, Production’da kullanıcı dostu hata sayfaları.

---

## 🖥️ Kullanım

**1.** Kullanıcı giriş ekranı → rol bazlı yönlendirme (Admin, Manager, Employee).

**2.** Dashboard üzerinden hızlı erişim kartları ve özet istatistikler.

**3.** Sidebar’dan modüllere erişim:

- Kullanıcı Yönetimi

- Departman & Pozisyon Yönetimi

- İzin Talepleri

- Görev Yönetimi

- Bildirimler (Inbox)

- Etkinlik Takvimi

- Raporlama

<img width="1900" height="917" alt="image" src="https://github.com/user-attachments/assets/816bd4e2-44fa-43b4-ae47-a6e87470fbc2" />
<img width="1919" height="903" alt="image" src="https://github.com/user-attachments/assets/0cab7bb0-4747-4d02-b50e-68e5bc9e1968" />
<img width="1919" height="905" alt="image" src="https://github.com/user-attachments/assets/2043a895-d51b-420e-9517-51a4dbcf83f6" />
<img width="1919" height="908" alt="image" src="https://github.com/user-attachments/assets/58050b15-ac53-4f91-b2a1-c1e78c96b4bb" />
<img width="1919" height="857" alt="image" src="https://github.com/user-attachments/assets/d12b9e08-fa4f-43ff-81c3-c93a5674d2e9" />
<img width="1919" height="830" alt="image" src="https://github.com/user-attachments/assets/b39bfe9d-2061-44cc-9303-be7e526f8969" />
<img width="1919" height="903" alt="image" src="https://github.com/user-attachments/assets/ad80123f-5f90-494b-9bd7-d49beffcd0cd" />
<img width="1919" height="910" alt="image" src="https://github.com/user-attachments/assets/26e00a08-e628-487d-a372-b1b73951ace6" />
<img width="1608" height="911" alt="image" src="https://github.com/user-attachments/assets/a2159bde-bc1c-40cd-98dc-61bcff105c22" />
<img width="1609" height="904" alt="image" src="https://github.com/user-attachments/assets/4d2b7a1a-f030-4cc1-ba09-0288e7791b5f" />
<img width="1606" height="510" alt="image" src="https://github.com/user-attachments/assets/d1717097-50ae-4850-b832-02e7b3abd2b8" />
<img width="1610" height="912" alt="image" src="https://github.com/user-attachments/assets/18cb4267-2dcd-4b87-ad12-934743658bc2" />


**4.** Bildirimler SignalR ile anlık güncellenir.

---

## 🛠️ Geliştirici Notları

- Areas yapısı sayesinde her rol için ayrı layout ve sidebar kullanılmıştır.

- AppNotification modülü hem görev hem izin süreçleriyle entegredir.

- TaskItem durumları enum ile yönetilir (Planned, In Progress, Completed, Incompleted).

- Raporlar LINQ üzerinden join, groupby ve orderby ile dinamik hazırlanır.

- Dashboard chartları için JavaScript chart kütüphaneleri entegre edilmiştir.

## 🔧 Sorun Giderme

- **Veritabanı hatası**: dotnet ef database update ile migration kontrol edin.

- **SignalR çalışmıyor**: Hub URL’sinin UI tarafında doğru tanımlandığını kontrol edin.

- **Login başarısız**: JWT/Identity ayarlarının appsettings.json dosyasında güncel olduğundan emin olun.

- **UI bozuk**: Tarayıcı cache temizleyin ve npm run build (varsa) çalıştırın.

---

## 🎥 Demo

- **Login & Rol Bazlı Erişim**: Admin → Dashboard & tüm modüller

- **LeaveRequest Workflow**: Employee → izin talebi → Manager onayı → HR raporlaması

- **Task Assignment**: Manager → çalışanlara görev → anlık bildirim + görev takibi

- **Calendar & Events**: FullCalendar üzerinde etkinlik/izin günleri

- **Reports**: Maaş dağılımı, görev istatistikleri, kullanıcı rol dağılımları

---

## ❓ Soru / İletişim

Herhangi bir sorunuz varsa  [GitHub Issues](https://github.com/zeyneppmk/HRManagement/issues) bölümünden yeni bir issue açabilirsiniz.

⭐ Bu projeyi beğendiyseniz yıldız vermeyi unutmayın!
