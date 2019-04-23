using System;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Integration.Test
{
    [TestFixture]
    public class IT07_UI_Door
    {

        private IOutput output;
        private UserInterface ui;
        private Door door;

        private IButton powerButton;
        private IButton timeButton;
        private IButton startCancelButton;

        private ILight light;
        private IDisplay display;

        private ICookController cooker;

        [SetUp]
        public void Setup()
        {
            powerButton = Substitute.For<IButton>();
            timeButton = Substitute.For<IButton>();
            startCancelButton = Substitute.For<IButton>();
            light = Substitute.For<ILight>();
            display = Substitute.For<IDisplay>();
            cooker = Substitute.For<ICookController>();

            door = new Door();
            output = new Output();
            ui = new UserInterface(
                powerButton,
                timeButton,
                startCancelButton,
                door,
                display,
                light,
                cooker);

        }

        [Test]
        public void DoorOpen_it()
        {
            door.Open();
        }

        [Test]
        public void DoorClosed_it()
        {
            door.Close();
        }
    }
}
