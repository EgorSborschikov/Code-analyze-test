// <copyright file="TodoList.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Todo.Core
{
    /// <summary>
    /// Представляет список дел.
    /// </summary>
    public class TodoList
    {
        /// <summary>
        /// Опции для сериализации JSON.
        /// </summary>
        private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

        private readonly List<TodoItem> items = new();

        /// <summary>
        /// Gets получает список элементов в виде коллекции только для чтения.
        /// </summary>
        public IReadOnlyList<TodoItem> Items => items.AsReadOnly();

        /// <summary>
        /// Добавляет новый элемент в список дел.
        /// </summary>
        /// <param name="title">Заголовок элемента.</param>
        /// <returns>Добавленный элемент.</returns>
        public TodoItem Add(string title)
        {
            TodoItem item = new(title);
            items.Add(item);
            return item;
        }

        /// <summary>
        /// Удаляет элемент по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор элемента.</param>
        /// <returns>Успешность удаления.</returns>
        public bool Remove(Guid id) => items.RemoveAll(i => i.Id == id) > 0;

        /// <summary>
        /// Удаляет элементы по префиксу идентификатора.
        /// </summary>
        /// <param name="idPrefix">Префикс идентификатора.</param>
        /// <returns>Успешность удаления.</returns>
        public bool RemoveByIdPrefix(string idPrefix) =>
            idPrefix != null && idPrefix.Length >= 4 &&
            items.RemoveAll(i => i.Id.ToString().StartsWith(idPrefix, StringComparison.OrdinalIgnoreCase)) > 0;

        /// <summary>
        /// Редактирует заголовок элемента.
        /// </summary>
        /// <param name="id">Идентификатор элемента.</param>
        /// <param name="newTitle">Новый заголовок.</param>
        /// <returns>Успешность редактирования.</returns>
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

        /// <summary>
        /// Находит элементы по подстроке в заголовке.
        /// </summary>
        /// <param name="substring">Подстрока для поиска.</param>
        /// <returns>Коллекция найденных элементов.</returns>
        public IEnumerable<TodoItem> Find(string substring) =>
            items.Where(i => i.Title.Contains(substring ?? string.Empty, StringComparison.OrdinalIgnoreCase));

        /// <summary>
        /// Сохраняет список дел в файл.
        /// </summary>
        /// <param name="filePath">Путь к файлу.</param>
        /// <param name="asJson">Флаг для сохранения в формате JSON.</param>
        public void SaveToFile(string filePath, bool asJson = false)
        {
            if (asJson)
            {
                var json = JsonSerializer.Serialize(items, JsonOptions);
                File.WriteAllText(filePath, json);
            }
            else
            {
                var lines = items.Select(i => $"{i.Id},{i.Title},{i.IsDone}");
                File.WriteAllLines(filePath, lines);
            }
        }

        /// <summary>
        /// Загружает список дел из файла.
        /// </summary>
        /// <param name="filePath">Путь к файлу.</param>
        /// <param name="isJson">Флаг для загрузки из JSON.</param>
        public void LoadFromFile(string filePath, bool isJson = false)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            items.Clear();

            if (isJson)
            {
                var json = File.ReadAllText(filePath);
                var loadedItems = JsonSerializer.Deserialize<List<TodoItem>>(json, JsonOptions);
                if (loadedItems != null)
                {
                    items.AddRange(loadedItems);
                }
            }
            else
            {
                foreach (var line in File.ReadAllLines(filePath))
                {
                    var parts = line.Split(',');
                    if (parts.Length >= 3 && Guid.TryParse(parts[0], out _))
                    {
                        var item = new TodoItem(parts[1]);
                        if (bool.TryParse(parts[2], out var isDone) && isDone)
                        {
                            item.MarkDone();
                        }

                        items.Add(item);
                    }
                }
            }
        }

        /// <summary>
        /// Gets получает количество элементов в списке.
        /// </summary>
        public int Count => items.Count;
    }
}
