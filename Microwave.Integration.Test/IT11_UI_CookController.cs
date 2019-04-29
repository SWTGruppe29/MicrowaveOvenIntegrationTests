using System.Security.Cryptography.X509Certificates;
using System.Threading;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Integration.Test
{
    [TestFixture]
    public class IT11_UI_CookController
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

        private ITimer timer;
        private IPowerTube powerTube;

        [SetUp]
        public void Setup()
        {
            powerButton = new Button();
            timeButton = new Button();
            startCancelButton = new Button();
            door = new Door();
            output = new Output();
            timer = new Timer();
            powerTube = new PowerTube(output);
            light = new Light(output);
            display = new Display(output);
            cooker = new CookController(timer,display,powerTube);

            ui = new UserInterface(
                powerButton, timeButton, startCancelButton,
                door, display, light, cooker);
        }

        [Test]
        public void StateSetTime_StartButton_StartCooking()
        {
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();
        }

        [Test]
        public void StateReady_PowerAndTime_StartCooking()
        {
            powerButton.Press();
            powerButton.Press();
            timeButton.Press();
            timeButton.Press();
            timeButton.Press();
            startCancelButton.Press();
        }

        [Test]
        public void StateReady_FullPower_StartCooking()
        {
            for (int i = 50; i <= 700; i+=50)
            {
                powerButton.Press();
            }
            timeButton.Press();
            startCancelButton.Press();
        }

        [Test]
        public void StateCooking_DoorIsOpened_CookerStopped()
        {
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();
            door.Open();
        }

        [Test]
        public void StateCooking_CancelButton_CookerStopped()
        {
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();
            startCancelButton.Press();
        }

        [TestCase(5, 30)]
        [TestCase(10, 20)]
        [TestCase(5, 24)]
        [TestCase(11, 30)]
        public void WaitUntilDoneCooking_CorrectOutput(int power, int time)
        {
            for (int i = 0; i < power; ++i)
            {
                powerButton.Press();
            }

            for (int i = 0; i < time; ++i)
            {
                timeButton.Press();
            }
            startCancelButton.Press();
            Thread.Sleep(time * 1000 + 2000);

        }

        [TestCase(4, 30)]
        [TestCase(2, 40)]
        public void WaitUntilDoneCooking_OpenDoor_CorrectOutput(int power, int time) //Mangler at teste hvorfor der ikke kommer output fra light
        {
            for (int i = 0; i < power; ++i)
            {
                powerButton.Press();
            }

            for (int i = 0; i < time; ++i)
            {
                timeButton.Press();
            }

            startCancelButton.Press();
            Thread.Sleep(time * 1000 + 2000);
            door.Open();
            Thread.Sleep(100); //Wait for output from light
        }
    }
}
