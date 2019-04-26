using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Integration.Test
{
    [TestFixture]
    public class IT09_UI_Light
    {
        private UserInterface ui;

        private IButton powerButton;
        private IButton timeButton;
        private IButton startCancelButton;

        private IDoor door;

        private IDisplay display;
        private ILight light;
        private IOutput output;
        private ICookController cooker;

        [SetUp]
        public void Setup()
        {
            powerButton = new Button();
            timeButton = new Button();
            startCancelButton = new Button();
            door = new Door();
            output = new Output();
            light = new Light(output);
            display = Substitute.For<IDisplay>();
            cooker = Substitute.For<ICookController>();

            ui = new UserInterface(
                powerButton,timeButton,startCancelButton,
                door,display,light,cooker);
        }

        [Test]
        public void StateReady_DoorOpened_LightOn()
        {
            door.Open();
        }
        [Test]
        public void StateDoorOpen_DoorClose_LightOff()
        {
            door.Open();
            door.Close();
        }

        [Test]
        public void StateSetPower_DoorOpened_LightOn()
        {
            powerButton.Press();
            door.Open();
        }

        [Test]
        public void StateSetTime_DoorOpened_LightOn()
        {
            powerButton.Press();
            timeButton.Press();
            door.Open();
        }

        [Test]
        public void StateSetTime_StartButton_LightOn()
        {
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();
        }

        [Test]
        public void StateCooking_CookingIsDone_LightOff()
        {
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();
            ui.CookingIsDone();
        }

        [Test]
        public void StateCooking_CancelButton_LightOff()
        {
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press(); //start cooking - state = Cooking
            startCancelButton.Press(); //cancel cooking - state = Ready
        }

    }
}
