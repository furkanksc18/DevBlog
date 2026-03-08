# DevBlog
BlogApp, kullanıcıların yazılım/sorun odaklı **soru paylaşabildiği** ve diğer kullanıcıların **yorum yapabildiği** bir web uygulamasıdır.

- **Teknolojiler**
  - C#
  - ASP.NET Core MVC (.NET 8)
  - Entity Framework Core
- **Hedef kitle**
  - Herkes
- **Platform**
  - Web (cross-platform)

## Özellikler

- **Soru paylaşımı**
  - Kullanıcılar soru oluşturabilir ve listeleyebilir.
- **Yorum sistemi**
  - Sorulara yorum eklenebilir.
- **MVC mimarisi**
  - Controller/View/Model katmanları ile düzenli yapı.
- **Veri erişimi (EF Core)**
  - Migration’lar ile şema yönetimi.

## Ekran Görüntüsü

> Placeholder

```text
docs/images/screenshot.png
```

## Kurulum

### Gereksinimler

- .NET SDK 8
- Bir veritabanı:
  - SQL Server (varsayılan geliştirme ayarı)
  - SQLite (opsiyonel)

### Projeyi çalıştırma

```bash
dotnet restore
dotnet build
dotnet run
```

Uygulama çalıştıktan sonra terminal çıktısındaki URL üzerinden erişebilirsin (ör. `https://localhost:xxxx`).

### Veritabanı / Connection String

Projede `Program.cs` içinde `DataContext` için SQL Server bağlantısı kullanılacak şekilde yapılandırma bulunur.

`appsettings.Development.json` içinde connection string ayarı yapılır.

> Not: İstersen SQLite kullanımı için connection string ve `UseSqlite(...)` yapılandırması tercih edilebilir.

### Migration (EF Core)

Yeni bir kurulumda migration’ları uygulamak için:

```bash
dotnet ef database update
```

## Kullanım

- **Soru ekleme**
  - Arayüz üzerinden soru oluştur.
- **Soruları görüntüleme**
  - Ana sayfada veya ilgili sayfalarda soruları listele.
- **Yorum ekleme**
  - Soru detayında yorum paylaş.

## Proje Yapısı

Temel klasörler:

```text
.
├── Controllers/      # MVC controller’ları
├── Data/             # DbContext ve veri erişimi
├── Entity/           # Entity sınıfları
├── Migrations/       # EF Core migration dosyaları
├── Models/           # ViewModel/Model sınıfları
├── Views/            # Razor view’lar
├── wwwroot/          # Statik dosyalar (css/js/img)
├── Program.cs        # Uygulama başlangıcı ve middleware pipeline
└── Blog.csproj       # Proje tanımı ve NuGet bağımlılıkları
```

## Katkıda Bulunma

Katkılar memnuniyetle karşılanır.

- **Issue açma**
  - Hata/öneri için açıklayıcı bir issue aç.
- **Pull Request**
  - Fork al, feature branch oluştur, değişikliklerini küçük ve odaklı tut.
  - PR açıklamasında:
    - Ne değişti?
    - Neden değişti?
    - Test adımları

## Lisans

Bu proje **kullanıma açıktır**.

- Kaynak kodu inceleyebilir ve kişisel/kurumsal değerlendirme amacıyla kullanabilirsin.
- **Fork, yeniden dağıtım, türev çalışma oluşturma ve ticari kullanım** gibi işlemler için **proje sahibinden izin alınması gerekir**.

İzin talepleri için depo üzerinden iletişime geçebilir veya `furkanksc18@gmail.com` adresine e-posta gönderebilirsin.
