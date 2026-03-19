# 📅 Timelines

Desktopová aplikace pro práci s časovou osou (timeline), vytvořená v C# pomocí WinForms.

---

## 🧠 Popis projektu

Aplikace umožňuje vizualizovat časovou osu a zobrazovat na ní objekty (např. události nebo intervaly).

Uživatel může:
- zobrazit timeline s osou
- přidávat objekty (např. čáry nebo bubliny)
- upravovat jejich vlastnosti
- pracovat s posunem a měřítkem

## 🛠️ Použité technologie

- C#
- .NET Framework (WinForms)
- Visual Studio


## 🧩 Struktura projektu

### 📌 Hlavní části:

- `Form1`
  - hlavní okno aplikace
  - vykreslování timeline

- `Meter`
  - stará se o vykreslení časové osy (ticks, roky, měřítko)

- `Line`
  - reprezentuje čáru na timeline

- `Bubble`
  - reprezentuje objekt (např. událost) s textem

- `Editor`
  - formulář pro úpravu objektů

- `Direction`
  - pomocná třída (např. směr vykreslování)

---

## 🎯 Funkcionalita

- vykreslení časové osy
- dynamické měřítko (zoom)
- posun timeline (offset)
- vykreslování různých typů objektů:
  - čáry
  - bubliny (textové události)
- editace objektů pomocí formuláře

---

## 🖥️ Grafika

Aplikace využívá:
- `Graphics` pro kreslení
- `Paint` event
- vlastní výpočty pozic na ose

---

## 🚀 Možná rozšíření

- ukládání a načítání timeline (soubor / JSON)
- drag & drop objektů myší
- zoom kolečkem myši
- více typů objektů (intervaly, skupiny)
- funkcionalitu bublin - Každá bublina půjde rozkliknout a ukáže se formulář pro práci s daty/textetm
