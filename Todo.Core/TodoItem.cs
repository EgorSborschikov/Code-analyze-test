// <copyright file="TodoItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Todo.Core
{
    public class TodoItem
    {
        public Guid Id { get; } = Guid.NewGuid();

        public string Title { get; private set; }

        public bool IsDone { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoItem"/> class.
        /// </summary>
        /// <param name="title">Заголовок элемента.</param>
        public TodoItem(string title)
        {
            Title = title?.Trim() ?? throw new ArgumentNullException(nameof(title));
        }

        public void MarkDone() => IsDone = true;

        public void MarkUndone() => IsDone = false;

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
