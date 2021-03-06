﻿namespace BunnyWars.Tests.Performance
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Diagnostics;

    [TestClass]
    public class NextPerformance : BaseTestClass
    {
        public Random Random { get; private set; }

        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            this.Random = new Random();
        }

        [TestCategory("Performance")]
        [TestMethod]
        public void PerformanceNext_With10000ConsecutiveNextCommands_With10000BunniesInOneRoom()
        {
            //Arrange
            var bunniesCount = 10000;
            this.BunnyWarCollection.AddRoom(0);
            for (int i = 0; i < bunniesCount; i++)
            {
                this.BunnyWarCollection.AddBunny(i.ToString(), i % 5, 0);
            }

            //Act
            Stopwatch timer = new Stopwatch();
            timer.Start();
            for (int i = 0; i < bunniesCount; i++)
            {
                this.BunnyWarCollection.Next(i.ToString());
            }
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 600);
        }

        [TestCategory("Performance")]
        [TestMethod]
        public void PerformanceNext_With10000ConsecutiveNextCommands_With10000BunniesIn5000Rooms()
        {
            //Arrange
            var roomsCount = 5000;
            var bunniesCount = 10000;
            for (int i = 0; i < roomsCount; i++)
            {
                this.BunnyWarCollection.AddRoom(i);
            }

            for (int i = 0; i < bunniesCount; i++)
            {
                this.BunnyWarCollection.AddBunny(i.ToString(), i % 5, i / 2);
            }

            //Act
            Stopwatch timer = new Stopwatch();
            timer.Start();
            for (int i = 0; i < bunniesCount; i++)
            {
                this.BunnyWarCollection.Next(i.ToString());
            }
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 500);
        }

        [TestCategory("Performance")]
        [TestMethod]
        public void PerformanceNext_With10000RandomNextCommands_With10000BunniesIn5000Rooms()
        {
            //Arrange
            var roomsCount = 5000;
            var bunniesCount = 10000;
            for (int i = 0; i < roomsCount; i++)
            {
                this.BunnyWarCollection.AddRoom(i);
            }

            for (int i = 0; i < bunniesCount; i++)
            {
                this.BunnyWarCollection.AddBunny(i.ToString(), i % 5, i / 2);
            }

            //Act
            Stopwatch timer = new Stopwatch();
            timer.Start();
            for (int i = 0; i < bunniesCount; i++)
            {
                this.BunnyWarCollection.Next(this.Random.Next(0, bunniesCount).ToString());
            }
            timer.Stop();
            Assert.IsTrue(timer.ElapsedMilliseconds < 500);
        }
    }
}