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
            light.Received().TurnOn();
        }

        [Test]
        public void DoorClosed_it()
        {
            door.Open(); // åbner døren for at skifte tilstand
            door.Close(); //lukning af døren er testen, som skal testes.
            light.Received().TurnOff();
        }

        [TestCase(3)]
        [TestCase(10)]
        [TestCase(7)]
        public void SetPowerMode_DoorOpened_LightOnCalled(int times)
        {
            for (int i = 0; i < times; ++i)
            {
                powerButton.Press();
            }
            door.Open();
            light.Received(1).TurnOn();
        }

        [TestCase(3)]
        [TestCase(10)]
        [TestCase(6)]
        public void SetTimeMode_DoorOpened_LighOnCalled(int times)
        {
            powerButton.Press();
            for (int i = 0; i < times; ++i)
            {
                timeButton.Press();
            }
            door.Open();
            light.Received(1).TurnOn();
        }
        /*[Test]
        public void IsCooking_DoorOpened_ExpectedStopCookingCalled()
        {
            cooker.StartCooking(50,10); //Start the cooker
            door.Open();
            cooker.Received(1).Stop();
        }*/


    }
}
