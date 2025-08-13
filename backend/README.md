# 🍰 Pastella - Pasta Sipariş ve Yönetim Sistemi

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-512BD4?style=for-the-badge&logo=dotnet)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core-512BD4?style=for-the-badge&logo=microsoft)
![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-CC2927?style=for-the-badge&logo=microsoft-sql-server)
![Docker](https://img.shields.io/badge/Docker-Containerized-2496ED?style=for-the-badge&logo=docker)
![Firebase](https://img.shields.io/badge/Firebase-Push%20Notifications-FFCA28?style=for-the-badge&logo=firebase)
![JWT](https://img.shields.io/badge/JWT-Authentication-000000?style=for-the-badge&logo=json-web-tokens)

## 📋 Proje Hakkında

Pastella, modern pasta siparişi ve yönetim sistemi için geliştirilmiş kapsamlı bir backend API'sidir. Müşterilerin özel pasta tasarımları yapabilmesi, sipariş verebilmesi ve siparişlerini takip edebilmesi için gerekli tüm özellikleri sunar.

## ✨ Temel Özellikler

### 🎂 Pasta Yönetimi
- **Hazır Pasta Kataloğu**: Fırınların mevcut pasta çeşitleri
- **Özel Pasta Tasarımı**: Müşterilerin kendi pasta tasarımlarını oluşturması
- **Süsleme Seçenekleri**: Çeşitli dekorasyon ve süsleme malzemeleri
- **Özel Durumlar**: Doğum günü, düğün, yıldönümü gibi özel günler için tasarımlar

### 👥 Kullanıcı Yönetimi
- **Müşteri Kayıt/Giriş**: Güvenli kullanıcı kimlik doğrulama
- **Profil Yönetimi**: Kullanıcı bilgileri ve tercihler
- **Admin Paneli**: Sistem yöneticileri için özel yetkiler
- **Fırın Yönetimi**: Fırın sahipleri için özel işlemler

### 📦 Sipariş Sistemi
- **Sipariş Oluşturma**: Hazır veya özel pasta siparişleri
- **Sipariş Takibi**: Gerçek zamanlı sipariş durumu güncellemeleri
- **Teslimat Yönetimi**: Adres ve teslimat zamanı planlaması
- **Ödeme Entegrasyonu**: Güvenli ödeme işlemleri

### 🔔 Bildirim Sistemi
- **Push Notifications**: Firebase FCM ile anlık bildirimler
- **Sipariş Bildirimleri**: Sipariş durumu değişikliklerinde otomatik bildirim
- **Promosyon Bildirimleri**: Kampanya ve indirim duyuruları
- **Doğum Günü Hatırlatıcıları**: Özel günler için otomatik hatırlatmalar

### 💬 İletişim ve Geri Bildirim
- **Yorum Sistemi**: Pasta ve hizmet değerlendirmeleri
- **Müşteri Desteği**: İletişim ve destek kanalları
- **Fotoğraf Paylaşımı**: Pasta tasarımları ve sonuçları için görsel paylaşım

## 🛠️ Teknoloji Stack'i

### Backend Framework
- **.NET 9.0**: En güncel .NET framework
- **ASP.NET Core Web API**: RESTful API geliştirme
- **Entity Framework Core**: ORM ve veritabanı yönetimi
- **SQL Server**: Ana veritabanı sistemi

### Güvenlik ve Kimlik Doğrulama
- **JWT (JSON Web Tokens)**: Stateless kimlik doğrulama
- **BCrypt.Net**: Şifre hashleme ve güvenlik
- **CORS**: Cross-origin resource sharing desteği

### Dış Servis Entegrasyonları
- **Firebase Admin SDK**: Push notification servisi
- **FCM (Firebase Cloud Messaging)**: Çoklu platform bildirim desteği

### Geliştirme Araçları
- **Swagger/OpenAPI**: API dokümantasyonu ve test arayüzü
- **Entity Framework Migrations**: Veritabanı şema yönetimi

## 🏗️ Proje Mimarisi

```
Pastella.Backend/
├── 📁 Application/           # İş mantığı katmanı
│   ├── Mappers/             # DTO dönüşümleri
│   ├── Services/            # İş servisleri
│   └── Validators/          # Veri doğrulama
├── 📁 Core/                 # Temel yapılar
│   ├── DTOs/               # Data Transfer Objects
│   ├── Entities/           # Veritabanı modelleri
│   └── Interfaces/         # Servis arayüzleri
├── 📁 Infrastructure/       # Altyapı katmanı
│   ├── Data/               # Veritabanı context
│   ├── ExternalServices/   # Dış servis entegrasyonları
│   └── Repositories/       # Veri erişim katmanı
├── 📁 WebAPI/              # API katmanı
│   ├── Configurations/     # Yapılandırma dosyaları
│   ├── Controllers/        # API kontrolcüleri
│   └── Middlewares/        # HTTP middleware'ler
└── 📁 Migrations/          # Veritabanı migration'ları
```

## 🗄️ Veritabanı Modelleri

### Ana Varlıklar
- **User**: Kullanıcı bilgileri ve rolleri
- **Bakery**: Fırın bilgileri ve lokasyonları
- **Cake**: Hazır pasta katalöğu
- **Order**: Sipariş detayları ve durumu
- **SweetDesign**: Özel pasta tasarımları
- **Decoration**: Süsleme malzemeleri
- **Notification**: Sistem bildirimleri
- **DeviceToken**: Push notification için cihaz tokenları

### İlişkisel Yapılar
- **CakeCustomization**: Pasta özelleştirme seçenekleri
- **SweetDecoration**: Tasarım-süsleme ilişkileri
- **DeliveryAddress**: Teslimat adresi bilgileri
- **Comment**: Kullanıcı yorumları ve değerlendirmeler
- **DesignImage**: Tasarım görselleri
- **Occasion**: Özel durumlar (doğum günü, düğün vb.)

## 🚀 API Endpoints

### 🔐 Kimlik Doğrulama
```
POST /api/auth/register     # Kullanıcı kaydı
POST /api/auth/login        # Kullanıcı girişi
POST /api/auth/refresh      # Token yenileme
```

### 🎂 Pasta Yönetimi
```
GET    /api/cake            # Pasta listesi
GET    /api/cake/{id}       # Pasta detayı
POST   /api/cake            # Yeni pasta ekleme (Admin)
PUT    /api/cake/{id}       # Pasta güncelleme (Admin)
DELETE /api/cake/{id}       # Pasta silme (Admin)
```

### 🎨 Özel Tasarım
```
GET    /api/designs         # Tasarım listesi
POST   /api/designs         # Yeni tasarım oluşturma
GET    /api/designs/{id}    # Tasarım detayı
PUT    /api/designs/{id}    # Tasarım güncelleme
DELETE /api/designs/{id}    # Tasarım silme
```

### 📦 Sipariş İşlemleri
```
GET    /api/order           # Sipariş listesi
POST   /api/order           # Yeni sipariş
GET    /api/order/{id}      # Sipariş detayı
PUT    /api/order/{id}      # Sipariş güncelleme
DELETE /api/order/{id}      # Sipariş iptal
```

### 🏪 Fırın Yönetimi
```
GET    /api/bakery          # Fırın listesi
GET    /api/bakery/{id}     # Fırın detayı
POST   /api/bakery          # Yeni fırın ekleme (Admin)
PUT    /api/bakery/{id}     # Fırın güncelleme
```

### 🎭 Süsleme ve Dekorasyon
```
GET    /api/decoration      # Süsleme listesi
POST   /api/decoration      # Yeni süsleme ekleme
PUT    /api/decoration/{id} # Süsleme güncelleme
DELETE /api/decoration/{id} # Süsleme silme
```

### 🔔 Bildirim Sistemi
```
POST   /api/fcm/register                    # Cihaz token kaydı
POST   /api/notification/promotion          # Promosyon bildirimi
POST   /api/notification/birthday           # Doğum günü hatırlatıcısı
POST   /api/notification/delivery           # Teslimat bildirimi
GET    /api/notification                    # Kullanıcı bildirimleri
PUT    /api/notification/{id}/read          # Bildirimi okundu olarak işaretle
```

### 💬 Yorum ve Değerlendirme
```
GET    /api/comment         # Yorum listesi
POST   /api/comment         # Yeni yorum ekleme
PUT    /api/comment/{id}    # Yorum güncelleme
DELETE /api/comment/{id}    # Yorum silme
```

### 🚚 Teslimat Yönetimi
```
GET    /api/delivery        # Teslimat listesi
POST   /api/delivery        # Teslimat oluşturma
PUT    /api/delivery/{id}   # Teslimat güncelleme
GET    /api/delivery/track/{id} # Teslimat takibi
```

### 👤 Kullanıcı Yönetimi
```
GET    /api/user/profile    # Kullanıcı profili
PUT    /api/user/profile    # Profil güncelleme
GET    /api/user/orders     # Kullanıcı siparişleri
GET    /api/user/designs    # Kullanıcı tasarımları
```

### 🛠️ Admin İşlemleri
```
GET    /api/admin/users     # Tüm kullanıcılar
GET    /api/admin/orders    # Tüm siparişler
GET    /api/admin/stats     # Sistem istatistikleri
POST   /api/admin/notification/broadcast # Toplu bildirim
```

## ⚙️ Kurulum ve Çalıştırma

### Gereksinimler
- .NET 9.0 SDK
- SQL Server (LocalDB veya tam sürüm)
- Visual Studio 2022 veya VS Code
- Firebase projesi (Push notifications için)

### Adım Adım Kurulum

1. **Projeyi klonlayın**
```bash
git clone https://github.com/ela-seyitali/Pastella.git
cd Pastella/Pastella.Backend
```

2. **Bağımlılıkları yükleyin**
```bash
dotnet restore
```

3. **Veritabanı bağlantısını yapılandırın**
```json
// appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=PastellaDB;Trusted_Connection=true;"
  }
}
```

4. **Firebase yapılandırması**
```json
// appsettings.json
{
  "Firebase": {
    "ProjectId": "your-firebase-project-id",
    "CredentialsPath": "path/to/firebase-adminsdk.json"
  }
}
```

5. **JWT yapılandırması**
```json
// appsettings.json
{
  "Jwt": {
    "Key": "your-super-secret-key-here",
    "Issuer": "Pastella.Backend",
    "Audience": "Pastella.Frontend",
    "ExpireMinutes": 60
  }
}
```

6. **Veritabanı migration'larını çalıştırın**
```bash
dotnet ef database update
```

7. **Uygulamayı başlatın**
```bash
dotnet run
```

## 🧪 Test ve Geliştirme

### Swagger UI
Uygulama çalıştırıldığında Swagger UI şu adreste erişilebilir:
```
https://localhost:7000/swagger
```

### Test Verileri
Geliştirme ortamında test verileri otomatik olarak oluşturulur:
- Admin kullanıcısı: `admin@pastella.com` / `Admin123!`
- Test fırını ve pasta örnekleri
- Örnek süsleme malzemeleri
- Örnek özel tasarımlar

### API Test Örnekleri

#### Kullanıcı Kaydı
```bash
curl -X POST "https://localhost:7000/api/auth/register" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "test@example.com",
    "password": "Test123!",
    "firstName": "Test",
    "lastName": "User",
    "phoneNumber": "+905551234567"
  }'
```

#### Sipariş Oluşturma
```bash
curl -X POST "https://localhost:7000/api/order" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "cakeId": 1,
    "quantity": 1,
    "deliveryDate": "2024-12-25T14:00:00",
    "deliveryAddress": {
      "street": "Test Sokak No:1",
      "city": "İstanbul",
      "district": "Kadıköy"
    }
  }'
```

## 📱 Push Notification Kurulumu

### Firebase Yapılandırması
1. Firebase Console'da yeni proje oluşturun
2. Admin SDK private key'ini indirin
3. `appsettings.json`'da Firebase ayarlarını yapılandırın
4. FCM için platform-specific yapılandırmaları tamamlayın

### Cihaz Token Kaydı
```javascript
// Örnek JavaScript kodu
const token = await messaging.getToken();
await fetch('/api/fcm/register', {
  method: 'POST',
  headers: { 
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${userToken}`
  },
  body: JSON.stringify({ 
    token: token, 
    platform: 'web',
    userId: currentUserId 
  })
});
```

### Bildirim Türleri
- **Sipariş Bildirimleri**: Sipariş durumu değişikliklerinde otomatik
- **Promosyon Bildirimleri**: Admin tarafından manuel gönderim
- **Doğum Günü Hatırlatıcıları**: Otomatik zamanlanmış bildirimler
- **Teslimat Bildirimleri**: Teslimat durumu güncellemeleri

## 🔒 Güvenlik Özellikleri

- **JWT Token Authentication**: Stateless kimlik doğrulama
- **Role-based Authorization**: Kullanıcı rolleri ve yetkilendirme
- **Password Hashing**: BCrypt ile güvenli şifre saklama
- **CORS Policy**: Cross-origin istekler için güvenlik
- **Input Validation**: Veri doğrulama ve sanitization
- **SQL Injection Protection**: Entity Framework ile güvenli veri erişimi
- **Rate Limiting**: API isteklerinde hız sınırlaması
- **HTTPS Enforcement**: Güvenli iletişim protokolü

## 📊 Performans Optimizasyonları

- **Async/Await Pattern**: Non-blocking I/O işlemleri
- **Entity Framework Optimizations**: Lazy loading ve query optimization
- **Caching Strategies**: Memory caching için hazır altyapı
- **Connection Pooling**: Veritabanı bağlantı havuzu
- **Pagination**: Büyük veri setleri için sayfalama
- **Image Optimization**: Görsel dosyaları için optimizasyon
- **Database Indexing**: Performans için veritabanı indeksleri

## 🚀 Deployment

### Production Hazırlığı
1. **appsettings.Production.json** dosyasını yapılandırın
2. SSL sertifikalarını ayarlayın
3. Veritabanı connection string'ini güncelleyin
4. Firebase production credentials'larını ekleyin
5. CORS policy'lerini production için ayarlayın
6. Logging seviyelerini ayarlayın

### 🐳 Docker ile Çalıştırma

Pastella Backend projesi Docker ile kolayca çalıştırılabilir. Projede hazır Docker dosyaları mevcut.

#### Gereksinimler
- Docker Desktop (Windows için)
- Docker Compose

#### Mevcut Docker Dosyaları
- `.Dockerfile` - Ana Docker image tanımı
- `docker-compose.yaml` - Çoklu servis yapılandırması
- `.dockerignore` - Build context'ten hariç tutulacak dosyalar

#### Hızlı Başlangıç

1. **Proje dizinine gidin**
```bash
cd Pastella.Backend
```

2. **Docker Compose ile başlatın**
```bash
docker-compose up -d
```

3. **API'ye erişin**
- Swagger UI: http://localhost:5000/swagger
- API Base URL: http://localhost:5000/api

#### Detaylı Docker Kurulumu

##### 1. Container'ları Başlatma
```bash
# Arka planda çalıştır
docker-compose up -d

