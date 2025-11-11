using Todo.Core;

namespace Todo.Test
{
    public class UnitTest1
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
            var i = list.Add("a");
            Assert.True(list.Remove(i.Id));
        }
    }
}