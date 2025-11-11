// <copyright file="TodoList.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Todo.Core
{
    public class TodoList
    {
        private readonly List<TodoItem> items = new();

        public IReadOnlyList<TodoItem> Items => items.AsReadOnly();

        public TodoItem Add(string title)
        {
            TodoItem item = new(title);
            this.items.Add(item);
            return item;
        }

        public bool RemoveByIdPrefix(string idPrefix)
        {
            if (idPrefix == null || idPrefix.Length < 4)
            {
                return false;
            }

            return this.items.RemoveAll(i => i.Id.ToString().StartsWith(idPrefix, StringComparison.OrdinalIgnoreCase)) > 0;
        }

        public bool Edit(Guid id, string newTitle)
        {
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return false;
            }

            item.Rename(newTitle);
            return true;
        }

        public void SaveToFile(string filePath, bool asJson = false)
        {
            if (asJson)
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(items, options);
                File.WriteAllText(filePath, json);
            }
            else
            {
                var lines = items.Select(i => $"{i.Id},{i.Title},{i.IsDone}");
                File.WriteAllLines(filePath, lines);
            }
        }

        public void LoadFromFile(string filePath, bool isJson = false)
        {
            items.Clear();
            if (!File.Exists(filePath))
                return;

            if (isJson)
            {
                var json = File.ReadAllText(filePath);
                var loadedItems = JsonSerializer.Deserialize<List<TodoItem>>(json);
                if (loadedItems != null)
                    items.AddRange(loadedItems);
            }
            else
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length >= 3 && Guid.TryParse(parts[0], out var id))
                    {
                        var item = new TodoItem(parts[1]);
                        if (bool.TryParse(parts[2], out var isDone) && isDone)
                            item.MarkDone();
                        items.Add(item);
                    }
                }
            }

        public bool Remove(Guid id)
        {
            return this.items.RemoveAll(i => i.Id == id) > 0;
        }

        public IEnumerable<TodoItem> Find(string substring)
        {
            return this.items.Where(i =>
                i.Title.Contains(substring ?? string.Empty, StringComparison.OrdinalIgnoreCase));
        }

        public int Count => this.items.Count;
    }
}