# Logları takip et
docker-compose up

# Sadece belirli servisi başlat
docker-compose up pastella-api
```

##### 2. Container Durumunu Kontrol Etme
```bash
# Çalışan container'ları listele
docker-compose ps

# Logları görüntüle
docker-compose logs pastella-api
docker-compose logs sqlserver

# Canlı log takibi
docker-compose logs -f pastella-api
```

##### 3. Container'ları Yönetme
```bash
# Container'ları durdur
docker-compose down

# Container'ları yeniden başlat
docker-compose restart

# Yeniden build et ve başlat
docker-compose up --build

# Volume'ları da sil (VERİ SİLİNİR!)
docker-compose down -v
```

#### Servis Detayları

##### Pastella API Container
- **Image**: .NET 9.0 ASP.NET Core
- **Portlar**: 5000 (HTTP), 5001 (HTTPS)
- **Environment**: Development
- **Bağımlılık**: SQL Server container

##### SQL Server Container
- **Image**: `mcr.microsoft.com/mssql/server:2022-latest`
- **Port**: 1433
- **Database**: PastellaDb
- **SA Password**: `YourStrong@Passw0rd`
- **Volume**: `sqlserver_data` (veri kalıcılığı)

#### Veritabanı İşlemleri

##### Migration'ları Çalıştırma
```bash
# Container içinde migration çalıştır
docker-compose exec pastella-api dotnet ef database update

