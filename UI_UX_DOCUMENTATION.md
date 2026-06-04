# Documentație Interfață UI/UX

Acest document explică arhitectura vizuală și funcționalitățile utilizate pentru a crea experiența premium a platformei E-learning.

## 1. Filozofia de Design: "Modern Glassmorphism"
Interfața se bazează pe stilul **Glassmorphism**, care simulează efectul de sticlă mată. Acesta oferă adâncime și un aspect futurist aplicației.

### Principii cheie:
- **Transparență și Blur**: Fundaluri translucide care lasă să se vadă culorile din spate, dar păstrează lizibilitatea.
- **Vibrant Gradients**: Utilizarea gradienților de tip "mesh" pentru a atrage atenția asupra elementelor importante.
- **Layering**: Ierarhie vizuală clară folosind umbre și borduri fine.

## 2. Sistemul de Design (CSS Tokens)
Toate stilurile sunt centralizate în `:root` în `site.css` sub formă de variabile (tokens), ceea ce face interfața ușor de personalizat.

| Variabilă | Descriere | Rol |
| :--- | :--- | :--- |
| `--primary` | `#0062ff` | Culoarea principală pentru butoane și highlight. |
| `--bg-dark` | `#050a15` | Culoarea de fundal profund pentru contrast maxim. |
| `--surface` | `rgba(255, 255, 255, 0.05)` | Baza pentru elementele de tip "glass". |
| `--glass-blur` | `15px` | Nivelul de blur aplicat elementelor transparente. |

## 3. Componente și Funcții Vizuale

### A. Glass Navigation (`.glass-nav`)
- **Funcție**: Navigation bar fix care utilizează `backdrop-filter: blur()`. 
- **UX**: Rămâne vizibil în timp ce utilizatorul face scroll, oferind acces rapid la meniu fără a bloca vizibilitatea conținutului din spate.

### B. Glass Cards (`.glass-card`)
- **Funcție**: Containere reutilizabile pentru cursuri, statistici sau testimoniale.
- **Interactivitate**: Include un efect de hover (`transform: translateY(-5px)`) care simulează "ridicarea" cardului deasupra planului principal.

### C. Premium Buttons (`.btn-premium`)
- **Funcție**: Butoane cu gradient și umbră strălucitoare (`box-shadow`).
- **UX**: Folosit pentru actiuni de tip "Call to Action" (CTA), ieșind în evidență față de restul elementelor neutre.

### D. Background Mesh Gradient
- **Funcție**: Creat folosind `radial-gradient` pe elementul `::before` al body-ului.
- **Efect**: Oferă o textură dinamică fundalului fără a încărca aplicația cu imagini mari, asigurând performanță ridicată.

## 4. Tipografie și Ierarhie
- **Font**: Utilizăm **Inter** (via Google Fonts) pentru claritate și un aspect profesional.
- **Text Gradient**: Titlurile importante utilizează `background-clip: text` pentru a aplica gradienți direct pe text, adăugând o notă premium.

## 5. Responsivitate (Mobile-First)
- Utilizăm sistemul Grid și Flexbox din Bootstrap, dar customizat cu variabilele noastre.
- Design-ul se adaptează automat: de la hero-section-ul pe două coloane pe Desktop, la o aranjare pe verticală pe Mobile pentru a asigura o experiență de citire optimă.
