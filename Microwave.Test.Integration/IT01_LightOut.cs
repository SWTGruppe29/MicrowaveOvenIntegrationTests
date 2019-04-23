﻿using MicrowaveOvenClasses.Boundary;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT01_LightOut
    {
        private Light _uut_light;
        private Output _uut_Output;

        [SetUp]
        public void Setup()
        {
            _uut_Output = new Output();
            _uut_light = new Light(_uut_Output);
        }

[Test]
        public void Press_NoSubscribers_NoThrow()
        {
            // We don't need an assert, as an exception would fail the test case
            
        }

        [Test]
        public void Press_1subscriber_IsNotified()
        {
            bool notified = false;

           
            Assert.That(notified, Is.EqualTo(true));
        }
    }
}