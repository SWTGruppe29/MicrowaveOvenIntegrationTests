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
    public class IT05_CookController_Display
    {
        private CookController uut;

        private IUserInterface ui;
        private ITimer timer;
        private IDisplay display;
        private IPowerTube powerTube;
        private IOutput output;

        [SetUp]
        public void Setup()
        {
            ui = Substitute.For<IUserInterface>();
            powerTube = Substitute.For<IPowerTube>();
            output = new Output();
            display = new Display(output);
            timer = new Timer();

            uut = new CookController(timer, display, powerTube, ui);
        }

        [TestCase(3000)]
        [TestCase(4000)]
        [TestCase(5000)]
        public void TimerTick_Seconds(int seconds)
        {
            uut.StartCooking(50, seconds);
            Thread.Sleep(seconds + 100);
        }
    }
}
