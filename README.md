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

Logging: ASP.NET Core built-in logging kullanıldı.

Exception Handling: Global exception middleware ile hata yakalama.

Validations: FluentValidation & DataAnnotations.

Debugging: Development ortamında ayrıntılı hata mesajları, Production’da kullanıcı dostu hata sayfaları.

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



