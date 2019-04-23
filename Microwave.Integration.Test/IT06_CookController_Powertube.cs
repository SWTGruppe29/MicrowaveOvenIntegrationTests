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
    public class IT06_CookController_Powertube
    {
        private ICookController _cookController;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private ITimer _N;
        private IOutput _output;

        [SetUp]
        public void Setup()
        {
            _output = new Output();
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _N = new Timer();
            _cookController = new CookController(_N, _display, _powerTube);
        }

        [TestCase(50,1000)]
        [TestCase(30,2000)]
        [TestCase(10,5000)]
        [TestCase(70,3000)]
        [TestCase(90,10000)]
        [TestCase(75,4000)]
        public void OutPutTest_CookControllerPowerTube(int power, int time)
        {
            _cookController.StartCooking(power,time);
            Thread.Sleep(time+10);
        }
    }
}
