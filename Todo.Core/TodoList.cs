// <copyright file="TodoList.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
