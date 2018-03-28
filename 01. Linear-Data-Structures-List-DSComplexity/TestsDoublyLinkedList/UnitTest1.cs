using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestsDoublyLinkedList
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddFirst_SeveralElements()
        {
            var list = new DoublyLinkedList<int>();

            list.AddFirst(10);
            list.AddFirst(5);
            list.AddFirst(3);

            Assert.AreEqual(3, list.Count);

            var items = new List<int>();
            list.ForEach(items.Add);
            CollectionAssert.AreEqual(items, new List<int> { 3, 5, 10 });
        }

        [TestMethod]
        public void ...()
        {
        }
}
}