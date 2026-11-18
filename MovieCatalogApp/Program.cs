using MovieCatalogApp;

var storage = new MovieStorage();

try
{
    Console.WriteLine("=== Все фильмы ===");
    var all = storage.GetAll();
    if (all.Count == 0) Console.WriteLine("Записей нет.");
    else foreach (var m in all) Console.WriteLine(m);

    Console.WriteLine("\n=== Поиск по Id = 2 ===");
    var found = storage.GetById(2);
    Console.WriteLine(found is null ? "Фильм не найден." : found.ToString());

    Console.WriteLine("\n=== Добавление ===");
    var added = storage.Add(new Movie("Матрица", 1999, 136, 8.7, ""));
    Console.WriteLine($"Изменено строк: {added}");

    Console.WriteLine("\n=== Изменение рейтинга Id=1 на 9.1 ===");
    var updated = storage.UpdateRating(1, 9.1);
    Console.WriteLine(updated == 0 ? "Фильм не найден." : $"Изменено строк: {updated}");
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка: {ex.Message}");
}