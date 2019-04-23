using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

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

        [Test]
        public void Test1()
        {

        }
    }
}
