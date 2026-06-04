# Instrucțiuni de Rulare

## 1. Rularea din Visual Studio (IDE)
Dacă folosești Visual Studio, este cel mai simplu mod:
1. Deschide fișierul soluției (ex: `E-learning platform.slnx`) în Visual Studio.
2. Asigură-te că proiectul **E-learning platform** este setat ca **Startup Project** (click dreapta pe el în Solution Explorer -> *Set as Startup Project*).
3. Apasă tasta **F5** (pentru Debug) sau **Ctrl + F5** (pentru rulare fără debug).
4. Browser-ul se va deschide automat la pagina principală a site-ului.

## 2. Rularea din Terminal (CLI)
Deschide un terminal în directorul proiectului și rulează:

```powershell
dotnet run --project "E-learning platform/E-learning platform.csproj"
```

Sau, pentru o experiență mai bună de dezvoltare (auto-reîncărcare la modificări):

```powershell
dotnet watch --project "E-learning platform/E-learning platform.csproj"
```

Aplicația va fi disponibilă de obicei la adresa: `https://localhost:5001` sau `http://localhost:5000`.

## 2. Rularea Testelor Unitare
Pentru a verifica implementarea paternurilor de proiectare:

```powershell
dotnet test "E-learning platform.Tests/E-learning platform.Tests.csproj"
```

## 3. Curățarea și Reconstrucția (Dacă apar erori)
```powershell
dotnet clean
dotnet build
```
