# Projekt – Správce florbalového týmu

Konzolová aplikace v jazyce C#, která umožňuje správu hráčů (přidávání, úpravu, mazání, vyhledávání a zobrazování). Data jsou ukládána do JSON souboru.

## Zadání projektu
Cílem projektu je vytvořit aplikaci Správce florbalového týmu, která umožní přidávat hráče, mazat hráče, zobrazovat tabulku hráčů a zobrazit konkrétního hráče. Aplikace bude sloužit jako jednoduchý informační systém pro trenéra nebo manažera florbalového týmu. Průběžně se to bude ukládat po každém přidání nebo předělání.

### Funkcionalita aplikace – Evidence hráčů
- přidávat nové hráče
- upravovat hráče
- mazat hráče
- zobrazovat seznam hráčů
- vyhledávat hráče podle jména nebo čísla dresu

### Každý hráč obsahuje:
- jméno
- číslo dresu
- statistiky bodů (góly, asistence)
- stav (aktivní/neaktivní)

## Model tříd

### Třída Hrac
Reprezentuje jednoho hráče.

Vlastnosti:
- string jmeno – jméno hráče
- int cisloDresu – číslo dresu (1–99)
- int body – počet bodů
- bool aktivita – zda je hráč aktivní

Vztahy:
- Třída Hrac se používá v seznamu List<Hrac> v Program.cs.
- Všechny metody pracují s instancemi této třídy.

## Struktura aplikace

Aplikace je konzolová a celá logika je v souboru Program.cs.

### Program.cs

#### Main()
- Načte JSON soubor HRAC_DATA.json
- Deserializuje ho do List<Hrac>
- Zobrazuje menu s volbami 1–5
- Podle volby volá jednotlivé metody
- Po každé akci ukládá data zpět do JSON
- Ptá se uživatele, zda chce pokračovat

### Metody v Program.cs

#### NactiHrace()
- Vytvoří nového hráče
- Načítá jméno, číslo dresu, body a aktivitu
- Ověřuje vstupy (číslo 1–99, body ≥ 0, ano/ne)
- Vrací nový objekt Hrac

#### UpravHrace(List<Hrac> hraci)
- Najde hráče podle jména
- Pokud nenalezne, nabídne opakování
- Umožňuje upravit jméno, číslo dresu, body a aktivitu
- Ověřuje vstupy
- Po úpravě uloží změny

#### VymazatHrace(List<Hrac> hraci)
- Najde hráče podle jména
- Pokud nenalezne, nabídne opakování
- Pokud najde, odstraní ho ze seznamu

#### ZobrazSeznamHracu(List<Hrac> hraci)
- Vypíše všechny hráče ve formátu: Jmeno - cisloDresu

#### VyhledatHrace(List<Hrac> hraci)
- Uživatel si vybere hledání podle jména nebo čísla dresu
- Najde hráče a vypíše jeho údaje

#### UlozDoSouboru(List<Hrac> hraci)
- Serializuje seznam hráčů do JSON
- Uloží do souboru HRAC_DATA.json

#### ChecPokračovat()
- Ptá se uživatele, zda chce pokračovat (a/n)
- Ověřuje vstup
- Vrací true/false

## Práce se soubory

Aplikace pracuje s jedním souborem:

### Soubor: HRAC_DATA.json
- Formát: JSON
- Obsahuje seznam hráčů

Struktura jednoho hráče:
{
  "jmeno": "Adam",
  "cisloDresu": 10,
  "body": 5,
  "aktivita": true
}

### Načítání dat
Probíhá na začátku programu:
File.ReadAllText("HRAC_DATA.json");
JsonSerializer.Deserialize<List<Hrac>>(mocData);

### Ukládání dat
Probíhá po každé akci:
UlozDoSouboru(hraci);

### Způsob ukládání
- Serializace do JSON
- Odsazený formát (WriteIndented = true)
- Přepisuje celý soubor

## Ovládání aplikace

Po spuštění se zobrazí menu:

1 – Přidat hráče
2 – Upravit hráče
3 – Vymazat hráče
4 – Zobrazit seznam hráčů
5 – Vyhledat hráče

Uživatel zadá číslo volby a potvrdí Enter. Program po každé akci uloží data a zeptá se, zda chce pokračovat.

## Závěr
Aplikace umožňuje kompletní správu hráčů pomocí jednoduchého konzolového rozhraní a ukládá data do JSON souboru.
