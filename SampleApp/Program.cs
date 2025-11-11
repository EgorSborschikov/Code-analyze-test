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
                var arg1 = parts.Length > 1 ? parts[1] : null;
                var arg2 = parts.Length > 2 ? parts[2] : null;

                switch (command)
                {
                    case "add":
                        if (arg1 == null)
                        {
                            Console.WriteLine("Error: Title is required.");
                            break;
                        }
                        todoList.Add(arg1);
                        Console.WriteLine("Task added.");
                        break;

                    case "list":
                        foreach (var item in todoList.Items)
                        {
                            Console.WriteLine($"{item.Id}: {item.Title} ({(item.IsDone ? "Done" : "Pending")})");
                        }
                        break;

                    case "remove":
                        if (Guid.TryParse(arg1, out var id))
                        {
                            if (todoList.Remove(id))
                                Console.WriteLine("Task removed.");
                            else
                                Console.WriteLine("Task not found.");
                        }
                        else
                        {
                            Console.WriteLine("Error: Invalid ID format.");
                        }
                        break;

                    case "remove-prefix":
                        if (arg1 == null || arg1.Length < 4)
                        {
                            Console.WriteLine("Error: Prefix must be at least 4 characters.");
                            break;
                        }
                        if (todoList.RemoveByIdPrefix(arg1))
                            Console.WriteLine("Task(s) removed.");
                        else
                            Console.WriteLine("No tasks found with this prefix.");
                        break;

                    case "edit":
                        if (arg1 == null || arg2 == null)
                        {
                            Console.WriteLine("Error: ID and new title are required.");
                            break;
                        }
                        if (Guid.TryParse(arg1, out var editId))
                        {
                            if (todoList.Edit(editId, arg2))
                                Console.WriteLine("Task updated.");
                            else
                                Console.WriteLine("Task not found.");
                        }
                        else
                        {
                            Console.WriteLine("Error: Invalid ID format.");
                        }
                        break;

                    case "find":
                        if (arg1 == null)
                        {
                            Console.WriteLine("Error: Search term is required.");
                            break;
                        }
                        var results = todoList.Find(arg1);
                        foreach (var item in results)
                        {
                            Console.WriteLine($"{item.Id}: {item.Title}");
                        }
                        break;

                    case "save":
                        if (arg1 == null)
                        {
                            Console.WriteLine("Error: File path is required.");
                            break;
                        }
                        var format = arg2?.ToLower();
                        todoList.SaveToFile(arg1, format == "json");
                        Console.WriteLine("Tasks saved.");
                        break;

                    case "load":
                        if (arg1 == null)
                        {
                            Console.WriteLine("Error: File path is required.");
                            break;
                        }
                        var isJson = arg2?.ToLower() == "json";
                        todoList.LoadFromFile(arg1, isJson);
                        Console.WriteLine("Tasks loaded.");
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