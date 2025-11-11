using Todo.Core;

namespace SampleApp
{
    public static class Program
    {
        public static void Main()
        {
            var todoList = new TodoList();

            Console.WriteLine("Todo List Application");
            Console.WriteLine("Commands: add, list, remove, find, exit");

            while (true)
            {
                Console.Write("> ");
                var input = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(input))
                {
                    continue;
                }

                var parts = input.Split(' ', 2);
                var command = parts[0].ToLower(System.Globalization.CultureInfo.CurrentCulture);
                var argument = parts.Length > 1 ? parts[1] : null;

                switch (command)
                {
                    case "add":
                        if (argument == null)
                        {
                            Console.WriteLine("Error: Title is required");
                            break;
                        }
                        _ = todoList.Add(argument);
                        Console.WriteLine("Task added.");
                        break;

                    case "list":
                        foreach (var item in todoList.Items)
                        {
                            Console.WriteLine($"{item.Id}: {item.Title} ({(item.IsDone ? "Done" : "Pending")})");
                        }
                        break;

                    case "remove":
                        if (Guid.TryParse(argument, out var id))
                        {
                            if (todoList.Remove(id))
                            {
                                Console.WriteLine("Task removed.");
                            }
                            else
                            {
                                Console.WriteLine("Task not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: Invalid ID format.");
                        }
                        break;

                    case "find":
                        if (argument == null)
                        {
                            Console.WriteLine("Error: Search term is required.");
                            break;
                        }
                        var results = todoList.Find(argument);
                        foreach (var item in results)
                        {
                            Console.WriteLine($"{item.Id}: {item.Title}");
                        }
                        break;

                    case "exit":
                        return;

                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }
            }
        }
    }
}