﻿using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;

namespace Microwave.Integration.Test
{
    [TestFixture]
    public class IT03_PowerOut
    {
        private PowerTube uut;
        private IOutput output;

        [SetUp]
        public void Setup()
        {
            output = new Output();
            uut = new PowerTube(output);
        }

        [Test]
        public void TurnOn_WasOff_CorrectOutput()
        {
            uut.TurnOn(50);
            
        }

        [Test]
        public void TurnOff_WasOn_CorrectOutput()
        {
            uut.TurnOn(50);
            uut.TurnOff();
            
        }

        [Test]
        public void TurnOff_WasOff_NoOutput()
        {
            uut.TurnOff();
            
        }

        [Test]
        public void TurnOn_WasOn_ThrowsException()
        {
            uut.TurnOn(50);
            Assert.Throws<System.ApplicationException>(() => uut.TurnOn(60));
        }

        [Test]
        public void TurnOn_NegativePower_ThrowsException()
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => uut.TurnOn(-1));
        }

        [Test]
        public void TurnOn_HighPower_ThrowsException()
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => uut.TurnOn(701));
        }

        [Test]
        public void TurnOn_ZeroPower_ThrowsException()
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => uut.TurnOn(0));
        }




    }
}