# Yeni migration oluştur
docker-compose exec pastella-api dotnet ef migrations add MigrationName
```

##### SQL Server'a Bağlanma
```bash
# SQL Server container'ına bağlan
docker exec -it pastella-backend-sqlserver-1 /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P YourStrong@Passw0rd
```

#### Yapılandırma Dosyaları

##### docker-compose.yaml
```yaml
version: '3.8'

services:
  pastella-api:
    build: .
    ports:
      - "5000:80"
      - "5001:443"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ConnectionStrings__DefaultConnection=Server=sqlserver;Database=PastellaDb;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;
    depends_on:
      - sqlserver
    networks:
      - pastella-network

  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=YourStrong@Passw0rd
    ports:
      - "1433:1433"
    volumes:
      - sqlserver_data:/var/opt/mssql
    networks:
      - pastella-network

volumes:
  sqlserver_data:

networks:
  pastella-network:
    driver: bridge
```

##### Dockerfile
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Pastella.Backend.csproj", "."]
RUN dotnet restore "./Pastella.Backend.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Pastella.Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pastella.Backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pastella.Backend.dll"]
```

#### Sorun Giderme

##### Port Çakışması
Eğer portlar kullanılıyorsa, `docker-compose.yaml` dosyasında değiştirin:
```yaml
ports:
  - "8000:80"  # 5000 yerine 8000
  - "8001:443" # 5001 yerine 8001
```

