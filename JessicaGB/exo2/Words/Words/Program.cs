// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

string[] words = { "bonjour", "hello", "monde", "vert", "rouge", "bleu", "jaune" };


//Calcule de la moyenne
double avgLength = words.Average(w => w.Length);

//Les filtres
Func<string, bool> wordsFilter = w => !w.Contains("x");
Func<string, bool> wordsFilter2 = w => w.Length >= 4;
Func<string, bool> wordsFilter3 = w => w.Length == (int)Math.Round(avgLength);

// Liste des filtres avec description
var filters = new (string Description, Func<string, bool> Predicate)[]
{
    ("Ne contiennent pas la lettre 'x'", wordsFilter),
    ("Ont 4 caractères ou plus", wordsFilter2),
    ($"Ont autant de caractères que la moyenne ({avgLength:F1})", wordsFilter3)
};

// Menu des filtres
Console.WriteLine("Choisissez un filtre :");
for (int i = 0; i < filters.Length; i++)
{
    Console.WriteLine($"{i} - {filters[i].Description}");
}

Console.Write("Votre choix : ");
if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice >= filters.Length)
{
    Console.WriteLine("Choix invalide.");
    return;
}

// Application du filtre choisi
var filtered = words.Where(filters[choice].Predicate);

// Menu d’affichage
Console.WriteLine("\nOptions d'affichage :");
Console.WriteLine("1 - Ordre naturel");
Console.WriteLine("2 - Ordre inverse");
Console.WriteLine("3 - Tri A-Z");
Console.WriteLine("4 - Tri Z-A");

Console.Write("Votre choix : ");
string? displayChoice = Console.ReadLine();

switch (displayChoice)
{
    case "1": // ordre naturel
        break;
    case "2": // ordre inverse
        filtered = filtered.Reverse();
        break;
    case "3": // tri A-Z
        filtered = filtered.OrderBy(w => w);
        break;
    case "4": // tri Z-A
        filtered = filtered.OrderByDescending(w => w);
        break;
    default:
        Console.WriteLine("Option invalide, affichage en ordre naturel.");
        break;
}

// Affichage final
Console.WriteLine("\nRésultats :");
foreach (var word in filtered)
{
    Console.WriteLine(word);
}

//B. Données parasites 1
string[] words2 = { "whatThe!!!", "bonjour", "hello", "monde", "vert", "rouge", "bleu", "jaune", "My kingdom for a horse !", "Ooops I did it again" };

//Filtrage supprimer le premier parasites du début avec le cheatsheet
var cleaned = words2.Skip(1).SkipLast(2).ToList();

Console.WriteLine("Données nettoyés :");
foreach(var word in cleaned)
{
    Console.WriteLine(word);
}

// C. Données parasites 2 (ici SkipWhile ne suffit pas)
string[] words3 = { "+++++", "<<<<<", ">>>>>", "bonjour", "hello", "@@@@", "vert", "rouge", "bleu", "jaune", "#####", "%%%%%%%" };

// Regex pour ne garder que les mots composés uniquement de lettres
Regex regex = new Regex("^[a-zA-Z]+$");

var cleaned = words
    .Where(w => regex.IsMatch(w)) // filtre avec regex
    .ToList();

Console.WriteLine("Mots nettoyés :");
    foreach (var word in cleaned)
    {
        Console.WriteLine(word);
    }
}