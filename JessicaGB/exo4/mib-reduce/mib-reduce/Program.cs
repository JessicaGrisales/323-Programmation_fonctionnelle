using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercice_Marché
{
    internal class Program
    {
        class Product
        {
            public int Location { get; set; }
            public string Producer { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public string Unit { get; set; }
            public double PricePerUnit { get; set; }
        }

        static void Main(string[] args)
        {
            var products = new List<Product>
            {
                new Product { Location = 1, Producer = "Bornand", ProductName = "Pommes", Quantity = 20, Unit = "kg", PricePerUnit = 5.50 },
                new Product { Location = 1, Producer = "Bornand", ProductName = "Poires", Quantity = 16, Unit = "kg", PricePerUnit = 5.50 },
                new Product { Location = 1, Producer = "Bornand", ProductName = "Pastèques", Quantity = 14, Unit = "pièce", PricePerUnit = 5.50 },
                new Product { Location = 1, Producer = "Bornand", ProductName = "Melons", Quantity = 5, Unit = "kg", PricePerUnit = 5.50 },
                new Product { Location = 2, Producer = "Dumont", ProductName = "Noix", Quantity = 20, Unit = "sac", PricePerUnit = 5.50 },
                new Product { Location = 2, Producer = "Dumont", ProductName = "Raisin", Quantity = 6, Unit = "kg", PricePerUnit = 5.50 },
                new Product { Location = 2, Producer = "Dumont", ProductName = "Pruneaux", Quantity = 13, Unit = "kg", PricePerUnit = 5.50 },
                new Product { Location = 2, Producer = "Dumont", ProductName = "Myrtilles", Quantity = 12, Unit = "kg", PricePerUnit = 5.50 },
            };

            // 1. Quantité de groseilles disponible sur le marché
            var qteGroseilles = products
                .Where(p => p.ProductName.Equals("Groseilles", StringComparison.OrdinalIgnoreCase))
                .Sum(p => p.Quantity);

            Console.WriteLine($"Quantité de groseilles : {qteGroseilles}");

            // 2. Chiffre d'affaires total par marchand
            var revenuePerProducer = products
                .GroupBy(p => p.Producer)
                .Select(g => new {
                    Producer = g.Key,
                    Revenue = g.Sum(p => p.Quantity * p.PricePerUnit)
                })
                .ToList();

            Console.WriteLine("\nChiffre d'affaires par marchand:");
            foreach (var r in revenuePerProducer)
                Console.WriteLine($"{r.Producer}: {r.Revenue:0.00} £");

            // 3. Min / Max / Moyenne des CA
            var chiffreMin = revenuePerProducer.Min(r => r.Revenue);
            var chiffreMax = revenuePerProducer.Max(r => r.Revenue);
            var chiffreAvg = revenuePerProducer.Average(r => r.Revenue);

            Console.WriteLine($"\nCA max: {chiffreMax:0.00} £, CA min: {chiffreMin:0.00} £, CA moyenne: {chiffreAvg:0.00} £");

            // 4. Marchand avec le plus de noix
            var topNoix = products
                .Where(p => p.ProductName.Equals("Noix", StringComparison.OrdinalIgnoreCase))
                .GroupBy(p => p.Producer)
                .Select(g => new {
                    Producer = g.Key,
                    Quantity = g.Sum(p => p.Quantity)
                })
                .OrderByDescending(x => x.Quantity)
                .FirstOrDefault();

            if (topNoix != null)
            {
                var unit = products.First(p => p.Producer == topNoix.Producer && p.ProductName.Equals("Noix", StringComparison.OrdinalIgnoreCase)).Unit;
                Console.WriteLine($"\nMarchand avec le plus de noix: {topNoix.Producer} ({topNoix.Quantity} {unit})");
            }
            else
            {
                Console.WriteLine("\nAucun produit 'Noix' trouvé.");
            }

            // 5. Affinité nom-product
            int Affinity(string name, string product)
            {
                var nameLetters = name.ToLower().Where(char.IsLetter).GroupBy(c => c);
                var prodLetters = product.ToLower().Where(char.IsLetter).GroupBy(c => c);
                return nameLetters.Union(prodLetters).Sum(g => g.Count());
            }

            var affinityPerProducer = products
                .GroupBy(p => p.Producer)
                .Select(g => new {
                    Producer = g.Key,
                    Affinity = g.Sum(p => Affinity(g.Key, p.ProductName))
                })
                .OrderByDescending(x => x.Affinity)
                .ToList();

            Console.WriteLine("\nAffinité par marchand (somme des affinités produit par produit):");
            foreach (var a in affinityPerProducer)
                Console.WriteLine($"{a.Producer}: {a.Affinity}");

            var topAffinity = affinityPerProducer.FirstOrDefault();
            if (topAffinity != null)
                Console.WriteLine($"\nMarchand avec la plus grande affinité: {topAffinity.Producer} ({topAffinity.Affinity})");
        }
    }
}