##### SSL Sertifika Sorunu
```bash
# Geliştirme sertifikası oluştur
dotnet dev-certs https --trust
```

##### Container'ları Temizleme
```bash
# Kullanılmayan container'ları temizle
docker system prune

# Tüm container'ları ve volume'ları sil
docker-compose down -v
docker system prune -a
```

#### Production için Docker

Production ortamında aşağıdaki değişiklikleri yapın:

1. **Environment değişkenleri**
```yaml
environment:
  - ASPNETCORE_ENVIRONMENT=Production
  - ConnectionStrings__DefaultConnection=YourProductionConnectionString
```

2. **Güvenli şifreler**
```yaml
environment:
  - SA_PASSWORD=YourSecureProductionPassword
```

3. **SSL sertifikaları**
```yaml
volumes:
  - ./certs:/https:ro
environment:
  - ASPNETCORE_Kestrel__Certificates__Default__Password=YourCertPassword
  - ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx
```

#### Docker Hub'a Yükleme

```bash
# Image'ı build et
docker build -t pastella-backend:latest .

# Tag ekle
docker tag pastella-backend:latest yourusername/pastella-backend:latest

# Docker Hub'a push et
docker push yourusername/pastella-backend:latest
```

Bu şekilde Pastella Backend projenizi Docker ile kolayca çalıştırabilir ve yönetebilirsiniz! 🐳

