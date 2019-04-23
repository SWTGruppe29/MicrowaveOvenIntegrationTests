using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT01_LightOut
    {
        private ILight _uut_light;

        [SetUp]
        public void Setup()
        {
            IOutput output = new Output();
            _uut_light = new Light(output);
        }

        [Test]
        public void TurnOn_WasOff_CorrectOutput()
        {
            _uut_light.TurnOn();
        }

        [Test]
        public void TurnOff_WasOn_CorrectOutput()
        {
            _uut_light.TurnOn();
            _uut_light.TurnOff();

        }

        [Test]
        public void TurnOn_WasOn_CorrectOutput()
        {
            _uut_light.TurnOn();
            _uut_light.TurnOn();

        }

        [Test]
        public void TurnOff_WasOff_CorrectOutput()
        {
            _uut_light.TurnOff();
        }


    }
}