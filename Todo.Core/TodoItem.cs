// <copyright file="TodoItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;

namespace Todo.Core
{
    /// <summary>
    /// Представляет элемент списка дел.
    /// </summary>
    public class TodoItem
    {
        /// <summary>
        /// Gets получает уникальный идентификатор элемента.
        /// </summary>
        public Guid Id { get; } = Guid.NewGuid();

        /// <summary>
        /// Gets получает или задает заголовок элемента.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets a value indicating whether получает или задает статус выполнения элемента.
        /// </summary>
        public bool IsDone { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoItem"/> class.
        /// Инициализирует новый экземпляр класса <see cref="TodoItem"/>.
        /// </summary>
        /// <param name="title">Заголовок элемента.</param>
        public TodoItem(string title)
        {
            Title = title?.Trim() ?? throw new ArgumentNullException(nameof(title));
        }

        /// <summary>
        /// Помечает элемент как выполненный.
        /// </summary>
        public void MarkDone() => IsDone = true;

        /// <summary>
        /// Помечает элемент как невыполненный.
        /// </summary>
        public void MarkUndone() => IsDone = false;

        /// <summary>
        /// Переименовывает элемент.
        /// </summary>
        /// <param name="newTitle">Новый заголовок элемента.</param>
        public void Rename(string newTitle)
        {
            if (string.IsNullOrWhiteSpace(newTitle))
            {
                throw new ArgumentNullException(nameof(newTitle), "Title is required");
            }

            Title = newTitle.Trim();
        }
    }
}
