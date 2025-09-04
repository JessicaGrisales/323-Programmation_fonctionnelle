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

//menu des filtres
Console.WriteLine("Choisissez");
for (int i = 0; i < filters.Length; i++)
{
    Console.WriteLine($"{i} - {filters[i].Description}");
}