## 🤝 Katkıda Bulunma

1. Fork yapın
2. Feature branch oluşturun (`git checkout -b feature/amazing-feature`)
3. Değişikliklerinizi commit edin (`git commit -m 'Add amazing feature'`)
4. Branch'inizi push edin (`git push origin feature/amazing-feature`)
5. Pull Request oluşturun

### Kod Standartları
- C# coding conventions'larını takip edin
- XML documentation ekleyin
- Unit testler yazın
- Code review sürecine katılın

## 📞 İletişim ve Destek

- **Proje Sahibi**: Ela Seyitali
- **GitHub**: [github.com/ela-seyitali](https://github.com/ela-seyitali)
- **Proje Repository**: [github.com/ela-seyitali/Pastella](https://github.com/ela-seyitali/Pastella)


## 👥 Katkıda Bulunanlar (Contributors)

- **[Ela Seyitali](https://github.com/ela-seyitali)** - Proje Sahibi & Ana Geliştirici
- **[Abdulhadi Alayoub](https://github.com/Abdulhadialayoub)** - Katkıda Bulunan Geliştirici

## 🔄 Versiyon Geçmişi

### v1.0.0 (Mevcut)
- ✅ Temel CRUD işlemleri
- ✅ JWT kimlik doğrulama
- ✅ Push notification sistemi
- ✅ Admin paneli
- ✅ Özel pasta tasarım sistemi
- ✅ Sipariş takip sistemi
- ✅ Firebase FCM entegrasyonu

---

⭐ **Bu projeyi beğendiyseniz yıldız vermeyi unutmayın!**

🍰 **Pastella ile lezzetli pasta siparişleri artık çok daha kolay!**
