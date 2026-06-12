using System.Runtime.Serialization;
using System.Text.Json;

namespace projekt
{
    internal class Program
    {
        static void Main(string[] args)
        { 
            Hrac hrac = new Hrac();
            string mocData = File.ReadAllText("HRAC_DATA.json");
            List<Hrac> hraci = JsonSerializer.Deserialize<List<Hrac>>(mocData);

            bool pokracovat = true;
            int vstup = 0;
            while (pokracovat)
            {
                Console.WriteLine("Co chceš dělat? (vyber jednu z možností 1 - 5)");
                Console.WriteLine("1) Přidat hráče");
                Console.WriteLine("2) Upravit hráče");
                Console.WriteLine("3) Vymazat hráče");
                Console.WriteLine("4) Zobrazit seznam hráčů");
                Console.WriteLine("5) Vyhledat hráče podle jména nebo čísla dresu");

                string input = Console.ReadLine();

                // Ověření, že je to číslo
                if (!int.TryParse(input, out vstup))
                {
                    Console.WriteLine("Neplatný vstup – musíš zadat číslo.");
                    continue;
                }

                // Ověření rozsahu 1–5
                if (vstup < 1 || vstup > 5)
                {
                    Console.WriteLine("Neplatný vstup – číslo musí být od 1 do 5.");
                    continue;
                }

                switch (vstup)
                {
                    case 1:
                        Hrac novyHrac = NactiHrace();
                        hraci.Add(novyHrac);
                        UlozDoSouboru(hraci);
                        break;
                    case 2:
                        UpravHrace(hraci);
                        UlozDoSouboru(hraci);
                        break;
                    case 3:
                        VymazatHrace(hraci);
                        UlozDoSouboru(hraci);
                        break;
                    case 4:
                        ZobrazSeznamHracu(hraci);
                        UlozDoSouboru(hraci);
                        break;
                    case 5:
                        VyhledatHrace(hraci);
                        UlozDoSouboru(hraci);
                        break;
                }
                pokracovat = ChcePokračovat();

                if (!pokracovat)
                {
                    break;
                }
            }
        }
        public static void UlozDoSouboru(List<Hrac> hraci)
        {
            string json = JsonSerializer.Serialize(hraci, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("HRAC_DATA.json", json);
        }
        public static bool ChcePokračovat()
        {
            Console.Write("Chceš pokračovat? (a/n): ");
            string odpoved = Console.ReadLine().ToLower();

            while (odpoved != "a" && odpoved != "n")
            {
                Console.Write("Neplatný vstup – napiš 'a' nebo 'n': ");
                odpoved = Console.ReadLine().ToLower();
            }

            return odpoved == "a";
        }
        public static Hrac NactiHrace()
        {
            Console.WriteLine("Jak se jmenuje hráč");
            string name = Console.ReadLine();

            int dres;
            do
            {
                Console.WriteLine("Jaké má číslo dresu (1–99)");
            }
            while (!int.TryParse(Console.ReadLine(), out dres) || dres < 1 || dres > 99);

            int bod;
            do
            {
                Console.WriteLine("Kolik má bodů");
            }
            while (!int.TryParse(Console.ReadLine(), out bod) || bod < 0);
            
            bool aktivni;
            string v;

            do
            {
                Console.Write("Je hráč aktivní? (ano/ne): ");
                v = Console.ReadLine().ToLower();
            }
            while (v != "ano" && v != "ne");

            if (v == "ano")
            {
                aktivni = true;
            }
            else 
            {
                aktivni = false;
            }

            Hrac novyHrac = new Hrac();
            novyHrac.jmeno = name;
            novyHrac.cisloDresu = dres;
            novyHrac.body = bod;
            novyHrac.aktivita = aktivni;

            return novyHrac;
        }
        public static void UpravHrace(List<Hrac> hraci)
        {
            while (true)
            {
                Console.Write("Zadej jméno hráče, kterého chceš upravit: ");
                string hledaneJmeno = Console.ReadLine();

                hledaneJmeno = char.ToUpper(hledaneJmeno[0]) + hledaneJmeno.Substring(1).ToLower();

                Hrac nalezeny = null;

                foreach (Hrac h in hraci)
                {
                    if (h.jmeno.Equals(hledaneJmeno, StringComparison.OrdinalIgnoreCase))
                    {
                        nalezeny = h;
                        break;
                    }
                }

                // Nenalezeno
                if (nalezeny == null)
                {
                    Console.WriteLine("Hráč nenalezen. Myslel jsi někoho jiného? (a/n): ");
                    string odpoved = Console.ReadLine().ToLower();

                    while (odpoved != "a" && odpoved != "n")
                    {
                        Console.Write("Neplatný vstup – napiš 'a' nebo 'n': ");
                        odpoved = Console.ReadLine().ToLower();
                    }

                    if (odpoved == "a")
                    {
                        continue; // zopakujeme dotaz na jméno
                    }
                    else
                    {
                        return; // konec metody
                    }
                }

                // Hráč nalezen → nabídka úprav
                Console.WriteLine("Co chceš upravit?");
                Console.WriteLine("1) Jméno");
                Console.WriteLine("2) Číslo dresu");
                Console.WriteLine("3) Body");
                Console.WriteLine("4) Aktivitu");

                int volba;
                while (!int.TryParse(Console.ReadLine(), out volba) || volba < 1 || volba > 4)
                {
                    Console.Write("Neplatný vstup – zadej číslo 1–4: ");
                }

                switch (volba)
                {
                    case 1:
                        Console.Write("Zadej nové jméno: ");
                        string noveJmeno = Console.ReadLine();
                        noveJmeno = char.ToUpper(noveJmeno[0]) + noveJmeno.Substring(1).ToLower();
                        nalezeny.jmeno = noveJmeno;
                        break;

                    case 2:
                        int novyDres;
                        do
                        {
                            Console.Write("Zadej nové číslo dresu (1–99): ");
                        }
                        while (!int.TryParse(Console.ReadLine(), out novyDres) || novyDres < 1 || novyDres > 99);

                        nalezeny.cisloDresu = novyDres;
                        break;

                    case 3:
                        int noveBody;
                        do
                        {
                            Console.Write("Zadej nový počet bodů (0+): ");
                        }
                        while (!int.TryParse(Console.ReadLine(), out noveBody) || noveBody < 0);

                        nalezeny.body = noveBody;
                        break;

                    case 4:
                        string v;
                        do
                        {
                            Console.Write("Je hráč aktivní? (ano/ne): ");
                            v = Console.ReadLine().ToLower();
                        }
                        while (v != "ano" && v != "ne");

                        nalezeny.aktivita = (v == "ano");
                        break;
                }
                Console.WriteLine("Hráč byl úspěšně upraven.");
                return;
            }
        }
        public static void VymazatHrace(List<Hrac> hraci)
        {
            Console.Write("Zadej jméno hráče, kterého chceš vymazat: ");
            string hledaneJmeno = Console.ReadLine();

            // Upravíme vstup na formát: První písmeno velké
            hledaneJmeno = char.ToUpper(hledaneJmeno[0]) + hledaneJmeno.Substring(1).ToLower();

            Hrac nalezeny = null;

            foreach (Hrac h in hraci)
            {
                if (h.jmeno.Equals(hledaneJmeno, StringComparison.OrdinalIgnoreCase))
                {
                    nalezeny = h;
                    break;
                }
            }

            if (nalezeny == null)
            {
                Console.WriteLine("Hráč nenalezen. Myslel jsi někoho jiného? (a/n)");
                string odpoved = Console.ReadLine().ToLower();

                while (odpoved != "a" && odpoved != "n")
                {
                    Console.Write("Neplatný vstup – napiš 'a' nebo 'n': ");
                    odpoved = Console.ReadLine().ToLower();
                }

                if (odpoved == "a")
                {
                    Console.WriteLine("Zkus zadat jméno znovu.");
                    VymazatHrace(hraci); // zavoláme funkci znovu
                }
                return;
            }
            hraci.Remove(nalezeny);
            Console.WriteLine("Je smazaný");
        }
        public static void ZobrazSeznamHracu(List<Hrac> hraci)
        {
            foreach (var h in hraci)
            {
                Console.WriteLine($"{h.jmeno} - #{h.cisloDresu}");
            }
        }
        public static void VyhledatHrace(List<Hrac> hraci)
        {
            string volba;

            do
            {
                Console.Write("Chceš hledat podle jména nebo čísla dresu? (jmeno/cislo): ");
                volba = Console.ReadLine().ToLower();
            }
            while (volba != "jmeno" && volba != "cislo");

            Hrac nalezeny = null;

            // Hledání podle jména
            if (volba == "jmeno")
            {
                Console.Write("Zadej jméno hráče: ");
                string hledaneJmeno = Console.ReadLine();
                hledaneJmeno = char.ToUpper(hledaneJmeno[0]) + hledaneJmeno.Substring(1).ToLower();

                foreach (Hrac h in hraci)
                {
                    if (h.jmeno.Equals(hledaneJmeno, StringComparison.OrdinalIgnoreCase))
                    {
                        nalezeny = h;
                        break;
                    }
                }
            }

            // Hledání podle čísla dresu
            if (volba == "cislo")
            {
                int cislo;
                do
                {
                    Console.Write("Zadej číslo dresu (1–99): ");
                }
                while (!int.TryParse(Console.ReadLine(), out cislo) || cislo < 1 || cislo > 99);

                foreach (Hrac h in hraci)
                {
                    if (h.cisloDresu == cislo)
                    {
                        nalezeny = h;
                        break;
                    }
                }
            }
            
            if (nalezeny == null)
            {
                Console.WriteLine("Hráč nenalezen.");
                return;
            }
            Console.WriteLine("Hráč nalezen:");
            Console.WriteLine($"Jméno: {nalezeny.jmeno}");
            Console.WriteLine($"Dres: {nalezeny.cisloDresu}");
            Console.WriteLine($"Body: {nalezeny.body}");
            Console.WriteLine($"Aktivní: {(nalezeny.aktivita ? "Ano" : "Ne")}");
        }
    }
}
