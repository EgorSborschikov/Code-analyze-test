using Todo.Core;

namespace Todo.Test
{
    public class UnitTests
    {
        [Fact]
        public void AddIncrementsCount()
        {
            var list = new TodoList();
            _ = list.Add("task");
            Assert.Equal(1, list.Count);
        }

        [Fact]
        public void RemoveByIdWorks()
        {
            var list = new TodoList();
            var item = list.Add("a");
            Assert.True(list.Remove(item.Id));
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void FindReturnsMatchingItems()
        {
            var list = new TodoList();
            _ = list.Add("Buy milk");
            _ = list.Add("Buy bread");
            _ = list.Add("Walk the dog");

            var results = list.Find("buy");
            Assert.Equal(2, results.Count());
        }

        [Fact]
        public void MarkDoneChangesStatus()
        {
            var item = new TodoItem("Test");
            Assert.False(item.IsDone);
            item.MarkDone();
            Assert.True(item.IsDone);
        }

        [Fact]
        public void RenameUpdatesTitle()
        {
            var item = new TodoItem("Old Title");
            item.Rename("New Title");
            Assert.Equal("New Title", item.Title);
        }
    }
}